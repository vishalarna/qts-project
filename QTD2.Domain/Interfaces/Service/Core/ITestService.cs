using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITestService : Common.IService<Test>
    {
        System.Threading.Tasks.Task<List<Test>> GetTestSpecificationsAsync(IEnumerable<int> testIds, bool includeTestStatistics);

        System.Threading.Tasks.Task<List<Test>> GetMinimalTests();
        System.Threading.Tasks.Task<List<Test>> GetMinimalTestsByTestIds(List<int> testIds);
        System.Threading.Tasks.Task<List<Test>> GetTestsForEMPTestStatistics(List<int> testIds);
        Task<Test> GetFullTestAsync(int testId);
        System.Threading.Tasks.Task<List<Test>> GetForPretestAndFinalTestComparison(List<int> testIds);
        System.Threading.Tasks.Task<List<Test>> GetTestSpecificationsByTestIdsAsync(IEnumerable<int> testIds, bool includeTestStatistics);
        System.Threading.Tasks.Task<List<Test>> GetTestsByTestTypeAsync(string testType);
    }
}
