using FloraBot.Services.QnA;
using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class QnACollectionServiceExtension
    {
        public static IServiceCollection AddQnAService(this IServiceCollection services, Action<QnAMakerEndpoint> setup = null)
        {
            services.AddOptions();
            if (setup != null) services.Configure<QnAMakerEndpoint>(setup);

            services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IPostConfigureOptions<QnAMakerEndpoint>, QnAPostConfigureOptions>());

            services.AddSingleton(serviceProvider =>
            {
                var qnAMakerEndpoint = serviceProvider.GetRequiredService<IOptions<QnAMakerEndpoint>>().Value;
                var qnAMakerOptions = new QnAMakerOptions() { Top = 1 };

                return new QnAMaker(qnAMakerEndpoint, qnAMakerOptions);
            });

            return services;
        }
    }
}
