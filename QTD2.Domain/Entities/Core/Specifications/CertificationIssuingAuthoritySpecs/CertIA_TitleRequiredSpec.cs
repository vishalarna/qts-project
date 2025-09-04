using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.CertificationIssuingAuthoritySpecs
{
    public class CertIA_TitleRequiredSpec : ISpecification<CertificationIssuingAuthority>
    {
        public bool IsSatisfiedBy(CertificationIssuingAuthority entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
