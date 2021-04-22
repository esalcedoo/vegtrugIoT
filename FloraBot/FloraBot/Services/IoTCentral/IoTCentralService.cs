using Microsoft.Extensions.Options;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FloraBot.Services.IoTCentral
{
    public class IoTCentralService
    {
        private readonly HttpClient _httpClient;

        public IoTCentralService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetFertilityAsync(string component_name)
        {
            string requestUri = $"/components/{component_name}/telemetry/fertility";
            return await _httpClient.GetFromJsonAsync<int>(requestUri);
        }

        public async Task<float> GetLightAsync(string component_name)
        {
            string requestUri = $"/components/{component_name}/telemetry/light";
            return await _httpClient.GetFromJsonAsync<int>(requestUri);
        }

        public async Task<int> GetHumidityAsync(string component_name)
        {
            string requestUri = $"/components/{component_name}/telemetry/humidity";
            return await _httpClient.GetFromJsonAsync<int>(requestUri);
        }

        public async Task<float> GetTemperatureAsync(string component_name)
        {
            string requestUri = $"/components/{component_name}/telemetry/temperature";
            return await _httpClient.GetFromJsonAsync<int>(requestUri);
        }
    }
}
