using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class MetaILA_Status : Common.Entity
    {
        public string Name { get; set; }

        public virtual ICollection<MetaILA> MetaILAs { get; set; } = new List<MetaILA>();

        public virtual ICollection<Version_MetaILA> Version_MetaILAs { get; set; } = new List<Version_MetaILA>();

        public MetaILA_Status()
        {
        }

        public MetaILA_Status(string name)
        {
            Name = name;
        }
    }
}
