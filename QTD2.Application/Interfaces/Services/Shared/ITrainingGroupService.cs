using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITrainingGroupService
    {
        public Task<List<TrainingGroup>> GetAllTrainingGroupsAsync();

        public Task<List<TrainingGroup>> GetTrainingGroupsInCategoryAsync(int catId);

        public Task<TrainingGroup> GetTrainingGroupAsync(int trId);

        public Task<List<TrainingGroup_Category>> GetAllGroupsWithCategory();
    }
}
