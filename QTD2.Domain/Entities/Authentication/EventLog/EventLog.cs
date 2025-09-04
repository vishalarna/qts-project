using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Authentication
{
    public enum EventLogTypes
    {
        Login,
        Logout,
        SelectInstance,
        LeaveInstance
    }

    public class EventLog : Common.Entity
    { 
        public string UserId { get; set; }
        public string EventType { get; set; }
        public DateTime DateTime { get; set; }
        public int SystemId { get; set; }

        public virtual AppUser User { get; set; }
        public string EventDataJSON { get; set; }

        public EventLog() { }

        public EventLog(int systemId, string userId, EventLogTypes eventType)
        {
            this.SystemId = systemId;
            this.UserId = userId;
            this.EventType = eventType.ToString();
            this.DateTime = DateTime.Now;
        }

        public EventLog(int systemId, string userId, EventLogTypes eventType, string eventDataJson)
        {
            this.SystemId = systemId;
            this.UserId = userId;
            this.EventType = eventType.ToString();
            this.DateTime = DateTime.Now;
            this.EventDataJSON = eventDataJson;
        }
    }
}
