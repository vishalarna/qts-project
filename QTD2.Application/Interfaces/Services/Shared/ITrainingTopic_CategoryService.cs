using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TrainingTopic_Category;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITrainingTopic_CategoryService
    {
        public Task<List<TrainingTopic_Category>> GetAsync();

        public Task<TrainingTopic_Category> GetAsync(int id);

        public Task<TrainingTopic_Category> CreateAsync(TrainingTopic_CategoryCreateOptions options);

        public Task<TrainingTopic_Category> UpdateAsync(int id, TrainingTopic_CategoryUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
