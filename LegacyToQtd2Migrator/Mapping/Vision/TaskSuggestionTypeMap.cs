using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class SourceSuggestionType
    {
        public string Name { get; set; }
    }

    public class TaskSuggestionTypeMap : Common.MigrationMap<SourceSuggestionType, Task_SuggestionTypes>
    {
        List<SourceSuggestionType> _sourceSuggestionTypes;
        public TaskSuggestionTypeMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<SourceSuggestionType> getSourceRecords()
        {
            _sourceSuggestionTypes = new List<SourceSuggestionType>()
            {
                new SourceSuggestionType()
                {
                    Name = "UNK Suggestion Type"
                }
            };

            return _sourceSuggestionTypes;
        }

        protected override Task_SuggestionTypes mapRecord(SourceSuggestionType obj)
        {

            return new Task_SuggestionTypes()
            {
                Name = obj.Name,
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _sourceSuggestionTypes.Count();
        }

        protected override void updateTarget(Task_SuggestionTypes record)
        {
            (_target as QTD2.Data.QTDContext).Task_SuggestionTypes.Add(record);
        }
    }
}
