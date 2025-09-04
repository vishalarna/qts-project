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
    public class TrainingIssue_ActionItemPriorityService : Common.Service<TrainingIssue_ActionItemPriority>, ITrainingIssue_ActionItemPriorityService
    {
        public TrainingIssue_ActionItemPriorityService(ITrainingIssue_ActionItemPriorityRepository repository, ITrainingIssue_ActionItemPriority_Validation validation)
          : base(repository, validation)
        {
        }
    }
}
