using QTD2.Infrastructure.Model.CertificationSubRequirement;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Model.CertifyingBody
{
    public class CertifyingBodyCEHUpdateOptions
    {
        public bool IsIncludeSimulation { get; set; }
        public bool IsEmergencyOpHours { get; set; }
        public bool IsPartialCreditHours { get; set; }
        public double CEHHours { get; set; }
        public List<SubRequirementUpdateOptions> SubRequirements { get; set; } = new List<SubRequirementUpdateOptions>();
    }

}
