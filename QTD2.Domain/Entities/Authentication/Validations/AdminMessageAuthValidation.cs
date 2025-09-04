using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication.Specifications.AdminMessageAuthSpecs;
using QTD2.Domain.Entities.Authentication.Specifications.InstanceSettingSpecs;
using QTD2.Domain.Interfaces.Validation.Authentication;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Authentication.Validations
{
    public class AdminMessageAuthValidation : Validation<AdminMessageAuth>, IAdminMessageAuthValidation
    {
        public AdminMessageAuthValidation(IStringLocalizerFactory stringLocalizerFactory) : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<AdminMessageAuth>(new AdminMessageAuth_MessageSpec(), _validationStringLocalizer["AdminMessageAuth_MessageSpec"]));
            AddRule(new ValidationRule<AdminMessageAuth>(new AdminMessageAuth_InstanceSpec(), _validationStringLocalizer["AdminMessageAuth_InstanceSpec"]));
        }
    }
}
