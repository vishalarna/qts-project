using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingProgramReview : Common.Entity
    {
        public int TrainingProgramId { get; set; }
        public virtual TrainingProgram TrainingProgram { get; set; }
        public ICollection<TrainingProgramReview_Employee_Link> Reviewers { get; set; } = new List<TrainingProgramReview_Employee_Link>();
        public DateTime? ReviewDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Purpose { get; set; }
        public string? Method { get; set; }
        public string? HistoricalBackground { get; set; }
        public string? ProgramDesign { get; set; }
        public string? ProgramMaterials { get; set; }
        public string? ProgramImplementation { get; set; }
        public string? EvaluationOfTraineeLearning { get; set; }
        public string? StudentEvaluationResults { get; set; }
        public string? Conclusion { get; set; }
        public string? Summary { get; set; }
        public string? TrainerName { get; set; }
        public string? Title { get; set; }
        public bool? TrainerSignOff { get; set; }
        public bool Published { get; set; } = false;

        public virtual List<TrainingProgramReview_SupportingDocument> TrainingProgramReview_SupportingDocuments { get; set; }

        public TrainingProgramReview()
        {

        }

        public TrainingProgramReview(int trainingProgramId,DateTime? reviewDate,DateTime? startDate,DateTime? endDate,string? purpose,string? method,string? historicalBackground,string? programDesign,string? programMaterials,string? programImplementation,string? evaluationOfTraineeLearning,string? studentEvaluationResults,string? conclusion,string? summary,string? trainerName,string? title,bool? trainerSignOff,bool published)
        {
            TrainingProgramId = trainingProgramId;
            ReviewDate = reviewDate;
            StartDate = startDate;
            EndDate = endDate;
            Purpose = purpose;
            Method = method;
            HistoricalBackground = historicalBackground;
            ProgramDesign = programDesign;
            ProgramMaterials = programMaterials;
            ProgramImplementation = programImplementation;
            EvaluationOfTraineeLearning = evaluationOfTraineeLearning;
            StudentEvaluationResults = studentEvaluationResults;
            Conclusion = conclusion;
            Summary = summary;
            TrainerName = trainerName;
            Title = title;
            TrainerSignOff = trainerSignOff;
            Published = published;
        }

        public void SetTrainingProgramId(int trainingProgramId)
        {
            TrainingProgramId = trainingProgramId;
        }

        public void SetReviewDate(DateTime? reviewDate)
        {
            ReviewDate = reviewDate;
        }

        public void SetStartDate(DateTime? startDate)
        {
            StartDate = startDate;
        }

        public void SetEndDate(DateTime? endDate)
        {
            EndDate = endDate;
        }

        public void SetPurpose(string? purpose)
        {
            Purpose = purpose;
        }

        public void SetMethod(string? method)
        {
            Method = method;
        }

        public void SetHistoricalBackground(string? historicalBackground)
        {
            HistoricalBackground = historicalBackground;
        }

        public void SetProgramDesign(string? programDesign)
        {
            ProgramDesign = programDesign;
        }

        public void SetProgramMaterials(string? programMaterials)
        {
            ProgramMaterials = programMaterials;
        }

        public void SetProgramImplementation(string? programImplementation)
        {
            ProgramImplementation = programImplementation;
        }

        public void SetEvaluationOfTraineeLearning(string? evaluationOfTraineeLearning)
        {
            EvaluationOfTraineeLearning = evaluationOfTraineeLearning;
        }

        public void SetStudentEvaluationResults(string? studentEvaluationResults)
        {
            StudentEvaluationResults = studentEvaluationResults;
        }

        public void SetConclusion(string? conclusion)
        {
            Conclusion = conclusion;
        }

        public void SetSummary(string? summary)
        {
            Summary = summary;
        }

        public void SetTrainerName(string? trainerName)
        {
            TrainerName = trainerName;
        }

        public void SetTitle(string? title)
        {
            Title = title;
        }

        public void SetTrainerSignOff(bool? trainerSignOff)
        {
            TrainerSignOff = trainerSignOff;
        }

        public void SetPublished(bool published)
        {
            Published = published;
        }

        public void UpdateEmployeeLinks(List<int> employeeIds)
        {
            List<TrainingProgramReview_Employee_Link> oldEmployeesLinked = Reviewers.Where(r => !employeeIds.Contains(r.EmployeeId)).ToList();

            foreach (TrainingProgramReview_Employee_Link oldEmployeeLinked in oldEmployeesLinked)
            {
                oldEmployeeLinked.Delete();
            }

            List<int> existingEmployeesLinkedIds = Reviewers.Where(r => !r.Deleted).Select(r => r.EmployeeId).ToList();
            List<int> newEmployeesLinkedIds = employeeIds.Where(t => !existingEmployeesLinkedIds.Contains(t)).ToList();

            foreach (int newEmployeeLinkedId in newEmployeesLinkedIds)
            {
                Reviewers.Add(new TrainingProgramReview_Employee_Link(Id, newEmployeeLinkedId));
            }
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnTrainingProgramReview_Deleted(this));
        }
    }
}
