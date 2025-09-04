using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class AdminMessage_QTDUser : Entity
    {
        public int AdminMessageId {  get; set; }
        public int QTDUserId { get; set; }
        public bool Dismissed { get; set; }
        public virtual AdminMessage AdminMessage { get; set; }
        public virtual QTDUser QTDUser { get; set; }
        public AdminMessage_QTDUser() { }
        public AdminMessage_QTDUser(int adminMessageId, int qtdUserId, bool dismissed)
        {
            AdminMessageId = adminMessageId;
            QTDUserId = qtdUserId;
            Dismissed = dismissed;
        }

    }
}
