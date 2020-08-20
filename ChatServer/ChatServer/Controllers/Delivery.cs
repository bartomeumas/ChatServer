using ChatServer.Models;
using ChatServer.SerDes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer.Controllers
{
    static class Delivery
    {
        /* This class will be in charge of delivering all the
         messages the client or server wants to send */

        public static void SendResponse(Response response, TcpClient destinatary)
        {
            TcpClient clientSocket;
            clientSocket = (TcpClient)destinatary;
            NetworkStream responseStream = clientSocket.GetStream();
            Byte[] responsecastBytes = Encoding.ASCII.GetBytes(Serializer.ResponseMaker(response));

            responseStream.Write(responsecastBytes, 0, responsecastBytes.Length);
            responseStream.Flush();
        }

        public static void UniCast(string msg, string sender, TcpClient destinatary)
        {
            TcpClient unicastSocket;
            unicastSocket = (TcpClient)destinatary;
            NetworkStream unicastStream = unicastSocket.GetStream();
            Byte[] unicastBytes = Encoding.ASCII.GetBytes(sender + msg); //Here we will pass the args and call Serializer

            unicastStream.Write(unicastBytes, 0, unicastBytes.Length);
            unicastStream.Flush();

        }

        public static void MultiCast(string msg, string sender, List <TcpClient> destinataries, Hashtable clientsList) // List will be a list of strings
                                                                                                                       // and we will find the value of that
                                                                                                                       // key in the hashtable

        // Hashtable (key: string) (value: TcpClient)
        {
            foreach (var item in destinataries)
            {
                TcpClient multicastSocket;
                multicastSocket = (TcpClient)item;
                NetworkStream multicastStream = multicastSocket.GetStream();
                Byte[] multicastBytes =  Encoding.ASCII.GetBytes(sender + msg); //Here we will pass the args and call Serializer

                multicastStream.Write(multicastBytes, 0, multicastBytes.Length);
                multicastStream.Flush();
            }

        }
        
        public static void BroadCast(string msg, string sender, Hashtable clientsList)
        {
            foreach (DictionaryEntry Item in clientsList)
            {
                if ((string)Item.Key == sender) // Not send the msg to itself
                {
                    continue;
                }
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;

                broadcastBytes = Encoding.ASCII.GetBytes(msg); // Create a response of type DELIVER\nreqID\nSource|msg

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }



    }
}
