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
    public class Employee_TasksMap : Common.MigrationMap<RsTblEmployeesTask, Employee_Task>
    {
        List<RsTblEmployeesTask> _employeetasks;

        public Employee_TasksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<RsTblEmployeesTask> getSourceRecords()
        {
            _employeetasks = (_source as EMP_DemoContext).RsTblEmployeesTasks.ToListAsync().Result;
            return _employeetasks;
        }

        protected override Employee_Task mapRecord(RsTblEmployeesTask obj)
        {
            var sourceEmployee = obj.EidNavigation;
            var sourceTask = obj.TidNavigation;
            var sourceDutyArea = obj.TidNavigation.Da;

            var targetEmployee = (_target as QTD2.Data.QTDContext).Employees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();
            var targetTask = (_target as QTD2.Data.QTDContext).DutyAreas.Where(r => r.Number == sourceDutyArea.Danum).Where(r => r.Letter == sourceDutyArea.Daletter).First().SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First().Tasks.Where(r => r.Number == sourceTask.Tnum).First();

            return new Employee_Task()
            {
                Active = true,
                EmployeeId= targetEmployee.Id,
                TaskId= targetTask.Id,
                //MajorVersion,
                //MinorVersion,
                //Archived,
                Deleted = false
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _employeetasks.Count();
        }

        protected override void updateTarget(Employee_Task record)
        {
            (_target as QTD2.Data.QTDContext).Employee_Tasks.Add(record);
        }
    }
}
