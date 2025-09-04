using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Version_Task_EnablingObjective_Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IVersion_Task_EnablingObjective_LinkService
    {
        public Task<List<Version_Task_EnablingObjective_Link>> GetAsync();

        public Task<Version_Task_EnablingObjective_Link> GetAsync(int id);

        public Task<Version_Task_EnablingObjective_Link> CreateAsync(Version_Task_EnablingObjective_LinkCreateOptions options);

        public Task<Version_Task_EnablingObjective_Link> UpdateAsync(int id, Version_Task_EnablingObjective_LinkUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
