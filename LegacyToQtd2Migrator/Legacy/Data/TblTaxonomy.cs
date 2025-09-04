using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTaxonomy
    {
        public TblTaxonomy()
        {
            TblTestItems = new HashSet<TblTestItem>();
        }

        public int TaxonomyLevelId { get; set; }
        public string TaxonomyLevel { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<TblTestItem> TblTestItems { get; set; }
    }
}
