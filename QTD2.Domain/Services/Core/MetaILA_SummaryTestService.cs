using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class MetaILA_SummaryTestService : Common.Service<MetaILA_SummaryTest>, IMetaILA_SummaryTestService
    {
        public MetaILA_SummaryTestService(IMetaILA_SummaryTestRepository repository, IMetaILA_SummaryTestValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<MetaILA_SummaryTest> GetAsync(int id)
        {
            var metaILA_SummaryTest = await FindWithIncludeAsync(x => x.Id == id, new string[] { "Test", "TestType", "Position" });
            return metaILA_SummaryTest.FirstOrDefault();
        }
    }
}
