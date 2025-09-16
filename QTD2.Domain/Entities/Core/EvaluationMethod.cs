using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EvaluationMethod : Common.Entity
    {
        public string Description { get; set; }

        public virtual ICollection<TaskQualification> TaskQualifications { get; set; } = new List<TaskQualification>();
        public virtual ICollection<TaskReQualificationEmp_SignOff> TaskReQualificationEmp_SignOff { get; set; } = new List<TaskReQualificationEmp_SignOff>();
        public virtual ICollection<SkillQualificationEmp_SignOff> SkillQualificationEmp_SignOff { get; set; } = new List<SkillQualificationEmp_SignOff>();
        public EvaluationMethod()
        {
        }

        public EvaluationMethod(string description)
        {
            Description = description;
        }
    }
}
