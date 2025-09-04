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
    public class Location_HistoryService : Common.Service<Location_History>, ILocation_HistoryService
    {
        public Location_HistoryService(ILocation_HistoryRepository repository, ILocation_HistoryValidation validation)
            :base (repository, validation)
        {

        }
    }
}
