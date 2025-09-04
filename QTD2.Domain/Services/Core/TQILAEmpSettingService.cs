using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TQILAEmpSettingService : Common.Service<TQILAEmpSetting>, ITQILAEmpSettingService
    {
        public TQILAEmpSettingService(ITQILAEmpSettingRepository repository, ITQILAEmpSettingValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<TQILAEmpSetting> GetByIlaIdAsync(int iLAID)
        {
           return (await FindAsync(r => r.ILAId == iLAID)).FirstOrDefault();
        }
    }
}
