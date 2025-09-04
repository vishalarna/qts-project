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
    public class Proc_IssuingAuthority_HistoryService : Common.Service<Proc_IssuingAuthority_History>, IProc_IssuingAuthority_HistoryService
    {
        public Proc_IssuingAuthority_HistoryService(IProc_IssuingAuthority_HistoryRepository repository, IProc_IssuingAuthority_HistoryValidation validation)
            : base(repository, validation)
        {
        }
    }
}
