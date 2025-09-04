using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvey_Task_TrainingFrequencySpecs
{
   public class DIFSurvey_Task_TrainingFrequencySpec : ISpecification<DIFSurvey_Task_TrainingFrequency>
    {
        public bool IsSatisfiedBy(DIFSurvey_Task_TrainingFrequency entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Status);
        }
    }
}