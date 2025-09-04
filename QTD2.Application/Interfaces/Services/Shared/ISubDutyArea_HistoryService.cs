using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.SubDutyArea_History;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISubDutyArea_HistoryService
    {
        public Task<List<SubDutyArea_History>> GetAsync();

        public Task<SubDutyArea_History> GetAsync(int id);

        public Task<SubDutyArea_History> CreateAsync(SubDutyArea_HistoryCreateOptions options);

    }
}
