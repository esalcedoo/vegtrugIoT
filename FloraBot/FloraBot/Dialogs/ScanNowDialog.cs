﻿using FloraBot.Dialogs.Messages;
using FloraBot.Services.Flora;
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
    public class ScanNowDialog : Dialog
    {
        private readonly FloraService _floraService;

        public ScanNowDialog(FloraService floraService) : base(nameof(ScanNowDialog))
        {
            _floraService = floraService;
        }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            List<StatusPlantModel> plantsStatus;
            List<PlantModel> plants;
            int tries = 0;
            do
            {
                plantsStatus = await _floraService.GetPlantsCurrentStatus();
                plants = await _floraService.GetPlantsInfo(plantsStatus.Select(plantStatus => plantStatus.PlantId).ToList());
                tries++;
            }
            while (!plantsStatus.Any(status => status.Timestamp < DateTime.UtcNow) || tries < 3);            

            string message = PlantMessages.Summary(plants, plantsStatus);
            
            await dc.Context.SendActivityAsync(MessageFactory.Text(message), cancellationToken);
            return await dc.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
