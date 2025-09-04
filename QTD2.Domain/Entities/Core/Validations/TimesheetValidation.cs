using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TimesheetSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TimesheetValidation : Validation<Timesheet>, ITimesheetValidation
    {
        public TimesheetValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Timesheet>(new Timesheet_EmployeeTaskIdRequiredSpec(), _validationStringLocalizer["Timesheet_EmployeeTaskIdRequired"]));
            AddRule(new ValidationRule<Timesheet>(new Timesheet_DateRequiredSpec(), _validationStringLocalizer["Timesheet_DateRequired"]));
            AddRule(new ValidationRule<Timesheet>(new Timesheet_MethodIdRequiredSpec(), _validationStringLocalizer["Timesheet_MethodIdRequired"]));
            AddRule(new ValidationRule<Timesheet>(new Timesheet_NoteRequiredSpec(), _validationStringLocalizer["Timesheet_NoteRequired"]));
        }
    }
}
