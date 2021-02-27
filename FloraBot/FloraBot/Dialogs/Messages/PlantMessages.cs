using FloraModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloraBot.Dialogs.Messages
{
    public static class PlantMessages
    {
        internal static string Summary(List<PlantModel> plants, IEnumerable<StatusPlantModel> plantsStatus)
        {
            Dictionary<string, List<string>> messageParameters = new();

            foreach (var plant in plants)
            {
                var plantStatus = plantsStatus.FirstOrDefault(ps => ps.PlantId == plant.Id);

                if (!plant.IsHappy(plantStatus))
                {
                    if (plant.NeedsWater(plantStatus.Moisture))
                    {
                        messageParameters.AddOrUpdate(key: "necesita agua la ", plantName: plant.Name);
                    }
                    if (plant.IsOverFloaded(plantStatus.Moisture))
                    {
                        messageParameters.AddOrUpdate(key: "se está inundando la ", plantName: plant.Name);
                    }
                    if (plant.NeedsMoreLight(plantStatus.Light))
                    {
                        messageParameters.AddOrUpdate(key: "necesita más luz la ", plantName: plant.Name);
                    }
                    if (plant.IsBurning(plantStatus.Light))
                    {
                        messageParameters.AddOrUpdate(key: "se está quemando la ", plantName: plant.Name);
                    }
                    if (plant.NeedsFertilizer(plantStatus.Conductivity))
                    {
                        messageParameters.AddOrUpdate(key: "necesita fertilizante la ", plantName: plant.Name);
                    }
                    if (plant.HasTooMuchFertilizer(plantStatus.Conductivity))
                    {
                        messageParameters.AddOrUpdate(key: "se están quemando las raíces la ", plantName: plant.Name);
                    }
                    if (plant.IsHot(plantStatus.Temperature))
                    {
                        messageParameters.AddOrUpdate(key: "tiene calor la ", plantName: plant.Name);
                    }
                    if (plant.IsCold(plantStatus.Temperature))
                    {
                        messageParameters.AddOrUpdate(key: "tiene frío la ", plantName: plant.Name);
                    }
                }
            }

            return CreateMessage(messageParameters);
        }

        private static void AddOrUpdate(this Dictionary<string, List<string>> messageParameters, string key, string plantName)
        {
            if (!messageParameters.ContainsKey(key))
            {
                messageParameters.Add(key, new List<string>() { plantName });
            }
            else if (messageParameters.TryGetValue(key, out List<string> plantNameList))
            {
                plantNameList.Add(plantName);
                messageParameters[key] = plantNameList;
            }
        }

        private static string CreateMessage(Dictionary<string, List<string>> messageParameters)
        {
            StringBuilder stringBuilder = new();
            foreach (KeyValuePair<string, List<string>> status in messageParameters)
            {
                stringBuilder.Append(status.Key);
                stringBuilder.Append(string.Join(" y la ", status.Value));
                stringBuilder.Append(". ");
            }
            return stringBuilder.Length > 0 ? stringBuilder.ToString() : "Todas están contentas";
        }
    }
}
