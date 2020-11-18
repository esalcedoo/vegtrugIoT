namespace IoTConsumer.Data
{
    public class FloraDeviceEntity
    {
        public string Id { get; set; }
        public int? PlantId { get; set; }
        public bool Active { get; set; }
        public int Battery { get; set; }
        public PlantEntity Plant { get; set; }
    }
}
