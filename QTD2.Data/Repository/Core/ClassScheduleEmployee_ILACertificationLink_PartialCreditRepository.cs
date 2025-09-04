using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class ClassScheduleEmployee_ILACertificationLink_PartialCreditRepository : Common.Repository<ClassScheduleEmployee_ILACertificationLink_PartialCredit>, IClassScheduleEmployee_ILACertificationLink_PartialCreditRepository
    {
        public ClassScheduleEmployee_ILACertificationLink_PartialCreditRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
