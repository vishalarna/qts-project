using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Test
{
    public class TestOptions
    {
        public string ActionType { get; set; }

        public int[] TestIds { get; set; }

        public string ChangeNotes { get; set; }

        public int? ILAId { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
