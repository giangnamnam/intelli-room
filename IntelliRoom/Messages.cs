using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelliRoom
{
    //Clase que gestiona mensajes de informacion o error en el sistema, es solo de caracter informativo
    class Messages
    {

        //evento de informacion
        public event EventHandler<EventArgs> informationEvent;


        public Messages()
        {
            //suscripcion a todos los modulos que envien mensajes

        }

        public void sendInformationEvent(object sender, EventArgs e)
        {
            this.informationEvent(sender, e);
        }
        
    }
}
