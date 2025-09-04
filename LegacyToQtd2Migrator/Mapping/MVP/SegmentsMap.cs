using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

using LegacyToQtd2Migrator.Extensions;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class SegmentsMap : Common.MigrationMap<TblCourseSegment, Segment>
    {
        List<TblCourseSegment> _courseSegments;
        List<TblCourseSegmentLearningObjective> _courseSegmentLearningObjectiveLinks;
        List<TblSkillsKnowledge> _skillsKnowledge;
        List<TblTask> _tasks;
        List<TblCourse> _courses;

        List<ILA> _qtd2Ilas;
        List<Task> _qtd2Tasks;
        List<CustomEnablingObjective> _qtd2CustomObjectives;
        List<EnablingObjective> _qtd2EnablingObjectives;
        List<TblObjectivesUserAdd> _customObjectives;

        public SegmentsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblCourseSegment> getSourceRecords()
        {
            _courseSegments = (_source as EMP_DemoContext).TblCourseSegments.ToListAsync().Result;
            _courseSegmentLearningObjectiveLinks = (_source as EMP_DemoContext).TblCourseSegmentLearningObjectives.ToListAsync().Result;
            _skillsKnowledge = (_source as EMP_DemoContext).TblSkillsKnowledges.ToListAsync().Result;
            _tasks = (_source as EMP_DemoContext).TblTasks.ToListAsync().Result;
            _courses = (_source as EMP_DemoContext).TblCourses.ToListAsync().Result;
            _customObjectives = (_source as EMP_DemoContext).TblObjectivesUserAdds.ToList();

            _qtd2Tasks = (_target as QTD2.Data.QTDContext).Tasks.ToListAsync().Result;
            _qtd2EnablingObjectives = (_target as QTD2.Data.QTDContext).EnablingObjectives.ToListAsync().Result;
            _qtd2CustomObjectives = (_target as QTD2.Data.QTDContext).CustomEnablingObjectives.ToListAsync().Result;
            _qtd2Ilas = (_target as QTD2.Data.QTDContext).ILAs.ToListAsync().Result;

            return _courseSegments;
        }

        protected override Segment mapRecord(TblCourseSegment obj)
        {
            return new Segment()
            {
                Title = obj.SegmentTitle ?? "",
                Duration = Convert.ToInt32(obj.Total.GetValueOrDefault()),
                IsNercStandard = obj.ChkStds,
                IsNercOperatingTopics = obj.ChkOper,
                IsNercSimulation = obj.ChkSim,
                Content = obj.Content ?? "",
                //Uploadss
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Deleted = false,
                Active = true,
                SegmentObjective_Links = getObjectiveLinks(obj),
                ILA_Segment_Links = getIlaSegmentLinks(obj)

            };
        }

        private ICollection<ILA_Segment_Link> getIlaSegmentLinks(TblCourseSegment obj)
        {
            var course = _courses.Where(r => r.Corid == obj.Corid).First();
            var qtd2Course = _qtd2Ilas.Where(r => r.Number == course.Cornum).FirstOrDefault();

            List<ILA_Segment_Link> links = new List<ILA_Segment_Link>();

            if (qtd2Course == null) return links;

            links.Add(new ILA_Segment_Link()
            {
                ILAId = qtd2Course.Id,
                DisplayOrder = obj.SegDisplayOrder
            });

            return links;
        }

        private ICollection<SegmentObjective_Link> getObjectiveLinks(TblCourseSegment obj)
        {
            List<SegmentObjective_Link> links = new List<SegmentObjective_Link>();

            var sourceLinks = _courseSegmentLearningObjectiveLinks.Where(r => r.Csid == obj.Csid);

            foreach (var sourceLink in sourceLinks)
            {
                var sourceEo = sourceLink.ObjType == "EO" ? _skillsKnowledge.Where(r => r.Skid == sourceLink.ObjId).FirstOrDefault() : null;
                var sourceTask = sourceLink.ObjType == "Task" ? _tasks.Where(r => r.Tid == sourceLink.ObjId).FirstOrDefault() : null;
                var userAdded = sourceLink.ObjType == "User Added" ? _customObjectives.Where(r => r.ObjId == sourceLink.ObjId).FirstOrDefault() : null;

                if (sourceEo == null && sourceTask == null && userAdded == null) continue;

                EnablingObjective targetEo = null;
                Task targetTask = null;
                CustomEnablingObjective customEnablingObjective = null;

                if (sourceEo != null)
                {
                    targetEo = _qtd2EnablingObjectives.FindEnablingObjective(sourceEo, sourceEo.CidNavigation, (_target as QTD2.Data.QTDContext));
                }

                if (sourceTask != null)
                {
                    targetTask = _qtd2Tasks.FindTask(sourceTask, sourceTask.Da, (_target as QTD2.Data.QTDContext));
                }

                if (userAdded != null)
                {
                    customEnablingObjective = _qtd2CustomObjectives.Where(r => r.CustomEONumber == userAdded.ObjId).FirstOrDefault();
                }

                if (customEnablingObjective == null && targetEo == null && targetTask == null) continue;


                links.Add(new SegmentObjective_Link()
                {
                    EnablingObjectiveId = targetEo == null ? null : targetEo.Id,
                    TaskId = targetTask == null ? null : targetTask.Id,
                    CustomEOId = userAdded == null ? null : _qtd2CustomObjectives.Where(r => r.CustomEONumber == userAdded.ObjId).First().Id
                });
            }

            return links;


        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _courseSegments.Count();
        }
        protected override void updateTarget(Segment record)
        {
            (_target as QTD2.Data.QTDContext).Segments.Add(record);
        }
    }
}
