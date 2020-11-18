using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraBot.Models
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
