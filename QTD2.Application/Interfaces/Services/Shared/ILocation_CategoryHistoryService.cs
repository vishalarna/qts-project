using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Location_CategoryHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ILocation_CategoryHistoryService
    {
        public Task<Location_CategoryHistory> CreateAsync(Location_CategoryHistoryCreateOptions options);

        public Task<Location_CategoryHistory> UpdateAsync(int id, Location_CategoryHistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<List<Location_CategoryHistory>> GetAllLocCatHistories();

        public Task<Location_CategoryHistory> GetLocCatHistory(int id);
    }
}
