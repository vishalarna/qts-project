using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Instructor;
using QTD2.Infrastructure.Model.Instructor_History;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IInstructorHistoryService
    {
        public Task<Instructor_History> CreateAsync(Instructor_HistoryCreateOptions options);

        public Task<Instructor_History> UpdateAsync(int id, Instructor_HistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<List<Instructor_History>> GetAllInsCatHistories();

        public Task<Instructor_History> GetInsCatHistory(int id);

        public Task<List<InstructorLatestActivityVM>> GetHistoryAsync();
    }
}
