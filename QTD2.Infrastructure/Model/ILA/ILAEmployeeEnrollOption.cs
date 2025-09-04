using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILAEmployeeEnrollOption
    {
        public int classScheduleId { get; set; }

        public int empId { get; set; }
        public int idpId { get; set; }
        public DateTime? plannedDate { get; set; }

    }
}
