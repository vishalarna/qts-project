using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Version_MetaILA;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IVersion_MetaILAService
    {
        public Task<List<Version_MetaILA>> GetAsync();

        public Task<Version_MetaILA> GetAsync(int id);

        public Task<Version_MetaILA> CreateAsync(Version_MetaILACreateOptions options);

        public Task<Version_MetaILA> UpdateAsync(int id, Version_MetaILAUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
