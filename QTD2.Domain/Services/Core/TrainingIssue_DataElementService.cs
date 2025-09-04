using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class TrainingIssue_DataElementService : Common.Service<TrainingIssue_DataElement>, ITrainingIssue_DataElementService
    {
        public TrainingIssue_DataElementService(ITrainingIssue_DataElementRepository repository, ITrainingIssue_DataElement_Validation validation)
          : base(repository, validation)
        {
        }

        public async Task<TrainingIssue_DataElement> GetDataElementWithAllIncludesAsync(int id)
        {
            var dataElement = await GetWithIncludeAsync(id, new string[]
            {
                "CBT_ScormUpload",
                "EnablingObjective.EnablingObjective_Category",
                "EnablingObjective.EnablingObjective_SubCategory",
                "EnablingObjective.EnablingObjective_Topic",
                "ILA",
                "MetaEnablingObjective.EnablingObjective_Category",
                "MetaEnablingObjective.EnablingObjective_SubCategory",
                "MetaEnablingObjective.EnablingObjective_Topic",
                "MetaILA",
                "MetaTask.SubdutyArea.DutyArea",
                "PreTest",
                "Procedure",
                "RegulatoryRequirement",
                "SafetyHazard",
                "Task.SubdutyArea.DutyArea",
                "Test",
                "TestItem",
                "Tool",
                "TrainingProgram"

            });
            return dataElement;
        }
    }
}
