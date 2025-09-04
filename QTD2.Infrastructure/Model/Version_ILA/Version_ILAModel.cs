using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Version_ILA
{
    public class Version_ILAModel
    {
        public int Id { get; set; }

        public int State { get; set; }

        public string ChangeDescription { get; set; }

        public string UserName { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public int VersionNumber { get; set; }
        
        public string ChangedBy { get; set; }

        public string ILANumber { get; set; }

        public string ILATitle { get; set; }

    }
}
