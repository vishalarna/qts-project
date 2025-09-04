using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.MetaILA_Status;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IMetaILA_StatusService
    {
        public Task<List<MetaILA_Status>> GetAsync();

        public Task<MetaILA_Status> GetAsync(int id);

        public Task<MetaILA_Status> CreateAsync(MetaILA_StatusCreateOptions options);

        public Task<MetaILA_Status> UpdateAsync(int id, MetaILA_StatusUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
