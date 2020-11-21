using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace FloraBot.Middlewares
{
    public class ConversationReferenceMiddleware : IMiddleware
    {
        private readonly ConcurrentDictionary<string, ConversationReference> _conversationReferences;

        public ConversationReferenceMiddleware(ConcurrentDictionary<string, ConversationReference> conversationReferences)
        {
            _conversationReferences = conversationReferences;
        }

        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default)
        {
            if (turnContext.Activity.Type == ActivityTypes.ContactRelationUpdate 
                || (turnContext.Activity.Type == ActivityTypes.Message && turnContext.Activity.Text == "/start"))
            {
                AddConversationReference(turnContext.Activity);
            }

            await next(cancellationToken).ConfigureAwait(false);
        }


        private void AddConversationReference(IActivity activity)
        {
            ConversationReference conversationReference = activity.GetConversationReference();
            _conversationReferences.AddOrUpdate(conversationReference.User.Id, conversationReference, (key, newValue) => conversationReference);
        }
    }
}
