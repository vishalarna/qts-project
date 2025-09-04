using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.QuestionBankSpecs
{
    public class QuestionBankStemRequiredSpec : ISpecification<QuestionBank>
    {
        public bool IsSatisfiedBy(QuestionBank entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Stem);
        }
    }
}