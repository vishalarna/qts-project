using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingGroupSpecs
{
    public class TrainingGroupNameRequiredSpec : ISpecification<TrainingGroup>
    {
        public bool IsSatisfiedBy(TrainingGroup entity, params object[] args)
        {
            return !String.IsNullOrEmpty(entity.GroupName);
        }
    }
}
