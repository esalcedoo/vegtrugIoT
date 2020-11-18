using FloraBot;
using FloraBot.IntentHandlers;
using FloraBot.Services.LUIS;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LUISServiceCollectionExtension
    {
        public static IServiceCollection AddLuisService(this IServiceCollection services, Action<LuisApplication> setup = null)
        {
            services.AddSingleton<LuisRecognizerMiddleware>();
            services.AddOptions();
            if (setup != null) services.Configure<LuisApplication>(setup);

            services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IPostConfigureOptions<LuisApplication>, LuisPostConfigureOptions>());

            services.AddSingleton(serviceProvider =>
            {
                var luisApplication = serviceProvider.GetRequiredService<IOptions<LuisApplication>>().Value;
                
                var luisRecognizerOptions = new LuisRecognizerOptionsV2(luisApplication);
                luisRecognizerOptions.PredictionOptions.IncludeAllIntents = false;

                return new LuisRecognizer(luisRecognizerOptions);
            });
            
            return services;
        }

        public static IServiceCollection AddLuisService(this IServiceCollection services, LuisApplication luisApplication)
        {
            luisApplication.Validate();

            var luisRecognizerOptions = new LuisRecognizerOptionsV3(luisApplication);
            luisRecognizerOptions.PredictionOptions.IncludeAllIntents = false;

            var luisRecognizer = new LuisRecognizer(luisRecognizerOptions);

            services.AddSingleton<LuisRecognizer>(luisRecognizer);

            return services;
        }

    }
}
