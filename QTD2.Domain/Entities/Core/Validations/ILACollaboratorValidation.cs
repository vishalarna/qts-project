using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILACollaboratorSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILACollaboratorValidation : Validation<ILACollaborator>, IILACollaboratorValidation
    {
        public ILACollaboratorValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILACollaborator>(new ILACollaboratorILAIdRequiredSpec(), _validationStringLocalizer["ILACollaboratorILAIdRequired"]));
            AddRule(new ValidationRule<ILACollaborator>(new ILACollaboratorCollaborateInviteIdRequiredSpec(), _validationStringLocalizer["ILACollaboratorEmployeeIdRequired"]));
        }
    }
}
