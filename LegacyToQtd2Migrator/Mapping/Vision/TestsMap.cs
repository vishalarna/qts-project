using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;
using LegacyToQtd2Migrator.Helpers;
using LegacyToQtd2Migrator.Vision.Data;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class TestsMap : Common.MigrationMap<ExamImpl, Test>
    {
        List<ExamImpl> _sourceExamImpls;
        List<ExamChoiceOrder> _sourceExamChoiceOrders;
        List<LsExamEvent> _sourceExamEvents;

        int _projectId;

        List<TestItemType> _targetTestItemTypes;
        List<TaxonomyLevel> _taxonomies;
        List<TestItem> _targetTestItems;
        List<Employee> _targetEmployees;
        List<ClassSchedule> _targetClassSchedules;

        public TestsMap(DbContext source, DbContext target, int projectId) : base(source, target)
        {
            _projectId = projectId;
        }

        protected override List<ExamImpl> getSourceRecords()
        {
            _sourceExamImpls = (_source as VisionContext).ExamImpls.Where(r => r.FkProject == _projectId).Where(r => r.FkExpiredBy == null).ToList();
            _sourceExamChoiceOrders = (_source as VisionContext).ExamChoiceOrders.Where(r => r.FkExpiredBy == null).ToList();
            _sourceExamEvents = (_source as VisionContext).LsExamEvents.Include("FkExamResultsNavigation").Include("FkLearnerNavigation").ToList();

            _targetTestItemTypes = (_target as QTD2.Data.QTDContext).TestItemTypes.ToList();
            _taxonomies = (_target as QTD2.Data.QTDContext).TaxonomyLevels.ToList();
            _targetTestItems = (_target as QTD2.Data.QTDContext).TestItems.ToList();
            _targetEmployees = (_target as QTD2.Data.QTDContext).Employees.Include("Person").ToList();
            _targetClassSchedules = (_target as QTD2.Data.QTDContext).ClassSchedules.Include("ILA").ToList();

            return _sourceExamImpls;
        }

        protected override Test mapRecord(ExamImpl obj)
        {
            var classScheduleRosters = getClassScheduleRosters(obj);

            return new Test()
            {
                Active = true,
                TestTitle = obj.Text,
                //ILATraineeEvaluations = getIlaTrainingEvaluations(sourceTest, classTest.Cl.Corid.GetValueOrDefault()),
                IsPublished = true,
                RandomizeDistractors = false,
                Test_Item_Links = getTestItemLinks(obj),
                ClassSchedule_Rosters = classScheduleRosters,
                ILATraineeEvaluations = getILATraineeEvaluations(classScheduleRosters, obj),
                //EffectiveDate,
                TestStatusId = 2
            };
        }

        private ICollection<ILATraineeEvaluation> getILATraineeEvaluations(ICollection<ClassSchedule_Roster> classScheduleRosters, ExamImpl obj)
        {
            List<ILATraineeEvaluation> iLATraineeEvaluations = new List<ILATraineeEvaluation>();

            foreach(var classScheduleId in classScheduleRosters.Select(r => r.ClassScheduleId).Distinct())
            {
                var ila = _targetClassSchedules.Where(r => r.Id == classScheduleId).First().ILA;

                iLATraineeEvaluations.Add(new ILATraineeEvaluation()
                {
                    ILA = ila,
                    TestTypeId = 2,
                    Active = true,
                    EvaluationTypeId = 1,
                    TestTitle = obj.Text
                });
            }

            return iLATraineeEvaluations;
        }

        private ICollection<Test_Item_Link> getTestItemLinks(ExamImpl sourceExam)
        {
            var links = new List<Test_Item_Link>();

            var sourceItems = _sourceExamChoiceOrders.Where(r => r.FkExam == sourceExam.FkExam).ToList();

            foreach (var sourceItem in sourceItems)
            {
                var targetTestItem = _targetTestItems.Where(r => r.Number == sourceItem.FkQuestion.ToString()).FirstOrDefault();

                if (targetTestItem == null) continue;

                if (links.Where(r => r.TestItemId == targetTestItem.Id).Count() > 0) continue;

                links.Add(new Test_Item_Link()
                {
                    Sequence = (int)sourceItem.Sequence,
                    TestItemId = targetTestItem.Id
                });
            }

            return links;
        }


        private ICollection<ClassSchedule_Roster> getClassScheduleRosters(ExamImpl sourceExam)
        {
            var rosters = new List<ClassSchedule_Roster>();
            var sourceExamEvents = _sourceExamEvents.Where(r => r.FkExam == sourceExam.FkExam);

            foreach (var sourceExamEvent in sourceExamEvents)
            {
                Employee targetEmployee = getTargetEmployee(sourceExamEvent.FkLearnerNavigation);
                ClassSchedule targetClass = getTargetClassSchedule(sourceExamEvent.FkLearningEventNavigation);

                if (targetClass == null) continue;

                if (targetEmployee == null) continue;

                var sourceExamResults = sourceExamEvent.FkExamResultsNavigation;

                string grade = sourceExamResults == null ? null : (sourceExamResults.ExamScore >= sourceExamResults.PassingScore ? "P" : "F");

                rosters.Add(new ClassSchedule_Roster()
                {
                    Active = true,
                    CompletedDate = sourceExamEvent.DateCompleted,
                    Disclaimer = true,
                    //Responses = getResponses(employeeTest),
                    EmpId = targetEmployee.Id,
                    ClassScheduleId = targetClass.Id,
                    Grade = grade,
                    Interrupted = false,
                    IsReleased = true,
                    ReleaseDate = sourceExamEvent.DateStarted,
                    Restarted = false,
                    Score = (int?)(sourceExamResults?.ExamScore),
                    StatusId = sourceExamEvent.DateStarted.HasValue ? (sourceExamEvent.DateCompleted.HasValue ? 3 : 2) : 4,
                    RetakeOrder = null,
                    TestTypeId = 2
                });
            }


            return rosters;
        }

        private ClassSchedule getTargetClassSchedule(LsLearningEvent fkLearningEventNavigation)
        {
            var classSchedule = _targetClassSchedules.Where(r => r.Notes == fkLearningEventNavigation.Id.ToString()).FirstOrDefault();

            return classSchedule;
        }

        private Employee getTargetEmployee(Learner learner)
        {
            string firstname = learner.FirstName;
            string lastname = learner.LastName;

            var employee = _targetEmployees
                                .Where(r => r.Person.FirstName.ToUpper() == firstname.ToUpper())
                                .Where(r => r.Person.LastName.ToUpper() == lastname.ToUpper())
                            .FirstOrDefault();

            return employee;
        }


        /*
         private ICollection<ClassSchedule_Roster_Response> getResponses(TblEmployeeTest employeeTest)
         {
             List<ClassSchedule_Roster_Response> responses = new List<ClassSchedule_Roster_Response>();

             var sourceResponses = _employeeTestResponses.Where(r => r.Etid == employeeTest.Etid).ToList();

             foreach (var sourceResponse in sourceResponses)
             {
                 var sourceTestItem = (_source as EMP_DemoContext).TblTestItems.Where(r => r.TestItemId == sourceResponse.TestItemId).First();
                 string stem = (sourceTestItem.Stem ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(sourceTestItem.Stem ?? "") : sourceTestItem.Stem;
                 string sourceStem = sourceTestItem.TestItemTypeNavigation.TestItemType == "Fill-in-the-blank" ? getFillInTheBlankStem(sourceTestItem, stem) : stem;

                 var targetTestItem = (_target as QTD2.Data.QTDContext).TestItems.IgnoreQueryFilters().Where(r => r.Description == sourceStem).First();

                 var targetResponse = responses.Where(r => r.TestItemId == targetTestItem.Id).FirstOrDefault();

                 if (targetResponse == null)
                 {
                     responses.Add(new ClassSchedule_Roster_Response()
                     {
                         Active = true,
                         Deleted = false,
                         IsComplete = sourceResponse.Complete.GetValueOrDefault(),
                         IsCorrect = sourceResponse.QuesScore.GetValueOrDefault() > 0,
                         TestItemId = targetTestItem.Id,
                         Selections = new List<ClassSchedule_Roster_Response_Selection>()
                         {
                             new ClassSchedule_Roster_Response_Selection()
                             {
                                 Active = true,
                                 UserAnswer = sourceResponse.Response,
                                 Deleted = false
                             }
                         }
                     });
                 }
                 else
                 {
                     targetResponse.Selections.Add(new ClassSchedule_Roster_Response_Selection()
                     {
                         Active = true,
                         UserAnswer = sourceResponse.Response,
                         Deleted = false
                     });

                     targetResponse.IsComplete = targetResponse.IsComplete && sourceResponse.Complete.GetValueOrDefault();
                     targetResponse.IsCorrect = targetResponse.IsCorrect.GetValueOrDefault() && (sourceResponse.QuesScore.GetValueOrDefault() > 0);
                 }
             }

             return responses;
         }

        private ICollection<ClassSchedule_Roster_Response_Selection> getEmpTests(TblEmployeeTest employeeTest)
        {
            List<ClassSchedule_Roster_Response_Selection> empTests = new List<ClassSchedule_Roster_Response_Selection>();

            var responses = _employeeTestResponses.Where(r => r.Etid == employeeTest.Etid).ToList();

            foreach (var response in responses)
            {
                var sourceTestItem = (_source as EMP_DemoContext).TblTestItems.Where(r => r.TestItemId == response.TestItemId).First();
                string stem = (sourceTestItem.Stem ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(sourceTestItem.Stem ?? "") : sourceTestItem.Stem;
                string sourceStem = sourceTestItem.TestItemTypeNavigation.TestItemType == "Fill-in-the-blank" ? getFillInTheBlankStem(sourceTestItem, stem) : stem;

                var targetTestItem = (_target as QTD2.Data.QTDContext).TestItems.Where(r => r.Description == sourceStem).First();

                empTests.Add(new ClassSchedule_Roster_Response_Selection()
                {
                    Active = true,
                    UserAnswer = response.Response,
                });
            }

            return empTests;
        }
        */

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _sourceExamImpls.Count();
        }

        protected override void updateTarget(Test record)
        {
            (_target as QTD2.Data.QTDContext).Tests.Add(record);
        }
    }
}
