using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Instructor_CategoryHistoryRepository : Common.Repository<Instructor_CategoryHistory>, IInstructor_CategoryHistoryRepository
    {
        public Instructor_CategoryHistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
