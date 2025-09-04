using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILAWithCountOptions
    {
        public string Number { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public int LinkCount { get; set; }

        public bool Active { get; set; }

        public ILAWithCountOptions()
        {
        }

        public ILAWithCountOptions(string number, string description, int id, int linkCount, bool active)
        {
            Number = number;
            Description = description;
            Id = id;
            LinkCount = linkCount;
            Active = active;
        }
    }
}
