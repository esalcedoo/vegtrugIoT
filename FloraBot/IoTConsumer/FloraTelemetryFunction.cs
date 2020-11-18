using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using IoTConsumer.Models;
using IoTConsumer.Services;
using System.Threading.Tasks;

namespace IoTConsumer
{
    public class FloraTelemetryFunction
    {
        private readonly IPlantService _plantService;
        private readonly BotClientService _botClientService;

        public FloraTelemetryFunction(IPlantService plantService, BotClientService botClientService)
        {
            _plantService = plantService;
            _botClientService = botClientService;
        }

        [FunctionName("FloraTelemetryFunction")]
        public async Task RunAsync([IoTHubTrigger("messages/events", Connection = "ConnectionString")] EventData message, ILogger log)
        {
            FloraMessageModel floraMessage = System.Text.Json.JsonSerializer.Deserialize<FloraMessageModel>(Encoding.UTF8.GetString(message.Body.Array));
            log.LogInformation($"C# IoT Hub trigger function processed a message: {floraMessage}");

            PlantModel plant = _plantService.FindById(floraMessage.Id);

            if (!plant.IsHappy(floraMessage) && plant.NeedsWater(floraMessage.Moisture))
            {
                await _botClientService.SendProactiveMessageAsync($"{plant.Name} necesita agua");
            }
        }
    }
}