using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Model.EmployeeTest;
using QTD2.Infrastructure.Model.Test;
using QTD2.Infrastructure.Model.Test_Item_Link;
using QTD2.Infrastructure.Model.TestItem;
using QTD2.Infrastructure.Model.TreeDataVMs;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestService
    {
        public Task<List<Test>> GetAsync();

        public Task<Test> GetAsync(int id);

        public Task<List<TestWithCountOptions>> GetTestLinkedtoILAAsync(int ilaId);

        public Task<List<ILATraineeEvaluation>> GetILAWithTestAsync();
        
        public Task<List<Test>> GetILAWithAllTestAsync();

        public Task<List<TestDataVM>> GetAllTestsByTypeAsync(string testType);

        public Task<List<ILAWithTestVM>> GetILAWithTestMinimalTreeAsync();

        public Task<Test> CreateAsync(TestCreateOptions options);

        public Task<List<TestEOwithCountOptions>> GetLinkedEOs(int id);

        public Task<List<RosterTestVM>> GetSpecificTestTypeForILAAsync(int classScheduleId, string type);

        public Task<List<EnablingObjective>> GetEOsLinkedtoILA(int id);

        public Task<List<TestItem>> FilterTestItems(TestItemFilter option);

        public Task<List<TestItem>> GetUnlinkedQuestionsAsync(int id);

        public Task<Test> UpdateAsync(int id, TestCreateOptions options);

        public Task<Test> UpdateTestItemSequenceAsync(int id, Test_Item_Link_LinkOptions options);

        public System.Threading.Tasks.Task DeleteAsync(TestOptions options);

        public System.Threading.Tasks.Task ActiveAsync(TestOptions options);

        public System.Threading.Tasks.Task InActiveAsync(TestOptions options);

        public System.Threading.Tasks.Task MarkAsDraft(TestOptions options);

        public System.Threading.Tasks.Task MarkAsPublished(TestOptions options);

        public System.Threading.Tasks.Task ChangeILA(TestOptions options);

        public Task<Test> LinkTestItem(int id, Test_Item_Link_LinkOptions options);

        public System.Threading.Tasks.Task UnLinkTestItem(int id, Test_Item_Link_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkAllTestItemsAsync(int id);

        public Task<List<TestItemLinkVM>> GetLinkedTestItemAsync(int id);

        public Task<TestStatsVM> GetTestStatsAsync();

        public Task<List<TestItem>> GetTestItemsForTestAsync(int id);
        public Task<List<TestItemVM>> GetTestItemsForTestVMAsync(int id);

        public Task<List<TestItem>> GetTestItemsForCopyModeAsync(TestItemCopyOptions option);

        public Task<bool> CheckIfRandomItems(int id);

        public Task<List<RetakeStatusVM>> GetRetakeStatusesAsync(int empId, int classId);

        public Task<List<ILA_EnablingObjective_Link>> GetEOsLinkedToTestsILAAsync(int testId);

        public Task<List<Test>> GetTestActiveInactive(string option);


        public System.Threading.Tasks.Task ReorderTestItemsAsync(int id, ReorderTestItemOptions options);
        public TestItemVM TestItemToTestItemVM(TestItem testItem, bool stripImages = false, bool randomizeDistractors = false);
    }
}
