using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_UploadSpecs
{
    public class ILA_UploadIlaIdRequiredSpec : ISpecification<ILA_Upload>
    {
        public bool IsSatisfiedBy(ILA_Upload entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
