using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class CoverSheetType : Entity
    {
        public string Name { get; set; }

        public CoverSheetType(string name)
        {
            Name = name;
        }
    }
}
