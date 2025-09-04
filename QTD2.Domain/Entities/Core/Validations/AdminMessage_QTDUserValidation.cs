using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.AdminMessage_QTDUserSpecs;
using QTD2.Domain.Entities.Core.Specifications.AdminMessage_Specs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class AdminMessage_QTDUserValidation : Validation<AdminMessage_QTDUser>, IAdminMessage_QTDUserValidation
    {
        public AdminMessage_QTDUserValidation(IStringLocalizerFactory stringLocalizerFactory) : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<AdminMessage_QTDUser>(new AdminMessage_QTDUser_MessageIdSpec(), _validationStringLocalizer["AdminMessage_QTDUser_MessageIdSpec"]));
            AddRule(new ValidationRule<AdminMessage_QTDUser>(new AdminMessage_QTDUser_QTDUserIdSpec(), _validationStringLocalizer["AdminMessage_QTDUser_QTDUserIdSpec"]));
        }
    }
}
