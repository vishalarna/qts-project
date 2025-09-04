using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class TasksbyPositionMatrixModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public object Data { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public string DefaultDateFormat { get; set; }
        public List<TasksByPositionMatrix_Position> Positions { get; set; } = new List<TasksByPositionMatrix_Position>();
        public List<TasksByPositionMatrix_Task> MatrixTasks { get; set; } = new List<TasksByPositionMatrix_Task>();
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public TasksbyPositionMatrixModel(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Position> positions, List<Task> tasks, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone, string defaultDateFormat)
        {
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            DisplayColumns = displayColumns;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            DefaultDateFormat = defaultDateFormat;

            if (positions != null)
            {
                foreach (var position in positions)
                {
                    Positions.Add(new TasksByPositionMatrix_Position(position));
                }
            }

            if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    MatrixTasks.Add(new TasksByPositionMatrix_Task(task, positions));
                }
            }
        }
    }

    public class TasksByPositionMatrix_Task
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool IsMeta { get; set; }
        public bool IsReliability { get; set; }
        public List<int> PositionIds { get; set; } = new List<int>();

        public TasksByPositionMatrix_Task(Task task, List<Position> positions)
        {
            Number = task.FullNumber;
            Description = task.Description;
            Active = task.Active;
            IsMeta = task.IsMeta;
            IsReliability = task.IsReliability;
            if (positions != null)
            {
                foreach (var position in positions)
                {
                    if (position.Position_Tasks?.Any(pt => pt.Task?.Id == task.Id) == true)
                    {
                        PositionIds.Add(position.Id);
                    }
                }
            }
        }

        public bool HasPosition(int positionId)
        {
            return PositionIds.Contains(positionId);
        }
    }

    public class TasksByPositionMatrix_Position
    {
        public int Id { get; set; }
        public string PositionAbbreviation { get; set; }

        public TasksByPositionMatrix_Position(Position position)
        {
            Id = position.Id;
            PositionAbbreviation = position.PositionAbbreviation;
        }
    }
}
