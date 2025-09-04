using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.DocumentTypeSpecs
{
    public class DocumentTypeLinkedDataTypeRequiredSpec : ISpecification<DocumentType>
    {
        public bool IsSatisfiedBy(DocumentType entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.LinkedDataType);
        }
    }
}
