using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_Suggestion : Entity
    {
        public int EOId { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public virtual ICollection<Version_EnablingObjective_Suggestions> Version_EnablingObjective_Suggestions { get; set; } = new List<Version_EnablingObjective_Suggestions>();
        public virtual ICollection<SkillReQualificationEmp_Suggestion> SkillReQualificationEmp_Suggestions { get; set; } = new List<SkillReQualificationEmp_Suggestion>();

        public EnablingObjective_Suggestion()
        {
        }

        public EnablingObjective_Suggestion(int eOId, string description, int number)
        {
            EOId = eOId;
            Description = description;
            Number = number;
        }

    }
}
