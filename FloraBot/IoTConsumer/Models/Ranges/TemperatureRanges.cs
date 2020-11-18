using System;

namespace IoTConsumer.Models
{
    class TemperatureRanges
    {
        public static Range HighRange => 12..35;
        public static Range LowRange => 8..32;
        public static Range ExtremeRange => 8..35;
    }
}
