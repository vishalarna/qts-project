using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CSE_ILACertPartialCreditSpecs.CSE_ILACertLink_SubReq_PartialCreditSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditValidation : Validation<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit>, IClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditValidation
    {
        public ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCreditValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
           AddRule(new ValidationRule<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit>(new CSE_ILACertLink_SubReq_PartialCredit_CSE_ILACert_PCIdRequiredIdSpec(), _validationStringLocalizer["CSE_ILACertLink_SubReq_PartialCredit_CSE_ILACert_PCIdRequiredIdSpec"]));
           AddRule(new ValidationRule<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit>(new CSE_ILACertLink_SubReq_PartialCredit_ILASubReqLinkIdRequiredSpec(), _validationStringLocalizer["CSE_ILACertLink_SubReq_PartialCredit_ILASubReqLinkIdRequiredSpec"]));
        }
    }
}
