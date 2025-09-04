using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.RatingScale;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IRatingScaleService
    {
        public Task<List<RatingScale>> GetAsync();

        public Task<RatingScale> GetAsync(int id);

        public Task<RatingScale> CreateAsync(RatingScaleCreateOptions options);

        public Task<RatingScale> UpdateAsync(int id, RatingScaleUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
