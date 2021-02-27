using FloraModels.Extensions;
using FloraModels.Ranges.Converters;
using System;
using System.Text.Json.Serialization;

namespace FloraModels
{
    public class PlantModel
    {
        protected PlantModel() { }

        public PlantModel(int id, string name, Range conductivity, Range light, Range moisture, Range temperature)
        {
            Id = id;
            Name = name;
            Conductivity = conductivity;
            Light = light;
            Moisture = moisture;
            Temperature = temperature;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(RangeConverter))]
        public Range Conductivity { get; set; }
        [JsonConverter(typeof(RangeConverter))]
        public Range Light { get; set; }
        [JsonConverter(typeof(RangeConverter))]
        public Range Moisture { get; set; }
        [JsonConverter(typeof(RangeConverter))]
        public Range Temperature { get; set; }

        public bool IsHappy(
            int conductivity, float light, int moisture, float temperature)
        {
            return Conductivity.Contains(conductivity)
                && Light.Contains(light)
                && Moisture.Contains(moisture)
                && Temperature.Contains(temperature);
        }

        public bool IsHappy(StatusPlantModel StatusPlant)
        {
            return Conductivity.Contains(StatusPlant.Conductivity)
                && Light.Contains(StatusPlant.Light)
                && Moisture.Contains(StatusPlant.Moisture)
                && Temperature.Contains(StatusPlant.Temperature);
        }

        public bool NeedsWater(int moisture)
        {
            return moisture < Moisture.Start.Value;
        }

        public bool IsOverFloaded(int moisture)
        {
            return moisture > Moisture.End.Value;
        }

        public bool NeedsMoreLight(float light)
        {
            return light < Light.Start.Value;
        }

        public bool IsBurning(float light)
        {
            return light > Light.End.Value;
        }

        public bool IsCold(float temperature)
        {
            return temperature < Temperature.Start.Value;
        }

        public bool IsHot(float temperature)
        {
            return temperature > Temperature.End.Value;
        }

        public bool NeedsFertilizer(int conductivity)
        {
            return conductivity < Conductivity.Start.Value;
        }

        public bool HasTooMuchFertilizer(int conductivity)
        {
            return conductivity > Conductivity.End.Value;
        }
    }
}
