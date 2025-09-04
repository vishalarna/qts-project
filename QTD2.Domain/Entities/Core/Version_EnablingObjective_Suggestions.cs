using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_Suggestions : Entity
    {
        public int EnablingObjective_SuugestionId { get; set; }

        public int Version_EOId { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual EnablingObjective_Suggestion EnablingObjective_Suggestion { get; set; }

        public Version_EnablingObjective_Suggestions()
        {
        }

        public Version_EnablingObjective_Suggestions(Version_EnablingObjective eo, EnablingObjective_Suggestion sugg)
        {
            EnablingObjective_SuugestionId = sugg.Id;
            Version_EOId = eo.Id;
            Description = sugg.Description;
            Number = sugg.Number;
        }
    }
}
