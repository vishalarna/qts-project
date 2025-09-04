using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Version_TrainingProgram_ILASpecs
{
    public class Version_TrainingProgram_ILA_Link_VTrainingProgramIdRequiredSpec : ISpecification<Version_TrainingProgram_ILA_Link>
    {
        public bool IsSatisfiedBy(Version_TrainingProgram_ILA_Link entity, params object[] args)
        {
            return entity.Version_TrainingProgramId > 0;
        }
    }
}
