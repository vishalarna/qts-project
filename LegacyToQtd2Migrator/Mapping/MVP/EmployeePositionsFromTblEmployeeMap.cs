using LegacyToQtd2Migrator.Helpers;
using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
  public class EmployeePositionsFromTblEmployeeMap : Common.MigrationMap<TblEmployee, EmployeePosition>
    {
        List<TblEmployee> _employees;
        public EmployeePositionsFromTblEmployeeMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblEmployee> getSourceRecords()
        {
            _employees = (_source as EMP_DemoContext).TblEmployees.ToListAsync().Result;
            return _employees;
        }

        protected override EmployeePosition mapRecord(TblEmployee obj)
        {
          
            return new EmployeePosition()
            {
                EmployeeId = obj.Eid,
                PositionId = obj.Pid??-1,
                StartDate = (obj.PosStartDate.HasValue) ? DateOnly.FromDateTime(obj.PosStartDate.Value) : DateOnly.FromDateTime(DateTime.Now),
                EndDate = (obj.PosEndDate.HasValue) ? DateOnly.FromDateTime(obj.PosEndDate.Value) : null,
                Trainee = obj.Trainee??false,
                QualificationDate = (obj.PosQualDate.HasValue) ? DateOnly.FromDateTime(obj.PosQualDate.Value): null,
                //ManagerName
                //TrainingProgramVersion
                //IsSignificant
                Deleted =false,
                Active=true,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _employees.Count();
        }
        protected override void updateTarget(EmployeePosition record)
        {
                (_target as QTD2.Data.QTDContext).EmployeePositions.Add(record);
        }
    }
}
