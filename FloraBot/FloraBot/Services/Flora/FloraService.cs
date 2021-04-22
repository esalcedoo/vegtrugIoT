using FloraModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FloraBot.Services.Flora
{
    public class FloraService
    {
        private readonly HttpClient _client;

        public FloraService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<StatusPlantModel>> GetPlantsCurrentStatus()
        {
            List<StatusPlantModel> plantsStatus = await _client.GetFromJsonAsync<List<StatusPlantModel>>("scannow");
            return plantsStatus;
        }

        public async Task<List<StatusPlantModel>> GetPlantsStatus()
        {            
            List<StatusPlantModel> plantsStatus = await _client.GetFromJsonAsync<List<StatusPlantModel>>("status");

            return plantsStatus;
        }

        public async Task<PlantModel> GetPlantInfo(int id)
        {
            PlantModel plantInfo = await _client.GetFromJsonAsync<PlantModel>(id.ToString());
            return plantInfo;
        }

        public async Task<List<PlantModel>> GetPlantsInfo(List<int> ids)
        {
            var content = new StringContent(JsonSerializer.Serialize(ids), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("findbyids", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<PlantModel>>();
            }

            return new List<PlantModel>();
        }
    
    }
}
