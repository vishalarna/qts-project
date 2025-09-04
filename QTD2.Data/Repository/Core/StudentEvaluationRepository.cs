using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class StudentEvaluationRepository : Common.Repository<StudentEvaluation>, IStudentEvaluationRepository
    {
        public StudentEvaluationRepository(QTDContext context)
            : base(context)
        {
        }
    }
}