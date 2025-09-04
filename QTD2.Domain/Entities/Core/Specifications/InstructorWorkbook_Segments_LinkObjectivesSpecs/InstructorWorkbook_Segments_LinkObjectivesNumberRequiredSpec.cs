
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_Segments_LinkObjectivesSpecs
{
    public class InstructorWorkbook_Segments_LinkObjectivesNumberRequiredSpec : ISpecification<InstructorWorkbook_Segments_LinkObjectives>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_Segments_LinkObjectives entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Number);
        }
   }
}
