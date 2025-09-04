using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;

namespace LegacyToQtd2Migrator.Extensions
{
    public static class FinderExtensions
    {
        public static EnablingObjective FindEnablingObjective(this List<EnablingObjective> eo, TblSkillsKnowledge sourceEo, TblCategory sourceCateogry, QTD2.Data.QTDContext target)
        {
            EnablingObjective targetEo = new EnablingObjective();
            if (sourceEo.Sknum == 0)
            {
                targetEo = target.EnablingObjective_Categories.Where(r => r.Number == sourceCateogry.Cnum).First()
                                                .EnablingObjectives.Where(r => r.Number == sourceEo.SksubNum.Value.ToString()).First();
            }
            else if (sourceEo.SksubNum.GetValueOrDefault() == 0)
            {
                return null;
            }
            else
            {
                targetEo = target.EnablingObjective_Categories.Where(r => r.Number == sourceCateogry.Cnum).First()
                            .EnablingObjective_SubCategories.Where(r => r.Number == sourceCateogry.CsubNum).First()
                            .EnablingObjective_Topics.Where(r => r.Number == sourceEo.Sknum).First()
                            .EnablingObjectives.Where(r => r.Number == sourceEo.SksubNum.Value.ToString()).First();
            }

            return targetEo;
        }

        public static QTD2.Domain.Entities.Core.Task FindTask(this List<QTD2.Domain.Entities.Core.Task> tasks, TblTask sourceTask, TblDutyArea sourceDutyArea, QTD2.Data.QTDContext target)
        {
            QTD2.Domain.Entities.Core.Task targetTask = target
                .Tasks
                .Where(r =>     r.Number == sourceTask.Tnum
                                && r.SubdutyArea.SubNumber == sourceDutyArea.DasubNum
                                && r.SubdutyArea.DutyArea.Number == sourceDutyArea.Danum
                                && r.SubdutyArea.DutyArea.Letter == sourceDutyArea.Daletter).FirstOrDefault();



                //.DutyAreas.Where(r => r.Number == sourceDutyArea.Danum && r.Letter == sourceDutyArea.Daletter).First()
                //.SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
                //.Tasks.Where(r => r.Number == sourceTask.Tnum).FirstOrDefault();

            return targetTask;
        }

        public static Employee FindEmployee(this List<Employee> employees, TblEmployee sourceEmployee)
        {
            return employees.Where(r => r.Person.FirstName == sourceEmployee.EfirstName).Where(r => r.Person.LastName == sourceEmployee.ElastName).Where(r => r.EmployeeNumber == sourceEmployee.Enum).FirstOrDefault();
        }

        public static Position FindPosition(this List<Position> positions, TblPosition sourcePosition)
        {
            return positions.Where(r => r.PositionTitle == sourcePosition.Pdesc).FirstOrDefault();
        }

        public static ClassSchedule FindClassSchedule(this List<ClassSchedule> classSchedules, TblClass sourceClass)
        {
            return classSchedules.Where(r => r.SpecialInstructions == sourceClass.Clid.ToString()).FirstOrDefault();
        }

    }
}
