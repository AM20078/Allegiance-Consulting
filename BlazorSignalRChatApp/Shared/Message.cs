using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSignalRChatApp.Shared
{
    public class Message
    {
        public Message(string user, string message, bool sender)
        {
            Username = user;
            Messages = message;
            Sender = sender;
        }

        public string Username { get; set; }
        public string Messages { get; set; }
        public bool Sender { get; set; }

        //Style in which the messages are send out
        public string Display => Sender ? "sent" : "received";
    }
}
