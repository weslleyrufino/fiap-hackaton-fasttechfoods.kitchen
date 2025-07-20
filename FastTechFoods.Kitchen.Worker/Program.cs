using FastTechFoods.Kitchen.Infrastructure.Repository;
using FastTechFoods.Kitchen.Worker.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        // DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

        // MassTransit + RabbitMQ
        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var section = configuration.GetSection("MassTransit_CustomerOrderCreated");

                cfg.Host(section["Servidor"], h =>
                {
                    h.Username(section["Usuario"]);
                    h.Password(section["Senha"]);
                });

                cfg.ReceiveEndpoint(section["NomeFila"], e =>
                {
                    e.ConfigureConsumer<OrderCreatedConsumer>(context);
                });
            });
        });

        services.AddMassTransitHostedService();
        services.AddScoped<OrderCreatedConsumer>();
    })
    .Build()
    .Run();

//Host.CreateDefaultBuilder(args)
//    .ConfigureServices((hostContext, services) =>
//    {
//        // Configurar DbContext
//        services.AddDbContext<ApplicationDbContext>(options =>
//            options.UseSqlServer(
//                hostContext.Configuration.GetConnectionString("ConnectionString")
//            )
//        );

//        // Configurar MassTransit + RabbitMQ
//        services.AddMassTransit(x =>
//        {
//            x.AddConsumer<OrderCreatedConsumer>();

//            x.UsingRabbitMq((context, cfg) =>
//            {
//                var rmq = hostContext.Configuration.GetSection("MassTransit_CustomerOrderCreated");
//                cfg.Host(rmq["Servidor"], h =>
//                {
//                    h.Username(rmq["Usuario"]);
//                    h.Password(rmq["Senha"]);
//                });

//                cfg.ReceiveEndpoint(rmq["NomeFila"], e =>
//                {
//                    e.ConfigureConsumer<OrderCreatedConsumer>(context);
//                });
//            });
//        });
//        services.AddMassTransitHostedService();

//        // Registrar Consumer
//        services.AddScoped<OrderCreatedConsumer>();
//    })
//    .Build()
//    .Run();
