using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class CBT_ScormRegistrationRepository : Common.Repository<CBT_ScormRegistration>, ICBT_ScormRegistrationRepository
    {
        public CBT_ScormRegistrationRepository(QTDContext context)
          : base(context)
        {
        }
    }
}
