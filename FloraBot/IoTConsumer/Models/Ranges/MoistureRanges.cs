using System;

namespace IoTConsumer.Models
{
    class MoistureRanges
    {
        public static Range HighRange => 15..65;
        public static Range MediumRange => 15..60;
        public static Range LowRange => 7..50;
    }
}
