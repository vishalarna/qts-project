using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class TrainingIssue_ActionItemPriorityRepository : Common.Repository<TrainingIssue_ActionItemPriority>, ITrainingIssue_ActionItemPriorityRepository
    {
        public TrainingIssue_ActionItemPriorityRepository(QTDContext context)
           : base(context)
        {
        }
    }
}
