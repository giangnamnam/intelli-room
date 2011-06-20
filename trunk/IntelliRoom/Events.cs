using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;

namespace IntelliRoom
{
    public class Events
    {
        public static event EventHandler<Data.InfoMessages.Message> newMessage;

        public Events()
        {
            Data.InfoMessages.newMessage += new EventHandler<Data.InfoMessages.Message>(InfoMessages_newMessage);
        }

        void InfoMessages_newMessage(object sender, Data.InfoMessages.Message e)
        {
            newMessage.Invoke(null, e);
        }
    }
}
