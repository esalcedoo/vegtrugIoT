using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace IoTConsumer.Serializer
{
    internal static class JsonSerializerOptionsProvider
    {
        private static JsonSerializerOptions _options;

        public static JsonSerializerOptions Options
        {
            get
            {
                if (_options == null)
                {
                    _options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        PropertyNameCaseInsensitive = true,
                    };
                }

                return _options;
            }
        }
    }
}
