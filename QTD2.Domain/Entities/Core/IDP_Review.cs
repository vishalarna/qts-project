using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class IDP_Review : Common.Entity
    {
        public int StatusId { get; set; }

        public int EmployeeId { get; set; }

        public string Title { get; set; }

        public string Instructions { get; set; }

        public string Comments { get; set; }

        public bool IsStarted { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual IDP_ReviewStatus IDP_ReviewStatus { get; set; }

        public virtual Employee Employee { get; set; }

        public override void Create(string username)
        {
            base.Create(username);
            AddDomainEvent(new Domain.Events.Core.OnIDP_ReviewCreated(this));
        }
        public IDP_Review(int statusId, string title, string instructions, string comments, DateTime? releaseDate, DateTime? completedDate, DateTime? endDate)
        {
            StatusId = statusId;
            Title = title;
            Instructions = instructions;
            Comments = comments;
            ReleaseDate = releaseDate;
            CompletedDate = completedDate;
            EndDate = endDate;
        }

        public IDP_Review()
        {
        }
    }
}
