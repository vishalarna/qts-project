using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Doclink
{
    public decimal Id { get; set; }

    public virtual ICollection<DoclinkHistory> DoclinkHistories { get; set; } = new List<DoclinkHistory>();

    public virtual ICollection<DoclinkImpl> DoclinkImpls { get; set; } = new List<DoclinkImpl>();

    public virtual ICollection<LsDoclinkTrack> LsDoclinkTracks { get; set; } = new List<LsDoclinkTrack>();
}
