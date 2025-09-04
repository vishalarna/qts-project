using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class ClassSchedule_Roster_ResponseRepository : Common.Repository<ClassSchedule_Roster_Response>, IClassSchedule_Roster_ResponseRepository
    {
        public ClassSchedule_Roster_ResponseRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
