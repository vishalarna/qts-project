using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TestReleaseEMPSettingsMap : Common.MigrationMap<TblClassTest, TestReleaseEMPSettings>
    {
        List<TblClassTest> _tests;
        List<TblEmpsetting> _empSettings;

        List<TestReleaseEMPSettings> results = new List<TestReleaseEMPSettings>();

        public TestReleaseEMPSettingsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblClassTest> getSourceRecords()
        {
            _tests = (_source as EMP_DemoContext).TblClassTests.Where(r => r.Sequence == 0 || r.Sequence == -1).ToList();
            _empSettings = (_source as EMP_DemoContext).TblEmpsettings.ToList();
            return _tests;
        }

        protected override TestReleaseEMPSettings mapRecord(TblClassTest obj)
        {
            var sourceCourse = obj.Cl.Cor;
            ILA targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceCourse.Cornum).First();

            var targetTest = targetIla.ILATraineeEvaluations.Where(r => r.TestTypeId == (obj.Sequence + 2)).First().Test;


            int devaultEvalTimePeroid = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultEvalTimePeriod").First().EmpsettingValue);
            int testCutScore = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultTestCutScore").First().EmpsettingValue);
            int defaultTestTimePeriod = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultTestTimePeriod").First().EmpsettingValue);
            //int defaultTestReleaseTime = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "DefaultTestReleaseTime").First().EmpsettingValue);
            //int courseAvailableStart = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "CourseAvailableStart").First().EmpsettingValue);

            int courseAvailableEnd = Convert.ToInt32(_empSettings.Where(r => r.EmpsettingDesc == "CourseAvailableEnd").First().EmpsettingValue);

            bool CorEMP_ShowAnswers = sourceCourse.CorEmpShowAnswers.GetValueOrDefault();
            bool CorEMP_Showcorrect = sourceCourse.CorEmpShowCorrect.GetValueOrDefault();
            bool CorEMP_AllowRetake = sourceCourse.CorEmpAllowRetake.GetValueOrDefault();
            int CorEMP_RetakeCount = sourceCourse.CorEmpRetakeCount.GetValueOrDefault();
            bool CorEMP_AutoReleaseRetake = sourceCourse.CorEmpAutoReleaseRetake.GetValueOrDefault();


            TestReleaseEMPSettings settings = new TestReleaseEMPSettings()
            {
                AutoReleaseRetake = CorEMP_AutoReleaseRetake,
                MakeAvailableBeforeDays = 0,
                MakeFinalTestAvailableAfterCBTCompleted = false,
                MakeFinalTestAvailableImmediatelyAfterStartDate = false,
                MakeFinalTestAvailableOnClassEndDate = false,
                MakeFinalTestAvailableOnSpecificTime = 0,
                PreTestAvailableOneStartDate = false,
                PreTestAvailableOnEnrollment = true,
                ShowCorrectIncorrectFinalTestAnswers = CorEMP_Showcorrect,
                ShowCorrectIncorrectPreTestAnswers = CorEMP_Showcorrect,
                ShowCorrectIncorrectRetakeTestAnswers = CorEMP_Showcorrect,
                ShowStudentSubmittedFinalTestAnswers = CorEMP_Showcorrect,
                RetakeEnabled = CorEMP_AllowRetake,
                ShowStudentSubmittedPreTestAnswers = CorEMP_ShowAnswers,
                ShowStudentSubmittedRetakeTestAnswers = CorEMP_ShowAnswers,
                UsePreTestAndTest = true,
                FinalTestDueDate = defaultTestTimePeriod,
                //FinalTestId
                PreTestRequired = false,
                PreTestScore = testCutScore,
                NumberOfRetakes = CorEMP_RetakeCount,
                FinalTestPassingScore = testCutScore.ToString(),
                FinalTestSpecificTimePrior = true,
                Active = true,
                ILAId = targetIla.Id
            };

            if (obj.Sequence == -1)
            {
                settings.PreTest = targetTest;
            }
            else
            {
                settings.FinalTest = targetTest;
            }

            return settings;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tests.Count();
        }

        protected override void updateTarget(TestReleaseEMPSettings record)
        {
            int finalTestId = record.FinalTest == null ? 0 : record.FinalTest.Id;
            int preTestId = record.PreTest == null ? 0 : record.PreTest.Id;

            var existing = results
                    .Where(r => r.ILAId == record.ILAId)
                    .Where(r => r.PreTestId == preTestId || r.FinalTestId == finalTestId).FirstOrDefault();

            if (existing == null)
            {
                results.Add(record); 
                (_target as QTD2.Data.QTDContext).TestReleaseEMPSettings.Add(record);
            }
                
        }
    }
}