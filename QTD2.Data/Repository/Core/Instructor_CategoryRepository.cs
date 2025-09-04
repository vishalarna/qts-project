using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Instructor_CategoryRepository : Common.Repository<Instructor_Category>, IInstructor_CategoryRepository
    {
        public Instructor_CategoryRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
