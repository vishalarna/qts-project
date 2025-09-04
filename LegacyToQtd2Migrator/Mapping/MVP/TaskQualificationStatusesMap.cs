using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class TaskQualificationStatusesMap  : Common.MigrationMap<TblOjthistory, TaskQualificationStatus>
    {
        List<TblOjthistory> _ojthistories;
        public TaskQualificationStatusesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblOjthistory> getSourceRecords()
        {
            _ojthistories = (_source as EMP_DemoContext).TblOjthistories.ToListAsync().Result;
            return _ojthistories;
        }

        protected override TaskQualificationStatus mapRecord(TblOjthistory obj)
        {
            return new TaskQualificationStatus()
            {
                //Name
                Description=obj.Tdesc,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Deleted = false,
                Active = true,
            };
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _ojthistories.Count();
        }
        protected override void updateTarget(TaskQualificationStatus record)
        {
            (_target as QTD2.Data.QTDContext).TaskQualificationStatuses.Add(record);
        }
    }
}
