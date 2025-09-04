using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Infrastructure.Model.Shared;

namespace QTD2.Infrastructure.Model.ILA_Cbt_ScormUpload
{
    public class ScormUploadAddOptions
    {
        public int CbtId { get; set; }        

        public FileUploadModel File { get; set; }
    }
}
