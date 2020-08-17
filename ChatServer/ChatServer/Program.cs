using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Collections;
using ChatServer.Controllers;

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


            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                //if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                //{
                //    break;
                //}
                clientSocket = server.AcceptTcpClient();
                byte[] bytesFrom = new byte[10025];
                string dataFromClient = null;

                NetworkStream stream = clientSocket.GetStream();
                
                stream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                clientsList.Add(dataFromClient, clientSocket);

                //broadcast(dataFromClient + " Joined ", dataFromClient, false);

                Console.WriteLine(dataFromClient + " Joined chat room");
                Handler client = new Handler();
                client.startClient(clientSocket, dataFromClient, clientsList);
            }

            clientSocket.Close();
            server.Stop();
            Console.WriteLine("Connection ended!");
            Console.ReadLine();
        }
    }
}
