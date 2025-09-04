using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Position
{
    public class PositionOption
    {
        public int[] positionIds { get; set; }

        public string ActionType { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string ChangeNotes { get; set; }
    }
}
