using QTD2.Infrastructure.Model.TrainingIssue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
  public  interface ITrainingIssueDriverTypeService
    {
        public Task<List<TrainingIssue_DriverType_VM>> GetAllWithSubTypesAsync();
    }
}
