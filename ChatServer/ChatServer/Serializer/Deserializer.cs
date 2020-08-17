using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServer.Serializer
{
    class Deserializer
    {
        public void Reciever(string req)
        {
            string[] sections = req.Split('\n');
            string verb = sections[0];

        }
    }
}
