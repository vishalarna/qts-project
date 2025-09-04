using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ToDoILAsVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public int SeatsAvailable { get; set; }

        [JsonIgnore]
        public bool SelfRegistration { get; set; }

        public bool WaitListEnabled { get; set; }

        public List<ToDoClassSchedulesVM> ClassSchedules { get; set; } = new List<ToDoClassSchedulesVM>();
    }

    public class ToDoClassSchedulesVM
    {
        public int Id { get; set; }

        public int ILAId { get; set; }

 
        
        [JsonIgnore]
        public int ClassSize { get; set; }
        [JsonIgnore]
        public int SeatsTaken { get; set; }

        public string Instructor { get; set; }

        public string Location { get; set; }

        public DateTime? StartDateTime { get; set; }
        
        public DateTime? EndDateTime { get; set; }

        public int SeatsAvailable { get
            {
                return ClassSize - SeatsTaken;
            }
        }
        public bool WaitListEnabled { get; set; }

        public List<ToDoEmployeeVM> ClassSchedule_Employee = new List<ToDoEmployeeVM>();
    }

    public class ToDoEmployeeVM
    {
        public int EmployeeId { get; set; }

        [JsonIgnore]
        public int ClassScheduleId { get; set; }
        [JsonIgnore]
        public int ILAId { get; set; }

        public bool IsWaitListed { get; set; }

        public bool IsEnrolled { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; } = "";

        public int TestId { get; set; }

        public string TestTitle { get; set; }

    }
}
