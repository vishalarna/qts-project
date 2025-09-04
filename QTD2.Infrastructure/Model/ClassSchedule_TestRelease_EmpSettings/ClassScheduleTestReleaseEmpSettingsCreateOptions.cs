using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_TestRelease_EmpSettings
{
    public class ClassScheduleTestReleaseEmpSettingsCreateOptions
    {
        public int ClassScheduleId { get; set; }

        public int? FinalTestId { get; set; }

        public int? PreTestId { get; set; }

        public bool UsePreTestAndTest { get; set; }

        public bool PreTestRequired { get; set; }

        public bool PreTestAvailableOnEnrollment { get; set; }

        public bool PreTestAvailableOneStartDate { get; set; }

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

        public int PreTestScore { get; set; }

        public List<int> retakesTestIds { get; set; }
        public int? EmpSettingsReleaseTypeId { get; set; }

        public ClassScheduleTestReleaseEmpSettingsCreateOptions() { }

        public ClassScheduleTestReleaseEmpSettingsCreateOptions(int classScheduleId, int? finalTestId, int? preTestId, bool usePreTestAndTest, bool preTestRequired, bool preTestAvailableOnEnrollment, bool preTestAvailableOneStartDate, bool showStudentSubmittedPreTestAnswers, bool showCorrectIncorrectPreTestAnswers, int? makeAvailableBeforeDays, string finalTestPassingScore, bool makeFinalTestAvailableImmediatelyAfterStartDate, bool makeFinalTestAvailableOnClassEndDate, bool makeFinalTestAvailableAfterCBTCompleted, int? makeFinalTestAvailableOnSpecificTime, bool finalTestSpecificTimePrior, int finalTestDueDate, bool showStudentSubmittedFinalTestAnswers, bool showStudentSubmittedRetakeTestAnswers, bool showCorrectIncorrectFinalTestAnswers, bool showCorrectIncorrectRetakeTestAnswers, bool autoReleaseRetake, bool retakeEnabled, int? numberOfRetakes, int? empSettingsReleaseTypeId)
        {
            ClassScheduleId = classScheduleId;
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
            EmpSettingsReleaseTypeId = empSettingsReleaseTypeId;
        }
    }
}
