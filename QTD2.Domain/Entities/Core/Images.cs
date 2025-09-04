using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core 
{
    public class Image : Common.Entity
    {
        public byte[] Body { get; set; }
        public string Description { get; set; }
    }
}
