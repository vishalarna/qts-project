
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILA_DesignSpecs
{
    public class InstructorWorkbook_ILA_DesignPrerequistiesStatusRequiredSpec : ISpecification<InstructorWorkbook_ILA_Design>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILA_Design entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.PrerequisitesStatus);
        }
    }
}
