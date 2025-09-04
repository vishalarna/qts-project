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
    public class TestItemShortAnswerRepository : Common.Repository<TestItemShortAnswer>, ITestItemShortAnswerRepository
    {
        public TestItemShortAnswerRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
