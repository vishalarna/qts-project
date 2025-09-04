using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.MetaILA_SummaryTest;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IMetaILA_SummaryTestService
    {
        public Task<MetaILA_SummaryTest_ViewModel> CreateAsync(MetaILA_SummaryTest_ViewModel options);

        public Task<MetaILA_SummaryTest_ViewModel> GetAsync(int id);

        public Task<MetaILA_SummaryTest_ViewModel> UpdateAsync(int id, MetaILA_SummaryTest_ViewModel options);
        public Task<List<TestItem>> GetTestItemsFromILAsAsync(GetTestItemsByILAsOption option);
    }
}
