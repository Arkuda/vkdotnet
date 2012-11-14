using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCore
{
    public class SessionInfo
    {
        public int AppId;
        public int Permissions;
        public string AccessToken;
        public string UserId;
        //public string Secret;
        //public string Signature;
        public int ExpiresIn;
    }
}
