using IoTConsumer.IoTCentral.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IoTConsumer.IoTCentral
{
    class IoTCentralCommandsService
    {
        private readonly HttpClient _client;
        private readonly ILogger _log;

        public IoTCentralCommandsService(HttpClient client, ILogger<IoTCentralCommandsService> log)
        {
            _client = client;
            _log = log;
        }

        internal async Task ScanNow()
        {
            var response = await _client.PostAsJsonAsync("/api/preview/devices/raspi_esalcedoo/commands/ScanNow", new ScanNowRequest());
            if (response.IsSuccessStatusCode)
            {
                _log.LogInformation($"ScanNow command sent");
            }
        }
    }
}
