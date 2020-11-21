using FloraBot.Middlewares;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FloraBot
{
    public class AdapterWithErrorHandler : BotFrameworkHttpAdapter
    {
        public AdapterWithErrorHandler(IConfiguration configuration, LuisRecognizerMiddleware luisRecognizerMiddleware, ConversationReferenceMiddleware firstTimeMiddleware, ILogger<BotFrameworkHttpAdapter> logger)
            : base(configuration, logger)
        {
            MiddlewareSet.Use(luisRecognizerMiddleware);
            MiddlewareSet.Use(firstTimeMiddleware);


            OnTurnError = async (turnContext, exception) =>
            {
                // Log any leaked exception from the application.
                logger.LogError($"Exception caught : {exception.Message}");

                // Send a catch-all apology to the user.
                await turnContext.SendActivityAsync("Sorry, it looks like something went wrong.");
            };
        }
    }
}
