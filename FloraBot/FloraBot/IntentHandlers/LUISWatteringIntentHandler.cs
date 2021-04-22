using FloraBot.Dialogs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FloraBot.IntentHandlers
{
    public class LUISWatteringIntentHandler : LUISIntentHandler
    {
        private readonly ConversationState _conversationState;
        private readonly WatteringDialog _watteringDialog;

        public LUISWatteringIntentHandler(ConversationState conversationState, WatteringDialog watteringDialog)
        {
            _conversationState = conversationState;
            _watteringDialog = watteringDialog;
        }

        public override async Task<DialogTurnResult> Handle(DialogContext dialogContext, CancellationToken cancellationToken)
        {
            return await dialogContext.BeginDialogAsync(_watteringDialog.Id, cancellationToken: cancellationToken);
        }

        public override async Task Handle(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await _watteringDialog.RunAsync(turnContext,
                            _conversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
        }

        public override bool IsValid(ITurnContext turnContext)
        {
            return GetTopIntentKey(turnContext) == "Wattering";
        }
    }
}
