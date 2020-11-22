using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using IoTConsumer.Services;

namespace IoTConsumer
{
    public class PlantsInfoFunction
    {
        private readonly PlantService _plantService;

        public PlantsInfoFunction(PlantService plantService)
        {
            _plantService = plantService;
        }

        [FunctionName("PlantsInfoFunction")]
        public async Task<IActionResult> GetPlantsInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "plants/findbyids")] List<int> ids,
            ILogger log)
        {
            var plants = await _plantService.FindPlantsById(ids);
            var output = System.Text.Json.JsonSerializer.Serialize(plants);
            return new OkObjectResult(output);
        }

        [FunctionName("PlantInfoFunction")]
        public async Task<IActionResult> GetPlantInfo(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "plants/{id:int}")] HttpRequest req, int id,
        ILogger log)
        {
            var plant = await _plantService.FindPlantById(id);
            var output = System.Text.Json.JsonSerializer.Serialize(plant);
            return new OkObjectResult(output);
        }
    }
}
