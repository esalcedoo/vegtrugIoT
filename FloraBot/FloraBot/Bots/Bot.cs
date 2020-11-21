using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FloraBot.Dialogs;
using FloraBot.IntentHandlers;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace FloraBot.Bots
{
    public class Bot : ActivityHandler
    {
        private ConversationState _conversationState;
        private readonly IEnumerable<ILUISeIntentHandler> _intentHandlers;
        private readonly QnADialog _qnADialog;

        public Bot(ConversationState conversationState,  IEnumerable<ILUISeIntentHandler> intentHandlers, QnADialog qnADialog)
        {
            _conversationState = conversationState;
            _intentHandlers = intentHandlers;
            _qnADialog = qnADialog;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occured during the turn.
            await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            DialogSet dialogSet = new DialogSet(_conversationState.CreateProperty<DialogState>("DialogState"));

            dialogSet.Add(_qnADialog);

            DialogContext dialogContext = await dialogSet.CreateContextAsync(turnContext, cancellationToken);
            DialogTurnResult results = await dialogContext.ContinueDialogAsync(cancellationToken);

            if (results.Status == DialogTurnStatus.Empty)
            {
                string luisIntentKey = GetTopIntentKey(turnContext);

                var intentHandler = _intentHandlers.FirstOrDefault(handler =>
                    handler.IsValid(luisIntentKey));

                if (intentHandler is null)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(
                        $"Intención {luisIntentKey} no soportada."));
                    return;
                }

                await intentHandler.Handle(dialogContext, cancellationToken);
            }
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hello W4TT!"), cancellationToken);
                }
            }
        }

        private static string GetTopIntentKey(ITurnContext turnContext)
        {
            RecognizerResult luisResult = turnContext.TurnState
                            .Get<RecognizerResult>("LuisRecognizerResult");
            return luisResult.Intents.FirstOrDefault().Key;
        }

    }
}
