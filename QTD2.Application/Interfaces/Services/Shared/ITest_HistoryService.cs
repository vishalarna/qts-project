using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Test_History;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITest_HistoryService
    {
        public System.Threading.Tasks.Task CreateTestHistoryAsync(Test_HistoryCreateOptions options);
    }
}
