using FloraBot.Dialogs;
using FloraBot.IntentHandlers;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace FloraBot.Services.LUIS
{
    public class ResumeIntentHandler : ILUISeIntentHandler
    {
        private readonly ConversationState _conversationState;
        private readonly ResumeDialog _resumeDialog;

        public ResumeIntentHandler(ConversationState conversationState, ResumeDialog resumeDialog)
        {
            _conversationState = conversationState;
            _resumeDialog = resumeDialog;
        }

        public async Task<DialogTurnResult> Handle(DialogContext dialogContext, CancellationToken cancellationToken)
        {
            return await dialogContext.BeginDialogAsync(_resumeDialog.Id, cancellationToken: cancellationToken);
        }

        public async Task Handle(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await _resumeDialog.RunAsync(turnContext,
                            _conversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
        }

        public bool IsValid(string intent) => intent == "Resume";
    }
}
