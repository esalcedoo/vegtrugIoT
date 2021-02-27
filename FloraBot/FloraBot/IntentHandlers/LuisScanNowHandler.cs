using FloraBot.Dialogs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FloraBot.IntentHandlers
{
    public class LuisScanNowHandler : LUISIntentHandler
    {
        private readonly ConversationState _conversationState;
        private readonly ScanNowDialog _scanNowDialog;

        public LuisScanNowHandler(ConversationState conversationState, ScanNowDialog scanNowDialog)
        {
            _conversationState = conversationState;
            _scanNowDialog = scanNowDialog;
        }

        public override async Task<DialogTurnResult> Handle(DialogContext dialogContext, CancellationToken cancellationToken)
        {
            return await dialogContext.BeginDialogAsync(_scanNowDialog.Id, cancellationToken: cancellationToken);
        }

        public override async Task Handle(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await _scanNowDialog.RunAsync(turnContext,
                            _conversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
        }

        public override bool IsValid(ITurnContext turnContext)
        {
            return GetTopIntentKey(turnContext) == "Summary"
                && ContainsEntity(turnContext,"now");
        }
    }
}
