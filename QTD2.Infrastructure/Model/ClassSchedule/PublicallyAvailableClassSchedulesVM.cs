using QTD2.Infrastructure.Model.PublicILA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class PublicallyAvailableClassSchedulesVM
    {
        public int Id { get; set; }
        public int? ILAId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string LocationName { get; set; }
        public string InstructorName { get; set; }
        public Public_ILA_VM PublicILA { get; set; }
        public PublicallyAvailableClassSchedulesVM()
        {
            
        }
        public PublicallyAvailableClassSchedulesVM(int id, int? ilaId, DateTime startDateTime, DateTime endDateTime, string locationName, string instructorName, string ilaNumber, string ilaName, string ilaNickName, double totalTrainingHours, string description)
        {
            Id = id;
            ILAId = ilaId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            LocationName = locationName;
            InstructorName = instructorName;
        }
    }
}
