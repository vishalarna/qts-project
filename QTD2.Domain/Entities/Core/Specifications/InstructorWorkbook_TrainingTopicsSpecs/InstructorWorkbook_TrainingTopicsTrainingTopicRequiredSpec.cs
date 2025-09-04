
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_TrainingTopicsSpecs
{
    public class InstructorWorkbook_TrainingTopicsTrainingTopicRequiredSpec : ISpecification<InstructorWorkbook_TrainingTopics>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_TrainingTopics entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.TrainingTopic);
        }
    }
}
