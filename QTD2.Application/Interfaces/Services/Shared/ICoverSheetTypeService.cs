using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.CoverSheetType;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ICoverSheetTypeService
    {
        public Task<List<CoverSheetType>> GetAsync();

        public Task<CoverSheetType> GetAsync(int id);

        public Task<CoverSheetType> CreateAsync(CoverSheetTypeCreateOptions options);

        public Task<CoverSheetType> UpdateAsync(int id, CoverSheetTypeUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
