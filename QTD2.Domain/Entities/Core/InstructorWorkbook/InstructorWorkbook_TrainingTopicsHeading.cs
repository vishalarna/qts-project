using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
  public partial  class InstructorWorkbook_TrainingTopicsHeading :Common.Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InstructorWorkbook_TrainingTopicsHeading()
        {
            this.InstructorWorkbook_TrainingTopics = new HashSet<InstructorWorkbook_TrainingTopics>();
        }
        public int Id { get; set; }
        public string TrainingTopicHeading { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorWorkbook_TrainingTopics> InstructorWorkbook_TrainingTopics { get; set; }
    }
}
