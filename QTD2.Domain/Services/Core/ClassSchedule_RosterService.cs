using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class ClassSchedule_RosterService : Common.Service<ClassSchedule_Roster>, IClassSchedule_RosterService
    {
        public ClassSchedule_RosterService(IClassSchedule_RosterRepository repository, IClassSchedule_RosterValidation validation)
            : base(repository, validation)
        {
        }


        public async Task<List<ClassSchedule_Roster>> GetClassScheduleRostersByEmpId(int employeeId)
        {
            var classRosterList = await FindWithIncludeAsync(x => x.EmpId == employeeId, new string[] { "Test.ILATraineeEvaluations", "TestType", "ClassSchedule.ILA", "ClassSchedule.Instructor", "ClassSchedule.Location", "ClassSchedule.Provider" });
            return classRosterList.ToList();
        }

        public async Task<int?> GetClassScheduleRostersStatus(int testId, int classScheduleId, int employeeId)
        {
            var rosterStatus = FindQuery(x => x.TestId == testId && x.ClassScheduleId == classScheduleId && x.EmpId == employeeId, false).FirstOrDefault().StatusId;
            return rosterStatus;
        }
        public async Task<List<ClassSchedule_Roster>> GetClassScheduleRostersByIdAsync(int employeeId)
        {
            var classScheduleRoster = await FindAsync(x => x.EmpId == employeeId, true);
            return classScheduleRoster.ToList();
        }

        public async Task<ClassSchedule_Roster> GetPretestAsync(int employeeId, int classScheduleId)
        {
            var val = await FindAsync(x => x.EmpId == employeeId && x.ClassScheduleId == classScheduleId && x.TestTypeId == 1);
            return val.FirstOrDefault();
        }
        public async Task<List<ClassSchedule_Roster>> GetFinalTestsToNotifyAsync()
        {
            var val = await FindWithIncludeAsync(r => (r.StatusId == 4 || r.StatusId == 1) && r.TestTypeId == 2, new string[] { "Employee.Person", "ClassSchedule.ClassSchedule_Employee", "ClassSchedule.ClassSchedule_TestReleaseEMPSettings" });
            return val.ToList();
        }

        public async Task<List<ClassSchedule_Roster>> GetPreTestsToNotifyAsync()
        {
            var val = await FindWithIncludeAsync(r => (r.StatusId == 4 || r.StatusId == 1) && r.TestTypeId == 1 && r.ClassSchedule.StartDateTime > DateTime.Now.ToUniversalTime(), new string[] { "Employee.Person", "ClassSchedule.ClassSchedule_Employee", "ClassSchedule" });
            return val.ToList();
        }

        public async Task<List<ClassSchedule_Roster>> GetClassScheduleRosterByUserId(int userId)
        {
            var result = (await FindWithIncludeAsync(x => x.EmpId == userId , new string[] { "Test", "ClassSchedule.ClassSchedule_TestReleaseEMPSettings" })).ToList();
            return result;
        }

        public async Task<string> GetTestTitleByIdAsync(int classScheduleRosterId)
        {
            var classScheduleRoster = await GetWithIncludeAsync(classScheduleRosterId, new[] { "Test" });
            return classScheduleRoster?.Test?.TestTitle;
        }

        public async Task<ClassSchedule_Roster> GetForNotificationAsync(int classScheduleRosterId)
        {
            var classScheduleRoster = await FindWithIncludeAsync(x => classScheduleRosterId == x.Id,
                 new string[] {
                    "ClassSchedule",
                    "ClassSchedule.Instructor",
                    "ClassSchedule.Location",
                    "ClassSchedule.ILA.Provider",
                    "ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest",
                    "ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest",
                    "Employee.Person"
                 });

            return classScheduleRoster.FirstOrDefault();
        }

        public async Task<List<ClassSchedule_Roster>> GetReleasedByEmployeeUsernameAsync(string userName)
        {
            var classScheduleRosters = await FindWithIncludeAsync(x => !x.ClassSchedule.Deleted && (x.IsReleased.HasValue && x.IsReleased.Value) && x.Employee.Person.Username.ToUpper() == userName.ToUpper(),
                new string[] {
                    "Test.ILATraineeEvaluations",
                    "TestType",
                    "ClassSchedule.Instructor",
                    "ClassSchedule.Location",
                    "ClassSchedule.ILA.Provider",
                    "ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest",
                    "ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest",
                    "Employee.Person",
                    "Status"
                });

            return classScheduleRosters.ToList();

        }

        public async Task<ClassSchedule_Roster> GetWithTestItemDetailsAsync(int rosterId)
        {
            var classScheduleRoster = await GetWithIncludeAsync(rosterId,
               new string[] {
                    "Test.TestReleaseEMPSettings",
                    "Test.Test_Item_Links.TestItem.TestItemType",
                    "Responses.Selections","TimeRecords"
               });
            var testItemsWithTrueFalse = (await GetWithIncludeAsync(rosterId, new[] { "Test.Test_Item_Links.TestItem.TestItemTrueFalses" })).Test.Test_Item_Links;
            var testItemsWithMatch = (await GetWithIncludeAsync(rosterId, new[] { "Test.Test_Item_Links.TestItem.TestItemMatches" })).Test.Test_Item_Links;
            var testItemsWithTFillBlanks = (await GetWithIncludeAsync(rosterId, new[] { "Test.Test_Item_Links.TestItem.TestItemFillBlanks" })).Test.Test_Item_Links;
            var testItemsWithMCQs = (await GetWithIncludeAsync(rosterId, new[] { "Test.Test_Item_Links.TestItem.TestItemMCQs" })).Test.Test_Item_Links;
            var testItemsWithShortAnswers = (await GetWithIncludeAsync(rosterId, new[] { "Test.Test_Item_Links.TestItem.TestItemShortAnswers" })).Test.Test_Item_Links;
            foreach(var testItemLink in classScheduleRoster.Test.Test_Item_Links)
            {
                testItemLink.TestItem.TestItemTrueFalses = testItemsWithTrueFalse.Where(x => x.Id == testItemLink.Id).First().TestItem.TestItemTrueFalses;
                testItemLink.TestItem.TestItemMatches = testItemsWithMatch.Where(x => x.Id == testItemLink.Id).First().TestItem.TestItemMatches.OrderBy(x=>x.Number).ToList();
                testItemLink.TestItem.TestItemFillBlanks = testItemsWithTFillBlanks.Where(x => x.Id == testItemLink.Id).First().TestItem.TestItemFillBlanks;
                testItemLink.TestItem.TestItemMCQs = testItemsWithMCQs.Where(x => x.Id == testItemLink.Id).First().TestItem.TestItemMCQs.OrderBy(x => x.Number).ToList();
                testItemLink.TestItem.TestItemShortAnswers = testItemsWithShortAnswers.Where(x => x.Id == testItemLink.Id).First().TestItem.TestItemShortAnswers;
            }
            return classScheduleRoster;
        }

        public async Task<List<ClassSchedule_Roster>> GetForPretestAndFinalTestComparison(List<int> classScheduleIds, List<int> employeeIds)
        {
            List<Expression<Func<ClassSchedule_Roster, bool>>> predicates = new List<Expression<Func<ClassSchedule_Roster, bool>>>();
            predicates.Add(csr => classScheduleIds.Contains(csr.ClassScheduleId ?? 0));
            if (employeeIds.Count() > 0)
            {
                predicates.Add(csr => employeeIds.Contains(csr.EmpId));
            }
            predicates.Add(csr => csr.Active);
            var classScheduleRosters = (await FindWithIncludeAsync(predicates, 
                new string[] {
                    "Responses"
                }, true)).ToList();

            return classScheduleRosters;
        }

        public async Task<List<ClassSchedule_Roster>> GetByClassScheduleIdListAsync(List<int> classScheduleIdList)
        {
            var val = await FindAsync(r => classScheduleIdList.Contains(r.ClassScheduleId ?? -1));
            return val.ToList();
        }

        public async Task<List<ClassSchedule_Roster>> GetClassScheduleRostersByEmployeeIdAndClassScheduleId(int employeeId, int classScheduleId)
        {
            var rosters = await FindAsync(x => x.ClassScheduleId == classScheduleId && x.EmpId == employeeId);
            return rosters.ToList();
        }

        public async Task<List<ClassSchedule_Roster>> GetPendingClassScheduleRosterByILAIdAsync(int ilaId)
        {
            return (await FindWithIncludeAsync(x => x.ClassSchedule.ILAID == ilaId && x.IsReleased.Value && x.Status.Name != "Completed", new[] { "TestType" } )).ToList();
        }

        public async Task<List<ClassSchedule_Roster>> GetClassScheduleRosterByMetaEmployeeIdAsync(int metaEmployeeId)
        {
            return (await FindWithIncludeAsync(x => x.MetaIla_EmployeeId == metaEmployeeId, new[] { "ClassSchedule" } )).ToList();
        }
    }
}
