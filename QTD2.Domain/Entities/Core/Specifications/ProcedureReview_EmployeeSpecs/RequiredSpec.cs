using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Authentication.Specifications.ProcedureReview_EmployeeSpecs
{
    public class RequiredSpec : ISpecification<ProcedureReview_Employee>
    {
        public bool IsSatisfiedBy(ProcedureReview_Employee entity, params object[] args)
        {
            return true;
        }
    }
}
