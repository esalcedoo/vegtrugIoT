using IoTConsumer.Extensions;
using System;

namespace IoTConsumer.Models
{
    public class PlantModel
    {
        protected PlantModel() { }

        public PlantModel(string name, Range conductivity, Range light, Range moisture, Range temperature)
        {
            Name = name;
            Conductivity = conductivity;
            Light = light;
            Moisture = moisture;
            Temperature = temperature;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Range Conductivity { get; set; }
        public Range Light { get; set; }
        public Range Moisture { get; set; }
        public Range Temperature { get; set; }

        internal bool IsHappy(FloraDeviceMessageModel floraMessage)
        {
            return Conductivity.Contains(floraMessage.Conductivity)
                && Light.Contains(floraMessage.Light)
                && Moisture.Contains(floraMessage.Moisture)
                && Temperature.Contains(floraMessage.Temperature);
        }

        internal bool NeedsWater(int moisture)
        {
            return moisture < Moisture.Start.Value;
        }

        internal bool IsOverFloaded(int moisture)
        {
            return moisture > Moisture.End.Value;
        }

        internal bool NeedsMoreLight(float light)
        {
            return light < Light.Start.Value;
        }

        internal bool IsBurning(float light)
        {
            return light > Light.End.Value;
        }

        internal bool IsCold(float temperature)
        {
            return temperature < Temperature.Start.Value;
        }

        internal bool IsHot(float temperature)
        {
            return temperature > Temperature.End.Value;
        }

        internal bool NeedsFertilizer(int conductivity)
        {
            return conductivity < Conductivity.Start.Value;
        }

        internal bool HasTooMuchFertilizer(int conductivity)
        {
            return conductivity > Conductivity.End.Value;
        }
    }
}
