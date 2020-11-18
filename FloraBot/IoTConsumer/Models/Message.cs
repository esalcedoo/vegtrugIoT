using System;
using System.Collections.Generic;
using System.Text;

namespace IoTConsumer.Models
{
    public class Message
    {
        public Message(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
