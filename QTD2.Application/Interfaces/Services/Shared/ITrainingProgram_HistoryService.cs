using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TrainingProgram_History;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITrainingProgram_HistoryService
    {
        public Task<TrainingProgram_History> CreateAsync(TrainingProgram_HistoryCreateOptions options);
    }
}
