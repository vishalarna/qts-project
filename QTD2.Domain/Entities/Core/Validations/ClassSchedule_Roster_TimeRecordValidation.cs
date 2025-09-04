using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_Roster_TimeRecordSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassSchedule_Roster_TimeRecordValidation : Validation<ClassSchedule_Roster_TimeRecord>, IClassSchedule_Roster_TimeRecordValidation
    {
        public ClassSchedule_Roster_TimeRecordValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule_Roster_TimeRecord>(new ClassSchedule_Roster_TimeRecordClassSchedule_RosterIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_Roster_TimeRecordClassSchedule_RosterIdRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_Roster_TimeRecord>(new ClassSchedule_Roster_TimeRecordStartDateTimeRequiredSpec(), _validationStringLocalizer["ClassSchedule_Roster_TimeRecordStartDateTimeRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_Roster_TimeRecord>(new ClassSchedule_Roster_TimeRecordEndDateTimeRequiredSpec(), _validationStringLocalizer["ClassSchedule_Roster_TimeRecordEndDateTimeRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_Roster_TimeRecord>(new ClassSchedule_Roster_TimeRecordSequenceRequiredSpec(), _validationStringLocalizer["ClassSchedule_Roster_TimeRecordSequenceRequiredSpec"]));
        }
    }
}