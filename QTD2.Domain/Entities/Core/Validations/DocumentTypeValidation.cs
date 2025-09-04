using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.DocumentTypeSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class DocumentTypeValidation : Validation<DocumentType>, IDocumentTypeValidation
    {
        public DocumentTypeValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<DocumentType>(new DocumentTypeLinkedDataTypeRequiredSpec(), _validationStringLocalizer["DocumentTypeLinkedDataTypeRequiredSpec"]));
            AddRule(new ValidationRule<DocumentType>(new DocumentTypeNameRequiredSpec(), _validationStringLocalizer["DocumentTypeNameRequiredSpec"]));
        }
    }
}
