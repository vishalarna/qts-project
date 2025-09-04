using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.CertifyingBody;

namespace QTD2.Infrastructure.Model.CertifyingBody
{
    public class CertifyingBodyWithSubRequirementsVM
    {
        public QTD2.Domain.Entities.Core.CertifyingBody CertifyingBody { get; set; }
        public virtual ICollection<SubRequirementVM> CertificationSubRequirements { get; set; }
        public bool IsIncludeSimulation { get; set; }
        public bool IsEmergencyOpHours { get; set; }
        public bool IsPartialCreditHours { get; set; }
        public double? CEHHours { get; set; }
        public virtual ICollection<CertifyingBodyConsistencyResult>  CertifyingBodyConsistencyResults { get; set; }
        public CertifyingBodyWithSubRequirementsVM(QTD2.Domain.Entities.Core.CertifyingBody certifyingBody, ICollection<SubRequirementVM> certificationSubRequirements,
                                                     bool isIncludeSimulation,bool isEmergencyOpHours,bool isPartialCreditHours, double? cehHours, ICollection<CertifyingBodyConsistencyResult> certifyingBodyConsistencyResults)
        {
            CertifyingBody = certifyingBody;
            CertificationSubRequirements = certificationSubRequirements;
            IsIncludeSimulation = isIncludeSimulation;
            IsEmergencyOpHours = isEmergencyOpHours;
            IsPartialCreditHours = isPartialCreditHours;
            CEHHours = cehHours;
            CertifyingBodyConsistencyResults = certifyingBodyConsistencyResults;
        }
        public CertifyingBodyWithSubRequirementsVM()
        {

        }
    }
}
