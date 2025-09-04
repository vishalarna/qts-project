using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILACollaborator
{
    public class ILACollaboratorOptions
    {
        public int ILAId { get; set; }

        public int[] CollaboratorInviteIds { get; set; }
    }
}
