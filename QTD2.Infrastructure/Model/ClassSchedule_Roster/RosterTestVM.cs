using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_Roster
{
    public class RosterTestVM
    {
        public int Id { get; set; }

        public int TestStatusId { get; set; }

        public string TestTitle { get; set; }

        public int? RetakeOrder { get; set; }
    }
}
