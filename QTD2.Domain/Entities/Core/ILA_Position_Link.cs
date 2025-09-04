using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_Position_Link : Entity
    {
        public int ILAId { get; set; }

        public int PositionId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual Position Position { get; set; }

        public ILA_Position_Link(ILA ila, Position position)
        {
            ILAId = ila.Id;
            PositionId = position.Id;
            ILA = ila;
            Position = position;
        }

        public ILA_Position_Link()
        {
        }
    }
}
