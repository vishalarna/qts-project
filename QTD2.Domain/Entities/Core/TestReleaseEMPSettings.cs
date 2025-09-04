using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TestReleaseEMPSettings: Common.Entity
    {
        private object value;

        public int ILAId { get; set; }

        public int? FinalTestId { get; set; }

        public int? PreTestId { get; set; }

        public bool UsePreTestAndTest { get; set; }

        public bool PreTestRequired { get; set; }
        public string jobDetails { get; set; }

        public bool PreTestAvailableOnEnrollment { get; set; }

        public bool PreTestAvailableOneStartDate { get; set; }

        public int PreTestScore { get; set; }

        public bool ShowStudentSubmittedPreTestAnswers { get; set; }

        public bool ShowCorrectIncorrectPreTestAnswers { get; set; }


        public int? MakeAvailableBeforeDays { get; set; }
        public int? MakeAvailableBeforeWeeks { get; set; }
        public int? DaysOrWeeks { get; set; }

        public string FinalTestPassingScore { get; set; }

        public bool MakeFinalTestAvailableImmediatelyAfterStartDate { get; set; }

        public bool MakeFinalTestAvailableOnClassEndDate { get; set; }

        public bool MakeFinalTestAvailableAfterCBTCompleted { get; set; }

        public int? MakeFinalTestAvailableOnSpecificTime { get; set; }

        public bool FinalTestSpecificTimePrior { get; set; }

        public int FinalTestDueDate { get; set; }

        public bool ShowStudentSubmittedFinalTestAnswers { get; set; }

        public bool ShowStudentSubmittedRetakeTestAnswers { get; set; }

        public bool ShowCorrectIncorrectFinalTestAnswers { get; set; }

        public bool ShowCorrectIncorrectRetakeTestAnswers { get; set; }

        public bool AutoReleaseRetake { get; set; }

        public bool RetakeEnabled { get; set; }

        public int? NumberOfRetakes { get; set; }
        public int? EmpSettingsReleaseTypeId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual Test PreTest { get; set; }

        public virtual Test FinalTest { get; set; }

        public virtual ICollection<TestReleaseEMPSetting_Retake_Link> TestReleaseEMPSetting_Retake_Links { get; set; } = new List<TestReleaseEMPSetting_Retake_Link>();

        public virtual EmpSettingsReleaseType EmpSettingsReleaseType { get; set; }
        public TestReleaseEMPSettings()
        {

        }
        public TestReleaseEMPSettings(int iLAId, int? finalTestId, int? preTestId, bool usePreTestAndTest, bool preTestRequired, bool preTestAvailableOnEnrollment, bool preTestAvailableOneStartDate, bool showStudentSubmittedPreTestAnswers, bool showCorrectIncorrectPreTestAnswers, int? makeAvailableBeforeDays, string finalTestPassingScore, bool makeFinalTestAvailableImmediatelyAfterStartDate, bool makeFinalTestAvailableOnClassEndDate, bool makeFinalTestAvailableAfterCBTCompleted, int? makeFinalTestAvailableOnSpecificTime, bool finalTestSpecificTimePrior, int finalTestDueDate, bool showStudentSubmittedFinalTestAnswers, bool showStudentSubmittedRetakeTestAnswers, bool showCorrectIncorrectFinalTestAnswers, bool showCorrectIncorrectRetakeTestAnswers, bool autoReleaseRetake, bool retakeEnabled, int? numberOfRetakes, int preTestScore,int? makeAvailableBeforeWeeks,int? daysOrWeeks,int? empSettingsReleaseTypeId)
        {
            ILAId = iLAId;
            FinalTestId = finalTestId;
            PreTestId = preTestId;
            UsePreTestAndTest = usePreTestAndTest;
            PreTestRequired = preTestRequired;
            PreTestAvailableOnEnrollment = preTestAvailableOnEnrollment;
            PreTestAvailableOneStartDate = preTestAvailableOneStartDate;
            ShowStudentSubmittedPreTestAnswers = showStudentSubmittedPreTestAnswers;
            ShowCorrectIncorrectPreTestAnswers = showCorrectIncorrectPreTestAnswers;
            MakeAvailableBeforeDays = makeAvailableBeforeDays;
            FinalTestPassingScore = finalTestPassingScore;
            MakeFinalTestAvailableImmediatelyAfterStartDate = makeFinalTestAvailableImmediatelyAfterStartDate;
            MakeFinalTestAvailableOnClassEndDate = makeFinalTestAvailableOnClassEndDate;
            MakeFinalTestAvailableAfterCBTCompleted = makeFinalTestAvailableAfterCBTCompleted;
            MakeFinalTestAvailableOnSpecificTime = makeFinalTestAvailableOnSpecificTime;
            FinalTestSpecificTimePrior = finalTestSpecificTimePrior;
            FinalTestDueDate = finalTestDueDate;
            ShowStudentSubmittedFinalTestAnswers = showStudentSubmittedFinalTestAnswers;
            ShowStudentSubmittedRetakeTestAnswers = showStudentSubmittedRetakeTestAnswers;
            ShowCorrectIncorrectFinalTestAnswers = showCorrectIncorrectFinalTestAnswers;
            ShowCorrectIncorrectRetakeTestAnswers = showCorrectIncorrectRetakeTestAnswers;
            AutoReleaseRetake = autoReleaseRetake;
            RetakeEnabled = retakeEnabled;
            NumberOfRetakes = numberOfRetakes;
            PreTestScore = preTestScore;
            DaysOrWeeks = daysOrWeeks;
            makeAvailableBeforeWeeks = makeAvailableBeforeWeeks;
            EmpSettingsReleaseTypeId = empSettingsReleaseTypeId;
        }

        public TestReleaseEMPSettings(int iLAId, object value)
        {
            ILAId = iLAId;
            this.value = value;
        }
        public DateTime GetDueDate(DateTime dueDate)
        {
            if (EmpSettingsReleaseTypeId == 3)
            {
                return dueDate.AddMonths(FinalTestDueDate);
            }
            else if (EmpSettingsReleaseTypeId == 2)
            {
                return dueDate.AddDays(FinalTestDueDate * 7);
            }
            else if (EmpSettingsReleaseTypeId == 1)
            {
                return dueDate.AddDays(FinalTestDueDate);
            }
            return DateTime.MinValue;
        }

        public TestReleaseEMPSetting_Retake_Link LinkRetake(Test test)
        {
            //TestReleaseEMPSetting_Retake_Link test_retake_link = TestReleaseEMPSetting_Retake_Links.FirstOrDefault(x => x.RetakeTestId == test.Id && x.TestReleaseSettingId == this.Id);
            //if (test_retake_link != null)
            //{
            //    return test_retake_link;
            //}

            TestReleaseEMPSetting_Retake_Link test_retake_link = new TestReleaseEMPSetting_Retake_Link(this, test);
            AddEntityToNavigationProperty<TestReleaseEMPSetting_Retake_Link>(test_retake_link);
            return test_retake_link;
        }

        public void UnlinkRetake(Test test)
        {
            TestReleaseEMPSetting_Retake_Link test_retake_link = TestReleaseEMPSetting_Retake_Links.FirstOrDefault(x => x.RetakeTestId == test.Id && x.TestReleaseSettingId == this.Id);
            if(test_retake_link != null)
            {
                RemoveEntityFromNavigationProperty(test_retake_link);
            }
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as TestReleaseEMPSettings;

            foreach (var retakeLinks in this.TestReleaseEMPSetting_Retake_Links)
            {
                var retakeLinkCopy = retakeLinks.Copy<TestReleaseEMPSetting_Retake_Link>(createdBy);
                retakeLinkCopy.TestReleaseSettingId = 0;
                copy.TestReleaseEMPSetting_Retake_Links.Add(retakeLinkCopy);
            }

            return (T)(object)copy;
        }
    }
}
