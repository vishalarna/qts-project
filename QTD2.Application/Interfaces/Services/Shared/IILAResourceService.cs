using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA_Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IILAResourceService
    {
        public Task<List<ILA_Resource>> GetAsync(int ilaId);
        public Task<ILA_Resource> CreateAsync(int ilaId, ILAResourceCreateOptions options);
        public Task<ILA_Resource> UpdateAsync(int ilaId, int editILAResourceId, ILAResourceCreateOptions options);
        public Task<ILA_Resource> RemoveResourceILA(int ilaResourceId);
    }
}
