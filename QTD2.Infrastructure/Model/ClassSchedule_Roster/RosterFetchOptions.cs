using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_Roster
{
    public class RosterFetchOptions
    {
        public int ClassId { get; set; }

        public int TestId { get; set; }

        public string TestType { get; set; }

        public int? RetakeOrder { get; set; }
    }
}
