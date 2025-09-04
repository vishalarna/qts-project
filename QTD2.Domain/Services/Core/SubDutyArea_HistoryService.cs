using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SubDutyArea_HistoryService : Common.Service<SubDutyArea_History>, ISubDutyArea_HistoryService
    {
        public SubDutyArea_HistoryService(ISubDutyArea_HistoryRepository repository, ISubDutyArea_HistoryValidation validation)
            : base(repository, validation)
        {
        }
    }
}
