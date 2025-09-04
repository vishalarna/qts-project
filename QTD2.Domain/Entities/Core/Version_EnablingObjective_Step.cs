using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_Step : Entity
    {
        public int EOStepId { get; set; }

        public string Description { get; set; }

        public int? Number { get; set; }

        public virtual EnablingObjective_Step EnablingObjective_Step { get; set; }

        public Version_EnablingObjective_Step()
        {
        }

        public Version_EnablingObjective_Step(EnablingObjective_Step step)
        {
            EOStepId = step.Id;
            Description = step.Description;
            Number = step.Number;
        }

        public Version_EnablingObjective_Step(Version_EnablingObjective eo, EnablingObjective_Step step)
        {
            EOStepId = step.Id;
            Description = step.Description;
            Number = step.Number;
        }
    }
}
