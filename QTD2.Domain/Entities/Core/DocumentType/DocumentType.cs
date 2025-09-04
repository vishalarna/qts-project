using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DocumentType : Entity
    {
        public string Name { get; set; }
        public string LinkedDataType { get; set; }

        public DocumentType()
        {

        }

        public DocumentType(string name,string linkedDataType)
        {
            Name = name;
            LinkedDataType = linkedDataType;
        }
    }
}
