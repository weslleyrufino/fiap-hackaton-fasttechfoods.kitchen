using FastTechFoods.Kitchen.Infrastructure.Repository;
using FastTechFoods.Kitchen.Worker;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // Configurar DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                hostContext.Configuration.GetConnectionString("ConnectionString")
            )
        );

        // Configurar MassTransit + RabbitMQ
        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var rmq = hostContext.Configuration.GetSection("MassTransit_ClientOrderCreated");
                cfg.Host(rmq["Servidor"], h =>
                {
                    h.Username(rmq["Usuario"]);
                    h.Password(rmq["Senha"]);
                });

                cfg.ReceiveEndpoint(rmq["NomeFila"], e =>
                {
                    e.ConfigureConsumer<OrderCreatedConsumer>(context);
                });
            });
        });
        services.AddMassTransitHostedService();

        // Registrar Consumer
        services.AddScoped<OrderCreatedConsumer>();
    })
    .Build()
    .Run();
