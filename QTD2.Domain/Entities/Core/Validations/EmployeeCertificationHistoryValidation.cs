using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EmployeeCertificationHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EmployeeCertificationHistoryValidation : Validation<EmployeeCertifictaionHistory>, IEmployeeCertificationHistoryValidation
    {
        public EmployeeCertificationHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EmployeeCertifictaionHistory>(new EmployeeCertHistory_EmployeeCertificationIdRequiredSpec(), _validationStringLocalizer["EmployeeCertHistory_EmployeeCertificationIdRequiredSpec"]));
        }
    }
}
