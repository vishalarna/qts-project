using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class PositionsMap : Common.MigrationMap<TblPosition, Position>
    {
        List<TblPosition> _positions;
        List<TblDutyArea> _dutyArea;
        List<TblEmployeePositionHistory> _employeepositionhistory;

        public PositionsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblPosition> getSourceRecords()
        {
            _positions = (_source as EMP_DemoContext).TblPositions.ToListAsync().Result;
            _dutyArea = (_source as EMP_DemoContext).TblDutyAreas.ToListAsync().Result;
            _employeepositionhistory = (_source as EMP_DemoContext).TblEmployeePositionHistories.ToListAsync().Result;
            return _positions;
        }

        protected override Position mapRecord(TblPosition obj)
        {
            return new Position()
            {
                Active = true,
                PositionAbbreviation = obj.Pabbrev,
                PositionDescription = obj.Pdescription,
                PositionNumber = obj.Pnum ?? -1,
                PositionTitle = obj.Pdesc,
                Position_SQs = getPosition_SQs(),
                Position_Histories= getPosition_Histories(),
           };
        }
    private ICollection<Positions_SQ> getPosition_SQs()
    {
        List<Positions_SQ> positions_SQs = new List<Positions_SQ>();

        return positions_SQs;
    }

        private ICollection<Position_History> getPosition_Histories()
        {
            List<Position_History> histories = new List<Position_History>();

            return histories;
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _positions.Count();
        }

        protected override void updateTarget(Position record)
        {
            (_target as QTD2.Data.QTDContext).Positions.Add(record);
        }
    }
}
