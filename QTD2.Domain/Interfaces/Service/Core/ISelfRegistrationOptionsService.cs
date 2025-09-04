using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISelfRegistrationOptionsService : Common.IService<ILA_SelfRegistrationOptions>
    {
        public System.Threading.Tasks.Task<ILA_SelfRegistrationOptions> GetSelfRegistrationAsync(int ilaId);
        public System.Threading.Tasks.Task<ILA_SelfRegistrationOptions> GetSelfRegistrationWithILAByILAIdAsync(int ilaId);
    }
}
