using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class StudentEvaluationWithoutEmpRepository : Common.Repository<StudentEvaluationWithoutEmp>, IStudentEvaluationWithoutEmpRepository
    {
        public StudentEvaluationWithoutEmpRepository(QTDContext context)
            : base(context)
        {
        }
    }
}