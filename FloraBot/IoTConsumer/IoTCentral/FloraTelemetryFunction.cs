using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventHubs;
using System.Text;
using Microsoft.Extensions.Logging;
using IoTConsumer.Services;
using System.Threading.Tasks;
using FloraModels;
using IoTConsumer.IoTCentralFunction.Models;
using IoTConsumer.Serializer;

namespace IoTConsumer.IoTCentralFunction
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

        [FunctionName("IoTCentralFloraTelemetryFunction")]
        public async Task RunAsync([EventHubTrigger("messages/events", Connection = "ConnectionString")] EventData message, ILogger log)
        {
            FloraDeviceMessageModel floraMessage = System.Text.Json.JsonSerializer.Deserialize <FloraDeviceMessageModel> (
                Encoding.UTF8.GetString(message.Body.Array),
                JsonSerializerOptionsProvider.Options);

            log.LogInformation($"C# Event Hub trigger function processed a message: {floraMessage}");


            if (floraMessage.MessageProperties.MAC != null)
            {
                await _plantService.ProcessFloraDeviceMessage(floraMessage);

                PlantModel plant = await _plantService.FindPlantByDeviceId(floraMessage.MessageProperties.MAC);

                if (!plant.IsHappy(floraMessage.Telemetry.Fertility,
                                   floraMessage.Telemetry.Light,
                                   floraMessage.Telemetry.Humidity,
                                   floraMessage.Telemetry.Temperature)
                    && plant.NeedsWater(floraMessage.Telemetry.Humidity))
                {
                    await _botClientService.SendProactiveMessageAsync($"{plant.Name} necesita agua");
                }
            }
        }
    }
}