using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Employee
{
    public class EmployeeDocumentOptions
    {
        public int EmployeeID { get; set; }

        public List<UploadFileEmployee> uploadFiles { get; set; }
    }

    public class UploadFileEmployee
    {
        public string FileName { get; set; }

        public string FileSize { get; set; }

        public string FileType { get; set; }

        public string FileAsBase64 { get; set; }
    }
}
