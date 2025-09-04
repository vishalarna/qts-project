using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class ClassScheduleEmployeesMap : Common.MigrationMap<RsTblClassStudent, ClassSchedule_Employee>
    {
        List<RsTblClassStudent> _tblClassScheduleEmployees;

        public ClassScheduleEmployeesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<RsTblClassStudent> getSourceRecords()
        {
            _tblClassScheduleEmployees = (_source as EMP_DemoContext).RsTblClassStudents.ToListAsync().Result;
            return _tblClassScheduleEmployees;
        }

        protected override ClassSchedule_Employee mapRecord(RsTblClassStudent obj)
        {
            return new ClassSchedule_Employee()
            {
                Active = true,
                //ClassScheduleId,
                //PreTestStatusId,
                //TestStatusId,
                //RetakeStatusId,
                //CBTStatusId,
                //FinalScore,
                //FinalGrade,
                //GradeNotes,
                EmployeeId=obj.Eid,
                //IsEnrolled,
                //IsWaitlisted,
                //TestId



            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblClassScheduleEmployees.Count();
        }

        protected override void updateTarget(ClassSchedule_Employee record)
        {
            (_target as QTD2.Data.QTDContext).ClassScheduleEmployees.Add(record);
        }
    }
}
