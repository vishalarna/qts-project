
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_TrainingTopicsSpecs
{
    class InstructorWorkbook_ILADesign_TrainingTopicsTrainingTopicRequiredSpec : ISpecification<InstructorWorkbook_ILADesign_TrainingTopics>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILADesign_TrainingTopics entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.TrainingTopic);
        }
    }
}
