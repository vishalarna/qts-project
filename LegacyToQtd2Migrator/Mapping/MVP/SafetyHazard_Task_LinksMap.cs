using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class SafetyHazard_Task_LinksMap : Common.MigrationMap<TblSafetyHazardTask, SafetyHazard_Task_Link>
    {
        List<TblSafetyHazardTask> _safetyHazardTaskLinks;
        public SafetyHazard_Task_LinksMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblSafetyHazardTask> getSourceRecords()
        {
            _safetyHazardTaskLinks = (_source as EMP_DemoContext).TblSafetyHazardTasks.ToListAsync().Result;
            return _safetyHazardTaskLinks;
        }

        protected override SafetyHazard_Task_Link mapRecord(TblSafetyHazardTask obj)
        {
            return new SafetyHazard_Task_Link()
            {
                Active = true,
                SaftyHazardId=obj.Shzid,
                TaskId=obj.Tid
                
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _safetyHazardTaskLinks.Count();
        }

        protected override void updateTarget(SafetyHazard_Task_Link record)
        {
            (_target as QTD2.Data.QTDContext).SafetyHazard_Task_Links.Add(record);
        }

    }
}
