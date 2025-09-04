using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_RosterSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassSchedule_RosterValidation : Validation<ClassSchedule_Roster>, IClassSchedule_RosterValidation
    {
        public ClassSchedule_RosterValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule_Roster>(new ClassSchedule_RosterEmpIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_RosterEmpIdRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_Roster>(new ClassSchedule_RosterTestIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_RosterTestIdRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_Roster>(new ClassSchedule_RosterTestTypeIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_RosterTestTypeIdRequiredSpec"]));
        }
    }
}
