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
    public class EvaluationReleaseEMPSettingsService : Common.Service<EvaluationReleaseEMPSettings>, IEvaluationReleaseEMPSettingsService
    {
        public EvaluationReleaseEMPSettingsService(IEvaluationReleaseEMPSettingsRepository repository, IEvaluationReleaseEMPSettingsValidation validation)
            : base(repository, validation)
        {
        }
        public async System.Threading.Tasks.Task<EvaluationReleaseEMPSettings> GetEvaluationSettingsByIdAsync(int ilaId)
        {
            var evalSettings = await FindAsync(x => x.ILAId == ilaId);
            return evalSettings.FirstOrDefault();
        }
    }
}
