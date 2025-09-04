using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Instructor
{
    public class InstructorLatestActivityVM
    {
        public int Id { get; set; }

        public string InstructorNum { get; set; }

        public string InstructorName { get; set; }

        public string ActivityDesc { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
