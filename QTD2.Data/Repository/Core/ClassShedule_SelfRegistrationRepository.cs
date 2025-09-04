using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class ClassShedule_SelfRegistrationRepository : Common.Repository<ClassSchedule_SelfRegistrationOptions>, IClassSchedule_SelfRegistrationRepository
    {
        public ClassShedule_SelfRegistrationRepository(QTDContext context)
    : base(context)
        {
        }
    }
}
