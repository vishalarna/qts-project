using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Position
{
    public class PositionLatestActivityVM
    {
        public int Id { get; set; }
        public int PositionNum { get; set; }

        public string PositionName { get; set; }

        public string ActivityDesc { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate
        {
            get; set;
        }
    }
}
