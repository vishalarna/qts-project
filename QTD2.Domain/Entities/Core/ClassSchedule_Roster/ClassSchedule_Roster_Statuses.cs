using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_Roster_Statuses : Entity
    {
        public string Name { get; set; }
        public ClassSchedule_Roster_Statuses()
        {
        }

        public ClassSchedule_Roster_Statuses(string name)
        {
            Name = name;
        }
    }
}
