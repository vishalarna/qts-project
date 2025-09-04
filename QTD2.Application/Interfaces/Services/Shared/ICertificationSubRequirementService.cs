using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.CertificationSubRequirement;


namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ICertificationSubRequirementService
    {

        public System.Threading.Tasks.Task CreateAsync(CertificationSubRequirementCreateOptions options);

        public Task<List<CertificationSubRequirement>> GetAsync(int id);

        public Task<List<CertificationSubRequirement>> UpdateAsync(CertificationSubRequirementCreateOptions options);

    }
}
