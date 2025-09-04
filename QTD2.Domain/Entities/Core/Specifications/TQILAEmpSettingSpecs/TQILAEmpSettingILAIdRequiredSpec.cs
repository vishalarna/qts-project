using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TQILAEmpSettingSpecs
{
    public class TQILAEmpSettingILAIdRequiredSpec : ISpecification<TQILAEmpSetting>
    {
        public bool IsSatisfiedBy(TQILAEmpSetting entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
