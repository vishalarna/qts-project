using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.DocumentSpecs
{
    public class DocumentFilePathRequiredSpec : ISpecification<Document>
    {
        public bool IsSatisfiedBy(Document entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.FilePath);
        }
    }
}
