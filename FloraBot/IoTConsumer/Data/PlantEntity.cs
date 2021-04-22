namespace IoTConsumer.Data
{
    public class PlantEntity
    {
        public int Id { get; set; }
        //public string Component_name { get; set; } //Vegtrug1
        public string Name { get; set; } //Monstera
        public int ConductivityLowerValue { get; set; }
        public int ConductivityHigherValue { get; set; }
        public float LightLowerValue { get; set; }
        public float LightHigherValue { get; set; }
        public int MoistureLowerValue { get; set; }
        public int MoistureHigherValue { get; set; }
        public float TemperatureLowerValue { get; set; }
        public float TemperatureHigherValue { get; set; }
    }
}
