
using QTD2.Infrastructure.Model.ClassSchedule;
using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILARequirementsDetailsVM
    {
        public string ProviderName { get; set; }
        public string ILANumber { get; set; }
        public string ILATitle { get; set; }
        public double? TotalCEHHours { get; set; }
        public List<ClassScheduleILARequirementDetailVM> ClassSchedules { get; set; }

        public ILARequirementsDetailsVM()
        {

        }

        public ILARequirementsDetailsVM(string providerName,string iLANumber,string iLATitle,List<ClassScheduleILARequirementDetailVM> classSchedules,double? totalCEHHours)
        {
            ProviderName = providerName;
            ILANumber = iLANumber;
            ILATitle = iLATitle;
            ClassSchedules = classSchedules;
            TotalCEHHours = totalCEHHours;
        }
    }
}
