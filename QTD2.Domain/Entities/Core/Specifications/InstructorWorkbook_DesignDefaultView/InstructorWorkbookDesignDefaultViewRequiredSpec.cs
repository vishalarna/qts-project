
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook
{
    public class InstructorWorkbookDesignDefaultViewRequiredSpec : ISpecification<InstructorWorkbook_DesignDefaultView>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_DesignDefaultView entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.DesignDefaultView);
        }
    }

}