﻿using System;

namespace IoTConsumer.Extensions
{
    public static class RangeExtension
    {
        public static bool Contains(this Range range, float value)
        {
            return range.Start.Value <= (int)value && (int)value <= range.End.Value;
        }

        public static bool Contains(this Range range, int value)
        {
            return range.Start.Value <= value && value <= range.End.Value;
        }
    }
}
