using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FloraBot.Dialogs
{
    public class ResumeDialog : Dialog
    {
        public override Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
