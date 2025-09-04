using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_Position_Link
{
    public class ILA_PositionChangeCount
    {
        public QTD2.Domain.Entities.Core.ILA ILA { get; set; }
        public int PositionsRemoved { get; set; }
        public int PositionsAdded { get; set; }

    }
}
