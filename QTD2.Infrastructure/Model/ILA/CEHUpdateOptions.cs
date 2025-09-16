using QTD2.Infrastructure.Model.Certification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class CEHUpdateOptions
    {
        public int CertificationId { get; set; }
        public bool IsIncludeSimulation { get; set; }
        public bool IsEmergencyOpHours { get; set; }
        public bool IsPartialCreditHours { get; set; }
        public double CEHHours { get; set; }
        public List<SubRequirementVM> SubRequirements { get; set; } = new List<SubRequirementVM>();
    }
}
