using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EmployeeDocumentSpecs
{
    public class EmployeeDocumentSpecs_EmpIDRequiredSpecs : ISpecification<EmployeeDocument>
    {
        public bool IsSatisfiedBy(EmployeeDocument entity, params object[] args)
        {
            return entity.EmployeeID > 0;
        }
    }
}
