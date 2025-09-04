using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Coversheet
{
    public class CoversheetUpdateOptions
    {
        public string CoversheetTitle { get; set; }

        public int CoversheetTypeId { get; set; }

        public string CoversheetInstructions { get; set; }

        public byte[] CoversheetFileUpload { get; set; }

        public string CoversheetImageUpload { get; set; }
    }
}
