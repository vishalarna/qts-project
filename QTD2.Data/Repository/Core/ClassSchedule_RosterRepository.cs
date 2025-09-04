using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClassSchedule_RosterRepository : Common.Repository<ClassSchedule_Roster>, IClassSchedule_RosterRepository
    {
        public ClassSchedule_RosterRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
