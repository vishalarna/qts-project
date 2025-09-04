using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_SafetyHazard_Link
{
    public class ILA_SafetyHazard_LinkOptions
    {
        public int ILAId { get; set; }

        public int[] SafetyHazardIds { get; set; }
    }
}
