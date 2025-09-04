using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Procedure_Task_LinksMap : Common.MigrationMap<RsTblProceduresTask, Procedure_Task_Link>
    {
        List<RsTblProceduresTask> _procedureTaskLinks;
        public Procedure_Task_LinksMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<RsTblProceduresTask> getSourceRecords()
        {
            _procedureTaskLinks = (_source as EMP_DemoContext).RsTblProceduresTasks.ToList();
            return _procedureTaskLinks;
        }

        protected override Procedure_Task_Link mapRecord(RsTblProceduresTask obj)
        {
            var sourceTask = (_source as EMP_DemoContext).TblTasks.Where(r => r.Tid == obj.Tid).FirstOrDefault();

            if (sourceTask == null) return null;

            var sourceProducedures = (_source as EMP_DemoContext).TblProcedures.Where(r => r.Prid == obj.Prid).First();

            var targetTask = (_target as QTD2.Data.QTDContext).Tasks.Where(r => r.Number == sourceTask.Tnum).First();
            var targetProcedure = (_target as QTD2.Data.QTDContext).Procedures.Where(r => r.Title == sourceProducedures.Prtitle).First();

            return new Procedure_Task_Link()
            {
                Active = true,
                ProcedureId = targetProcedure.Id,
                TaskId = targetTask.Id
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _procedureTaskLinks.Count();
        }

        protected override void updateTarget(Procedure_Task_Link record)
        {
            if (record != null)
                (_target as QTD2.Data.QTDContext).Procedure_Task_Links.Add(record);
        }

    }
}
