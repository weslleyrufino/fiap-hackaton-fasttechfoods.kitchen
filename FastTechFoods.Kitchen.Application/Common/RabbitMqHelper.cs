using MassTransit;
using Microsoft.Extensions.Configuration;

namespace FastTechFoods.Kitchen.Application.Common;
internal static class RabbitMqHelper
{
    public static async Task SendMessageAsync<T>(
        ISendEndpointProvider sendEndpointProvider,
        IConfiguration configuration,
        T message,
        string configurationKey) where T : class
    {
        var queueName = configuration[configurationKey];
        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
        await endpoint.Send(message);
    }
}