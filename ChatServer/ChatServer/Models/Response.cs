using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServer.Models
{

    //Hashtable responses = new Hashtable()
    //    {
    //        {"100", "OK" },
    //        {"101", "Delivered" },
    //        {"102", "Authenticated" },
    //        {"103", "Recieved" },
    //        {"200", "Bad Request" },
    //        {"201", "Not Delivered" },
    //        {"202", "Partial Delivery"},
    //        {"203", "Unavailable" },
    //        {"204", "Invalid User" }
    //    };
    class Response
    {
        public string Code;
        public string ReqID;
        public string UserName;
        public List<string> Users;
        public string Msg; // Deliver
        public string Verb;// Deliver

        public Response(string code, string reqID, string username) //Connect
        {
            Code = code;
            ReqID = reqID;
            UserName = username;

        }

        public Response(string code, string reqID) //Average Response
        {
            Code = code;
            ReqID = reqID;

        }

        public Response(string code, string reqID, List<string> users) //List and Partial Deliver
        {
            Code = code;
            ReqID = reqID;
            Users = users;

        }

        public Response(string verb, string reqID, string username, string msg) //Deliver
        {
            Verb = verb;
            ReqID = reqID;
            UserName = username;
            Msg = msg;
        }

    }
}
