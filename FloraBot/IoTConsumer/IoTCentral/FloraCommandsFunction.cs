using IoTConsumer.IoTCentral;
using IoTConsumer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTConsumer.IoTCentralFunction
{
    class FloraCommandsFunction
    {
        private readonly IoTCentralCommandsService _ioTCentralCommandsService;
        private readonly PlantService _plantService;

        public FloraCommandsFunction(IoTCentralCommandsService ioTCentralCommandsService, PlantService plantService)
        {
            _ioTCentralCommandsService = ioTCentralCommandsService;
            _plantService = plantService;
        }

        [FunctionName("ScanNowFunction")]
        public async Task<IActionResult> ScanNow(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "plants/scannow")] HttpRequest req, ILogger log)
        {
            await _ioTCentralCommandsService.ScanNow();

            var status = await _plantService.GetStatus();
            return new OkObjectResult(status);
        }
    }
}
