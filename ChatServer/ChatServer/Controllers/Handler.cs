using ChatServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ChatServer.SerDes;

namespace ChatServer.Controllers
{
    public class Handler // This class is in charge of creating a thread for each client
    {
        TcpClient ClientSocket;
        string Username;
        Hashtable ClientsList;

        public void startClient(TcpClient clientSocket, string username, Hashtable cList, string reqID)
        {
            ClientSocket = clientSocket;
            Username = username;
            ClientsList = cList;

            Delivery.SendResponse(new Response("102", reqID, username), ClientSocket); // Confirm that it's logged in

            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }

        private void doChat()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[ClientSocket.ReceiveBufferSize];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while (true)
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = ClientSocket.GetStream();
                    int dataLength = networkStream.Read(bytesFrom, 0, (int)ClientSocket.ReceiveBufferSize);
                    dataFromClient = Encoding.ASCII.GetString(bytesFrom, 0, dataLength);
                    rCount = Convert.ToString(requestCount);
                    Request request = Deserializer.Reciever(dataFromClient);


                    Console.WriteLine("From client - " + Username + " : " + dataFromClient);
                    

                    //Program.broadcast(dataFromClient, clNo, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }//end while
        }//end doChat

        
    } 



    //class Handler
    //{
    //    public void handleRequest(string req)
    //    {
    //        //Assures that the user is CONNECTED

    //    }
    //}
}
