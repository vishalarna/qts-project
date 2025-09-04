using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Instructor_CategoryHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IInstructor_CategoryHistoryService
    {
        public Task<Instructor_CategoryHistory> CreateAsync(Instructor_CategoryHistoryCreateOptions options);

        public Task<Instructor_CategoryHistory> UpdateAsync(int id, Instructor_CategoryHistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<List<Instructor_CategoryHistory>> GetAllInsCatHistories();

        public Task<Instructor_CategoryHistory> GetInsCatHistory(int id);
    }
}
