using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Web.Authentication.Models
{
    public class PostResult
    {
        public string Message { get; set; }
        public bool Error { get; set; }

        public PostResult()
        {
            Error = false;
        }

        public PostResult(string message)
        {
            Message = message;
            Error = true;
        }
    }
}
