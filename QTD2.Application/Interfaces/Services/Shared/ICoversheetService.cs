using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Coversheet;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ICoversheetService
    {
        public Task<List<Coversheet>> GetAsync();

        public Task<Coversheet> GetAsync(int id);

        public Task<Coversheet> CreateAsync(CoversheetCreateOptions options);

        public Task<Coversheet> UpdateAsync(int id, CoversheetUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
