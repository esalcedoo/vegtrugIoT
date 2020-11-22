using FloraBot.Dialogs.Messages;
using FloraBot.Services.Flora;
using FloraModels;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FloraBot.Dialogs
{
    public class SummaryDialog : Dialog
    {
        private readonly FloraService _floraService;

        public SummaryDialog(FloraService floraService) : base(nameof(SummaryDialog))
        {
            _floraService = floraService;
        }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            List<CurrentStatusPlantModel> plantsStatus = await _floraService.GetPlantsStatus();
            List<PlantModel> plants = await _floraService.GetPlantsInfo(plantsStatus.Select(plantStatus => plantStatus.PlantId).ToList());

            string message = PlantMessages.Summary(plants, plantsStatus);

            await dc.Context.SendActivityAsync(MessageFactory.Text(message), cancellationToken);
            return await dc.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
