using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace FloraBot.Middlewares
{
    public class LuisRecognizerMiddleware : IMiddleware
    {
        private readonly IRecognizer _luisRecognizer;

        public LuisRecognizerMiddleware(LuisRecognizer luisRecognizer)
        {
            _luisRecognizer = luisRecognizer;
        }

        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default)
        {
            BotAssert.ContextNotNull(turnContext);

            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                var utterance = turnContext.Activity.AsMessageActivity().Text;
                if (!string.IsNullOrWhiteSpace(utterance))
                {
                    RecognizerResult luisRecognizerResult = 
                        await _luisRecognizer.RecognizeAsync(turnContext, CancellationToken.None).ConfigureAwait(false);

                    turnContext.TurnState.Add("LuisRecognizerResult", luisRecognizerResult);
                }
            }

            await next(cancellationToken).ConfigureAwait(false);
        }
    }
}
