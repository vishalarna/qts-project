using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnSkillQualification_Evalutor_LinkCreated : Common.IDomainEvent, INotification
    {
        public SkillQualification_Evaluator_Link SkillQualification_Evaluator_Link { get; }

        public OnSkillQualification_Evalutor_LinkCreated(SkillQualification_Evaluator_Link skillQualificationEvaluatorLink)
        {
            SkillQualification_Evaluator_Link = skillQualificationEvaluatorLink;
        }
    }
}