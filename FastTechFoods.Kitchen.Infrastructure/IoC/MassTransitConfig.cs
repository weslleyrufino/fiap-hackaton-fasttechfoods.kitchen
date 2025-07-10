using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTechFoods.Kitchen.Infrastructure.IoC;
public static class MassTransitConfig
{
    public static IServiceCollection AddMassTransitConfigured(this IServiceCollection services, IConfiguration configuration)
    {
        var usuario = configuration["MassTransit:Usuario"];
        var senha = configuration["MassTransit:Senha"];
        var host = configuration["MassTransit:Servidor"];

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            // Se você tiver consumers, adicione aqui
            // x.AddConsumer<SeuConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{host}"), h =>
                {
                    h.Username(usuario);
                    h.Password(senha);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}

//var configurationMassTransit = builder.Configuration;
//var fila = configurationMassTransit.GetSection("MassTransit")["NomeFila"] ?? string.Empty;
//var servidor = configurationMassTransit.GetSection("MassTransit")["Servidor"] ?? string.Empty;
//var usuario = configurationMassTransit.GetSection("MassTransit")["Usuario"] ?? string.Empty;
//var senha = configurationMassTransit.GetSection("MassTransit")["Senha"] ?? string.Empty;

//builder.Services.AddMassTransit(x =>
//{
//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host(new Uri("rabbitmq://rabbitmq:5672"), h =>
//        {
//            h.Username(usuario);
//            h.Password(senha);
//        });

//        cfg.ConfigureEndpoints(context);
//    });
//});