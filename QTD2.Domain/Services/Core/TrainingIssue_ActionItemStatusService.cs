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
    public class TrainingIssue_ActionItemStatusService : Common.Service<TrainingIssue_ActionItemStatus>, ITrainingIssue_ActionItemStatusService
    {
        public TrainingIssue_ActionItemStatusService(ITrainingIssue_ActionItemStatusRepository repository, ITrainingIssue_ActionItemStatus_Validation validation)
          : base(repository, validation)
        {
        }
    }
}
