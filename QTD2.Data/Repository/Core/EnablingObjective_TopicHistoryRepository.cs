using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EnablingObjective_TopicHistoryRepository : Common.Repository<EnablingObjective_TopicHistory>, IEnablingObjective_TopicHistoryRepository
    {
        public EnablingObjective_TopicHistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
