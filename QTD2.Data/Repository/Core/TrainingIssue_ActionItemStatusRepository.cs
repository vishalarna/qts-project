using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class TrainingIssue_ActionItemStatusRepository : Common.Repository<TrainingIssue_ActionItemStatus>, ITrainingIssue_ActionItemStatusRepository
    {
        public TrainingIssue_ActionItemStatusRepository(QTDContext context)
           : base(context)
        {
        }
    }
}
