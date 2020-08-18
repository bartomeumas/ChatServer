using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace ChatServer.SerDes
{
    static class Deserializer
    {
        // Ejemplo:
        // SEND\n0000|Multicast\nMark,Cesar,Andres|"Este es el mensaje"
        public static Request Reciever(string req)
        {
            //Request request;
            string[] sections = req.Split('\n');
            string verb = sections[0].ToUpper();
            switch (verb)
            {
                case "CONNECT":
                    return ConnectDeserializer(sections);
                    break;

                case "SEND":
                    return SendDeserializer(sections);
                    break;

                case "LIST":
                    return ListDeserializer(sections);
                    break;

                case "DISCONNECT":
                    return DisconnectDeserializer(sections);
                    break;

            }
            return new Request("INVALID", "0000"); //Invalid Verb
        }

        private static Request ConnectDeserializer(string[] sections)
        {
            try
            {
                string reqID = sections[1];
                string username = sections[2];

                return new Request(sections[0], reqID, username);

            }
            catch (Exception)
            {
                return new Request("INVALID", "0000");
            }

        }

        private static Request ListDeserializer(string[] sections)
        {
            try
            {
                string reqID = sections[1];

                return new Request(sections[0], reqID);

            }
            catch (Exception)
            {
                return new Request("INVALID", "0000");
            }

        }

        private static Request SendDeserializer(string[] section)
        {
            try
            {
                string[] header = section[1].Split('|');
                string body = section[2];

                string reqID = header[0];
                string typeOfCast = header[1].ToUpper();

                switch (typeOfCast)
                {
                    case "UNICAST":
                        string[] bodyElementsUNI = body.Split('|');
                        string user = bodyElementsUNI[0];
                        string msgUNI = bodyElementsUNI[1];
                        return new Request(section[0], reqID, typeOfCast, user, msgUNI);
                        break;

                    case "MULTICAST":
                        string[] bodyElementsMULTI = body.Split('|');
                        string users = bodyElementsMULTI[0];
                        string msgMULTI = bodyElementsMULTI[1];
                        return new Request(section[0], reqID, typeOfCast, users, msgMULTI);
                        break;

                    case "BROADCAST":
                        return new Request(section[0],reqID, typeOfCast, body);
                        break;
                }
                return new Request("INVALID", "0000"); //Invalid type

            }
            catch (Exception)
            {
                return new Request("INVALID", "0000");
            }
        }

        private static Request DisconnectDeserializer(string[] sections)
        {
            try
            {
                string reqID = sections[1];

                return new Request(sections[0], reqID);

            }
            catch (Exception)
            {
                return new Request("INVALID", "0000");
            }

        }
    }


}
