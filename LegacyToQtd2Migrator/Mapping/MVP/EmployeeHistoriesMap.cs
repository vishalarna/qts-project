using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class EmployeeHistoriesMap : Common.MigrationMap<TblEmployee, EmployeeHistory>
    {
        List<TblEmployee> _employees;

        public EmployeeHistoriesMap(DbContext source, DbContext target) : base(source, target)
        { 

        }

        protected override List<TblEmployee> getSourceRecords()
        {
            _employees = (_source as EMP_DemoContext).TblEmployees.ToListAsync().Result;
            return _employees;
        }

        protected override EmployeeHistory mapRecord(TblEmployee obj)
        {
            return new EmployeeHistory()
            {
                EmployeeID = obj.Eid,
                //ChangeEffectiveDate,
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _employees.Count();
        }

        protected override void updateTarget(EmployeeHistory record)
        {
            (_target as QTD2.Data.QTDContext).EmployeeHistories.Add(record);
        }
    }
}
