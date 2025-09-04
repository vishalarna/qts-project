using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanStatementIndex
    {
        public TinCanStatementIndex()
        {
            TinCanAgents = new HashSet<TinCanAgent>();
            TinCanContextActivities = new HashSet<TinCanContextActivity>();
            TinCanRelatedActivities = new HashSet<TinCanRelatedActivity>();
            TinCanRelatedAgents = new HashSet<TinCanRelatedAgent>();
        }

        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public byte[] StatementId { get; set; }
        public int? Version { get; set; }
        public int? VersionFamily { get; set; }
        public byte[] ActorId { get; set; }
        public byte[] Verb { get; set; }
        public byte[] TargetId { get; set; }
        public string TargetType { get; set; }
        public bool? Authoritative { get; set; }
        public bool? Voided { get; set; }
        public long Stored { get; set; }
        public byte[] CtxInstructorId { get; set; }
        public byte[] CtxRegistration { get; set; }

        public virtual ScormRegistrationStatementMap ScormRegistrationStatementMap { get; set; }
        public virtual ICollection<TinCanAgent> TinCanAgents { get; set; }
        public virtual ICollection<TinCanContextActivity> TinCanContextActivities { get; set; }
        public virtual ICollection<TinCanRelatedActivity> TinCanRelatedActivities { get; set; }
        public virtual ICollection<TinCanRelatedAgent> TinCanRelatedAgents { get; set; }
    }
}
