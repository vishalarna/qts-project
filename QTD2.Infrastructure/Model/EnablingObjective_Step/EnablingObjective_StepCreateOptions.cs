using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EnablingObjective_Step
{
    public class EnablingObjective_StepCreateOptions
    {
        public string Description { get; set; }

        public int Number { get; set; }

        public bool Active { get; set; }

        public int ParentStepId { get; set; }

        public bool isSignificant { get; set; }
    }
}
