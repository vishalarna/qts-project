using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ClassSchedule_Evaluator_LinksService : Common.Service<ClassSchedule_Evaluator_Link>, IClassSchedule_Evaluator_LinksService
    {
        public ClassSchedule_Evaluator_LinksService(IClassSchedule_Evaluator_LinksRepository repository, IClassSchedule_Evaluator_LinksValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ClassSchedule_Evaluator_Link>> GetClassScheduleTQEvaluators(int classScheduleId)
        {
            return ( await FindWithIncludeAsync(x => x.ClassScheduleId == classScheduleId,new string[] { "Evaluator.Person" })).ToList();
        }
    }
}