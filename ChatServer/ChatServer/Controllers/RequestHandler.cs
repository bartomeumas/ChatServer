using ChatServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace ChatServer.Controllers
{
    static class RequestHandler
    {
        static public Response Selector(Request request, string sender, Hashtable clientsList)
        {
            switch (request.Verb)
            {
                case "SEND":
                    return Delivery.Selector(request, sender, clientsList);

                case "LIST":
                    return new Response("100", request.ReqID, GetList(clientsList, sender));

                case "DISCONNECT":
                    Program.clientsList.Remove(sender);
                    return new Response("100", request.ReqID);
            }
            return new Response("200", request.ReqID);
        }

        static private List<string> GetList(Hashtable clientsList, string sender)
        {
            List<string> users = new List<string>();
            foreach (var client in clientsList.Keys)
            {
                if ((string)client == sender) // Not add itself to the list
                {
                    continue;
                }
                users.Add((string)client);
            }

            return users;
        }
    }
}
