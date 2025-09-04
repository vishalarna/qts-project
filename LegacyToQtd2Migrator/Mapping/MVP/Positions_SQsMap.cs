using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class Positions_SQsMap : Common.MigrationMap<TblDutyArea, Positions_SQ>
    {
        List<TblDutyArea> _dutyArea;
        public Positions_SQsMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblDutyArea> getSourceRecords()
        {
            _dutyArea = (_source as EMP_DemoContext).TblDutyAreas.ToListAsync().Result;
            return _dutyArea;
        }

        protected override Positions_SQ mapRecord(TblDutyArea obj)
        {
            return new Positions_SQ()
            {
                //PositionId
                //EOId
                Active=true,
                Deleted=false,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _dutyArea.Count();
        }

        protected override void updateTarget(Positions_SQ record)
        {
            (_target as QTD2.Data.QTDContext).Positions_SQs.Add(record);
        }

    }
}
