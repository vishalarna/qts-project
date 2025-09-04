
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_TrainingTopicsHeadingSpecs
{
 public class InstructorWorkbook_TrainingTopicsHeadingTrainingTopicHeadingRequiredSpec : ISpecification<InstructorWorkbook_TrainingTopicsHeading>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_TrainingTopicsHeading entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.TrainingTopicHeading);
        }
    }
}
