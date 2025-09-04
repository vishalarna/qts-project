using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class LabelReplacementMap : Common.MigrationMap<TblLabelReplacementText, ClientSettings_LabelReplacement>
    {
        List<TblLabelReplacementText> _labelReplacements;

        Dictionary<string, string> legacyTo2DefaultTexts = new Dictionary<string, string>()
        {
            // new KeyValuePair( "Employee"),
            { "Positions", "Position" },
            { "Task", "Task"},
            // new KeyValuePair( "Enabling Objectives"),
            // new KeyValuePair( "Certifications"),
            // new KeyValuePair( "Procedures"),
            { "Safety Hazard", "Safety Hazards" },
            // new KeyValuePair( "Tools"),
            // new KeyValuePair( "Regulatory Requirements"),
            // new KeyValuePair( "Definitions"),
            // new KeyValuePair( "Instructions"),
            // new KeyValuePair( "Locations"),
        };



        public LabelReplacementMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblLabelReplacementText> getSourceRecords()
        {
            _labelReplacements = (_source as EMP_DemoContext).TblLabelReplacementTexts.ToList();
            return _labelReplacements;
        }

        protected override ClientSettings_LabelReplacement mapRecord(TblLabelReplacementText obj)
        {
            string label;

            bool found = legacyTo2DefaultTexts.TryGetValue(obj.DefaultText, out label);
            if (!found) return null;

            var existing = (_target as QTD2.Data.QTDContext).ClientSettings_LabelReplacements.Where(r => r.DefaultLabel == label).FirstOrDefault();
            if (existing == null) return null;

            existing.LabelReplacement = obj.ReplacementText;

            return existing;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _labelReplacements.Count();
        }

        protected override void updateTarget(ClientSettings_LabelReplacement record)
        {
            if (record == null) return;

            (_target as QTD2.Data.QTDContext).ClientSettings_LabelReplacements.Update(record);
        }
    }
}
