using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CollaboratorInvitationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class CollaboratorInvitationValidation : Validation<CollaboratorInvitation>, ICollaboratorInvitationValidation
    {
        public CollaboratorInvitationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<CollaboratorInvitation>(new CollaboratorInvitationInviteDateRequiredSpec(), _validationStringLocalizer["CollaboratorInvitationInviteDateRequiredSpec"]));
            AddRule(new ValidationRule<CollaboratorInvitation>(new CollaboratorInvitationInvitedByEIDRequiredSpec(), _validationStringLocalizer["CollaboratorInvitationInvitedByEIDRequiredSpec"]));
            AddRule(new ValidationRule<CollaboratorInvitation>(new CollaboratorInvitationInviteeEIDRequiredSpec(), _validationStringLocalizer["CollaboratorInvitationInviteeEIDRequiredSpec"]));
            AddRule(new ValidationRule<CollaboratorInvitation>(new CollaboratorInvitationInviteeEmailID(), _validationStringLocalizer["CollaboratorInvitationInviteeEmailID"]));
        }
    }
}
