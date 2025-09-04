using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class MetaILA_EmployeeValidation : Validation<MetaILA_Employee>, IMetaILA_EmployeeValidation
    {
        public MetaILA_EmployeeValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<MetaILA_Employee>(new MetaILA_Employee_EmployeeIdRequiredSpec(), _validationStringLocalizer["MetaILA_Employee_EmployeeIdRequiredSpec"]));
            AddRule(new ValidationRule<MetaILA_Employee>(new MetaILA_Employee_MetaILAIdRequiredSpec(), _validationStringLocalizer["MetaILA_Employee_MetaILAIdRequiredSpec"]));
        }
    }
}
