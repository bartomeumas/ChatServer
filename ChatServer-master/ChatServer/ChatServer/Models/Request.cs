using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServer.Models
{
    class Request
    {
        public string Verb;
        public string ReqID;
        public string Type;
        public string UserName;
        public List<string> Users;
        public string Msg;


        public Request(string verb, string reqID, string username) // Connect
        {
            Verb = verb;
            ReqID = reqID;
            UserName = username;
        }

        public Request(string verb, string reqID) // Disconnect and List
        {
            Verb = verb;
            ReqID = reqID;
        }

        public Request(string verb, string reqID, string type, string users, string msg) //SEND Uni and Multi
        {
            Verb = verb;
            ReqID = reqID;
            Type = type;
            Users = new List<string>(users.Split(',')); // If their is no ',' in the string it means its just 1 user.
                                                        // We will be using the same one for unicast and multicast
                                                        // And just use Users[0] when its Unicast.
            Msg = msg;
        }

        public Request(string verb, string reqID, string type, string msg) //SEND Broad
        {
            Verb = verb;
            ReqID = reqID;
            Type = type;
            Msg = msg;
        }
    }
}
