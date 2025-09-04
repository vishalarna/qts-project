using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Interfaces
{
    public interface IReportContentGenerator
    {
        string GetReportContent(IReportModel model);
    }
}
