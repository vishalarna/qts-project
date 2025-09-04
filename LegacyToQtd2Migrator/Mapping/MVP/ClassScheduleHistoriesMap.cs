using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
  public  class ClassScheduleHistoriesMap : Common.MigrationMap<TblClass, ClassScheduleHistory>
    {

        List<TblClass> _class;
        public ClassScheduleHistoriesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblClass> getSourceRecords()
        {
            _class = (_source as EMP_DemoContext).TblClasses.ToListAsync().Result;
            return _class;
        }

        protected override ClassScheduleHistory mapRecord(TblClass obj)
        {
            return new ClassScheduleHistory()
            {
                //ChangeNotes
                //ChangeEffectiveDate
                //CreatedBy
                //Createddate
                //ModifiedBy,
                //ModifiedDate
                //ClassScheduleID
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _class.Count();
        }

        protected override void updateTarget(ClassScheduleHistory record)
        {
            (_target as QTD2.Data.QTDContext).ClassScheduleHistories.Add(record);
        }
    }
}
