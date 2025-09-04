using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_Collaborator_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_Collaborator_LinkValidation : Validation<Task_Collaborator_Link>, ITask_Collaborator_LinkValidation
    {
        public Task_Collaborator_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_Collaborator_Link>(new Task_Collaborator_LinkTaskIdRequiredSpec(), _validationStringLocalizer["Task_Collaborator_LinkTaskIdRequiredSpec"]));
            AddRule(new ValidationRule<Task_Collaborator_Link>(new Task_Collaborator_LinkTaskColabInviteIdRequiredSpec(), _validationStringLocalizer["Task_Collaborator_LinkTaskColabInviteIdRequiredSpec"]));
        }
    }
}
