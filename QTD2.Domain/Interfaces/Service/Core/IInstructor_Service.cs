using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IInstructor_Service : Common.IService<Entities.Core.Instructor>
    {
        public System.Threading.Tasks.Task<Entities.Core.Instructor> GetInstructorByIdAsync(int? instructorId);
        public System.Threading.Tasks.Task<List<Entities.Core.Instructor>> GetAllInstructorsAsync();

    }
}
