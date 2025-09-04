using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class ClassSchedule_Evaluation_RosterRepository : Common.Repository<ClassSchedule_Evaluation_Roster>, IClassSchedule_Evaluation_RosterRepository
    {

        public ClassSchedule_Evaluation_RosterRepository(QTDContext qtdContext)
            : base(qtdContext)
        {

        }
    }
}
