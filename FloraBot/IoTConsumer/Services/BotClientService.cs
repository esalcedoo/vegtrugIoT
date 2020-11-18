using IoTConsumer.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace IoTConsumer.Services
{
    public class BotClientService
    {
        private readonly HttpClient _client;
        private readonly ILogger _log;

        public BotClientService(HttpClient client, ILogger<BotClientService> log)
        {
            _client = client;
            _log = log;
        }

        internal async Task SendProactiveMessageAsync(string notificationMessage)
        {
            var response = await _client.PostAsJsonAsync("notify", new Message(notificationMessage));

            if (response.IsSuccessStatusCode)
            {
                _log.LogInformation($"Notification sent to bot: {notificationMessage}");
            }
        }
    }
}
