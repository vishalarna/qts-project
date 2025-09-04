using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class ClassSchedule_SelfRegistrationOptionsService : Common.Service<ClassSchedule_SelfRegistrationOptions>, IClassSchedule_SelfRegistrationOptionsService
    {
        public ClassSchedule_SelfRegistrationOptionsService(IClassSchedule_SelfRegistrationRepository repository, IClassSchedule_SelfRegistrationValidation validation)
           : base(repository, validation)
        {
        }
    }
}
