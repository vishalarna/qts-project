using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Task_QuestionSpecs
{
    public class Task_Question_QuestionRequriedSpec : ISpecification<Task_Question>
    {
        public bool IsSatisfiedBy(Task_Question entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Question);
        }
    }
}
