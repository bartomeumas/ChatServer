using ChatServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ChatServer.SerDes
{
    static class Serializer
    {
        // code\nreqID\nListOfUsers
        // code\nreqId\nUsername
        // code\nreqID
        // DELIVER\nreqID\nSource|msg

        public static string ResponseMaker(Response response) //Average Response
        {
            if (response.Code != null && response.ReqID != null && response.Users != null)
            {
                return $"{response.Code}\n{response.ReqID}\n{string.Join(',',response.Users)}";
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
    }
}
