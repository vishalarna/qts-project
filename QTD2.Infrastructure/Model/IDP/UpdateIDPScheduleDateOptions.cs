using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.IDP
{
    public class UpdateIDPScheduleDateOptions
    {

        public int classScheduleId { get; set; }
        public int idpId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
