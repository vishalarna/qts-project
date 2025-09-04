using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TrainingGroupService : Common.Service<TrainingGroup>, ITrainingGroupService
    {
        public TrainingGroupService(ITrainingGroupRepository repository, ITrainingGroupValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<TrainingGroup>> GetAllTrainingTaskGroupWithCategories()
        {
            return (await AllWithIncludeAsync(new string[] { "TrainingGroup_Category", "Task_TrainingGroups.Task.Position_Tasks.Position" })).ToList();
        }
    }
}
