using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class SelfRegistrationOptionsService : Common.Service<ILA_SelfRegistrationOptions>, ISelfRegistrationOptionsService
    {
        public SelfRegistrationOptionsService(ISelfRegistrationOptionsRepository repository, ISelfRegistrationOptionsValidation validation)
            : base(repository, validation)
        {
        }
        public async System.Threading.Tasks.Task<ILA_SelfRegistrationOptions> GetSelfRegistrationAsync(int ilaId)
        {
            var selfRegOption = (await FindAsync(x => x.ILAId == ilaId)).FirstOrDefault();
            return selfRegOption;
        }

        public async System.Threading.Tasks.Task<ILA_SelfRegistrationOptions> GetSelfRegistrationWithILAByILAIdAsync(int ilaId)
        {
            return (await FindWithIncludeAsync(x => x.ILAId == ilaId,new[] { "ILA" })).FirstOrDefault();
        }
    }
}
