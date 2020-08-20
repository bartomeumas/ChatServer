using ChatServer.Models;
using ChatServer.SerDes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace ChatServer.Controllers
{
    static class Delivery
    {
        /* This class will be in charge of delivering all the
         messages the client or server wants to send */
        public static Response Selector(Request request, string sender, Hashtable clientsList)
        {
            switch (request.Type)
            {
                case "UNICAST":
                    return UniCast(request, sender, clientsList);

                case "MULTICAST":
                    return MultiCast(request, sender, clientsList);

                case "BROADCAST":
                    return BroadCast(request, sender, clientsList);

            }
            return new Response("201", request.ReqID);
        }

        private static Response UniCast(Request request, string sender, Hashtable clientsList)
        {
            if (clientsList.ContainsKey(request.UserName)) // Check if the user is online
            {
                TcpClient unicastSocket;
                unicastSocket = (TcpClient)clientsList[request.UserName];
                NetworkStream unicastStream = unicastSocket.GetStream();
                Response response = new Response("DELIVER", sender, request.ReqID, request.Msg);
                Byte[] unicastBytes = Encoding.ASCII.GetBytes(Serializer.ResponseMaker(response)); //Here we will pass the args and call Serializer

                unicastStream.Write(unicastBytes, 0, unicastBytes.Length);
                unicastStream.Flush();

                return new Response("101", request.ReqID);
            }
            return new Response("203", request.ReqID);
        }

        private static Response MultiCast(Request request, string sender, Hashtable clientsList)
        {
            List<string> notAvailable = new List<string>();

            foreach (var destUser in request.Users)
            {
                if (clientsList.ContainsKey(request.UserName)) // Check if the user is online
                {
                    TcpClient multicastSocket;
                    multicastSocket = (TcpClient)clientsList[destUser];
                    NetworkStream multicastStream = multicastSocket.GetStream();
                    Response response = new Response("DELIVER", sender, request.ReqID, request.Msg);
                    Byte[] multicastBytes = Encoding.ASCII.GetBytes(Serializer.ResponseMaker(response)); //Here we will pass the args and call Serializer

                    multicastStream.Write(multicastBytes, 0, multicastBytes.Length);
                    multicastStream.Flush();
                }
                else
                {
                    notAvailable.Add(destUser);
                }
            }
            if (notAvailable.Count == 0)
                return new Response("101", request.ReqID); // Delivered to all users
            else if (notAvailable.Count == request.Users.Count)
                return new Response("203", request.ReqID); // Delivered any user
            else
                return new Response("204", request.ReqID, notAvailable); // Partial delivery
        }

        private static Response BroadCast(Request request, string sender, Hashtable clientsList)
        {
            foreach (DictionaryEntry Item in clientsList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;
                Response response = new Response("DELIVER", sender, request.ReqID, request.Msg);

                broadcastBytes = Encoding.ASCII.GetBytes(Serializer.ResponseMaker(response)); // Create a response of type DELIVER\nreqID\nSource|msg

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();

            }
            return new Response("101", request.ReqID);
        }

        public static void SendResponse(Response response, TcpClient destinatary)
        {
            TcpClient clientSocket;
            clientSocket = (TcpClient)destinatary;
            NetworkStream responseStream = clientSocket.GetStream();
            Byte[] responsecastBytes = Encoding.ASCII.GetBytes(Serializer.ResponseMaker(response));

            responseStream.Write(responsecastBytes, 0, responsecastBytes.Length);
            responseStream.Flush();
        }
    }
}
