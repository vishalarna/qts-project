using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;


namespace QTD2.Data.Repository.Core
{
    public class Certification_HistoryRepository : Common.Repository<Certification_History>, ICertification_HistoryRepository
    {
        public Certification_HistoryRepository(QTDContext context)
            : base(context)
        {

        }
    }
}
