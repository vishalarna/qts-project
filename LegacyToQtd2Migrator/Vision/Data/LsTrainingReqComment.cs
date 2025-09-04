using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsTrainingReqComment
{
    public decimal Id { get; set; }

    public decimal FkLsTrainingReqItem { get; set; }

    public decimal FkDeveloper { get; set; }

    public string Comments { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual Developer FkDeveloperNavigation { get; set; }

    public virtual LsTrainingReqItem FkLsTrainingReqItemNavigation { get; set; }
}
