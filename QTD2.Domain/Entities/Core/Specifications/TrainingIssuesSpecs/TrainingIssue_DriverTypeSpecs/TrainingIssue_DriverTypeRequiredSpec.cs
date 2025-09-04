using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_DriverTypeSpecs
{
    public class TrainingIssue_DriverTypeRequiredSpec : ISpecification<TrainingIssue_DriverType>
    {
         public bool IsSatisfiedBy(TrainingIssue_DriverType entity, params object[] args)
           {
               return !string.IsNullOrEmpty(entity.Type);
           }
    }
}
