using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsTaskSurveyItemsArch
{
    public decimal Id { get; set; }

    public decimal Project { get; set; }

    public decimal FkAnalysisItem { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal? CreatedBy { get; set; }
}
