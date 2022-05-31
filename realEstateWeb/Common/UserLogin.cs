using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace realEstateWeb
{
    [Serializable]
    public class UserLogin
    {
        public int UserID { get; set; }
        public string Username { get; set; }
    }
}