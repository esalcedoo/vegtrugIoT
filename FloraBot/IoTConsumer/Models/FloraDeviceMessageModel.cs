using System;
using System.Text.Json.Serialization;

namespace IoTConsumer.Models
{
    public class FloraDeviceMessageModel
    {
        [JsonPropertyName("MI_ID")]
        public string DeviceId { get; set; }

        [JsonPropertyName("MI_BATTERY")]
        public int Battery { get; set; }

        [JsonPropertyName("MI_CONDUCTIVITY")]
        public int Conductivity { get; set; }

        [JsonPropertyName("MI_LIGHT")]
        public float Light { get; set; }

        [JsonPropertyName("MI_MOISTURE")]
        public int Moisture { get; set; }

        [JsonPropertyName("MI_TEMPERATURE")]
        public float Temperature { get; set; }

        public DateTime? Timestamp { get; set; }

        public override string ToString()
        {
            return $"DeviceId: {DeviceId}, Battery: {Battery}, Conductivity: {Conductivity}, Light: {Light}, Moisture: {Moisture}, Temperature: {Temperature}";
        }
    }
}
