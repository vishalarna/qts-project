using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class PretestAndFinalTestComparisonModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public string DefaultDateFormat { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<PretestAndFinalTestComparison_ClassDetails> ClassDetails { get; set; } = new List<PretestAndFinalTestComparison_ClassDetails>();
        public bool IncludeTestItemDetails { get; set; } 
        public PretestAndFinalTestComparisonModel(
            string title,
            string templatePath,
            List<string> displayColumns,
            string companyLogo,
            string defaultTimeZone,
            List<Domain.Entities.Core.ClientSettings_LabelReplacement> clientSettings_LabelReplacements,
            List<int> employeeIds,
            List<ClassSchedule> classSchedules,
            List<Test> tests,
            List<ClassSchedule_Roster> classScheduleRosters,
            bool showOnlyFailedPretestGrades,
            bool includeTestItemDetails,string defaultDateFormat)
        {
            Title = title;
            TemplatePath = templatePath;
            DisplayColumns = displayColumns;
            CompanyLogo = companyLogo;
            DefaultTimeZone = defaultTimeZone;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            IncludeTestItemDetails = includeTestItemDetails;

            foreach (var classSchedule in classSchedules)
            {
                ClassDetails.Add(new PretestAndFinalTestComparison_ClassDetails(classSchedule, tests, classScheduleRosters, employeeIds, showOnlyFailedPretestGrades));
            }

            DefaultDateFormat = defaultDateFormat;
        }
    }

    public class PretestAndFinalTestComparison_ClassDetails {
        public string ILATitle { get; set; }
        public string ILANumber { get; set; }
        public DateTime ClassStartDate { get; set; }
        public DateTime ClassEndDate { get; set; }
        public int NumberOfEmployeesEnrolled { get; set; }
        public int? PretestCutScore { get; set; }
        public string FinalTestCutScore { get; set; }
        public double? ClassPretestAverage { get; set; }
        public double? ClassFinalTestAverage { get; set; }
        public List<PretestAndFinalTestComparison_EmployeeDetails> EmployeeDetails { get; set; } = new List<PretestAndFinalTestComparison_EmployeeDetails>();
        public List<PretestAndFinalTestComparison_TestItemDetails> TestItemDetails { get; set; } = new List<PretestAndFinalTestComparison_TestItemDetails>();

        public PretestAndFinalTestComparison_ClassDetails(ClassSchedule classSchedule, List<Test> tests, List<ClassSchedule_Roster> classScheduleRosters, List<int> employeeIds, bool showOnlyFailedPretestGrades)
        {
            ILATitle = classSchedule?.ILA?.Name;
            ILANumber = classSchedule?.ILA?.Number;
            ClassStartDate = classSchedule.StartDateTime;
            ClassEndDate = classSchedule.EndDateTime;
            PretestCutScore = classSchedule?.ClassSchedule_TestReleaseEMPSettings?.PreTestScore;
            FinalTestCutScore = classSchedule?.ClassSchedule_TestReleaseEMPSettings?.FinalTestPassingScore;

            var preTestId = classSchedule?.ClassSchedule_TestReleaseEMPSettings?.PreTestId;
            var finalTestId = classSchedule?.ClassSchedule_TestReleaseEMPSettings?.FinalTestId;
            var classScheduleEmployees = classSchedule?.ClassSchedule_Employee.Where(cse => cse.IsEnrolled && cse.Active);
            if (employeeIds.Count() > 0)
            {
                classScheduleEmployees = classScheduleEmployees.Where(cse => employeeIds.Contains(cse.EmployeeId));
            }
            var filteredClassScheduleRosters = classScheduleRosters.Where(csr => csr.ClassScheduleId == classSchedule.Id).ToList();
            List<TestItem> testItems = new List<TestItem>();

            foreach (var classScheduleEmployee in classScheduleEmployees)
            {
                EmployeeDetails.Add(new PretestAndFinalTestComparison_EmployeeDetails(classScheduleEmployee, filteredClassScheduleRosters, preTestId, finalTestId));
            }

            if (showOnlyFailedPretestGrades)
            {
                // Limit Employees to only those that failed the pretest
                EmployeeDetails = EmployeeDetails.Where(ed => ed.PretestScore < PretestCutScore).ToList();

                // Refilter the rosters to only those for Employees with failed pretests, so the whole report's data is based on only the Employees shown
                filteredClassScheduleRosters = filteredClassScheduleRosters.Where(csr => EmployeeDetails.Select(ed => ed._EmployeeId).Contains(csr.EmpId)).ToList();
            }

            NumberOfEmployeesEnrolled = EmployeeDetails.Count();

            if (preTestId != null)
            {
                ClassPretestAverage = EmployeeDetails.Select(ed => ed.PretestScore).Average();
                var pretestItem = tests.FirstOrDefault(t => t.Id == preTestId)?.Test_Item_Links?.Select(til => til.TestItem);
                if(pretestItem != null)
                {
                    testItems.AddRange(pretestItem);
                }
            }
            if (finalTestId != null)
            {
                ClassFinalTestAverage = EmployeeDetails.Select(ed => ed.FinalTestScore).Average();
                var finalTestItem = tests.FirstOrDefault(t => t.Id == finalTestId)?.Test_Item_Links?.Select(til => til.TestItem);
                if(finalTestItem != null)
                {
                    testItems.AddRange(finalTestItem);
                }
            }

            testItems = testItems.Distinct().ToList();

            foreach (var testItem in testItems)
            {
                TestItemDetails.Add(new PretestAndFinalTestComparison_TestItemDetails(testItem, filteredClassScheduleRosters, preTestId, finalTestId));
            }
        }
    }

    public class PretestAndFinalTestComparison_EmployeeDetails
    {
        public int _EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string CertificateNumber { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public int? PretestCompletionStatus { get; set; }
        public int? PretestScore { get; set; }
        public int? FinalTestCompletionStatus { get; set; }
        public int? FinalTestScore { get; set; }
        public bool Active { get; set; }
        public PretestAndFinalTestComparison_EmployeeDetails(ClassSchedule_Employee classScheduleEmployee, List<ClassSchedule_Roster> classScheduleRosters, int? preTestId, int? finalTestId)
        {
            _EmployeeId = classScheduleEmployee.EmployeeId;
            EmployeeFirstName = classScheduleEmployee?.Employee?.Person?.FirstName;
            EmployeeLastName = classScheduleEmployee?.Employee?.Person?.LastName;
            CertificateNumber = classScheduleEmployee?.Employee?.EmployeeCertifications.Where(ec => ec.Active).FirstOrDefault(ec => ec.Certification.CertifyingBody.Name == "NERC")?.CertificationNumber;
            Organization = string.Join(", ", classScheduleEmployee.Employee.EmployeeOrganizations.Where(eo => eo.Active).Select(eo => eo.Organization.Name));
            Position = string.Join(", ", classScheduleEmployee.Employee.EmployeePositions.Where(ep => ep.Active).Select(ep => ep.Position.PositionAbbreviation));
            Active = classScheduleEmployee.Employee.Active;
            if (preTestId != null)
            {
                var preTestRoster = classScheduleRosters.FirstOrDefault(csr => csr.EmpId == classScheduleEmployee.EmployeeId && csr.TestId == preTestId);
                PretestCompletionStatus = preTestRoster?.StatusId;
                PretestScore = preTestRoster?.Score;
            }
            if (finalTestId != null)
            {
                var finalTestRoster = classScheduleRosters.FirstOrDefault(csr => csr.EmpId == classScheduleEmployee.EmployeeId && csr.TestId == finalTestId);
                FinalTestCompletionStatus = finalTestRoster?.StatusId;
                FinalTestScore = finalTestRoster?.Score;
            }
        }
    }

    public class PretestAndFinalTestComparison_TestItemDetails
    {
        public string ItemNumber { get; set; }
        public string ItemDescription { get; set; }
        public decimal? PercentCorrectPretest { get; set; }
        public decimal? PercentCorrectFinalTest { get; set; }
        public int TestItemTypeId { get; set; }
        public PretestAndFinalTestComparison_TestItemDetails(TestItem testItem, List<ClassSchedule_Roster> classScheduleRosters, int? preTestId, int? finalTestId)
        {
            ItemNumber = testItem.Number;
            ItemDescription = testItem.Description;
            TestItemTypeId = testItem.TestItemTypeId;

            if (preTestId != null) {
                var preTestResponses = classScheduleRosters.Where(csr => csr.TestId == preTestId).SelectMany(csr => csr.Responses).Where(r => r.TestItemId == testItem.Id).ToList();
                if (preTestResponses.Count() > 0)
                {
                    PercentCorrectPretest = Math.Round((decimal)preTestResponses.Count(r => r.IsCorrect ?? false) / preTestResponses.Count() * 100,2);
                }
            }
            if (finalTestId != null)
            {
                var finalTestResponses = classScheduleRosters.Where(csr => csr.TestId == finalTestId).SelectMany(csr => csr.Responses).Where(r => r.TestItemId == testItem.Id).ToList();
                if (finalTestResponses.Count() > 0)
                {
                    PercentCorrectFinalTest = Math.Round((decimal)finalTestResponses.Count(r => r.IsCorrect ?? false) / finalTestResponses.Count()*100,2);
                }
            }
        }
    }
}
