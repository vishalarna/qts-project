using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.EmployeeHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEmployeeHistoryService
    { 
        public Task<EmployeeHistory> CreateEmployeeHistory(EmployeeHistoryCreateOptions options);
    }
}
