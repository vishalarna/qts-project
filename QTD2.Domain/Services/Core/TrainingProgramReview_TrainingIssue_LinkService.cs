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
    public class TrainingProgramReview_TrainingIssue_LinkService : Common.Service<TrainingProgramReview_TrainingIssue_Link>, ITrainingProgramReview_TrainingIssue_LinkService
    {
        public TrainingProgramReview_TrainingIssue_LinkService(ITrainingProgramReview_TrainingIssue_LinkRepository repository, ITrainingProgramReview_TrainingIssue_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<TrainingProgramReview_TrainingIssue_Link>> GetTrainingProgramTrainingIssueLinkByTrainingProgramId(int id)
        {
            return (await FindWithIncludeAsync(x => x.TrainingProgramId == id, new[] { "TrainingIssue.Severity", "TrainingIssue.Status" })).ToList();
        }
    }
}
