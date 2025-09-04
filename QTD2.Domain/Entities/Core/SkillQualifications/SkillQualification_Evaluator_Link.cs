using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class SkillQualification_Evaluator_Link : Entity
    {
        public int EvaluatorId { get; set; }

        public int SkillQualificationId { get; set; }

        public int Number { get; set; }

        public virtual Employee Evaluator { get; set; }

        public virtual SkillQualification SkillQualification { get; set; }
    }
}
