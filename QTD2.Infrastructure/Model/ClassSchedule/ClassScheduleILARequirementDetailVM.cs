using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ClassScheduleILARequirementDetailVM
    {
        public string InstructorName { get; set; }
        public string LocationAddress { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public DateTime StartDateTime { get; set; }

        public ClassScheduleILARequirementDetailVM(){}

        public ClassScheduleILARequirementDetailVM(string instructorName,string locationAddress,string locationCity,string locationState,DateTime startDateTime)
        {
            InstructorName = instructorName;
            LocationAddress = locationAddress;
            LocationCity = locationCity;
            LocationState = locationState;
            StartDateTime = startDateTime;
        }
    }

}
