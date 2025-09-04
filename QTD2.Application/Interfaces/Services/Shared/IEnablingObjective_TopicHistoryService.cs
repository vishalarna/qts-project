using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.EnablingObjective_TopicHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEnablingObjective_TopicHistoryService
    {
        public Task<List<EnablingObjective_TopicHistory>> GetAllEOTopicHistories();

        public Task<EnablingObjective_TopicHistory> GetEOTopicHistory(int id);

        public Task<EnablingObjective_TopicHistory> CreateEOTopicHistory(EnablingObjective_TopicHistoryCreateOptions options);

        public Task<EnablingObjective_TopicHistory> UpdateEOTopicHistory(int id, EnablingObjective_TopicHistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteEOTopicHistory(int id);

        public System.Threading.Tasks.Task ActiveEOTopicHistory(int id);

        public System.Threading.Tasks.Task InActiveEOTopicHistory(int id);
    }
}
