using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class Task_ToolsFromTblTaskToolListMap : Common.MigrationMap<TblTaskToolList, Task_Tool>
    {
        List<TblTaskToolList> _taskToolLists;

        public Task_ToolsFromTblTaskToolListMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTaskToolList> getSourceRecords()
        {
            _taskToolLists = (_source as EMP_DemoContext).TblTaskToolLists.ToListAsync().Result;
            return _taskToolLists;
        }

        protected override Task_Tool mapRecord(TblTaskToolList obj)
        {
            return new Task_Tool()
            {
                //TaskId
                //ToolId
                Deleted=false,
                Active=true,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _taskToolLists.Count();
        }

        protected override void updateTarget(Task_Tool record)
        {
                (_target as QTD2.Data.QTDContext).Task_Tools.Add(record);
        }
    }
}
