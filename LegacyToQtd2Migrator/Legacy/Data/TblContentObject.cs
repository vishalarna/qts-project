using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblContentObject
    {
        public TblContentObject()
        {
            TblCoSks = new HashSet<TblCoSk>();
            TblCopresentationCos = new HashSet<TblCopresentationCo>();
        }

        public int Coid { get; set; }
        public int Conum { get; set; }
        public int ShelfId { get; set; }
        public string Coname { get; set; }
        public string Codesc { get; set; }
        public string FileName { get; set; }
        public string SourceFile { get; set; }
        public DateTime CodateStamp { get; set; }

        public virtual TblCoshelf Shelf { get; set; }
        public virtual ICollection<TblCoSk> TblCoSks { get; set; }
        public virtual ICollection<TblCopresentationCo> TblCopresentationCos { get; set; }
    }
}
