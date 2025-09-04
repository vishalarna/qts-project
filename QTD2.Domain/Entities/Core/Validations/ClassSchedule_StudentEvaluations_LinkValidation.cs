using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_StudentEvaluations_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassSchedule_StudentEvaluations_LinkValidation : Validation<ClassSchedule_StudentEvaluations_Link>, IClassSchedule_StudentEvaluations_LinkValidation
    {
        public ClassSchedule_StudentEvaluations_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule_StudentEvaluations_Link>(new ClassSchedule_StudentEvaluationClassIdRequiredSpec(), _validationStringLocalizer["classScheduleClassIdRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_StudentEvaluations_Link>(new ClassSchedule_StudentEvaluationStuduentEvaluationIdRequiredSpec(), _validationStringLocalizer["classScheduleevaluationIdRequiredSpec"]));

        }
    }
}
