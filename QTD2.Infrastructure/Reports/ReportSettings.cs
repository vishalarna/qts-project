using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports
{
    public class ReportSettings
    {
        public string Path { get; set; }
        public bool ShouldCache { get; internal set; }
    }
}
