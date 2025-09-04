namespace QTD2.Infrastructure.Model.TrainingProgramReview
{
    public class TrainingProgram_VersionTitleViewModel
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int TrainingProgramTypeId { get; set; }
        public string TrainingProgramType { get; set; }
        public string ProgramTitle { get; set; }
        public string Version { get; set; }

        public TrainingProgram_VersionTitleViewModel(int id, int positionId, string positionName, int trainingProgramTypeId, string trainingProgramType, string programTitle, string version)
        {
            Id = id;
            PositionId = positionId;
            PositionName = positionName;
            TrainingProgramTypeId = trainingProgramTypeId;
            TrainingProgramType = trainingProgramType;
            ProgramTitle = programTitle;
            Version = version;
        }
    }
}
