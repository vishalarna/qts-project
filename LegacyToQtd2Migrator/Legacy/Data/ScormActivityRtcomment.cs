using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormActivityRtcomment
    {
        public int ScormActivityId { get; set; }
        public int CommentIndex { get; set; }
        public int FromLms { get; set; }
        public string CommentText { get; set; }
        public bool? CommentTextNull { get; set; }
        public string Language { get; set; }
        public bool? LanguageNull { get; set; }
        public string Location { get; set; }
        public bool? LocationNull { get; set; }
        public DateTime? TimestampUtc { get; set; }
        public string TimestampText { get; set; }
        public bool? TimestampNull { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public string Id { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormActivityRt ScormActivityRt { get; set; }
    }
}
