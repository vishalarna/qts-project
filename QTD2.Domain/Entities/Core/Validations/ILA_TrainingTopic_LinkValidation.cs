using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_TrainingTopic_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_TrainingTopic_LinkValidation : Validation<ILA_TrainingTopic_Link>, IILA_TrainingTopic_LinkValidation
    {
        public ILA_TrainingTopic_LinkValidation(IStringLocalizerFactory localizer)
            : base(localizer)
        {
            AddRule(new ValidationRule<ILA_TrainingTopic_Link>(new ILA_TrTopic_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_TrainingTopic_LinkILAIdRequiredSpec"]));
            AddRule(new ValidationRule<ILA_TrainingTopic_Link>(new ILA_TrTopic_LinkTrTopicIdRequiredSpec(), _validationStringLocalizer["ILA_TrTopic_LinkTrTopicIdRequiredSpec"]));
        }
    }
}
