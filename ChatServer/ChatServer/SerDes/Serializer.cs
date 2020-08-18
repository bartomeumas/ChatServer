using ChatServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ChatServer.SerDes
{
    class Serializer
    {
        // code\nreqID
        // code\nreqID\nListOfUsers
        // DELIVER\nreqID\nSource|msg


        public static string ResponseMaker(Response response) //Average Response
        {



            return "";

        }


        //public static string ResponseMaker(string code, string reqID) //Average Response
        //{


        //    return "";

        //}
        //public static string ResponseMaker(string code, string reqID, List<string>users) //Respose for list of users
        //{
        //    return "";
        //}

        //public static string ResponseDeliver(string reqID, string sourceUser, string msg)
        //{
        //    return "";
        //}


    }
}
