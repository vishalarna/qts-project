using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Interfaces
{
    public interface IReportExporter
    {
        /// <summary>
        /// Returns a file name to the caller
        /// </summary>
        string ExportReportToFile(string fileName, string content);
    }
}
