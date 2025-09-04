using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Position_History : Common.Entity
    {
        public Position_History(int positionId , string changenotes, DateTime effectivedate)
        {
            PositionId = positionId;
            ChangeNotes = changenotes;
            ChangeEffectiveDate = effectivedate;
        }

        public Position_History()
        {
        }

        public int PositionId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime? ChangeEffectiveDate { get; set; }

        public virtual Position Position { get; set; }
    }
}
