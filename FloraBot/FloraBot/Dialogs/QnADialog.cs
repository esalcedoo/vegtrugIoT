using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.Bot.Builder.AI.QnA.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FloraBot.Dialogs
{
    public class QnADialog : QnAMakerDialog
    {
        // Dialog Options parameters
        public const string DefaultNoAnswer = "No tengo una buena respuesta para eso";
        public const string DefaultCardNoMatchText = "None of the above.";

        private readonly QnAMaker _qnAMakerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="QnADialog"/> class.
        /// Dialog helper to generate dialogs.
        /// </summary>
        /// <param name="qnAMakerService">QnAMaker</param>
        public QnADialog(QnAMaker qnAMakerService) : base()
        {
            _qnAMakerService = qnAMakerService;
        }

        protected async override Task<IQnAMakerClient> GetQnAMakerClientAsync(DialogContext dc)
        {
            return _qnAMakerService;
        }

        protected override Task<QnAMakerOptions> GetQnAMakerOptionsAsync(DialogContext dc)
        {
            return Task.FromResult(new QnAMakerOptions
            {
                ScoreThreshold = DefaultThreshold,
                Top = DefaultTopN,
                QnAId = 0,
                RankerType = "Default",
                IsTest = false
            });
        }

        protected override Task<QnADialogResponseOptions> GetQnAResponseOptionsAsync(DialogContext dc)
        {
            var noAnswer = (Activity)Activity.CreateMessageActivity();
            noAnswer.Text = DefaultNoAnswer;

            var responseOptions = new QnADialogResponseOptions
            {
                NoAnswer = noAnswer,
                CardNoMatchText = DefaultCardNoMatchText
            };

            return Task.FromResult(responseOptions);
        }
    }
}
