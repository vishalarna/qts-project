using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace QTD2.Infrastructure.Rustici.EngineApi
{
    public class RusticiFile
    {
        public MemoryStream Contents { get; set; }
        public string FileName { get; set; }

        public RusticiFile(byte[] file, string fileName)
        {
            Contents = new MemoryStream(file);
            FileName = fileName;
        }
    }
}
