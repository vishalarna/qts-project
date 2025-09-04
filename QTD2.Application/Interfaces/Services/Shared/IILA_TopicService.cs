using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.ILA_Topic;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IILA_TopicService
    {
        public Task<List<ILA_Topic>> GetAsync();
        public Task<List<ILA_TopicVM>> GetILA_TopicsWithCountAndFilterAsync(FilterByOptions filterOptions);

        public Task<ILA_Topic> GetAsync(int id);

        public Task<ILA_Topic> CreateAsync(ILA_TopicCreateOptions options);

        public Task<ILA_Topic> UpdateAsync(int id, ILA_TopicUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
