using Common.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Implementations.Extensions
{
    public static class OptionCollectionExtension
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.Configure<SwaggerOptions>(configuration.GetSection("SwaggerOptions"));

            services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQOptions"));

            services.Configure<HttpServiceOptions>(configuration.GetSection("HttpServiceOptions"));

            services.Configure<TokenProviderOptions>(configuration.GetSection("TokenProviderOptions"));

            services.Configure<OAuthOptions>(configuration.GetSection("OAuthOptions"));

        }
    }
}
