using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DIFSurvey_DevStatus : Common.Entity
    {
        public string Status { get; set; }

        public virtual ICollection<DIFSurvey> DIFSurveys { get; set; } = new List<DIFSurvey>();
    }
}
