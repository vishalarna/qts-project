using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Instructor
{
    public class InstructorStatsVM
    {
        public int ICategoryActive { get; set; }

        public int ICategoryInactive { get; set; }

        public int InstructorActive { get; set; }

        public int InstructorInactive { get; set; }

        public int TotalWorkbookAdmins { get; set; }
    }
}
