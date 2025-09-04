using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClassScheduleEmployeeSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassScheduleEmployeeValidation : Validation<ClassSchedule_Employee>, IClassSchedule_EmployeeValidation
    {
        public ClassScheduleEmployeeValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule_Employee>(new ClassScheduleEmployeeClassIdRequiredSpec(), _validationStringLocalizer["ClassIdRequired"]));
            AddRule(new ValidationRule<ClassSchedule_Employee>(new ClassScheduleEmployeeEmployeeIdRequiredSpec(), _validationStringLocalizer["EmployeeIdRequired"]));
            AddRule(new ValidationRule<ClassSchedule_Employee>(new ClassSchedule_EmployeeCBTIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_EmployeeCBTIdRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_Employee>(new ClassSchedule_EmployeeTestIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_EmployeeTestIdRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_Employee>(new ClassSchedule_EmployeeRetakeIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_EmployeeRetakeIdRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_Employee>(new ClassSchedule_EmployeePreTestIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_EmployeePreTestIdRequiredSpec"]));
        }
    }
}
