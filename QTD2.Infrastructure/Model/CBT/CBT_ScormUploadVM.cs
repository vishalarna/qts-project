using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.CBT
{
    public class CBT_ScormUploadVM
    {
        public int Id { get; set; }
        public int? CbtId { get; set; }
        public int? ILAId { get; set; }
        public int? ProviderId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public CBT_ScormUploadVM()
        {
        }

        public CBT_ScormUploadVM(int id, int? cbtId, int? ilaId, int? providerId, string name,bool active)
        {
            Id = id;
            CbtId = cbtId;
            ILAId = ilaId;
            ProviderId = providerId;
            Name = name;
            Active = active;
        }
    }

}
