using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITrainingProgramReview_TrainingIssue_LinkService : Common.IService<TrainingProgramReview_TrainingIssue_Link>
    {
        Task<List<TrainingProgramReview_TrainingIssue_Link>> GetTrainingProgramTrainingIssueLinkByTrainingProgramId(int id);
    }
}
