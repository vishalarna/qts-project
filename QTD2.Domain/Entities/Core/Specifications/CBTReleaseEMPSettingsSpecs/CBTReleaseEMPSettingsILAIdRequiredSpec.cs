using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.CBTReleaseEMPSettingsSpecs
{
    public class CBTReleaseEMPSettingsILAIdRequiredSpec : ISpecification<CBT>
    {
        public bool IsSatisfiedBy(CBT entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
