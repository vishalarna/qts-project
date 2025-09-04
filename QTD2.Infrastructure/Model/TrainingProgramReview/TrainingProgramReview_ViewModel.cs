using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Model.TrainingProgramReview
{
    public class TrainingProgramReview_ViewModel
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int TrainingProgramTypeId { get; set; }
        public string TrainingProgramType { get; set; }
        public int TrainingProgramId { get; set; }
        public string TrainingProgram_ProgramTitle { get; set; }
        public string TrainingProgram_Version { get; set; }
        public List<TrainingProgramReview_Employee_Link_ViewModel> Reviewers { get; set; }
        public DateTime ReviewDate { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public string Purpose { get; set; }
        public string Method { get; set; }
        public string HistoricalBackground { get; set; }
        public string ProgramDesign { get; set; }
        public string ProgramMaterials { get; set; }
        public string ProgramImplementation { get; set; }
        public string EvaluationOfTraineeLearning { get; set; }
        public string StudentEvaluationResults { get; set; }
        public string Conclusion { get; set; }
        public string Summary { get; set; }
        public string TrainerName { get; set; }
        public string Title { get; set; }
        public bool TrainerSignOff { get; set; }
        public bool Published { get; set; }

    }
}
