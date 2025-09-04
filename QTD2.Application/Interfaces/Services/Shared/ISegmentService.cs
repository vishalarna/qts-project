using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Segment;
using QTD2.Infrastructure.Model.SegmentObjective_Link;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISegmentService
    {
        public Task<List<Segment>> GetAsync();

        public Task<Segment> GetAsync(int id);

        public Segment GetWithObjectives(int id);

        public Task<Segment> CreateAsync(SegmentCreateOptions options);

        public Task<Segment> UpdateAsync(int id, SegmentUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public System.Threading.Tasks.Task LinkObjective(int id, UpdateSegmentObjectiveOrderListVM options);

        public System.Threading.Tasks.Task UnlinkObjective(int id, SegmentObjective_LinkOptions options);

        public System.Threading.Tasks.Task<List<SegmentObjective_Link>> GetLinkedObjectives(int id);
    }
}
