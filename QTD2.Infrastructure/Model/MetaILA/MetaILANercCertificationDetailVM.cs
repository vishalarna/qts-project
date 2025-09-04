using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.MetaILA
{
    public class MetaILANercCertificationDetailVM
    {
        public string ILAName { get; set; }
        public double? TotalCEHHours { get; set; }
        public double? TotalStandardReqHour { get; set; }
        public double? TotalSimulationReqHour { get; set; }
        public bool IsEmergencyOpHours { get; set; }
        public double? TotalTrainingHours { get; set; }

        public MetaILANercCertificationDetailVM(){}

        public MetaILANercCertificationDetailVM(string ilaName,double? totalCEHHours,double? totalStandardReqHour,double? totalSimulationReqHour,bool isEmergencyOpHours, double? totalTrainingHours)
        {
            ILAName = ilaName;
            TotalCEHHours = totalCEHHours;
            TotalStandardReqHour = totalStandardReqHour;
            TotalSimulationReqHour = totalSimulationReqHour;
            IsEmergencyOpHours = isEmergencyOpHours;
            TotalTrainingHours = totalTrainingHours;
        }
    }

}
