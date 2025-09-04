using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Document
{
    public class DocumentCreationOptions
    {
        public int DocumentTypeId { get; set; }
        public string LinkedDataId { get; set; }
        public string Comments { get; set; }
        public string file { get; set; }
    }
}
