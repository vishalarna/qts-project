using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.EmployeeCertificationHistorySpecs
{
    public class EmployeeCertHistory_EmployeeCertificationIdRequiredSpec : ISpecification<EmployeeCertifictaionHistory>
    {
        public bool IsSatisfiedBy(EmployeeCertifictaionHistory entity, params object[] args)
        {
            return entity.EmployeeCertificationId > 0;
        }
    }
}
