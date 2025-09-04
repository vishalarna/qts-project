using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskRRWithCount
    {
        public string Number { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public int LinkCount { get; set; }

        public bool Active { get; set; }
        public string? RR_IssuingAuthority_Title { get; set; }

        public TaskRRWithCount(string number, string description, int id, int linkCount, bool active, string? rr_IssuingAuthority_Title)
        {
            Number = number;
            Description = description;
            Id = id;
            LinkCount = linkCount;
            Active = active;
            RR_IssuingAuthority_Title = rr_IssuingAuthority_Title;
        }

        public TaskRRWithCount()
        {
        }
    }
}
