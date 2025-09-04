using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Instructor;
using QTD2.Infrastructure.Model.Instructor_History;
using QTD2.Infrastructure.Model.Person;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IInstructorService
    {
        public Task<List<Instructor>> GetAsync();

        public Task<Instructor> GetAsync(int id);

        public Task<Instructor> CreateAsync(Instructor_CreateOptions options, bool isReturnConflictExp = false);

        public Task<Instructor> UpdateAsync(int id, Instructor_CreateOptions options, bool isReturnConflictExp = false);

        public Task<Instructor> UpdateByEmailAsync(Instructor_UpdateByEmailOptions options);

        public  System.Threading.Tasks.Task DeleteAsync(Instructor_HistoryCreateOptions options);

        public System.Threading.Tasks.Task ActiveAsync(Instructor_HistoryCreateOptions options);

        public System.Threading.Tasks.Task InActiveAsync(Instructor_HistoryCreateOptions options);

        public Task<int> getCount();

        public Task<InstructorStatsVM> GetStatsCount();

        public System.Threading.Tasks.Task InstructorDeactivateAsync(int id, InstructorOptions options);

        public System.Threading.Tasks.Task InstructorActivateAsync(int id, InstructorOptions options);

        public Task<List<Instructor>> GetInsActiveInactive(string option);

        public Task<List<Instructor_Category>> GetCatActiveInactive(string option);
        public  Task<Instructor> ActivateAsync(int id);
        public  Task<Instructor> DeactivateAsync(int id);


    }
}
