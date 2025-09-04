using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILAUploadOptions
    {
        public int ILAId { get; set; }

        public List<UploadFile> uploadFiles { get; set; }
    }

    public class UploadFile
    {
        public string FileName { get; set; }

        public string FileSize { get; set; }

        public string FileType { get; set; }

        public string FileAsBase64 { get; set; }
    }
}
