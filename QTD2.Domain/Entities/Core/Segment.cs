using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Segment : Entity
    {
        public string Title { get; set; }

        public int Duration { get; set; }

        public bool IsNercStandard { get; set; }

        public bool IsNercOperatingTopics { get; set; }

        public bool IsNercSimulation { get; set; }

        public bool IsPartialCredit { get; set; }

        public string Content { get; set; }

        public byte[] Uploads { get; set; }

        public virtual ICollection<ILA_Segment_Link> ILA_Segment_Links { get; set; } = new List<ILA_Segment_Link>();

        public virtual ICollection<SegmentObjective_Link> SegmentObjective_Links { get; set; } = new List<SegmentObjective_Link>();

        public Segment(string title, int duration, bool isNercStandard, bool isNercOperatingTopics, bool isNercSimulation, string content, byte[] uploads, bool isPartialCredit)
        {
            Title = title;
            Duration = duration;
            IsNercStandard = isNercStandard;
            IsNercOperatingTopics = isNercOperatingTopics;
            IsNercSimulation = isNercSimulation;
            Content = content;
            Uploads = uploads;
            IsPartialCredit = isPartialCredit;
        }

        public Segment()
        {
        }

        public SegmentObjective_Link LinkObjective(Task task, EnablingObjective eo,CustomEnablingObjective co)
        {
            SegmentObjective_Link seg_taskObj_link = SegmentObjective_Links.FirstOrDefault(x => x.TaskId == task.Id && x.EnablingObjectiveId == eo.Id && x.SegmentId == this.Id);
            if (seg_taskObj_link != null)
            {
                return seg_taskObj_link;
            }

            seg_taskObj_link = new SegmentObjective_Link(this, task, eo,co);
            AddEntityToNavigationProperty<SegmentObjective_Link>(seg_taskObj_link);
            return seg_taskObj_link;
        }

        public void UnlinkObjective(Task task, EnablingObjective eo)
        {
            SegmentObjective_Link seg_taskObj_link = SegmentObjective_Links.FirstOrDefault(x => x.TaskId == task.Id && x.EnablingObjectiveId == eo.Id && x.SegmentId == this.Id);
            if (seg_taskObj_link != null)
            {
                RemoveEntityFromNavigationProperty<SegmentObjective_Link>(seg_taskObj_link);
            }
        }

        public void UnlinkObjective()
        {
            List<SegmentObjective_Link> seg_taskObj_link = SegmentObjective_Links.Where(x => x.SegmentId == this.Id).ToList();
            if (seg_taskObj_link != null)
            {
                RemoveEntitiesFromNavigationProperty<SegmentObjective_Link>(seg_taskObj_link);
            }
        }

        public SegmentObjective_Link LinkObjective(Task task)
        {
            SegmentObjective_Link seg_taskObj_link = SegmentObjective_Links.FirstOrDefault(x => x.TaskId == task.Id && x.SegmentId == this.Id);
            if (seg_taskObj_link != null)
            {
                return seg_taskObj_link;
            }
            seg_taskObj_link = new SegmentObjective_Link(this, task, null,null);
            AddEntityToNavigationProperty<SegmentObjective_Link>(seg_taskObj_link);
            return seg_taskObj_link;
        }

        public void UnlinkObjective(Task task)
        {
            SegmentObjective_Link seg_taskObj_link = SegmentObjective_Links.FirstOrDefault(x => x.TaskId == task.Id && x.SegmentId == this.Id);
            if (seg_taskObj_link != null)
            {
                RemoveEntityFromNavigationProperty<SegmentObjective_Link>(seg_taskObj_link);
            }
        }

        public void UnlinkObjective(CustomEnablingObjective co)
        {
            SegmentObjective_Link seg_taskObj_link = SegmentObjective_Links.FirstOrDefault(x => x.CustomEOId == co.Id && x.SegmentId == this.Id);
            if (seg_taskObj_link != null)
            {
                RemoveEntityFromNavigationProperty<SegmentObjective_Link>(seg_taskObj_link);
            }
        }

        public SegmentObjective_Link LinkObjective(EnablingObjective eo)
        {
            SegmentObjective_Link seg_taskObj_link = SegmentObjective_Links.FirstOrDefault(x => x.EnablingObjectiveId == eo.Id && x.SegmentId == this.Id);
            if (seg_taskObj_link != null)
            {
                return seg_taskObj_link;
            }
            seg_taskObj_link = new SegmentObjective_Link(this, null, eo ,null);
            AddEntityToNavigationProperty<SegmentObjective_Link>(seg_taskObj_link);
            return seg_taskObj_link;
        }

        public SegmentObjective_Link LinkObjective(CustomEnablingObjective co)
        {
            SegmentObjective_Link seg_taskObj_link = SegmentObjective_Links.FirstOrDefault(x => x.CustomEOId == co.Id && x.SegmentId == this.Id);
            if (seg_taskObj_link != null)
            {
                return seg_taskObj_link;
            }
            seg_taskObj_link = new SegmentObjective_Link(this, null,null, co);
            AddEntityToNavigationProperty<SegmentObjective_Link>(seg_taskObj_link);
            return seg_taskObj_link;
        }

        public void UnlinkObjective(EnablingObjective eo)
        {
            SegmentObjective_Link seg_taskObj_link = SegmentObjective_Links.FirstOrDefault(x => x.EnablingObjectiveId == eo.Id && x.SegmentId == this.Id);
            if (seg_taskObj_link != null)
            {
                RemoveEntityFromNavigationProperty<SegmentObjective_Link>(seg_taskObj_link);
            }
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as Segment;

            if (String.IsNullOrEmpty(copy.Title))
                copy.Title = "Original Unnamed";

            copy.SegmentObjective_Links = new List<SegmentObjective_Link>();
            foreach (var segmentObjective_Link in this.SegmentObjective_Links)
            {
                var segmentObjective_LinkCopy = segmentObjective_Link.Copy<SegmentObjective_Link>(createdBy);
                segmentObjective_LinkCopy.SegmentId = 0;
                copy.SegmentObjective_Links.Add(segmentObjective_LinkCopy);
            }

            return (T)(object)copy;

        }
    }
}
