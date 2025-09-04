using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
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
    public class EnablingObjectivesByPositionMatrixModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<EnablingObjectivesByPositionMatrix_Position> Positions { get; set; } = new List<EnablingObjectivesByPositionMatrix_Position>();
        public List<EnablingObjectivesByPositionMatrix_EnablingObjective> EnablingObjectives { get; set; } = new List<EnablingObjectivesByPositionMatrix_EnablingObjective>();
        public EnablingObjectivesByPositionMatrixModel(
            string title,
            string templatePath,
            List<string> displayColumns,
            string companyLogo,
            List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements,
            string defaultTimeZone,
            List<QTD2.Domain.Entities.Core.Position> positions,
            List<EnablingObjective> enablingObjectives)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            foreach (var position in positions)
            {
                Positions.Add(new EnablingObjectivesByPositionMatrix_Position(position));
            }
            foreach (var enablingObjective in enablingObjectives)
            {
                EnablingObjectives.Add(new EnablingObjectivesByPositionMatrix_EnablingObjective(enablingObjective, positions));
            }
        }
    }

    public class EnablingObjectivesByPositionMatrix_EnablingObjective
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool isMetaEO { get; set; }
        public bool IsSkillQualification { get; set; }
        public List<int> PositionIds { get; set; } = new List<int>();

        public EnablingObjectivesByPositionMatrix_EnablingObjective(EnablingObjective enablingObjective, List<QTD2.Domain.Entities.Core.Position> positions)
        {
            Number = enablingObjective.FullNumber;
            Description = enablingObjective.Description;
            Active = enablingObjective.Active;
            isMetaEO = enablingObjective.isMetaEO;
            IsSkillQualification = enablingObjective.IsSkillQualification;
            foreach (var position in positions)
            {
                if (position.Position_SQs.Any(pt => pt.EOId == enablingObjective.Id))
                {
                    PositionIds.Add(position.Id);
                }

                if (position.Position_Tasks != null)
                {
                    foreach (var pt in position.Position_Tasks.Where(m => m.Task != null).SelectMany(m => m.Task.Task_EnablingObjective_Links ?? Enumerable.Empty<Task_EnablingObjective_Link>()))
                    {
                        if (pt.Task.Task_EnablingObjective_Links.Any(m => m.EnablingObjectiveId == enablingObjective.Id))
                        {
                            PositionIds.Add(position.Id);
                        }
                        if (pt.Task.Task_EnablingObjective_Links.Any(o => o.EnablingObjective != null && o.EnablingObjective.EnablingObjective_MetaEO_Links.Any(r => r.EOID == enablingObjective.Id)))
                        {
                            PositionIds.Add(position.Id);
                        }
                    }
                }
            }
        }

        public bool HasPosition(int positionId)
        {
            return PositionIds.Contains(positionId);
        }

        public bool IsLinkedWithMultiplePositions()
        {
            return PositionIds.Distinct().Count() > 1;
        }
    }

    public class EnablingObjectivesByPositionMatrix_Position
    {
        public int Id { get; set; }
        public string PositionAbbreviation { get; set; }

        public EnablingObjectivesByPositionMatrix_Position(QTD2.Domain.Entities.Core.Position position)
        {
            Id = position.Id;
            PositionAbbreviation = position.PositionAbbreviation;
        }
    }
}
