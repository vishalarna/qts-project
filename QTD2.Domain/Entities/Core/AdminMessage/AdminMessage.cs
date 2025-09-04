using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class AdminMessage :Entity
    {
        public int SourceAdminMessageId { get; set; }
        public string Message { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual ICollection<AdminMessage_QTDUser> AdminMessage_QTDUsers { get; set; } = new List<AdminMessage_QTDUser>();
        public bool IsExpired
        {
            get { return ExpirationDate > DateTime.UtcNow; }
        }
        public AdminMessage() { }
        public AdminMessage(int sourceAdminMessageId, string message, DateTime receivedDate, DateTime expirationDate)
        {
            SourceAdminMessageId = sourceAdminMessageId;
            Message = message;
            ReceivedDate = receivedDate;
            ExpirationDate = expirationDate;
        }
    }
}
