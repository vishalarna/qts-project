using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Instructor_Category
{
    public class InstructorCategoryOptions
    {
        public int ICategoryId { get; set; }

        public string ActionType { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
