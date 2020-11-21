namespace FloraBot.Models
{
    public class FloraMessageModel
    {
        public string Id { get; set; }
        public int Battery { get; set; }
        public int Conductivity { get; set; }
        public float Light { get; set; }
        public int Moisture { get; set; }
        public float Temperature { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Battery: {Battery}, Conductivity: {Conductivity}, Light: {Light}, Moisture: {Moisture}, Temperature: {Temperature}";
        }
    }
}
