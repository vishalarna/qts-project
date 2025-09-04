using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Procedure_EnablingObjective_LinksMap : Common.MigrationMap<TblSkProcedure, Procedure_EnablingObjective_Link>
    {
        List<TblSkProcedure> _procedureEnablingObjectiveLinks;
        public Procedure_EnablingObjective_LinksMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblSkProcedure> getSourceRecords()
        {
            _procedureEnablingObjectiveLinks = (_source as EMP_DemoContext).TblSkProcedures.ToListAsync().Result;
            return _procedureEnablingObjectiveLinks;
        }

        protected override Procedure_EnablingObjective_Link mapRecord(TblSkProcedure obj)
        {
            var sourceEo = obj.Sk;
            var sourceProducedures = obj.Proc;

            var targetEo = (_target as QTD2.Data.QTDContext).Tasks.Where(r => r.Number == sourceEo.Sknum).First();
            var targetProcedure = (_target as QTD2.Data.QTDContext).Procedures.Where(r => r.Number == sourceProducedures.Prnum.Value.ToString()).First();

            return new Procedure_EnablingObjective_Link()
            {
                Active = true,
                ProcedureId = targetProcedure.Id,
                EnablingObjectiveId = targetEo.Id
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _procedureEnablingObjectiveLinks.Count();
        }

        protected override void updateTarget(Procedure_EnablingObjective_Link record)
        {
            (_target as QTD2.Data.QTDContext).Procedure_EnablingObjective_Links.Add(record);
        }

    }
}
