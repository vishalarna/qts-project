using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.Helpers;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class ProceduresMap : Common.MigrationMap<TblProcedure, Procedure>
    {
        List<TblProcedure> _procedures;
        public ProceduresMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblProcedure> getSourceRecords()
        {
            _procedures = (_source as EMP_DemoContext).TblProcedures.ToListAsync().Result;
            return _procedures;
        }

        protected override Procedure mapRecord(TblProcedure obj)
        {
            return new Procedure()
            {
                IssuingAuthorityId=obj.Iaid??-1,
                Number=obj.Prnum.ToString(),
                Title=obj.Prtitle,
                RevisionNumber=obj.Prrevision.ToString(),
                EffectiveDate = DateOnly.FromDateTime(DateTime.Now),
                IsDeleted=false,
                IsPublished=false,
                Deleted=false,
                Active= obj.Prinactive
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _procedures.Count();
        }

        protected override void updateTarget(Procedure record)
        {
            (_target as QTD2.Data.QTDContext).Procedures.Add(record);
        }
    }
}
