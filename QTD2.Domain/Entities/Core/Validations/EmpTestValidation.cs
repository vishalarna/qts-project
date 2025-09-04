using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EmpTestSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EmpTestValidation : Validation<ClassSchedule_Roster_Response_Selection>, IEmpTestValidation
    {
        public EmpTestValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule_Roster_Response_Selection>(new EmpTes_TestIdRequiredSpec(), _validationStringLocalizer["EmpTes_TestIdRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_Roster_Response_Selection>(new EmpTest_TestTypeIdRequiredSpec(), _validationStringLocalizer["EmpTest_TestTypeIdRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_Roster_Response_Selection>(new EmpTestEmpIdRequiredSpec(), _validationStringLocalizer["EmpTestEmpIdRequiredSpec"]));
        }
    }
}