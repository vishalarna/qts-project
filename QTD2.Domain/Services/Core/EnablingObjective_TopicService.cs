using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class EnablingObjective_TopicService : Common.Service<EnablingObjective_Topic>, IEnablingObjective_TopicService
    {
        public EnablingObjective_TopicService(IEnablingObjective_TopicRepository enablingObjective_TopicRepository, IEnablingObjective_TopicValidation enablingObjective_TopicValidation)
        : base(enablingObjective_TopicRepository, enablingObjective_TopicValidation)
        {
        }

        public async Task<List<EnablingObjective_Topic>> GetMinimalEOTopicData()
        {
            var topics = await AllQuery().Select(s => new EnablingObjective_Topic
            {
                Active = s.Active,
                Id = s.Id,
                SubCategoryId = s.SubCategoryId,
                Description = s.Description,
                Deleted = s.Deleted,
                Number = s.Number,
                Title = s.Title,
                EffectiveDate = s.EffectiveDate
            }).ToListAsync();

            return topics;
        }
    }
}
