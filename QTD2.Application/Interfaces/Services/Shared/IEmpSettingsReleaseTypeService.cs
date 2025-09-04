using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.EmpSettingsReleaseType;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEmpSettingsReleaseTypeService
    {
        public Task<List<EmpSettingsReleaseTypeVM>> GetAllAsync();
    }
}
