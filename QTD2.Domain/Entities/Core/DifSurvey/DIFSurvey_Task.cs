using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DIFSurvey_Task : Common.Entity
    {
        public int DifSurveyId { get; set; }
        public int TaskId { get; set; }
        public float? AverageDifficulty { get; set; }
        public float? AverageImportance { get; set; }
        public float? AverageFrequency { get; set; }
        public int TrainingStatus_CalculatedId { get; set; }
        public int? TrainingStatus_OverrideId { get; set; }
        public string? Comments { get; set; }
        public int? CommentingEmployeeId { get; set; }
        public int? DIFSurvey_Task_TrainingFrequencyId { get; set; }
        public virtual DIFSurvey DifSurvey { get; set; }
        public virtual Task Task { get; set; }
        public virtual Employee CommentingEmployee { get; set; }
        public virtual ICollection<DIFSurvey_Employee_Response> Responses { get; set; } = new List<DIFSurvey_Employee_Response>();
        public virtual DIFSurvey_Task_Status TrainingStatus_Calculated { get; set; }
        public virtual DIFSurvey_Task_Status TrainingStatus_Override { get; set; }
        public virtual DIFSurvey_Task_TrainingFrequency DIFSurvey_Task_TrainingFrequency { get; set; }

        public DIFSurvey_Task()
        {
        }

        public DIFSurvey_Task(int difSurveyId, int taskId, float? averageDifficulty, float? averageImportance, float? averageFrequency)
        {
            DifSurveyId = difSurveyId;
            TaskId = taskId;
            AverageDifficulty = averageDifficulty;
            AverageImportance = averageImportance;
            AverageFrequency = averageFrequency;
            Update();
        }

        public void Update()
        {
            if (Responses != null && Responses.Any())
            {
                var validResponses = Responses.Where(response => !response.NA && (response.DIFSurvey_Employee?.Complete ?? false));
                if (validResponses.Any())
                {
                    AverageDifficulty = validResponses.Average(response => response.Difficulty);
                    AverageImportance = validResponses.Average(response => response.Importance);
                    AverageFrequency = validResponses.Average(response => response.Frequency);
                }
                else
                {
                    AverageDifficulty = null;
                    AverageImportance = null;
                    AverageFrequency = null;
                }
            }

            if (AverageDifficulty.HasValue && AverageImportance.HasValue && AverageFrequency.HasValue)
            {
                switch (AverageDifficulty)
                {
                    case >= 3.5f:
                        switch (AverageImportance)
                        {
                            case >= 2.5f:
                                switch (AverageFrequency)
                                {
                                    case >= 3.5f:
                                        TrainingStatus_CalculatedId = 1;
                                        break;
                                    case >= 2.5f and < 3.5f:
                                        TrainingStatus_CalculatedId = 2;
                                        break;
                                    case < 2.5f:
                                        TrainingStatus_CalculatedId = 2;
                                        break;
                                }
                                break;
                            case < 2.5f:
                                switch (AverageFrequency)
                                {
                                    case >= 3.5f:
                                        TrainingStatus_CalculatedId = 3;
                                        break;
                                    case >= 2.5f and < 3.5f:
                                        TrainingStatus_CalculatedId = 1;
                                        break;
                                    case < 2.5f:
                                        TrainingStatus_CalculatedId = 1;
                                        break;
                                }
                                break;
                        }
                        break;

                    case > 2.5f and < 3.5f:
                        switch (AverageImportance)
                        {
                            case >= 2.5f:
                                switch (AverageFrequency)
                                {
                                    case >= 3.5f:
                                        TrainingStatus_CalculatedId = 1;
                                        break;
                                    case >= 2.5f and < 3.5f:
                                        TrainingStatus_CalculatedId = 2;
                                        break;
                                    case < 2.5f:
                                        TrainingStatus_CalculatedId = 2;
                                        break;
                                }
                                break;
                            case < 2.5f:
                                switch (AverageFrequency)
                                {
                                    case >= 3.5f:
                                        TrainingStatus_CalculatedId = 3;
                                        break;
                                    case >= 2.5f and < 3.5f:
                                        TrainingStatus_CalculatedId = 3;
                                        break;
                                    case < 2.5f:
                                        TrainingStatus_CalculatedId = 1;
                                        break;
                                }
                                break;
                        }
                        break;
                    case <= 2.5f:
                        switch (AverageImportance)
                        {
                            case >= 2.5f:
                                switch (AverageFrequency)
                                {
                                    case >= 3.5f:
                                        TrainingStatus_CalculatedId = 3;
                                        break;
                                    case >= 2.5f and < 3.5f:
                                        TrainingStatus_CalculatedId = 3;
                                        break;
                                    case < 2.5f:
                                        TrainingStatus_CalculatedId = 1;
                                        break;
                                }
                                break;
                            case < 2.5f:
                                switch (AverageFrequency)
                                {
                                    case >= 3.5f:
                                        TrainingStatus_CalculatedId = 3;
                                        break;
                                    case >= 2.5f and < 3.5f:
                                        TrainingStatus_CalculatedId = 3;
                                        break;
                                    case < 2.5f:
                                        TrainingStatus_CalculatedId = 3;
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }
            else
            {
                TrainingStatus_CalculatedId = 4;
            }
        }
    }
}
