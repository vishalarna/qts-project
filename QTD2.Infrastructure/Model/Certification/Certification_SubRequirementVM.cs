using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Certification
{
    public class Certification_SubRequirementVM
    {
        public string Name { get; set; }
        public double? CEHHours { get; set; }
        public virtual ICollection<CertificationSubRequirementVM> CertificationSubRequirements { get; set; } = new List<CertificationSubRequirementVM>();

        public Certification_SubRequirementVM(string name, double? cEHHours, ICollection<CertificationSubRequirementVM> certificationSubRequirements)
        {
            Name = name;
            CEHHours = cEHHours;
            CertificationSubRequirements = certificationSubRequirements;
        }

        public Certification_SubRequirementVM() { }
    }

    public class CertificationSubRequirementVM
    {
        public string ReqName { get; set; }
        public double ReqHour { get; set; }

        public CertificationSubRequirementVM(string reqName, double reqHour)
        {
            ReqName = reqName;
            ReqHour = reqHour;
        }

        public CertificationSubRequirementVM() { }
    }
}
