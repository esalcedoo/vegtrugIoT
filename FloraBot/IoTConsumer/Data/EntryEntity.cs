using System;

namespace IoTConsumer.Data
{
    public class EntryEntity
    {
        public int Id { get; set; }

        public int PlantId { get; set; }

        public int Conductivity { get; set; }

        public float Light { get; set; }

        public int Moisture { get; set; }

        public float Temperature { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
