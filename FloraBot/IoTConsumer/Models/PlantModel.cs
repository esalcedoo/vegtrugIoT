using IoTConsumer.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTConsumer.Models
{
    public class PlantModel
    {
        public PlantModel(string id, string name, Range conductivity, Range light, Range moisture, Range temperature)
        {
            Id = id;
            Name = name;
            Conductivity = conductivity;
            Light = light;
            Moisture = moisture;
            Temperature = temperature;
        }

        public string Id { get; }
        public string Name { get; }
        public Range Conductivity { get; }
        public Range Light { get; }
        public Range Moisture { get; }
        public Range Temperature { get; }

        internal bool IsHappy(FloraMessageModel floraMessage)
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
