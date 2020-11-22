using FloraModels;
using System;

namespace IoTConsumer.Data
{
    static class PlantEntityExtensions
    {
        public static PlantModel ToModel(this PlantEntity plantEntity)
        {
            var conductivity = new Range(new Index(plantEntity.ConductivityLowerValue), new Index(plantEntity.ConductivityHigherValue));
            var light = new Range(new Index((int)plantEntity.LightLowerValue), new Index((int)plantEntity.LightHigherValue));
            var temperature = new Range(new Index((int)plantEntity.TemperatureLowerValue), new Index((int)plantEntity.TemperatureHigherValue));
            var moisture = new Range(new Index(plantEntity.MoistureLowerValue), new Index(plantEntity.MoistureHigherValue));

            return new PlantModel(plantEntity.Id, plantEntity.Name, conductivity, light, moisture, temperature);
        }
    }
}
