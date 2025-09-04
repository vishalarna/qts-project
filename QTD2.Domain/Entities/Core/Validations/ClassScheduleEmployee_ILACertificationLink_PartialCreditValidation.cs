using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CSE_ILACertPartialCreditSpecs.ClassScheduleEmployee_ILACertificationLink_PartialCreditSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassScheduleEmployee_ILACertificationLink_PartialCreditValidation : Validation<ClassScheduleEmployee_ILACertificationLink_PartialCredit>, IClassScheduleEmployee_ILACertificationLink_PartialCreditValidation
    {
        public ClassScheduleEmployee_ILACertificationLink_PartialCreditValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassScheduleEmployee_ILACertificationLink_PartialCredit>(new PartialCredit_ClassScheduleEmployeeIdRequiredSpec(), _validationStringLocalizer["PartialCredit_ClassScheduleEmployeeIdRequiredSpecs"]));
            AddRule(new ValidationRule<ClassScheduleEmployee_ILACertificationLink_PartialCredit>(new PartialCredit_ILACertificationLinkIdRequiredSpec(), _validationStringLocalizer["PartialCredit_ILACertificationLinkIdRequiredSpec"]));
        }
    }
}
