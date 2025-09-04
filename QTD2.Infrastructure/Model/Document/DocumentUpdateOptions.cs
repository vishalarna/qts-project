using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Document
{
    public class DocumentUpdateOptions
    {
        public int DocumentTypeiD { get; set; }
        public int LinkedDataId { get; set; }
        public string Comments { get; set; }
        public string LinkedData { get; set; }
    }
}
