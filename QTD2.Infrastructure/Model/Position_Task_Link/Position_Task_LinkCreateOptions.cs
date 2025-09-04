using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Position_Task_Link
{
    public class Position_Task_LinkCreateOptions
    {
        public int PositionId { get; set; }

        public int[] TaskIds { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
