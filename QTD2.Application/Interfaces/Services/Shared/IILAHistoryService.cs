using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILAHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IILAHistoryService
    {
        public Task<List<ILAHistory>> GetAllILAHistories();

        public Task<ILAHistory> GetILAHistory(int id);

        public Task<ILAHistory> CreateILAHistory(ILAHistoryCreateOptions options);

        public Task<ILAHistory> UpdateILAHistory(int id, ILAHistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteILAHistory(int id);

        public System.Threading.Tasks.Task ActiveILAHistory(int id);

        public System.Threading.Tasks.Task InActiveILAHistory(int id);
    }
}
