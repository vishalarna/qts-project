using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Document
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Comments { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public string LinkedDataType { get; set; }
        public int LinkedDataId { get; set; }
        public string LinkedDataName { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
