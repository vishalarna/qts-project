using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Test : Common.Entity
    {
        public int TestStatusId { get; set; }

        public string TestTitle { get; set; }

        public bool IsPublished { get; set; }

        public bool RandomizeDistractors { get; set; }

        public DateTime? EffectiveDate { get; set; }
        public bool RandomizeQuestionsSequence { get; set; }

        public virtual TestStatus TestStatus { get; set; }

        public virtual ICollection<ILATraineeEvaluation> ILATraineeEvaluations { get; set; } = new List<ILATraineeEvaluation>();

        public virtual ICollection<Test_Item_Link> Test_Item_Links { get; set; } = new List<Test_Item_Link>();

        public virtual ICollection<Version_Test> Version_Tests { get; set; } = new List<Version_Test>();

        public virtual ICollection<Test_History> Test_Histories { get; set; } = new List<Test_History>();

        public virtual ICollection<TestReleaseEMPSettings> TestReleaseEMPSettings { get; set; } = new List<TestReleaseEMPSettings>();

        public virtual ICollection<TestReleaseEMPSetting_Retake_Link> TestReleaseEMPSetting_Retake_Links { get; set; } = new List<TestReleaseEMPSetting_Retake_Link>();

        public virtual ICollection<ClassSchedule_Roster> ClassSchedule_Rosters { get; set; } = new List<ClassSchedule_Roster>();

        public virtual ICollection<ClassSchedule_Employee> ClassSchedule_Employees { get; set; } = new List<ClassSchedule_Employee>();
        //public virtual ICollection<EmpTest> EmpTests { get; set; } = new List<EmpTest>();
        public virtual ICollection<ClassSchedule_TestReleaseEMPSetting> ClassSchedule_TestReleaseEMPSettings { get; set; } = new List<ClassSchedule_TestReleaseEMPSetting>();
        public virtual ICollection<ClassSchedule_TestReleaseEMPSetting_Retake_Link> ClassSchedule_TestReleaseEMPSetting_RetakeLinks { get; set; } = new List<ClassSchedule_TestReleaseEMPSetting_Retake_Link>();
        public Test(int testStatusId, string testTitle)
        {
            TestStatusId = testStatusId;
            TestTitle = testTitle;
        }

        public Test()
        {
        }

        public Test_Item_Link LinkTestItem(TestItem testItem)
        {
            Test_Item_Link test_Item_Link = Test_Item_Links.FirstOrDefault(x => x.TestId == this.Id && x.TestItemId == testItem.Id);
            if (test_Item_Link != null)
            {
                return test_Item_Link;
            }

            test_Item_Link = new Test_Item_Link(this, testItem);
            AddEntityToNavigationProperty<Test_Item_Link>(test_Item_Link);
            return test_Item_Link;
        }

        public void UnLinkTestItem(TestItem testItem)
        {
            Test_Item_Link test_Item_Link = Test_Item_Links.FirstOrDefault(x => x.TestItemId == testItem.Id && x.TestId == this.Id);
            if (test_Item_Link != null)
            {
                RemoveEntityFromNavigationProperty<Test_Item_Link>(test_Item_Link);
            }
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as Test;

            foreach (var testItemLink in this.Test_Item_Links)
            {
                var testItemLinkCopy = testItemLink.Copy<Test_Item_Link>(createdBy);
                testItemLinkCopy.TestId = 0;
                copy.Test_Item_Links.Add(testItemLinkCopy);
            }

            return (T)(object)copy;
        }

        public override void Delete()
        {
            AddDomainEvent(new Domain.Events.Core.OnTestDeleted(this));
            base.Delete();
        }
    }
}
