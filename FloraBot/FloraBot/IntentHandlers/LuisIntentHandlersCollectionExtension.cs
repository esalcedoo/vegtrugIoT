using FloraBot.IntentHandlers;
using FloraBot.Services.LUIS;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LuisIntentHandlersCollectionExtension
    {
        public static IServiceCollection AddLuisIntentHandlers(this IServiceCollection services)
        {
            services.AddTransient<LUISIntentHandler, LuisScanNowHandler>();
            services.AddTransient<LUISIntentHandler, SummaryIntentHandler>();
            services.AddTransient<LUISIntentHandler, LuisNoneHandler>();
            return services;
        }
    }
}
