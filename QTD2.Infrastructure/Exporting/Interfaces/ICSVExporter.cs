using System.IO;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Exporting.Interfaces
{
    public interface ICSVExporter
    {
        Stream ExportToCsv(string data);
    }
}
