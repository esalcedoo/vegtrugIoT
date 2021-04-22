using FloraBot.Services.Flora;
using FloraBot.Services.IoTCentral;
using FloraModels;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FloraBot.Dialogs
{
    public class WatteringDialog : Dialog
    {
        private readonly FloraService _floraService;
        private readonly IoTCentralService _ioTCentralService;

        public WatteringDialog(FloraService floraService, IoTCentralService ioTCentralService) : base(nameof(WatteringDialog))
        {
            _floraService = floraService;
            _ioTCentralService = ioTCentralService;
        }
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            int plantId = GetPlanId(dc);
            PlantModel plantModel = await _floraService.GetPlantInfo(plantId);
            await dc.Context.SendActivityAsync(MessageFactory.Text(plantModel.ToString()), cancellationToken);
            return await dc.EndDialogAsync(cancellationToken: cancellationToken);
        }

        private int GetPlanId(DialogContext dc)
        {
            //RecognizerResult luisResult = dc.TurnState
            //                .Get<RecognizerResult>("LuisRecognizerResult");
            ////TODO
            //return luisResult.Entities.ContainsKey("now");

            return 1;
        }
    }
}
