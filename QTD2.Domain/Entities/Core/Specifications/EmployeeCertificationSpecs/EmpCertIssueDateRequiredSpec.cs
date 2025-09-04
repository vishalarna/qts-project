using System;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EmployeeCertificationSpecs
{
    public class EmpCertIssueDateRequiredSpec : ISpecification<EmployeeCertification>
    {
        public bool IsSatisfiedBy(EmployeeCertification entity, params object[] args)
        {
            return entity.IssueDate != DateOnly.MinValue;
        }
    }
}
