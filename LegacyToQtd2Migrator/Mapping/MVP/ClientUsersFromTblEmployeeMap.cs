using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class ClientUsersFromTblEmployeeMap : Common.MigrationMap<TblEmployee, ClientUser>
    {
        List<TblEmployee> _employee;
        public ClientUsersFromTblEmployeeMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblEmployee> getSourceRecords()
        {
            _employee = (_source as EMP_DemoContext).TblEmployees.ToListAsync().Result;
            return _employee;
        }

        protected override ClientUser mapRecord(TblEmployee obj)
        {
            return new ClientUser()
            {
                PersonId=obj.Pid??-1,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _employee.Count();
        }

        protected override void updateTarget(ClientUser record)
        {
            (_target as QTD2.Data.QTDContext).ClientUsers.Add(record);
        }

    }
}
