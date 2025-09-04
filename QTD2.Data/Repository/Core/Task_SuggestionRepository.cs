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
    public class Task_SuggestionRepository : Common.Repository<Task_Suggestion>, ITask_SuggestionRepository
    {
        public Task_SuggestionRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
