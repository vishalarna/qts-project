using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_CollaboratorInvitationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_CollaboratorInvitationValidation : Validation<Task_CollaboratorInvitation>, ITask_CollaboratorInvitationValidation
    {
        public Task_CollaboratorInvitationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_CollaboratorInvitation>(new Task_CollaboratorInvitationInvitedByEIdRequiredSpec(), _validationStringLocalizer["Task_CollaboratorInvitationInvitedByEIdRequiredSpec"]));
            AddRule(new ValidationRule<Task_CollaboratorInvitation>(new Task_CollaboratorInvitationInvitedForTaskIdRequiredSpec(), _validationStringLocalizer["Task_CollaboratorInvitationInvitedForTaskIdRequiredSpec"]));
            AddRule(new ValidationRule<Task_CollaboratorInvitation>(new Task_CollaboratorInvitationInviteeEIdRequiredSpec(), _validationStringLocalizer["Task_CollaboratorInvitationInviteeEIdRequiredSpec"]));
            AddRule(new ValidationRule<Task_CollaboratorInvitation>(new Task_CollaboratorInvitationInviteeEmailIdRequiredSpec(), _validationStringLocalizer["Task_CollaboratorInvitationInviteeEmailIdRequiredSpec"]));
        }
    }
}
