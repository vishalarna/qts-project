using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class StudentEvaluationHistoryRepository : Common.Repository<StudentEvaluationHistory>, IStudentEvaluationHistoryRepository
    {
        public StudentEvaluationHistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}