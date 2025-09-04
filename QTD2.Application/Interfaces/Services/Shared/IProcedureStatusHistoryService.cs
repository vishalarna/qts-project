using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Procedure_StatusHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IProcedureStatusHistoryService
    {
        public Task<Procedure_StatusHistory> CreateAsync(Procedure_StatusHistoryCreateOptions options);
    }
}
