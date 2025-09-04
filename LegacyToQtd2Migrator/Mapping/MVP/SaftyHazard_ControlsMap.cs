using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
 public   class SaftyHazard_ControlsMap : Common.MigrationMap<TblSafetyHazardControl, SaftyHazard_Control>
    {
        List<TblSafetyHazardControl> _saftyhazardcontrol;
        public SaftyHazard_ControlsMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblSafetyHazardControl> getSourceRecords()
        {
            _saftyhazardcontrol = (_source as EMP_DemoContext).TblSafetyHazardControls.ToListAsync().Result;
            return _saftyhazardcontrol;
        }

        protected override SaftyHazard_Control mapRecord(TblSafetyHazardControl obj)
        {
            return new SaftyHazard_Control()
            {
                Active = true,
                Deleted = false,
                SaftyHazardId = obj.Shzid,
                Number=obj.Cnum,
                Description=obj.Shzcontrol
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _saftyhazardcontrol.Count();
        }

        protected override void updateTarget(SaftyHazard_Control record)
        {
            (_target as QTD2.Data.QTDContext).SaftyHazard_Controls.Add(record);
        }
    }
}
