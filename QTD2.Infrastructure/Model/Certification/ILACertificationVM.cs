using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Certification
{
    public class ILACertificationVM
    {
        public string Name { get; set; }
        public int CertificationId { get; set; }
        public int IlaId { get; set; }
        public double TotalHours { get; set; }
        public bool IsIncludeSimulation { get; set; }
        public bool IsAlreadySaved { get; set; }
        public bool IsEmergencyOpHours { get; set; }
        public bool IsPartialCreditHours { get; set; }

        public bool IsNerc { get; set; }
        public double? CEHHours { get; set; }

        public virtual ICollection<SubRequirementVM> CertificationSubRequirements { get; set; }

    }

    public class SubRequirementVM {
        public int SubRequirementId { get; set; }
        public double ReqHour { get; set; }
        public string ReqName { get; set; }


    }

}
