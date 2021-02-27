using System;
using System.Collections.Generic;
using System.Text;

namespace IoTConsumer.IoTCentral.Models
{
    class ScanNowRequest
    {
        public Request Request { get; set; }
        public ScanNowRequest()
        {
            Request = new Request();
        }
    }
    public class Request
    {
        public int TempVal { get; set; } = 30;
    }
}
