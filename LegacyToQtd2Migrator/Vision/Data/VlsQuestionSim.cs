using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsQuestionSim
{
    public decimal ItemId { get; set; }

    public decimal QuestionId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateExpired { get; set; }
}
