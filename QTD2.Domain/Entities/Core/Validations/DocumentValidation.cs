using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.DocumentSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class DocumentValidation : Validation<Document>, IDocumentValidation
    {
        public DocumentValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Document>(new DocumentDocumentTypeIdRequiredSpec(), _validationStringLocalizer["DocumentDocumentTypeIdRequiredSpec"]));
            AddRule(new ValidationRule<Document>(new DocumentFileNameRequiredSpec(), _validationStringLocalizer["DocumentFileNameRequiredSpec"]));
            AddRule(new ValidationRule<Document>(new DocumentFilePathRequiredSpec(), _validationStringLocalizer["DocumentFilePathRequiredSpec"]));
            AddRule(new ValidationRule<Document>(new DocumentDateAddedRequiredSpec(), _validationStringLocalizer["DocumentDateAddedRequiredSpec"]));
            AddRule(new ValidationRule<Document>(new DocumentLinkedDataIdRequiredSpec(), _validationStringLocalizer["DocumentLinkedDataIdRequiredSpec"]));
        }
    }
}
