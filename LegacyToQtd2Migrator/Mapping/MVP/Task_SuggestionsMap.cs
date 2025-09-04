using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Task_SuggestionsMap : Common.MigrationMap<TblTasksIntroduction, Task_Suggestion>
    {
        List<TblTasksIntroduction> _tbltaskIntroduction;

        public Task_SuggestionsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTasksIntroduction> getSourceRecords()
        {
            _tbltaskIntroduction = (_source as EMP_DemoContext).TblTasksIntroductions.ToListAsync().Result;
            return _tbltaskIntroduction;
        }

        protected override Task_Suggestion mapRecord(TblTasksIntroduction obj)
        {
            return new Task_Suggestion()
            {
                Active = true,
                TaskId = obj.Tid,
                Description=obj.Description,
                //Number
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tbltaskIntroduction.Count();
        }

        protected override void updateTarget(Task_Suggestion record)
        {
            (_target as QTD2.Data.QTDContext).Task_Suggestions.Add(record);
        }
    }
}
