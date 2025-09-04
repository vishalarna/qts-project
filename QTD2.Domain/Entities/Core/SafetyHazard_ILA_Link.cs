using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SafetyHazard_ILA_Link : Entity
    {
        public int ILAId { get; set; }

        public int SafetyHazardId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual SaftyHazard SaftyHazard { get; set; }

        public SafetyHazard_ILA_Link(ILA iLA, SaftyHazard saftyHazard)
        {
            ILAId = iLA.Id;
            SafetyHazardId = saftyHazard.Id;
            ILA = iLA;
            SaftyHazard = saftyHazard;
        }

        public SafetyHazard_ILA_Link()
        {
        }
    }
}
