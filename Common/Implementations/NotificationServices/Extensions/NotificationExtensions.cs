using Common.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Common.Implementations.NotificationServices.RabbitMQ;

namespace Common.Implementations.NotificationServices.Extensions
{
    public static class NotificationExtensions
    {
        public static IServiceCollection AddNotificationsService(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionService, ConnectionService>();
            services.AddSingleton<INotificationService, RabbitMQService>();
            return services;
        }
    }


}
