using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class EmployeeOrganizationsMap : Common.MigrationMap<TblEmployee, EmployeeOrganization>
    {
        List<TblEmployee> _employees;
        public EmployeeOrganizationsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblEmployee> getSourceRecords()
        {
            _employees = (_source as EMP_DemoContext).TblEmployees.ToListAsync().Result;
            return _employees;
        }

        protected override EmployeeOrganization mapRecord(TblEmployee obj)
        {
            return new EmployeeOrganization()
            {
               EmployeeId=obj.Eid,
               IsManager=false,
               Deleted =false,
               Active=true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _employees.Count();
        }

        protected override void updateTarget(EmployeeOrganization record)
        {
            (_target as QTD2.Data.QTDContext).EmployeeOrganizations.Add(record);
        }
    }
}
