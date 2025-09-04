using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_Certification_Link
{
    public class ILACertificationLinkVM
    {
        public string CertificationName { get; set; }
        public double? CEHHours { get; set; }
        public double? StandardReqHour { get; set; }
        public double? SimulationReqHour { get; set; }
        public bool IsEmergencyOpHours { get; set; }

        public ILACertificationLinkVM()
        {
        }

        public ILACertificationLinkVM(string certificationName, double? cehHours, double? standardReqHour,
                                   double? simulationReqHour, bool isEmergencyOpHours)
        {
            CertificationName = certificationName;
            CEHHours = cehHours;
            StandardReqHour = standardReqHour;
            SimulationReqHour = simulationReqHour;
            IsEmergencyOpHours = isEmergencyOpHours;
        }
    }

}
