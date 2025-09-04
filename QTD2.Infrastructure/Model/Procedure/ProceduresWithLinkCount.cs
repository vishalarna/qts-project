using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Procedure
{
    public class ProceduresWithLinkCount
    {

        public int Id { get; set; }

        public int LinkCount { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public ProceduresWithLinkCount(int id, int linkCount, string number, string description, bool active)
        {
            Id = id;
            LinkCount = linkCount;
            Number = number;
            Description = description;
            Active = active;
        }
    }
}
