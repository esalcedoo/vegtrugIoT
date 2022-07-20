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
        private readonly IEnumerable<LUISIntentHandler> _intentHandlers;
        private readonly QnADialog _qnADialog;
        private readonly SummaryDialog _summaryDialog;
        private readonly ScanNowDialog _scanNowDialog;

        public Bot(ConversationState conversationState,
            IEnumerable<LUISIntentHandler> intentHandlers,
            QnADialog qnADialog,
            SummaryDialog summaryDialog,
            ScanNowDialog scanNowDialog)
        {
            _conversationState = conversationState;
            _intentHandlers = intentHandlers;
            _qnADialog = qnADialog;
            _summaryDialog = summaryDialog;
            _scanNowDialog = scanNowDialog;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occured during the turn.
            await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            DialogSet dialogSet = InitializeDialogSet();

            DialogContext dialogContext = await dialogSet.CreateContextAsync(turnContext, cancellationToken);
            DialogTurnResult results = await dialogContext.ContinueDialogAsync(cancellationToken);

            if (results.Status == DialogTurnStatus.Empty)
            {
                var intentHandler = _intentHandlers.FirstOrDefault(handler =>
                    handler.IsValid(turnContext));

                if (intentHandler is null)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(
                        $"Funcionalidad no soportada."));
                    return;
                }

                await intentHandler.Handle(dialogContext, cancellationToken);
            }
        }

        //public class RootDialog : ComponentDialog
        //{
        //    private readonly ConversationState _conversationState;

        //    public RootDialog(ConversationState conversationState, WhatToWatchDialog whatToWatchDialog) : base(nameof(RootDialog))
        //    {
        //        _conversationState = conversationState;
        //        AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
        //        {
        //        Redirect
        //        }));
        //        AddDialog(whatToWatchDialog);
        //        InitialDialogId = nameof(WaterfallDialog);
        //    }

        //    private async Task<DialogTurnResult> Redirect(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        //    {
        //        var statePropertyAccessor = _conversationState.CreateProperty<RecognizerResult>(nameof(RecognizerResult));
        //        var luisRecognizerResult = await statePropertyAccessor.GetAsync(stepContext.Context);

        //        luisRecognizerResult.GetTopScoringIntent().intent;
        //    }
        //}


        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hello!"), cancellationToken);
                }
            }
        }

        private DialogSet InitializeDialogSet()
        {
            DialogSet dialogSet = new DialogSet(_conversationState.CreateProperty<DialogState>("DialogState"));

            dialogSet.Add(_qnADialog);
            dialogSet.Add(_summaryDialog);
            dialogSet.Add(_scanNowDialog);

            return dialogSet;
        }

    }
}
