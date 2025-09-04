using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnSkillQualificationCompleted : Common.IDomainEvent, INotification
    {
        public SkillQualification SkillQualification { get; }

        public OnSkillQualificationCompleted(SkillQualification skillQualification)
        {
            SkillQualification = skillQualification;
        }
    }
}
