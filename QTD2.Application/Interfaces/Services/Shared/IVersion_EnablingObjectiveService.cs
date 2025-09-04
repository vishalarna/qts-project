using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Version_EnablingObjective;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IVersion_EnablingObjectiveService
    {
        public Task<Version_EnablingObjective> CreateAsync(EnablingObjective options, int state);

        public System.Threading.Tasks.Task CreateEOPositionLinkVersion(int eoId, int posId);

        public System.Threading.Tasks.Task CreateEMPEmployeeLinkVersion(int eoId, int empId);

        public System.Threading.Tasks.Task CreateLinkVersioning(EnablingObjective eo, int state);

        public Task<Version_EnablingObjective> VersionAndCreateCopy(Domain.Entities.Core.EnablingObjective eo, int state = 2);

        public Task<List<Version_EnablingObjective>> GetAllVersionsForEOAsync(int eoId);

        //public Task<Version_EnablingObjective> UpdateAsync(int id, Version_EnablingObjectiveUpdateOptions options);

        //public Task<List<Version_EnablingObjective>> GetAsync();

        //public Task<Version_EnablingObjective> GetAsync(int id);

    }
}
