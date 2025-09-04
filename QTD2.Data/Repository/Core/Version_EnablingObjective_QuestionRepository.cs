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
    public class Version_EnablingObjective_QuestionRepository : Common.Repository<Version_EnablingObjective_Question>, IVersion_EnablingObjective_QuestionRepository
    {
        public Version_EnablingObjective_QuestionRepository(QTDContext context) : base(context)
        {
        }
    }
}
