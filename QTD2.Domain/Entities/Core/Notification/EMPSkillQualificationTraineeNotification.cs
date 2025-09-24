using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPSkillQualificationTraineeNotification : Notification
    {
        public int SkillQualificationId { get; set; }

        public virtual SkillQualification SkillQualification { get; set; }

        public EMPSkillQualificationTraineeNotification() { }

        public EMPSkillQualificationTraineeNotification(DateTime dueDate, int skillQualificationId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            SkillQualificationId = skillQualificationId;
        }
    }
}
