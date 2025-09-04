using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Meta_ILAMembers_LinkSpecs
{
    public class Meta_ILA_Memebers_LinkConfigPublishOptionIDRequiredSpec : ISpecification<Meta_ILAMembers_Link>
    {
        public bool IsSatisfiedBy(Meta_ILAMembers_Link entity, params object[] args)
        {
            return entity.MetaILAConfigPublishOptionID > 0;
        }
    }
}
