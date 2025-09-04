using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Authentication
{
    public class AdminMessageAuth : Entity
    {
        public string Message { get; set; }
        public string Instance { get; set; }
        public bool Received { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExpired
        {
            get { return ExpirationDate > DateTime.UtcNow; }
        }
        public AdminMessageAuth() { }

        public AdminMessageAuth(string message, string instance, DateTime expirationDate)
        {
            Message = message;
            Instance = instance;
            ExpirationDate = expirationDate;
        }

        
    }
}
