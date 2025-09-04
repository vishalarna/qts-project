using LegacyToQtd2Migrator.Helpers;
using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class EmployeePositionsFromTblEmployeeAdditionalPositionMap : Common.MigrationMap<TblEmployeeAdditionalPosition, EmployeePosition>
    {
        List<TblEmployeeAdditionalPosition> _employeeAdditionalPositions;
        public EmployeePositionsFromTblEmployeeAdditionalPositionMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblEmployeeAdditionalPosition> getSourceRecords()
        {
            _employeeAdditionalPositions = (_source as EMP_DemoContext).TblEmployeeAdditionalPositions.ToListAsync().Result;
            return _employeeAdditionalPositions;
        }

        protected override EmployeePosition mapRecord(TblEmployeeAdditionalPosition obj)
        {

            return new EmployeePosition()
            {
                EmployeeId = obj.Eid,
                PositionId = obj.Pid,
                StartDate = (obj.StartDate.HasValue) ? DateOnly.FromDateTime(obj.StartDate.Value) : DateOnly.FromDateTime(DateTime.Now),
                EndDate = (obj.EndDate.HasValue) ? DateOnly.FromDateTime(obj.EndDate.Value) : null,
                Trainee = obj.Trainee ?? false,
                QualificationDate = (obj.QualificationDate.HasValue) ? DateOnly.FromDateTime(obj.QualificationDate.Value) : null,
                //ManagerName
                //TrainingProgramVersion,
                //IsSignificant
                Deleted = false,
                Active = true,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _employeeAdditionalPositions.Count();
        }
        protected override void updateTarget(EmployeePosition record)
        {
            (_target as QTD2.Data.QTDContext).EmployeePositions.Add(record);
        }
    }
}
