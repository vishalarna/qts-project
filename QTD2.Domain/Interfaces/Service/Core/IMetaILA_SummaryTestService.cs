using QTD2.Domain.Entities.Core;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IMetaILA_SummaryTestService : Common.IService<MetaILA_SummaryTest>
    {
        public Task<MetaILA_SummaryTest> GetAsync(int id);
    }
}
