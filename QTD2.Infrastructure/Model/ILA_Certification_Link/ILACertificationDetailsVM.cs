using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_Certification_Link
{
    public class ILACertificationDetailsVM
    {
        public double? NercCEHHours { get; set; }
        public double? StandardReqHour { get; set; }
        public double? SimulationReqHour { get; set; }
        public bool? IsEmergencyOpHours { get; set; }
        public bool? IsPartialCreditHours { get; set; }

        public ILACertificationDetailsVM(){}

        public ILACertificationDetailsVM(double? nercCEHHours,double? standardReqHour,double? simulationReqHour,bool? isEmergencyOpHours,bool? isPartialCreditHours)
        {
            NercCEHHours = nercCEHHours;
            StandardReqHour = standardReqHour;
            SimulationReqHour = simulationReqHour;
            IsEmergencyOpHours = isEmergencyOpHours;
            IsPartialCreditHours = isPartialCreditHours;
        }
    }

}
