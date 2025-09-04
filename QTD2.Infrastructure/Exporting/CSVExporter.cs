using QTD2.Infrastructure.Exporting.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace QTD2.Infrastructure.Exporting
{
    public class CSVExporter : ICSVExporter
    {
        public Stream ExportToCsv(string data)
        {
            
            //string serialized = JsonSerializer.Serialize(data);
            byte[] byteArray = Encoding.ASCII.GetBytes(data);
            MemoryStream stream = new MemoryStream(byteArray);

            return stream;
        }
    }
}
