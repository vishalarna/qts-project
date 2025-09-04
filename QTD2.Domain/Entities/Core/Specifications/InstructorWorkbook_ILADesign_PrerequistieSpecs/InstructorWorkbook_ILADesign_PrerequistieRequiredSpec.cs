
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_PrerequistieSpecs
{
    public class InstructorWorkbook_ILADesign_PrerequistieRequiredSpec : ISpecification<InstructorWorkbook_ILADesign_Prerequistie>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILADesign_Prerequistie entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Prerequisite);
        }
    }
}
