using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_TrainingTopic_LinkSpecs
{
    public class ILA_TrTopic_LinkTrTopicIdRequiredSpec : ISpecification<ILA_TrainingTopic_Link>
    {
        public bool IsSatisfiedBy(ILA_TrainingTopic_Link entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
