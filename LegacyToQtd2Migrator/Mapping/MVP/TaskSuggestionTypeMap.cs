using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TaskSuggestionTypeMap : Common.MigrationMap<TblTaskIntroductionType, Task_SuggestionTypes>
    {
        List<TblTaskIntroductionType> _taskIntroductionsType;
        public TaskSuggestionTypeMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblTaskIntroductionType> getSourceRecords()
        {
            _taskIntroductionsType = (_source as EMP_DemoContext).TblTaskIntroductionTypes.ToList();
            return _taskIntroductionsType;
        }

        protected override Task_SuggestionTypes mapRecord(TblTaskIntroductionType obj)
        {

            return new Task_SuggestionTypes()
            {
                Name = obj.IntroTypeText,
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _taskIntroductionsType.Count();
        }

        protected override void updateTarget(Task_SuggestionTypes record)
        {
            (_target as QTD2.Data.QTDContext).Task_SuggestionTypes.Add(record);
        }
    }
}
