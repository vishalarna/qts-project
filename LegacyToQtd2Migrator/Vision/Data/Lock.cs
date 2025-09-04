using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Lock
{
    public decimal Id { get; set; }

    public decimal DataType { get; set; }

    public decimal FkDataId { get; set; }

    public DateTime DateLocked { get; set; }

    public decimal FkLockedBy { get; set; }

    public string DeviceName { get; set; }

    public virtual Developer FkLockedByNavigation { get; set; }
}
