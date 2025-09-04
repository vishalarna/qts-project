using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsVersionInfo
{
    public decimal Id { get; set; }

    public string VersionType { get; set; }

    public string VersionSubtype { get; set; }

    public string Data { get; set; }

    public decimal? Id2 { get; set; }

    public decimal? Id3 { get; set; }

    public DateTime DateCreated { get; set; }
}
