using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Instructor_CategoryHistory
{
    public class Instructor_CategoryHistoryUpdateOptions
    {
        public int ICategoryId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string CategoryNotes { get; set; }
    }
}
