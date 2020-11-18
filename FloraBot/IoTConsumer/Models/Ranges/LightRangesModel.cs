using System;
using System.Collections.Generic;
using System.Text;

namespace IoTConsumer.Models
{
    public static class LightRangesModel
    {
        public static Range HighRange => 4000..60000;
        public static Range MediumRange => 800..30000;
        public static Range LowRange => 800..1500;
    }
}
