using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_Step : Entity
    {
        public int EOId { get; set; }

        public string Description { get; set; }

        public int? Number { get; set; }

        public int? ParentStepId { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public virtual ICollection<Version_EnablingObjective_Step> Version_EnablingObjective_Steps { get; set; } = new List<Version_EnablingObjective_Step>();
        public virtual ICollection<SkillReQualificationEmp_Step> SkillReQualificationEmp_Steps { get; set; } = new List<SkillReQualificationEmp_Step>();


        public EnablingObjective_Step()
        {
        }

        public EnablingObjective_Step(int eOId, string description, int? number, int? parentStepId, bool active)
        {
            EOId = eOId;
            Description = description;
            Number = number;
            ParentStepId = parentStepId;
            Active = active;
        }

        //public Version_EnablingObjective_Step CreateSnapshot()
        //{
        //    return new Version_EnablingObjective_Step(this);
        //}
    }
}
