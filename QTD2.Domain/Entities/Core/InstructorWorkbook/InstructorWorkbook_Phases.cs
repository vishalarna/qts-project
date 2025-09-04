using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public class InstructorWorkbook_Phases : Common.Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InstructorWorkbook_Phases()
        {
            this.InstructorWorkbook_ILAPhases = new HashSet<InstructorWorkbook_ILAPhases>();
        }
        public string CoursePhaseDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_ILAPhases> InstructorWorkbook_ILAPhases { get; set; }
    }
}
