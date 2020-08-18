using ChatServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ChatServer.SerDes
{
    static class Serializer
    {
        // code\nreqID
        // code\nreqID\nListOfUsers
        // DELIVER\nreqID\nSource|msg
        // code/nreqId/nusername

        public static string ResponseMaker(Response response) //Average Response
        {
            if (response.Code != null && response.ReqID != null && response.Users != null)
            {
                return $"{response.Code}\n{response.ReqID}\n{response.Users}";
            }

            else if (response.Code != null && response.ReqID != null && response.UserName != null)
            {
                return $"{response.Code}\n{response.ReqID}\n{response.UserName}";
            }

            else if (response.Code != null && response.ReqID != null)
            {
                return $"{response.Code}\n{response.ReqID}";
            }

            else 
            {
                return $"{response.Verb}\n{response.ReqID}\n{response.UserName}|{response.Msg}";
            }

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
