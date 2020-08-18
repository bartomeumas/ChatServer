using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace ChatServer.SerDes
{
    class Deserializer
    {
        // Ejemplo:
        // SEND\n0000|Unicast\nMark,Cesar,Andres|"Este es el mensaje"
        public Request Reciever(string req)
        {
            Request request;
            string[] sections = req.Split('\n');
            string verb = sections[0].ToUpper();
            switch (verb)
            {
                case "SEND":
                    return SendDeserializer(sections);




                default:
                    break;
            }

            return new Request("INVALID", "0000"); //Invalid Verb
        }

        public Request SendDeserializer(string[] section)
        {
            string[] header = section[1].Split('|');
            string body = section[2];

            string reqID = header[0];
            string typeOfCast = header[1].ToUpper();

            switch (typeOfCast)
            {
                case "UNICAST":
                    return new Request("SEND", "0000");

                case "MULTICAST":
                    return new Request("SEND", "0000");

                case "BROADCAST":
                    return new Request("SEND", "0000");


                default:
                    break;
            }
            return new Request("INVALID", "0000"); //Invalid type
        }
    }


}
