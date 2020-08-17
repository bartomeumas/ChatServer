using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServer.Serializer
{
    class Deserializer
    {
        // Ejemplo:
        // SEND\n0000|Unicast\nMark,Cesar,Andres|"Este es el mensaje"
        public void Reciever(string req)
        {

            string[] sections = req.Split('\n');
            string verb = sections[0];

            if (verb == "SEND")
            {

            }

        }
    }
}
