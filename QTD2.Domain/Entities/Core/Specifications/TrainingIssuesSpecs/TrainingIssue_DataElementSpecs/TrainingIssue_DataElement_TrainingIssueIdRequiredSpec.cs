using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_DataElementSpecs
{
    public class TrainingIssue_DataElement_TrainingIssueIdRequiredSpec : ISpecification<TrainingIssue_DataElement>
    {
        public bool IsSatisfiedBy(TrainingIssue_DataElement entity, params object[] args)
        {
            return entity.TrainingIssueId > 0;
        }
    }
}
