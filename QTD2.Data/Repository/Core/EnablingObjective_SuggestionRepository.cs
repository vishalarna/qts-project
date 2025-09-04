using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class EnablingObjective_SuggestionRepository : Common.Repository<EnablingObjective_Suggestion>, IEnablingObjective_SuggestionRepository
    {
        public EnablingObjective_SuggestionRepository(QTDContext context) : base(context)
        {
        }
    }
}
