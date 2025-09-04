using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_DriverSubTypeSpecs
{
    public class TrainingIssue_DriverSubType_SubTypeRequiredSpec : ISpecification<TrainingIssue_DriverSubType>
    {
        public bool IsSatisfiedBy(TrainingIssue_DriverSubType entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.SubType);
        }
    }
}
