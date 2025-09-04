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
    public class TestItem_HistoryRepository : Common.Repository<TestItem_History>, ITestItem_HistoryRepository
    {
        public TestItem_HistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
