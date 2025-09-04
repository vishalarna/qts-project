
using QTD2.Domain.Interfaces.Specification;


namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_PhasesSpecs
{
    class InstructorWorkbook_PhasesCoursePhaseDescriptionRequiredSpec : ISpecification<InstructorWorkbook_Phases>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_Phases entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.CoursePhaseDescription);
        }
    }
}
