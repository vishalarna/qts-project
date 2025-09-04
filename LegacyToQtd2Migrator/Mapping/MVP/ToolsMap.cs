using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class ToolsMap : Common.MigrationMap<TblTaskToolList, Tool>
    {
        List<TblTaskToolList> _tools;

        public ToolsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTaskToolList> getSourceRecords()
        {
            _tools = (_source as EMP_DemoContext).TblTaskToolLists.ToListAsync().Result;
            return _tools;
        }

        protected override Tool mapRecord(TblTaskToolList obj)
        {
            return new Tool()
            {
                Active = true,
                Deleted = false,
                ToolCategoryId = 2,
                Number = obj.Ttid.ToString(),
                Name = obj.TaskTool,
                //Hyperlink,
                //EffectiveDate,
                Description = obj.TaskTool,
                //Upload
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tools.Count();
        }

        protected override void updateTarget(Tool record)
        {
            (_target as QTD2.Data.QTDContext).Tools.Add(record);
        }
    }
}
