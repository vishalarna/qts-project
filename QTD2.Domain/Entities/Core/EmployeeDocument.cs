using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EmployeeDocument : Common.Entity
    {
        public int EmployeeID { get; set; }

        public string FileName { get; set; }

        public string FileSize { get; set; }

        public string FileType { get; set; }

        public string FileAsBase64 { get; set; }

        public virtual Employee Employee { get; set; }

        public EmployeeDocument()
        {
        }

        public EmployeeDocument(int employeeId, string fileName, string fileSize, string fileType, string fileAsBase64)
        {
            EmployeeID = employeeId;
            FileName = fileName;
            FileSize = fileSize;
            FileType = fileType;
            FileAsBase64 = fileAsBase64;
        }

    }
}
