using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITrainingIssue_DataElementService : Common.IService<TrainingIssue_DataElement> 
    {
        public Task<TrainingIssue_DataElement> GetDataElementWithAllIncludesAsync(int id);
    }
}
