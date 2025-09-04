using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Instructor_CategoryService : Common.Service<Entities.Core.Instructor_Category>, Interfaces.Service.Core.IInstructor_CategoryService
    {
        public Instructor_CategoryService(IInstructor_CategoryRepository instructor_CategoryRepository, IInstructor_CategoryValidation instructor_CategoryValidation)
            : base(instructor_CategoryRepository, instructor_CategoryValidation)
        {
        }
    }
}
