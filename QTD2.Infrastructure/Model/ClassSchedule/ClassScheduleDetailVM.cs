using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ClassScheduleDetailVM
    {
        public int Id { get; set; }
        public int? ILAId { get; set; }
        public int? InstructorId { get; set; }
        public int? LocationId { get; set; }
        public int? ProviderId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string LocationName { get; set; }
        public string InstructorName { get; set; }
        public string ILANumber { get; set; }
        public string ILAName { get; set; }
        public bool? IsILASelfPaced { get; set; }
        public string LocationAddress { get; set; }
        public bool? UseForEmp { get; set; }
        public bool IsStartAndEndTimeEmpty { get; set; }
        public int? ClassSize { get; set; }
        public bool IsRecurring { get; set; }
        public string SpecialInstruction { get; set; }
        public string WebinarLink { get; set; }
        public string ProviderName { get; set; }
        public bool IsPubliclyAvailable { get; set; }

        public ClassScheduleDetailVM()
        {
        }

        public ClassScheduleDetailVM(int id,int? ilaId,int? instructorId,int? locationId,int? providerId,DateTime startDateTime,DateTime endDateTime,string locationName,string instructorName,string ilaNumber,string ilaName,bool? isILASelfPaced,string locationAddress,bool? useForEmp,bool isStartAndEndTimeEmpty,int? classSize,bool isRecurring,string specialInstruction,string webinarLink,string providerName, bool isPubliclyAvailable)
        {
            Id = id;
            ILAId = ilaId;
            InstructorId = instructorId;
            LocationId = locationId;
            ProviderId = providerId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            LocationName = locationName;
            InstructorName = instructorName;
            ILANumber = ilaNumber;
            ILAName = ilaName;
            IsILASelfPaced = isILASelfPaced;
            LocationAddress = locationAddress;
            UseForEmp = useForEmp;
            IsStartAndEndTimeEmpty = isStartAndEndTimeEmpty;
            ClassSize = classSize;
            IsRecurring = isRecurring;
            SpecialInstruction = specialInstruction;
            WebinarLink = webinarLink;
            ProviderName = providerName;
            IsPubliclyAvailable = isPubliclyAvailable;
        }
    }

}
