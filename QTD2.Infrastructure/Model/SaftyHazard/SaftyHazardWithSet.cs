using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SaftyHazard
{
    public class SaftyHazardWithSet
    {
        public virtual Domain.Entities.Core.SaftyHazard SaftyHazard { get; set; }

        public virtual ICollection<Domain.Entities.Core.SafetyHazard_Set> SafetyHazard_Sets { get; set; } = new List<Domain.Entities.Core.SafetyHazard_Set>();

        public virtual ICollection<Domain.Entities.Core.Tool> Tools { get; set; } = new List<Domain.Entities.Core.Tool>();
    }
}
