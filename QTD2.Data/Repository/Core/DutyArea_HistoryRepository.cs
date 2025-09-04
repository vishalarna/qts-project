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
    public class DutyArea_HistoryRepository : Common.Repository<DutyArea_History>, IDutyArea_HistoryRepository
    {
        public DutyArea_HistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
