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
builder.Services.AddScoped<IOrderService, OrderService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



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

var app = builder.Build();

// Registra m�tricas de uso de CPU e mem�ria
var cpuUsage = Metrics.CreateGauge("system_cpu_usage_percent", "Current CPU usage percentage.");
var memoryUsage = Metrics.CreateGauge("system_memory_usage_bytes", "Current memory usage in bytes.");

// Vari�veis para c�lculo do uso de CPU
var lastTotalProcessorTime = TimeSpan.Zero;
var lastTime = DateTime.UtcNow;


// Middleware para coletar m�tricas de uso de CPU e mem�ria
app.Use(async (context, next) =>
{
    var process = Process.GetCurrentProcess();

    // C�lculo do uso de CPU (percentual)
    var currentTime = DateTime.UtcNow;
    var currentTotalProcessorTime = process.TotalProcessorTime;

    var elapsedTime = (currentTime - lastTime).TotalMilliseconds;
    var cpuElapsedTime = (currentTotalProcessorTime - lastTotalProcessorTime).TotalMilliseconds;

    var cpuPercent = elapsedTime > 0 ? (cpuElapsedTime / elapsedTime) * 100 / Environment.ProcessorCount : 0;

    // Atualiza valores para o pr�ximo c�lculo
    lastTime = currentTime;
    lastTotalProcessorTime = currentTotalProcessorTime;

    // Define a m�trica de CPU
    cpuUsage.Set(cpuPercent);

    // C�lculo do uso de mem�ria (percentual)
    var totalMemory = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes; // Total de mem�ria dispon�vel para o processo
    var usedMemory = process.PrivateMemorySize64; // Mem�ria usada pelo processo

    var memoryPercent = totalMemory > 0 ? (usedMemory / (double)totalMemory) * 100 : 0;

    // Define a m�trica de mem�ria
    memoryUsage.Set(memoryPercent);

    await next();
});


// Middleware para medir lat�ncia das requisi��es
app.Use(async (context, next) =>
{
    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    await next();
    stopwatch.Stop();
    httpDuration
        .WithLabels(context.Request.Method, context.Request.Path)
        .Observe(stopwatch.Elapsed.TotalSeconds);
});

// Middleware de tratamento de exce��es
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

// Middleware padr�o

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

app.UseAuthorization();

app.UseHttpMetrics();

app.MapMetrics();

app.MapControllers();

app.Run();
