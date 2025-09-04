using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.DocumentSpecs
{
    public class DocumentLinkedDataIdRequiredSpec : ISpecification<Document>
    {
        public bool IsSatisfiedBy(Document entity, params object[] args)
        {
            return entity.LinkedDataId > 0;
        }
    }
}
