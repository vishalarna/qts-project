using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Coversheet : Entity
    {
        public string CoversheetTitle { get; set; }

        public int CoversheetTypeId { get; set; }

        public string CoversheetInstructions { get; set; }

        public byte[] CoversheetFileUpload { get; set; }

        public string CoversheetImageUpload { get; set; }

        public Coversheet(string coversheetTitle, int coversheetTypeId, string coversheetInstructions, byte[] coversheetFileUpload, string coversheetImageUpload)
        {
            CoversheetTitle = coversheetTitle;
            CoversheetTypeId = coversheetTypeId;
            CoversheetInstructions = coversheetInstructions;
            CoversheetFileUpload = coversheetFileUpload;
            CoversheetImageUpload = coversheetImageUpload;
        }

        public Coversheet()
        {
        }
    }
}
