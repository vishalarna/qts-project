
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILA_DevelopSpecs
{
    public class InstructorWorkbook_ILA_DevelopResultRequiredSpecs : ISpecification<InstructorWorkbook_ILA_Develop>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILA_Develop entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.DevelopResult);
        }
    }
}
