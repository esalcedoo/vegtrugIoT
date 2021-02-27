using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IoTConsumer.Services;

namespace IoTConsumer
{
    public class PlantsStatusFunction
    {
        private readonly PlantService _plantService;

        public PlantsStatusFunction(PlantService plantService)
        {
            _plantService = plantService;
        }

        [FunctionName("PlantsStatusFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "plants/status")] HttpRequest req,
            ILogger log)
        {
            var status = await _plantService.GetStatus();
            return new OkObjectResult(status);
        }
    }
}
