using DocumentFormat.OpenXml.Drawing;
using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using QTD2.Infrastructure.Rustici.EngineApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class SafetyHazardsByPositionMatrixModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<SafetyHazardsByPositionMatrix_Position> Positions { get; set; } = new List<SafetyHazardsByPositionMatrix_Position>();
        public List<SafetyHazardsByPositionMatrix_SafetyHazard> SafetyHazards { get; set; } = new List<SafetyHazardsByPositionMatrix_SafetyHazard>();
        public SafetyHazardsByPositionMatrixModel(
            string title,
            string templatePath,
            List<string> displayColumns,
            string companyLogo,
            List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements,
            string defaultTimeZone,
            List<QTD2.Domain.Entities.Core.Position> positions,
            List<SaftyHazard> safetyHazards)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            foreach (var position in positions)
            {
                Positions.Add(new SafetyHazardsByPositionMatrix_Position(position));
            }
            foreach (var safetyHazard in safetyHazards)
            {
                SafetyHazards.Add(new SafetyHazardsByPositionMatrix_SafetyHazard(safetyHazard, positions));
            }
        }
    }

    public class SafetyHazardsByPositionMatrix_SafetyHazard
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public List<int> PositionIds { get; set; } = new List<int>();

        public SafetyHazardsByPositionMatrix_SafetyHazard(SaftyHazard safetyHazard, List<QTD2.Domain.Entities.Core.Position> positions)
        {
            Number = safetyHazard.Number;
            Title = safetyHazard.Title;
            Active = safetyHazard.Active;
            foreach (var position in positions)
            {
                if (position.Position_Tasks.Any(pt => pt.Task.SafetyHazard_Task_Links.Any(shtl => shtl.SaftyHazardId == safetyHazard.Id)))
                {
                    PositionIds.Add(position.Id);
                }
            }
        }

        public bool HasPosition(int positionId)
        {
            return PositionIds.Contains(positionId);
        }
    }

    public class SafetyHazardsByPositionMatrix_Position
    {
        public int Id { get; set; }
        public string PositionAbbreviation { get; set; }

        public SafetyHazardsByPositionMatrix_Position(QTD2.Domain.Entities.Core.Position position)
        {
            Id = position.Id;
            PositionAbbreviation = position.PositionAbbreviation;
        }
    }
}
