using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsAppOrgspecificparm
{
    public decimal Id { get; set; }

    public decimal ParamValue { get; set; }

    public string Name { get; set; }

    public decimal ValueType { get; set; }

    public string Description { get; set; }

    public decimal? ValueBit { get; set; }

    public string ValueText { get; set; }

    public decimal? ValueNumericInt { get; set; }

    public decimal? ValueNumericFloat { get; set; }
}
