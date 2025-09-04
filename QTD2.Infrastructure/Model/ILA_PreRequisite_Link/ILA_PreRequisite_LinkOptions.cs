using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_PreRequisite_Link
{
    public class ILA_PreRequisite_LinkOptions
    {
        public int ILAId { get; set; }

        public int[] PreRequisiteIds { get; set; }
    }
}
