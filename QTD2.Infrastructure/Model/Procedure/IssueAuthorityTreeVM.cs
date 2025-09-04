
using System.Collections.Generic;

namespace QTD2.Infrastructure.Model.Procedure
{
    public  class IssueAuthorityTreeVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }

        public ICollection<ProcedureSummaryVM> Procedures { get; set; }

    }
}
