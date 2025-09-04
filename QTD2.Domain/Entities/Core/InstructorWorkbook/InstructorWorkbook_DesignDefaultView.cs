using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public partial class InstructorWorkbook_DesignDefaultView : Common.Entity
    {
        public string DesignDefaultView { get; set; }
        public int? UserId { get; set; }
       
    }
}
