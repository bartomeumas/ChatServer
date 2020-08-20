using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Collections;
using ChatServer.Controllers;
using ChatServer.SerDes;
using ChatServer.Models;

namespace ChatServer
{
    class Program
    {
        public static Hashtable clientsList = new Hashtable();

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Chat server...");
            int port = 8080;
            TcpListener server = new TcpListener(IPAddress.Loopback, port); //The Loopback field is equivalent to 127.0.0.1 in dotted-quad notation.
            TcpClient clientSocket = default(TcpClient);

            server.Start();
            Console.WriteLine("Listening on port 8080...");


            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)) // Esc key stops the server
            {
                clientSocket = server.AcceptTcpClient();
                byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                string dataFromClient = null;

                NetworkStream stream = clientSocket.GetStream();
                
                int dataLength = stream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                dataFromClient = Encoding.ASCII.GetString(bytesFrom, 0, dataLength);
                
                Request request = Deserializer.Reciever(dataFromClient);
                if (request.Verb != "CONNECT") // Asssures that the first step of a client is to Connect
                {
                    Delivery.SendResponse(new Response("200", request.ReqID), clientSocket);
                    continue;
                }
                else if (clientsList.ContainsKey(request.UserName)) // Assures that the username is not used
                {
                    Delivery.SendResponse(new Response("204", request.ReqID), clientSocket);
                    continue;
                }

                clientsList.Add(request.UserName, clientSocket);

                //broadcast(dataFromClient + " Joined ", dataFromClient, false);

                Console.WriteLine(request.UserName + " Joined chat room");
                Handler client = new Handler();
                client.startClient(clientSocket, request.UserName, clientsList, request.ReqID);
            }

            clientSocket.Close();
            server.Stop();
            Console.WriteLine("Connection ended!");
            Console.ReadLine();
        }
    }
}
