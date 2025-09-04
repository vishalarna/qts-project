using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_MetaTask_Link
{
    public class Task_MetaTask_LinkOptions
    {
        public int TaskId { get; set; }

        public int Meta_TaskId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
