using System;

namespace IoTConsumer.IoTCentralFunction.Models
{
    public class FloraDeviceMessageModel
    {
        public DateTime EnqueuedTime { get; set; }
        public Telemetry Telemetry { get; set; }
        public Messageproperties MessageProperties { get; set; }
        public string Component { get; set; }
    }

    public class Telemetry
    {
        public int Battery { get; set; }
        public int Fertility { get; set; }
        public float Light { get; set; }
        public int Humidity { get; set; }
        public float Temperature { get; set; }
    }

    public class Messageproperties
    {
        public string MAC { get; set; }
    }
}
