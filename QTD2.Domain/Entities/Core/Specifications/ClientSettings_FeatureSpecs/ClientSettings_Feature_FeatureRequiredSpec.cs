using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSettings_FeatureSpecs
{
    public class ClientSettings_Feature_FeatureRequiredSpec : ISpecification<ClientSettings_Feature>
    {
        public bool IsSatisfiedBy(ClientSettings_Feature entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Feature);
        }
    }
}
