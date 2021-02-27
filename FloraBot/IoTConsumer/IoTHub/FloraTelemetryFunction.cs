using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventHubs;
using System.Text;
using Microsoft.Extensions.Logging;
using IoTConsumer.Services;
using System.Threading.Tasks;
using FloraModels;
using IoTConsumer.IoTHub.Models;

namespace IoTConsumer.IoTHub
{
    public class FloraTelemetryFunction
    {
        private readonly PlantService _plantService;
        private readonly BotClientService _botClientService;

        public FloraTelemetryFunction(PlantService plantService, BotClientService botClientService)
        {
            _plantService = plantService;
            _botClientService = botClientService;
        }

        [FunctionName("IoTHubFloraTelemetryFunction")]
        public async Task RunAsync([IoTHubTrigger("messages/events", Connection = "ConnectionString")] EventData message, ILogger log)
        {
            FloraDeviceMessageModel floraMessage = System.Text.Json.JsonSerializer.Deserialize<FloraDeviceMessageModel>(Encoding.UTF8.GetString(message.Body.Array));
            log.LogInformation($"C# IoT Hub trigger function processed a message: {floraMessage}");

            await _plantService.ProcessFloraDeviceMessage(floraMessage);

            PlantModel plant = await _plantService.FindPlantByDeviceId(floraMessage.DeviceId);

            if (!plant.IsHappy(floraMessage.Conductivity,
                               floraMessage.Light,
                               floraMessage.Moisture,
                               floraMessage.Temperature) && plant.NeedsWater(floraMessage.Moisture))
            {
                await _botClientService.SendProactiveMessageAsync($"{plant.Name} necesita agua");
            }
        }
    }
}