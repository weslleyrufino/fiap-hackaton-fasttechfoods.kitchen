using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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