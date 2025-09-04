using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class Position_EmployeesFromTblEmployeeAdditionalPositionsMap : Common.MigrationMap<TblEmployeeAdditionalPosition, Position_Employee>
    {
        List<TblEmployeeAdditionalPosition> _employeeAdditionalPositions;
        public Position_EmployeesFromTblEmployeeAdditionalPositionsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblEmployeeAdditionalPosition> getSourceRecords()
        {
            _employeeAdditionalPositions = (_source as EMP_DemoContext).TblEmployeeAdditionalPositions.ToListAsync().Result;
            return _employeeAdditionalPositions;
        }

        protected override Position_Employee mapRecord(TblEmployeeAdditionalPosition obj)
        {
            return new Position_Employee()
            {
                PositionId = obj.Pid,
                EmployeeId = obj.Eid,
                //StartTime
                Trainee = obj.Trainee ?? false,
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
            TotalRecordsToConvert = _employeeAdditionalPositions.Count();
        }
        protected override void updateTarget(Position_Employee record)
        {
            (_target as QTD2.Data.QTDContext).Position_Employees.Add(record);
        }
    }
}
