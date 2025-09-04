using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingProgram_HistorySpecs
{
    public class TrainingProgramHistory_TrainingProgramVersionIdRequiredSpec : ISpecification<TrainingProgram_History>
    {
        public bool IsSatisfiedBy(TrainingProgram_History entity, params object[] args)
        {
            return (entity.TrainingProgramId > 0);
        }
    }
}

