using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TrainingTopic;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITrainingTopicService
    {
        public Task<List<TrainingTopic>> GetAsync();

        public Task<TrainingTopic> GetAsync(int id);

        public Task<TrainingTopic> CreateAsync(TrainingTopicCreateOptions options);

        public Task<TrainingTopic> UpdateAsync(int id, TrainingTopicUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
        public System.Threading.Tasks.Task<object> GetCategoriesAsync();
    }
}
