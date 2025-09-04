using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.AdminMessage_Specs;
using QTD2.Domain.Entities.Core.Specifications.PublicClassScheduleSpecs;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class AdminMessageValidation : Validation<AdminMessage>, IAdminMessageValidation
    {
        public AdminMessageValidation(IStringLocalizerFactory stringLocalizerFactory) : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<AdminMessage>(new AdminMessage_MessageSpec(), _validationStringLocalizer["AdminMessage_MessageSpec"]));
        }
    }
}
