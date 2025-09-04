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
    public class Location_CategoryHistoryService : Common.Service<Location_CategoryHistory>, ILocation_CategoryHistoryService
    {
        public Location_CategoryHistoryService (ILocation_CategoryHistoryRepository repository, ILocation_CategoryHistoryValidation validation)
            :base (repository, validation)
        {

        }
    }
}
