using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class SourceProcedureIssuingAuthority
    {
        public string Name { get; set; }
    }

    public class Procedure_IssuingAuthoritiesMap : Common.MigrationMap<SourceProcedureIssuingAuthority, Procedure_IssuingAuthority>
    {
        public List<SourceProcedureIssuingAuthority> _sourceAuthorities;

        public Procedure_IssuingAuthoritiesMap(DbContext source, DbContext target, int projectId) : base(source, target)
        {

        }
        protected override List<SourceProcedureIssuingAuthority> getSourceRecords()
        {
            _sourceAuthorities = new List<SourceProcedureIssuingAuthority>()
            {
                new SourceProcedureIssuingAuthority()
                {
                    Name  = "From Migration"
                }
            };

            return _sourceAuthorities;
        }

        protected override Procedure_IssuingAuthority mapRecord(SourceProcedureIssuingAuthority obj)
        {
            return new Procedure_IssuingAuthority()
            {
                Title = obj.Name,
                Website = "",
                Deleted = false,
                Active = true,
                EffectiveDate = DateOnly.FromDateTime(DateTime.Now),
                IsActive = true,
                IsDeleted = false
            };
        }     

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _sourceAuthorities.Count();
        }

        protected override void updateTarget(Procedure_IssuingAuthority record)
        {
            (_target as QTD2.Data.QTDContext).Procedure_IssuingAuthorities.Add(record);
        }
    }
}
