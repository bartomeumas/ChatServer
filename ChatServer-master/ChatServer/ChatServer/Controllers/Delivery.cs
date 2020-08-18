using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ChatServer.Controllers
{
    class Delivery
    {
        /* This class will be in charge of delivering all the
         messages the user wants to send */
        public static void UniCast(string msg, string sender, TcpClient destinatary)
        {
            TcpClient unicastSocket;
            unicastSocket = (TcpClient)destinatary;
            NetworkStream unicastStream = unicastSocket.GetStream();
            Byte[] broadcastBytes = Encoding.ASCII.GetBytes(sender + msg); //Here we will pass the args and call Serializer

            unicastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
            unicastStream.Flush();

        }

        public static void MultiCast(string msg, string sender, List <TcpClient> destinataries)
        {
            foreach (var item in destinataries)
            {
                TcpClient multicastSocket;
                multicastSocket = (TcpClient)item;
                NetworkStream multicastStream = multicastSocket.GetStream();
                Byte[] broadcastBytes =  Encoding.ASCII.GetBytes(sender + msg); //Here we will pass the args and call Serializer

                multicastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                multicastStream.Flush();
            }

        }
        
        public static void BroadCast(string msg, string sender, bool flag, Hashtable clientsList) //Flag?
        {
            foreach (DictionaryEntry Item in clientsList)
            {
                if ((string)Item.Key == sender)
                {
                    continue;
                }
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;

                broadcastBytes = Encoding.ASCII.GetBytes(msg);
                //if (flag == true)
                //{
                //    broadcastBytes = Encoding.ASCII.GetBytes(sender + " says : " + msg);
                //}
                //else
                //{
                //    broadcastBytes = Encoding.ASCII.GetBytes(msg);
                //}

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }

    }
}
