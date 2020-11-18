﻿using FloraBot.IntentHandlers;
using FloraBot.Services.LUIS;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LuisIntentHandlersCollectionExtension
    {
        public static IServiceCollection AddLuisIntentHandlers(this IServiceCollection services)
        {
            services.AddTransient<ILUISeIntentHandler, ResumeIntentHandler>();
            services.AddTransient<ILUISeIntentHandler, LuisNoneHandler>();
            return services;
        }
    }
}
