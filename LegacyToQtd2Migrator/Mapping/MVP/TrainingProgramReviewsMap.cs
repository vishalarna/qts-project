using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TrainingProgramReviewsMap : Common.MigrationMap<TblTrainingIssueAnnualReview, TrainingProgramReview>
    {
        List<TblPositionTrainingProgram> _positionTrainingPrograms;
        List<TblTrainingIssueAnnualReview> _tblTrainingIssueAnnualReview;
        List<TblPosition> _tblPositions;

        List<TrainingProgram> _trainingPrograms;
        List<Position> _positions;
        List<Employee> _employees;

        public TrainingProgramReviewsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTrainingIssueAnnualReview> getSourceRecords()
        {
            _tblTrainingIssueAnnualReview = (_source as EMP_DemoContext).TblTrainingIssueAnnualReviews.ToList();
            _tblPositions = (_source as EMP_DemoContext).TblPositions.ToList();
            _positionTrainingPrograms = (_source as EMP_DemoContext).TblPositionTrainingPrograms.ToList();

            _trainingPrograms = (_target as QTD2.Data.QTDContext).TrainingPrograms.ToList();
            _positions = (_target as QTD2.Data.QTDContext).Positions.ToList();
            _employees = (_target as QTD2.Data.QTDContext).Employees.ToList();

            return _tblTrainingIssueAnnualReview;
        }

        protected override TrainingProgramReview mapRecord(TblTrainingIssueAnnualReview obj)
        {
            var sourcePosition = _tblPositions.Where(r => r.Pid == obj.PositionId).FirstOrDefault();

            if (sourcePosition == null) return null;

            var targetPosition = _positions.Where(r => r.PositionAbbreviation == sourcePosition.Pabbrev).First();

            string revision = "";
            if (obj.InitVersion.HasValue)
            {
                var trainingProgram = _positionTrainingPrograms.Where(r => r.Ptpid == obj.InitVersion.Value).First();
                revision = trainingProgram.Revision.ToString();
            }

            var targetTrainingProgram = _trainingPrograms
                                        .Where(r => r.PositionId == targetPosition.Id)
                                        .Where(r => r.TrainingProgramTypeId == obj.ProgramType)
                                        .Where(r => r.Year.Year == obj.TypeYear || (obj.ProgramType == 1 && r.TrainingProgramTypeId == 1 && r.TPVersionNo == revision)).FirstOrDefault();

            if (targetTrainingProgram == null)
            {
                return new TrainingProgramReview()
                {
                    Active = true,
                    Published = true,
                    TrainingProgram = new TrainingProgram()
                    {
                        Active = true,
                        EndDate = new DateTime(obj.TypeYear == null ? 2000 : obj.TypeYear.Value, 12, 31).ToQtsTime(false),
                        PositionId = targetPosition.Id,
                        TrainingProgramTypeId = obj.ProgramType,
                        StartDate = new DateTime(obj.TypeYear == null ? 2000 : obj.TypeYear.Value, 1, 1).ToQtsTime(false),
                    },
                    Conclusion = obj.Conclusions,
                    EndDate = obj.EvalEndDate.ToQtsTime(false),
                    EvaluationOfTraineeLearning = obj.EvalTraineeLrn,
                    HistoricalBackground = obj.Background,
                    Method = obj.Method,
                    ProgramDesign = obj.Design,
                    ProgramImplementation = obj.Implementation,
                    ProgramMaterials = obj.Materials,
                    Purpose = obj.Purpose,
                    ReviewDate = obj.ReviewDate.ToQtsTime(false),
                    Reviewers = getReviewers(obj),
                    StartDate = obj.EvalStartDate.ToQtsTime(false),
                    Summary = obj.Summary,
                    Title = obj.SignatureTitle,
                    TrainerName = obj.SignatureName,
                    TrainerSignOff = !String.IsNullOrEmpty(obj.SignatureName),
                    StudentEvaluationResults = obj.Evaluation,
                    TrainingProgramReview_SupportingDocuments = getSupportingDocuments(obj)
                };
            }
            else
            {
                return new TrainingProgramReview()
                {
                    Active = true,
                    Published = true,
                    TrainingProgramId = targetTrainingProgram.Id,
                    Conclusion = obj.Conclusions,
                    EndDate = obj.EvalEndDate.ToQtsTime(false),
                    EvaluationOfTraineeLearning = obj.EvalTraineeLrn,
                    HistoricalBackground = obj.Background,
                    Method = obj.Method,
                    ProgramDesign = obj.Design,
                    ProgramImplementation = obj.Implementation,
                    ProgramMaterials = obj.Materials,
                    Purpose = obj.Purpose,
                    ReviewDate = obj.ReviewDate.ToQtsTime(false),
                    Reviewers = getReviewers(obj),
                    StartDate = obj.EvalStartDate.ToQtsTime(false),
                    Summary = obj.Summary,
                    Title = obj.SignatureTitle,
                    TrainerName = obj.SignatureName,
                    TrainerSignOff = !String.IsNullOrEmpty(obj.SignatureName),
                    StudentEvaluationResults = obj.Evaluation,
                    TrainingProgramReview_SupportingDocuments = getSupportingDocuments(obj)
                };
            }

        }

        private List<TrainingProgramReview_SupportingDocument> getSupportingDocuments(TblTrainingIssueAnnualReview obj)
        {
            List<TrainingProgramReview_SupportingDocument> docs = new List<TrainingProgramReview_SupportingDocument>();

            foreach (var supportingDoc in obj.TblTrainingIssueSupportingDocs)
            {
                docs.Add(new TrainingProgramReview_SupportingDocument()
                {
                    Active = true,
                    Deleted = false,
                    Hyperlink = supportingDoc.Hyperlink,
                    Name = supportingDoc.SupportingDocs,
                    Number = supportingDoc.Arsdnum.GetValueOrDefault()
                });
            }

            return docs;
        }

        private ICollection<TrainingProgramReview_Employee_Link> getReviewers(TblTrainingIssueAnnualReview obj)
        {
            List<TrainingProgramReview_Employee_Link> employeeLinks = new List<TrainingProgramReview_Employee_Link>();

            var reviewers = String.IsNullOrEmpty(obj.EvaluatorNames) ? new string[] { } : obj.EvaluatorNames.Replace(" and ", "&").Split(new Char[] { ',', ';', '&' });

            foreach (var reviewer in reviewers)
            {
                var clean = reviewer.Split('-')[0].Trim();
                clean = System.Text.RegularExpressions.Regex.Replace(clean, @"\s+", " ");
                clean = System.Text.RegularExpressions.Regex.Replace(clean, @"[^A-Za-z ]", "");
                var parts = clean.Split(' ');

                Employee targetEmployee;

                if (parts.Length == 1)
                {
                    targetEmployee = _employees.Where(r => r.Person.LastName.ToUpper() == parts[0].ToUpper()).FirstOrDefault();
                }
                else
                {
                    targetEmployee = _employees.Where(r => r.Person.FirstName.ToUpper() == parts[0].ToUpper() && r.Person.LastName.ToUpper() == parts[1].ToUpper()).FirstOrDefault();
                }

                if (targetEmployee == null)
                {
                    targetEmployee = new Employee()
                    {
                        Active = false,
                        Deleted = false,
                        TQEqulator = true,
                        EmployeeNumber = Guid.NewGuid().ToString(),
                        Notes = "Added in migration from TrainingProgramReview",
                        Person = new Person()
                        {
                            Active = false,
                            Deleted = false,
                            Username = Guid.NewGuid().ToString(),
                            FirstName = parts[0],
                            LastName = parts.Length > 1 ? parts[1] : "Last Name Unk"
                        }
                    };

                    employeeLinks.Add(new TrainingProgramReview_Employee_Link()
                    {
                        Active = true,
                        Deleted = false,
                        Employee = targetEmployee
                    });

                    _employees.Add(targetEmployee);
                }
                else
                {
                    employeeLinks.Add(new TrainingProgramReview_Employee_Link()
                    {
                        Active = true,
                        Deleted = false,
                        Employee= targetEmployee
                    });
                }

            }

            return employeeLinks;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblTrainingIssueAnnualReview.Count();
        }

        protected override void updateTarget(TrainingProgramReview record)
        {
            if (record != null)
                (_target as QTD2.Data.QTDContext).TrainingProgramReviews.Add(record);
        }
    }
}
