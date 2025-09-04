using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TaxonomyLevelsMap : Common.MigrationMap<TblTaxonomy, TaxonomyLevel>
    {
        List<TblTaxonomy> _tblTaxonomy;

        public TaxonomyLevelsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTaxonomy> getSourceRecords()
        {
            _tblTaxonomy = (_source as EMP_DemoContext).TblTaxonomies.ToListAsync().Result;
            return _tblTaxonomy;
        }

        protected override TaxonomyLevel mapRecord(TblTaxonomy obj)
        {
            return new TaxonomyLevel()
            {
                Active = true,
                Deleted=false,
                Description = obj.TaxonomyLevel,
                
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblTaxonomy.Count();
        }

        protected override void updateTarget(TaxonomyLevel record)
        {
            (_target as QTD2.Data.QTDContext).TaxonomyLevels.Add(record);
        }
    }
}
