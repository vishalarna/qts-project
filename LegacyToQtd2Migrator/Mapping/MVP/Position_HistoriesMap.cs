using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Position_HistoriesMap : Common.MigrationMap<TblEmployeePositionHistory, Position_History>
    {
        List<TblEmployeePositionHistory> _employeepositionhistory;
        public Position_HistoriesMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblEmployeePositionHistory> getSourceRecords()
        {
            _employeepositionhistory = (_source as EMP_DemoContext).TblEmployeePositionHistories.ToListAsync().Result;
            return _employeepositionhistory;
        }

        protected override Position_History mapRecord(TblEmployeePositionHistory obj)
        {
            return new Position_History()
            {
                PositionId=obj.Pid,
                //ChangeEffectiveDate,
                Deleted=false,
                Active=true,
                //ChangeNotes
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _employeepositionhistory.Count();
        }

        protected override void updateTarget(Position_History record)
        {
            (_target as QTD2.Data.QTDContext).Position_Histories.Add(record);
        }

    }
}
