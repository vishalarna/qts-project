using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Instructor_CategoryHistory : Entity
    {
        public Instructor_CategoryHistory(int icategoryId, string categorynotes, DateTime effectivedate)
        {
            ICategoryId = icategoryId;
            ICategoryNotes = categorynotes;
            EffectiveDate = effectivedate;
        }

        public Instructor_CategoryHistory()
        {
        }

        public int ICategoryId { get; set; }

        public string ICategoryNotes { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public virtual Instructor_Category Instructor_Category { get; set; }
    }
}
