using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Version_Task_SaftyHazard_Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IVersion_Task_SaftyHazard_LinkService
    {
        public Task<List<Version_Task_SaftyHazard_Link>> GetAsync();

        public Task<Version_Task_SaftyHazard_Link> GetAsync(int id);

        public Task<Version_Task_SaftyHazard_Link> CreateAsync(Version_Task_SaftyHazard_LinkCreateOptions options);

        public Task<Version_Task_SaftyHazard_Link> UpdateAsync(int id, Version_Task_SaftyHazard_LinkUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
