using System;
using System.Collections.Generic;
using System.Text;

namespace IoTConsumer.Models
{
    public class ConductivityRangesModel
    {
        public static Range HighRange => 350..2000;
        public static Range MediumRange => 300..1500;
        public static Range LowRange => 100..1000;
    }
}
