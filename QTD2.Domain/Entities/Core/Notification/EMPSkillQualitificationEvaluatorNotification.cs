using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPSkillQualitificationEvaluatorNotification : Notification
    {
        public int SkillQualification_Evaluator_LinkId { get; set; }

        public virtual SkillQualification_Evaluator_Link SkillQualification_Evaluator_Link { get; set; }

        public EMPSkillQualitificationEvaluatorNotification() { }

        public EMPSkillQualitificationEvaluatorNotification(DateTime dueDate, int skillQualification_Evaluator_LinkId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            SkillQualification_Evaluator_LinkId = skillQualification_Evaluator_LinkId;
        }
    }
}
