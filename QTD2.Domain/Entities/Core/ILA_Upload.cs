using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_Upload : Entity
    {
        public int ILAId { get; set; }

        public string FileName { get; set; }

        public string FileSize { get; set; }

        public string FileType { get; set; }

        public string FileAsBase64 { get; set; }

        public virtual ILA ila { get; set; }

        public ILA_Upload(int iLAId, string fileName, string fileSize, string fileType, string fileAsBase64)
        {
            ILAId = iLAId;
            FileName = fileName;
            FileSize = fileSize;
            FileType = fileType;
            FileAsBase64 = fileAsBase64;
        }

        public ILA_Upload()
        {
        }
    }
}
