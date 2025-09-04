using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Reports
{
    public class ReportFilterOption
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool? Active { get; set; }
        public bool IsDefaultOrder { get; set; } = false;

        public List<ReportFilterOptionParent> FilterOptionParents { get; set; }
        public List<ReportFilterTableColumns> FilterTableColumns { get; set; }

        public ReportFilterOption()
        {

        }
    }

    public class ReportFilterOptionParent
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }
        public bool IsCascade { get; set; } = false;
        public bool IsTableVisible { get; set; } = true;
        public string ControlType{ get; set; } = "Dropdown";
    }

    public class ReportFilterTableColumns
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }
    }
}
