using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.PublicClassScheduleRequest
{
    public class PublicClassScheduleVM
    {
        public int? IlaId { get; set; }
        public int? ClassId { get; set; }
        public string LocationName { get; set; }
        public string InstructorName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public PublicClassScheduleVM()
        {

        }
        public PublicClassScheduleVM(string locationName, string instructorName, DateTime startDateTime, DateTime endDateTime)
        {
            LocationName = locationName;
            InstructorName = instructorName;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }
    }
}
