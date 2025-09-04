using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TestStatusesMap : Common.MigrationMap<TblTestStatus, TestStatus>
    {
        List<TblTestStatus> _teststatus;
        List<TblTest> _tests;
        List<TblEmployee> _employees;
        List<TblClass> _classes;
        List<TblEmpTestInstance> _instances;
        List<TblEmployeeTest> _employeeTests;
        List<RsTblClassStudent> _students;
        List<TblClassTest> _classTests;
        List<TblCourse> _courses;
        List<RsTblTestTestItem> _rsTblTestTestItems;
        List<TblEmployeeTestResponse> _employeeTestResponses;

        List<TestStatus> _targetTestStatuses;

        List<TblEmpsetting> _empSettings;

        List<TestItemType> _targetTestItemTypes;
        List<TaxonomyLevel> _taxonomies;
        List<TestItem> _targetTestItems;
        List<TblTestitemDistractor> _testItemDistractiors;

        public TestStatusesMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblTestStatus> getSourceRecords()
        {
            _teststatus = (_source as EMP_DemoContext).TblTestStatuses.ToList();
            _tests = (_source as EMP_DemoContext).TblTests.ToList();
            _employeeTestResponses = (_source as EMP_DemoContext).TblEmployeeTestResponses.ToList();
            _targetTestStatuses = (_target as QTD2.Data.QTDContext).TestStatuses.ToList();
            _employees = (_source as EMP_DemoContext).TblEmployees.ToList();
            _classes = (_source as EMP_DemoContext).TblClasses.ToList();
            _instances = (_source as EMP_DemoContext).TblEmpTestInstances.ToList();
            _employeeTests = (_source as EMP_DemoContext).TblEmployeeTests.ToList();
            _students = (_source as EMP_DemoContext).RsTblClassStudents.ToList();
            _courses = (_source as EMP_DemoContext).TblCourses.ToList();
            _classTests = (_source as EMP_DemoContext).TblClassTests.ToList();
            _empSettings = (_source as EMP_DemoContext).TblEmpsettings.ToList();
            _rsTblTestTestItems = (_source as EMP_DemoContext).RsTblTestTestItems.ToList();

            _targetTestItemTypes = (_target as QTD2.Data.QTDContext).TestItemTypes.ToList();
            _taxonomies = (_target as QTD2.Data.QTDContext).TaxonomyLevels.ToList();
            _targetTestItems = (_target as QTD2.Data.QTDContext).TestItems.ToList();
            _testItemDistractiors = (_source as EMP_DemoContext).TblTestitemDistractors.ToList();

            return _teststatus;
        }

        protected override TestStatus mapRecord(TblTestStatus obj)
        {
            var targetTestStatus = _targetTestStatuses.Where(r => r.Description == obj.TestStatus.Trim()).FirstOrDefault();

            if (targetTestStatus == null)
                return new TestStatus()
                {
                    Description = obj.TestStatus,
                    Deleted = false,
                    Active = true,
                    Tests = getTests(obj)
                };

            else
            {
                targetTestStatus.Tests = getTests(obj);
                return targetTestStatus;
            }
        }

        private ICollection<Test> getTests(TblTestStatus obj)
        {
            List<Test> tests = new List<Test>();
            var sourceTests = _tests.Where(r => r.TestStatusId == obj.TestStatusId);

            foreach (var sourceTest in sourceTests)
            {
                var classTests = _classTests.Where(r => r.TestId == sourceTest.TestId);

                if (classTests.Count() == 0)
                {
                    tests.Add(new Test()
                    {
                        Active = obj.TestStatus.ToUpper() == "INACTIVE" || obj.TestStatus.ToUpper() == "DELETED" ? false : true,
                        Deleted = obj.TestStatus.ToUpper() == "DELETED" ? true : false,
                        TestTitle = sourceTest.TestTitle ?? "",
                        ILATraineeEvaluations = sourceTest.Corid.HasValue ? getIlaTrainingEvaluations(sourceTest, sourceTest.Corid.Value) : null,
                        IsPublished = obj.TestStatus == "Published",
                        RandomizeDistractors = false,
                        Test_Item_Links = getTestItemLinks(sourceTest),
                        //naming mixup, class schedule rosters is really test instances while class schedule employees is a truely a person in a class
                        //ClassSchedule_Rosters = getClassScheduleRosters(sourceTest),
                        //TestReleaseEMPSettings = getTestReleaseEmpSettings(sourceTest)
                        //EffectiveDate,               
                    });
                }
                else
                {
                    List<int> corIds = new List<int>();

                    foreach (var classTest in classTests)
                    {
                        if (classTest.Cl.Corid != null)
                        {
                            if (corIds.Contains(classTest.Cl.Corid.GetValueOrDefault())) continue;
                        }

                        corIds.Add(classTest.Cl.Corid.GetValueOrDefault());

                        tests.Add(new Test()
                        {
                            Active = obj.TestStatus.ToUpper() == "INACTIVE" || obj.TestStatus.ToUpper() == "DELETED" ? false : true,
                            Deleted = obj.TestStatus.ToUpper() == "DELETED" ? true : false,
                            TestTitle = sourceTest.TestTitle ?? "",
                            ILATraineeEvaluations = getIlaTrainingEvaluations(sourceTest, classTest.Cl.Corid.GetValueOrDefault()),
                            IsPublished = obj.TestStatus == "Published",
                            RandomizeDistractors = false,
                            Test_Item_Links = getTestItemLinks(sourceTest),
                            //naming mixup, class schedule rosters is really test instances while class schedule employees is a truely a person in a class
                            ClassSchedule_Rosters = getClassScheduleRosters(sourceTest),
                            //TestReleaseEMPSettings = getTestReleaseEmpSettings(sourceTest)
                            //EffectiveDate,               
                        });

                        if (sourceTest.Corid != null)
                        {
                            if (corIds.Contains(sourceTest.Corid.Value)) continue;
                        }

                        tests.Add(new Test()
                        {
                            Active = obj.TestStatus.ToUpper() == "INACTIVE" || obj.TestStatus.ToUpper() == "DELETED" ? false : true,
                            Deleted = obj.TestStatus.ToUpper() == "DELETED" ? true : false,
                            TestTitle = sourceTest.TestTitle ?? "",
                            //ILATraineeEvaluations = getIlaTrainingEvaluations(sourceTest, classTest.Cl.Corid.GetValueOrDefault()),
                            IsPublished = obj.TestStatus == "Published",
                            RandomizeDistractors = false,
                            Test_Item_Links = getTestItemLinks(sourceTest),
                            //naming mixup, class schedule rosters is really test instances while class schedule employees is a truely a person in a class
                            //ClassSchedule_Rosters = getClassScheduleRosters(sourceTest),
                            //TestReleaseEMPSettings = getTestReleaseEmpSettings(sourceTest)
                            //EffectiveDate,               
                        });

                    }
                }
            }

            return tests;
        }

        private ICollection<ILATraineeEvaluation> getIlaTrainingEvaluations(TblTest sourceTest, int corId)
        {
            List<ILATraineeEvaluation> evaluations = new List<ILATraineeEvaluation>();

            var sourceCourse = _courses.Where(r => r.Corid == corId).First();
            var targetILA = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceCourse.Cornum).First();

            evaluations.Add(new ILATraineeEvaluation()
            {
                ILAId = targetILA.Id,
                Deleted = false,
                Active = true,
                //Default to Id
                EvaluationTypeId = 1,
                TestTitle = sourceTest.TestTitle ?? "",
                TestTypeId = 2,
                //TrainingEvaluationMethod
            });

            return evaluations;
        }

        private TestReleaseEMPSettings getTestReleaseEmpSettings(TblTest sourceTest)
        {
            var CorEMP_AutoReleaseRetake = _empSettings.Where(r => r.EmpsettingDesc.ToUpper() == "DefaultEvalTimePeriod".ToUpper()).FirstOrDefault();

            TestReleaseEMPSettings settings = new TestReleaseEMPSettings()
            {
                AutoReleaseRetake = false,
                MakeAvailableBeforeDays = 0,
                MakeFinalTestAvailableAfterCBTCompleted = false,
                MakeFinalTestAvailableImmediatelyAfterStartDate = false,
                MakeFinalTestAvailableOnClassEndDate = false,
                MakeFinalTestAvailableOnSpecificTime = 0,
                PreTestAvailableOneStartDate = false,
                PreTestAvailableOnEnrollment = true,
                ShowCorrectIncorrectFinalTestAnswers = true,
                ShowCorrectIncorrectPreTestAnswers = true,
                ShowCorrectIncorrectRetakeTestAnswers = true,
                ShowStudentSubmittedFinalTestAnswers = true,
                RetakeEnabled = true,
                ShowStudentSubmittedPreTestAnswers = true,
                ShowStudentSubmittedRetakeTestAnswers = true,
                UsePreTestAndTest = true,
                FinalTestDueDate = 2,
                //FinalTestId
                PreTestRequired = false,
                PreTestScore = 0,
                NumberOfRetakes = 0,
                FinalTestPassingScore = "50",
                FinalTestSpecificTimePrior = true,
                Active = true
            };

            return settings;
        }

        //private ICollection<EmpTest> getEmpTests(TblTest sourceTest)
        //{
        //    List<EmpTest> empTest = new List<EmpTest>();

        //    var employeeTests = _employeeTests.Where(r => r.TestId == sourceTest.TestId);

        //    foreach (var employeeTest in employeeTests)
        //    {
        //        var responses = (_source as EMP_DemoContext)
        //            .TblEmployeeTestResponses
        //            .Where(r => r.Eid == employeeTest.Eid)
        //            .Where(r => r.TestId == employeeTest.TestId)
        //            .ToList();
        //        var sourceEmployee = _employees.Where(r => r.Eid == employeeTest.Eid).First();
        //        var sourceEmployeeTest = _employeeTests.Where(r => r.Eid == employeeTest.Eid).Where(r => r.TestId == employeeTest.TestId).First();
        //        var sourceClass = _classes.Where(r => r.Clid == sourceEmployeeTest.Clid).First();

        //        var targetEmployee = (_target as QTD2.Data.QTDContext).Employees
        //                            .Where(r => r.Person.FirstName == sourceEmployee.EfirstName)
        //                            .Where(r => r.Person.LastName == sourceEmployee.ElastName)
        //                            .Where(r => r.EmployeeNumber == sourceEmployee.Enum).First();

        //        var targetCourse = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Name == sourceClass.Cor.Cordesc).First();
        //        var targetClass = targetCourse.ClassSchedules.Where(r => r.StartDateTime == sourceClass.ClstartDate.GetValueOrDefault()).FirstOrDefault();

        //        if (targetClass == null) continue;
        //        /////   Changes made in the EMPTest Inorder to accomodate same retakes being released multiple times
        //        foreach (var response in responses)
        //        {
        //            empTest.Add(new EmpTest()
        //            {
        //                Active = true,
        //                UserAnswer = response.Response,
        //                //EmployeeId = targetEmployee.Id,
        //                //ClassScheduleId = targetClass.Id,
        //                //TestTypeId = 2,
        //                TestItemTypeId = 1
        //            });
        //        }               
        //    }

        //    return empTest;
        //}

        private ICollection<ClassSchedule_Roster> getClassScheduleRosters(TblTest sourceTest)
        {
            var rosters = new List<ClassSchedule_Roster>();
            var employeeTests = _employeeTests.Where(r => r.TestId == sourceTest.TestId);

            foreach (var employeeTest in employeeTests)
            {
                var sourceEmployee = _employees.Where(r => r.Eid == employeeTest.Eid).FirstOrDefault();

                if (sourceEmployee == null) continue;

                var sourceClass = _classes.Where(r => r.Clid == employeeTest.Clid).FirstOrDefault();

                if (sourceClass == null) continue;

                var targetEmployee = (_target as QTD2.Data.QTDContext).Employees
                                    .Where(r => r.Person.FirstName == sourceEmployee.EfirstName)
                                    .Where(r => r.Person.LastName == sourceEmployee.ElastName)
                                    .Where(r => r.EmployeeNumber == sourceEmployee.Enum).First();

                var targetCourse = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceClass.Cor.Cornum).First();

                //int startAMPM = sourceClass.StartAmPm.HasValue ? sourceClass.StartAmPm.Value : -1;
                //int endAMPM = sourceClass.EndAmPm.HasValue ? sourceClass.EndAmPm.Value : -1;

                //string startTime = sourceClass.StartTimeStr;
                //string endTime = sourceClass.EndTimeStr;

                //string[] startTimeParts = (startTime ?? "").Split(":");
                //string[] endTimeParts = (endTime ?? "").Split(":");

                //int starTimeHours = (startAMPM == -1 || startTimeParts.Length == 1) ? 0 :
                //         startAMPM == 0 ?
                //            startTimeParts[0] == "12" ? 0 : Convert.ToInt32(startTimeParts[0])
                //         :
                //            startTimeParts[0] == "12" ? 12 : 12 + Convert.ToInt32(startTimeParts[0]);


                //int endTimeHours = (endAMPM == -1 || endTimeParts.Length == 1) ? 0 :
                //         endAMPM == 0 ?
                //            endTimeParts[0] == "12" ? 0 : Convert.ToInt32(endTimeParts[0])
                //         :
                //            endTimeParts[0] == "12" ? 12 : 12 + Convert.ToInt32(endTimeParts[0]);

                //int startTimeMinutes = startAMPM == -1 ? 0 : starTimeHours;
                //int endTimeMinutes = endAMPM == -1 ? 0 : endTimeHours;

                //DateTime classStartDate;

                //if (sourceClass.Cor.SelfPased)
                //{
                //    var student = _students.Where(r => r.Clid == sourceClass.Clid).FirstOrDefault();

                //    if (student == null || !student.SecondDate.HasValue)
                //    {
                //        classStartDate = sourceClass.ClstartDate == null ? sourceClass.Cldate.GetValueOrDefault() : sourceClass.ClstartDate.GetValueOrDefault();
                //    }
                //    else
                //    {
                //        classStartDate = student.SecondDate.Value;
                //    }
                //}
                //else
                //{S
                //    classStartDate = sourceClass.StartDate == null ? sourceClass.ClstartDate.GetValueOrDefault() : sourceClass.StartDate.GetValueOrDefault();
                //}

                //classStartDate = classStartDate.Hour == 0 ? classStartDate.AddHours(starTimeHours).AddMinutes(startTimeMinutes).ToUniversalTime() : classStartDate.ToUniversalTime();

                var targetClass = targetCourse.ClassSchedules.Where(r => r.SpecialInstructions == sourceClass.Clid.ToString()).FirstOrDefault();

                if (targetClass == null) continue;

                rosters.Add(new ClassSchedule_Roster()
                {
                    Active = true,
                    CompletedDate = employeeTest.DateComplete.HasValue ? employeeTest.DateComplete.Value.ToQtsTime(false) : employeeTest.DateComplete.ToQtsTime(false),
                    Disclaimer = employeeTest.DisclaimerSigned.GetValueOrDefault(),
                    Responses = getResponses(employeeTest),
                    EmpId = targetEmployee.Id,
                    ClassScheduleId = targetClass.Id,
                    Grade = employeeTest.TestGrade,
                    Interrupted = employeeTest.TestInterrupt.GetValueOrDefault(),
                    IsReleased = true,
                    ReleaseDate = employeeTest.DateAdded.HasValue ? employeeTest.DateAdded.Value.ToQtsTime(false) : null,
                    Restarted = employeeTest.Restart.GetValueOrDefault(),
                    Score = employeeTest.TestScore,
                    StatusId = employeeTest.TestComplete.GetValueOrDefault() ? 3 : employeeTest.TestInterrupt.GetValueOrDefault() ? 2 : 1,
                    RetakeOrder = employeeTest.Retake.GetValueOrDefault() ? employeeTests.Where(r => r.Etid < employeeTest.Etid).Where(r => r.Retake.GetValueOrDefault()).Count() + 1 : null,
                    TestTypeId = employeeTest.IsPreTest.GetValueOrDefault() ? 1 :
                                    employeeTest.Retake.GetValueOrDefault() ? 3 :
                                    2
                });
            }

            return rosters;
        }

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



        private string getFillInTheBlankStem(TblTestItem sourceItem, string originalStem)
        {
            var fibs = _testItemDistractiors.Where(r => r.TestItemId == sourceItem.TestItemId).ToList();
            var fib = fibs.First();

            System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
            xdoc.LoadXml(fib.DistractorDetails);
            var parts = xdoc.FirstChild.InnerText.Split("&blank;");

            string stem = originalStem;

            if (parts.Length == 3)
                stem = parts[0] + "<u>" + parts[1] + "</u>" + parts[2];

            return "<p>" + stem + "</p>";
        }

        private ICollection<Test_Item_Link> getTestItemLinks(TblTest sourceTest)
        {
            var links = new List<Test_Item_Link>();

            var sourceItems = _rsTblTestTestItems.Where(r => r.TestId == sourceTest.TestId).ToList();

            foreach (var sourceItem in sourceItems)
            {
                var stem = (sourceItem.TestItem.Stem ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(sourceItem.TestItem.Stem ?? "") : sourceItem.TestItem.Stem;
                var sourceEo = sourceItem.TestItem.Sk;
                EnablingObjective targetEo = null;

                if (sourceEo != null)
                {
                    var sourceCateogry = (_source as EMP_DemoContext).TblCategories.Where(r => r.Cid == sourceEo.Cid).First();

                    string sksubnum = sourceEo.SksubNum.HasValue ? sourceEo.SksubNum.Value.ToString() : "";
                    if (!String.IsNullOrEmpty(sksubnum) && sksubnum != "0")
                    {
                        int skNum = sourceEo.Sknum.HasValue ? sourceEo.Sknum.Value : 0;
                        int csubnum = sourceCateogry.CsubNum.GetValueOrDefault();

                        var targetCatgory = (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Where(r => r.Number == sourceCateogry.Cnum).First();
                        var targetSubCategory = targetCatgory.EnablingObjective_SubCategories.Where(r => r.Number == csubnum).FirstOrDefault();
                        var targetopic = targetSubCategory == null ? null : targetSubCategory.EnablingObjective_Topics.Where(r => r.Number == skNum).FirstOrDefault();

                        if (skNum == 0)
                        {
                            if (csubnum == 0)
                            {
                                targetEo = (_target as QTD2.Data.QTDContext).EnablingObjectives.Where(r => r.Number == sksubnum).Where(r => r.CategoryId == targetCatgory.Id).First();
                            }
                            else
                            {
                                targetEo = (_target as QTD2.Data.QTDContext).EnablingObjectives.Where(r => r.Number == sksubnum).Where(r => r.SubCategoryId == targetSubCategory.Id).First();
                            }
                        }
                        else
                        {
                            targetEo = (_target as QTD2.Data.QTDContext).EnablingObjectives.Where(r => r.Number == sksubnum).Where(r => r.TopicId == targetopic.Id).First();
                        }
                    }
                }

                var testItemType = getQtd2TestItemTypeName(sourceItem.TestItem.TestItemTypeNavigation.TestItemType);
                var targetTestItemType = _targetTestItemTypes.Where(r => r.Description == testItemType).First();
                var targetTaxonomy = _taxonomies.Where(r => r.Description == sourceItem.TestItem.Taxonomy.TaxonomyLevel).First();


                var targetTestItem = _targetTestItems.Where(r => r.Number == sourceItem.TestItemId.ToString()).First();

                links.Add(new Test_Item_Link()
                {
                    Sequence = sourceItems.IndexOf(sourceItem),
                    TestItemId = targetTestItem.Id
                });
            }

            return links;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _teststatus.Count();
        }

        protected override void updateTarget(TestStatus record)
        {
            if (record.Id > 0)
            {
                (_target as QTD2.Data.QTDContext).TestStatuses.Update(record);
            }
            else
            {
                (_target as QTD2.Data.QTDContext).TestStatuses.Add(record);
            }

        }
    }
}
