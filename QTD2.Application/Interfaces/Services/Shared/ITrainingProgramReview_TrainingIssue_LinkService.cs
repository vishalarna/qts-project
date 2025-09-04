using QTD2.Infrastructure.Model.TrainingIssue;
using QTD2.Infrastructure.Model.TrainingProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITrainingProgramReview_TrainingIssue_LinkService
    {
        Task<List<TrainingIssue_VM>> GetTrainingProgramTrainingIssuesLinkById(int id);
        System.Threading.Tasks.Task CreateTrainingProgramTrainingIssueLinks(int id, TrainingProgramTrainingIssueLinkOption options);
        System.Threading.Tasks.Task RemoveTrainingProgramTrainingIssueLinks(int id, TrainingProgramTrainingIssueLinkOption options);
    }
}
