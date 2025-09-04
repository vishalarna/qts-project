using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QTD2.Domain.Entities.Core
{
    public class Positions_SQ : Common.Entity
    {
        public Positions_SQ(Position position, EnablingObjective enablingObjective)
        {
            PositionId = position.Id;
            EOId = enablingObjective.Id;
            Position = position;
            EnablingObjective = enablingObjective;
        }
        public Positions_SQ()
        {
        }
        public int PositionId { get; set; }

        public int EOId { get; set; }
        public virtual Position Position { get; set; }
        public virtual EnablingObjective EnablingObjective { get; set; }
    }
}
