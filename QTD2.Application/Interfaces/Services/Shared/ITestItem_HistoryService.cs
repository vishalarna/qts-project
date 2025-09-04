using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TestItem_History;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestItem_HistoryService
    {
        public Task<TestItem_History> CreateTestItemHistory(TestItem_HistoryCreateOptions options);
    }
}
