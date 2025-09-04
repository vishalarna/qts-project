using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Exceptions
{
    public class QTDServerException : Exception
    {
        public string DisplayMessage { get; set; }

        public bool UseGenericMessage { get { return String.IsNullOrEmpty(DisplayMessage); } }

        public bool SendEmail { get; set; } = true;

        public HttpStatusCode StatusCode { get; set; }

        public QTDServerException(string displayMessage, bool sendEmail = true, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(displayMessage)
        {
            DisplayMessage = displayMessage;
            SendEmail = sendEmail;
            StatusCode = statusCode;
        }
    }
}
