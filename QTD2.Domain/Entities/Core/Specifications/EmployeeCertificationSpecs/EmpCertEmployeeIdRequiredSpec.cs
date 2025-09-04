using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EmployeeCertificationSpecs
{
    public class EmpCertEmployeeIdRequiredSpec : ISpecification<EmployeeCertification>
    {
        public bool IsSatisfiedBy(EmployeeCertification entity, params object[] args)
        {
            return entity.EmployeeId > 0;
        }
    }
}
