using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Shared
{
    public class FileUploadModel
    {
        public string Name { get; set; }
        
        public string ContentType { get; set; }
    }
}
