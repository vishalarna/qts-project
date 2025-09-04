using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
    public class Instructor_Service : Common.Service<Entities.Core.Instructor>, Interfaces.Service.Core.IInstructor_Service
    {
        public Instructor_Service(IInstructor_Repository instructor_Repository, IInstructor_Validation instructor_Validation)
            : base(instructor_Repository, instructor_Validation)
        {
        }
        public async System.Threading.Tasks.Task<Entities.Core.Instructor> GetInstructorByIdAsync(int? instructorId)
        {
            var instructor = await FindAsync(x => x.Id == instructorId);
            return instructor.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<Entities.Core.Instructor>> GetAllInstructorsAsync()
        {
            var instructors = (await FindAsync(x => x.Active)).ToList(); ;
            return instructors;
        }
    }
}
