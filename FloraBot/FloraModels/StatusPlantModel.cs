using System;

namespace FloraModels
{
    public class StatusPlantModel
    {
        public int PlantId { get; set; }
        public string Name { get; set; }
        public int Conductivity { get; set; }
        public float Light { get; set; }
        public int Moisture { get; set; }
        public float Temperature { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Conductivity: {Conductivity}, Light: {Light}, Moisture: {Moisture}, Temperature: {Temperature}";
        }
    }
}
