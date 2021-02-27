using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FloraBot.IntentHandlers
{
    public abstract class LUISIntentHandler
    {
        //internal Task Handle(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken);
        public abstract bool IsValid(ITurnContext turnContext);
        public abstract Task<DialogTurnResult> Handle(DialogContext dialogContext, CancellationToken cancellationToken);
        public abstract Task Handle(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken);

        public static string GetTopIntentKey(ITurnContext turnContext)
        {
            RecognizerResult luisResult = turnContext.TurnState
                            .Get<RecognizerResult>("LuisRecognizerResult");
            return luisResult.Intents.FirstOrDefault().Key;
        }

        public static bool ContainsEntity(ITurnContext turnContext, string entityKey)
        {
            RecognizerResult luisResult = turnContext.TurnState
                            .Get<RecognizerResult>("LuisRecognizerResult");
            //TODO
            return luisResult.Entities.ContainsKey("now");
        }
    }
}
