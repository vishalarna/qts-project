using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_Evaluation_RosterSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassSchedule_Evaluation_RosterValidation : Validation<ClassSchedule_Evaluation_Roster>, IClassSchedule_Evaluation_RosterValidation
    {
        public ClassSchedule_Evaluation_RosterValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
       //     AddRule(new ValidationRule<ClassSchedule_Evaluation_Roster>(new ClassSchedule_Evaluation_RosterSpec(), _validationStringLocalizer["RS_DescriptionRequired"]));
       
        }
    }

}
