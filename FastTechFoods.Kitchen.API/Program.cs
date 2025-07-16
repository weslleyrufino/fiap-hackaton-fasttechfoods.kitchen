using FastTechFoods.Kitchen.API.Logging;
using FastTechFoods.Kitchen.Application.Interfaces.Repository;
using FastTechFoods.Kitchen.Application.Interfaces.Services;
using FastTechFoods.Kitchen.Application.Services;
using FastTechFoods.Kitchen.Infrastructure.Repository;
using FastTechFoods.Kitchen.Infrastructure.IoC;
//using MassTransit;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using System.Diagnostics;
using FastTechFoods.Kitchen.Application.Interfaces.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FastTechFoods.Kitchen.Infrastructure.Services.Authentication;
using Microsoft.OpenApi.Models;
using FastTechFoods.Kitchen.API.Security;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

var httpDuration = Metrics.CreateHistogram("http_request_duration_seconds_sum", "Histogram of HTTP request durations.", new HistogramConfiguration
{
    LabelNames = new[] { "method", "endpoint" }
});

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddScoped<IContatoRepository, ContatoRepository>(); // TODO: DELETE
builder.Services.AddScoped<IContatoService, ContatoService>(); // TODO: DELETE

builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services
       .AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    // defines the “Bearer” security scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert JWT token as: Bearer {your_token}"
    });

    // makes Swagger require this scheme on all protected endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



builder.Services.AddMassTransitConfigured(builder.Configuration);

// LOG
builder.Logging.ClearProviders();
builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information,
}));
//LOG

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
}, ServiceLifetime.Scoped);


// ************************************************ Config. Token JWT. ************************************************ 
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                                          Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();
// ************************************************ Config. Token JWT. ************************************************ 

var app = builder.Build();

// Registra métricas de uso de CPU e mem�ria
var cpuUsage = Metrics.CreateGauge("system_cpu_usage_percent", "Current CPU usage percentage.");
var memoryUsage = Metrics.CreateGauge("system_memory_usage_bytes", "Current memory usage in bytes.");

// Vari�veis para cálculo do uso de CPU
var lastTotalProcessorTime = TimeSpan.Zero;
var lastTime = DateTime.UtcNow;


// Middleware para coletar métricas de uso de CPU e memória
app.Use(async (context, next) =>
{
    var process = Process.GetCurrentProcess();

    // C�lculo do uso de CPU (percentual)
    var currentTime = DateTime.UtcNow;
    var currentTotalProcessorTime = process.TotalProcessorTime;

    var elapsedTime = (currentTime - lastTime).TotalMilliseconds;
    var cpuElapsedTime = (currentTotalProcessorTime - lastTotalProcessorTime).TotalMilliseconds;

    var cpuPercent = elapsedTime > 0 ? (cpuElapsedTime / elapsedTime) * 100 / Environment.ProcessorCount : 0;

    // Atualiza valores para o próximo cálculo
    lastTime = currentTime;
    lastTotalProcessorTime = currentTotalProcessorTime;

    // Define a métrica de CPU
    cpuUsage.Set(cpuPercent);

    // Cálculo do uso de memória (percentual)
    var totalMemory = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes; // Total de memória disponível para o processo
    var usedMemory = process.PrivateMemorySize64; // Memória usada pelo processo

    var memoryPercent = totalMemory > 0 ? (usedMemory / (double)totalMemory) * 100 : 0;

    // Define a métrica de memória
    memoryUsage.Set(memoryPercent);

    await next();
});


// Middleware para medir latência das requisições
app.Use(async (context, next) =>
{
    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    await next();
    stopwatch.Stop();
    httpDuration
        .WithLabels(context.Request.Method, context.Request.Path)
        .Observe(stopwatch.Elapsed.TotalSeconds);
});

// Middleware de tratamento de exceções
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        if (exceptionHandlerPathFeature?.Error != null)
        {
            logger.LogError(exceptionHandlerPathFeature.Error, "Erro não tratado.");
        }

        await context.Response.WriteAsync("{\"error\":\"Erro interno do servidor.\"}");
    });
});

// Middleware padrão

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.UseHttpMetrics();

app.MapMetrics();

app.MapControllers();

app.Run();
