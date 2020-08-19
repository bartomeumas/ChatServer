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
        // SEND\n0000|Multicast\nMark,Cesar,Andres|Este es el mensaje
        public static Request Reciever(string req)
        {
            //Request request;
            string[] sections = req.Split('\n');
            string verb = sections[0].ToUpper();
            switch (verb)
            {
                case "CONNECT":
                    return ConnectDeserializer(sections);

                case "SEND":
                    return SendDeserializer(sections);

                case "LIST":
                    return ListDeserializer(sections);

                case "DISCONNECT":
                    return DisconnectDeserializer(sections);

            }
            return new Request("INVALID", sections[1].Split('|')[0]); //Invalid Verb
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
                return new Request("INVALID", sections[1].Split('|')[0]);
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
                return new Request("INVALID", sections[1].Split('|')[0]);
            }

        }

        private static Request SendDeserializer(string[] sections)
        {
            try
            {
                string[] header = sections[1].Split('|');
                string body = sections[2];

                string reqID = header[0];
                string typeOfCast = header[1].ToUpper();

                switch (typeOfCast)
                {
                    case "UNICAST":
                        string[] bodyElementsUNI = body.Split('|');
                        string user = bodyElementsUNI[0];
                        string msgUNI = bodyElementsUNI[1];
                        return new Request(sections[0], reqID, typeOfCast, user, msgUNI);

                    case "MULTICAST":
                        string[] bodyElementsMULTI = body.Split('|');
                        string users = bodyElementsMULTI[0];
                        string msgMULTI = bodyElementsMULTI[1];
                        return new Request(sections[0], reqID, typeOfCast, users, msgMULTI);

                    case "BROADCAST":
                        return new Request(sections[0],reqID, typeOfCast, body);
                }
                return new Request("INVALID", sections[1].Split('|')[0]); //Invalid type

            }
            catch (Exception)
            {
                return new Request("INVALID", sections[1].Split('|')[0]);
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
                return new Request("INVALID", sections[1].Split('|')[0]);
            }

        }
    }
}
