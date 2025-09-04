using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Instructor_Repository : Common.Repository<Instructor>, IInstructor_Repository
    {
        public Instructor_Repository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
