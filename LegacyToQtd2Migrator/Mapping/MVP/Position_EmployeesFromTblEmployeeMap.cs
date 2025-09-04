using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class Position_EmployeesFromTblEmployeeMap : Common.MigrationMap<TblEmployee, Position_Employee>
    {
        List<TblEmployee> _employees;
        public Position_EmployeesFromTblEmployeeMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblEmployee> getSourceRecords()
        {
            _employees = (_source as EMP_DemoContext).TblEmployees.ToListAsync().Result;
            return _employees;
        }

        protected override Position_Employee mapRecord(TblEmployee obj)
        {
            return new Position_Employee()
            {
                PositionId = obj.Pid??-1,
                EmployeeId = obj.Eid,
                //StartTime
                Trainee = obj.Trainee??false,
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
            TotalRecordsToConvert = _employees.Count();
        }
        protected override void updateTarget(Position_Employee record)
        {
            (_target as QTD2.Data.QTDContext).Position_Employees.Add(record);
        }
    }
}
