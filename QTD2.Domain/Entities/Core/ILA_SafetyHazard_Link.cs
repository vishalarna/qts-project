using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_SafetyHazard_Link : Entity
    {
        public int ILAId { get; set; }

        public int SafetyHazardId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual SaftyHazard SafetyHazard { get; set; }

        public ILA_SafetyHazard_Link(ILA ila, SaftyHazard safetyHazard)
        {
            ILAId = ila.Id;
            SafetyHazardId = safetyHazard.Id;
            ILA = ila;
            SafetyHazard = safetyHazard;
        }

        public ILA_SafetyHazard_Link()
        {
        }
    }
}
