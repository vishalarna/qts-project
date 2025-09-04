using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

using LegacyToQtd2Migrator.Extensions;
using LegacyToQtd2Migrator.Vision.Data;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class SegmentsMap : Common.MigrationMap<ObjectiveImpl, Segment>
    {
        List<ObjectiveImpl> _sourceSegments;

        //Description
        List<ObjectiveComment> _sourceObjectiveComments;

        int _projectId;

        //SIMS, STDS,OPER, Parial Credit -> 
        //select * From XREF_LIB_IMPL where FK_PARENT = 4939
        //SEGMENT CATEGORIES - OPER
        //SEGMENT CATEGORIES - STDS
        //SEGMENT CATEGORIES - SIM
        //PARTIAL CREDIT

        List<XrefLibLink> _sourceXrefLinks;
        List<Sequencing> _sourceSequencing;
        List<ObjectiveTask> _sourceObjectiveTasks;

        XrefLibImpl _sourceOperationXref;
        XrefLibImpl _sourceStandardsXref;
        XrefLibImpl _sourceSimXref;
        XrefLibImpl _sourceParialCredit;

        List<ILA> _targetIlas;
        List<Task> _targetTasks;
        List<EnablingObjective> _targetEnablingObjectives;

        public SegmentsMap(DbContext source, DbContext target, int projectId) : base(source, target)
        {
            _projectId = projectId;
        }

        protected override List<ObjectiveImpl> getSourceRecords()
        {
            _sourceSegments = (_source as VisionContext)
                                    .ObjectiveImpls
                                        .Where(r => r.FkProject == _projectId)
                                        .Where(r => r.FkExpiredBy == null)
                                        .Where(r => r.Topic.ToUpper().Contains("SEGMENT"))
                                    .ToList();

            _sourceObjectiveComments = (_source as VisionContext).ObjectiveComments.Where(r => r.FkExpiredBy == null).ToList();
            _sourceSequencing = (_source as VisionContext)
                    .Sequencings
                        .Where(r => r.FkExpiredBy == null)
                        .Include("FkObjectiveNavigation.ObjectiveImpls")
                        .Include("FkProgramNavigation.ProgramImpls")
                       .ToList();
            _sourceObjectiveTasks = (_source as VisionContext).ObjectiveTasks.Include("FkAnalysisNavigation.AnalysisImpls").Where(r => r.FkExpiredBy == null).ToList();

            _sourceXrefLinks = (_source as VisionContext).XrefLibLinks.Where(r => r.FkExpiredBy == null).ToList();

            _sourceOperationXref = (_source as VisionContext).XrefLibImpls.Where(r => r.TextSort == "SEGMENT CATEGORIES - OPER").Where(r => r.FkExpiredBy == null).FirstOrDefault();
            _sourceStandardsXref = (_source as VisionContext).XrefLibImpls.Where(r => r.TextSort == "SEGMENT CATEGORIES - STDS").Where(r => r.FkExpiredBy == null).FirstOrDefault();
            _sourceSimXref = (_source as VisionContext).XrefLibImpls.Where(r => r.TextSort == "SEGMENT CATEGORIES - SIM").Where(r => r.FkExpiredBy == null).FirstOrDefault();
            _sourceParialCredit = (_source as VisionContext).XrefLibImpls.Where(r => r.TextSort == "PARTIAL CREDIT").Where(r => r.FkExpiredBy == null).FirstOrDefault();

            _targetTasks = (_target as QTD2.Data.QTDContext).Tasks.ToList();
            _targetIlas = (_target as QTD2.Data.QTDContext).ILAs.ToList();
            _targetEnablingObjectives = (_target as QTD2.Data.QTDContext).EnablingObjectives.ToList();

            return _sourceSegments;
        }

        protected override Segment mapRecord(ObjectiveImpl obj)
        {
            var comments = _sourceObjectiveComments
                                .Where(r => r.FkExpiredBy == null)
                                .Where(r => r.FkObjective == obj.FkObjective)
                                .FirstOrDefault();

            var nercStandards = _sourceXrefLinks
                                        .Where(r => r.FkLinkTo == obj.FkObjective)
                                        .Where(r => r.FkExpiredBy == null)
                                        .Where(r => r.LinkToType == 2)
                                        .Where(r => r.FkItem == _sourceStandardsXref.FkXrefLib)
                                    .FirstOrDefault();

            var nercSims = _sourceXrefLinks
                                        .Where(r => r.FkLinkTo == obj.FkObjective)
                                        .Where(r => r.FkExpiredBy == null)
                                        .Where(r => r.LinkToType == 2)
                                        .Where(r => r.FkItem == _sourceSimXref.FkXrefLib)
                                    .FirstOrDefault();

            var nercOper = _sourceXrefLinks
                                        .Where(r => r.FkLinkTo == obj.FkObjective)
                                        .Where(r => r.FkExpiredBy == null)
                                        .Where(r => r.LinkToType == 2)
                                        .Where(r => r.FkItem == _sourceOperationXref.FkXrefLib)
                                    .FirstOrDefault();

            var partialCredit = _sourceXrefLinks
                                        .Where(r => r.FkLinkTo == obj.FkObjective)
                                        .Where(r => r.FkExpiredBy == null)
                                        .Where(r => r.LinkToType == 2)
                                        .Where(r => r.FkItem == _sourceParialCredit.FkXrefLib)
                                    .FirstOrDefault();

            return new Segment()
            {
                Title = obj.Topic,
                Duration = Convert.ToInt32(obj.TrainingTime.GetValueOrDefault()),
                IsNercStandard = nercStandards != null,
                IsNercOperatingTopics = nercOper != null,
                IsNercSimulation = nercSims != null,
                IsPartialCredit = partialCredit != null,
                Content = comments?.Comments.RtfToHtml() ?? obj.Text.RtfToHtml(),
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

        private ICollection<ILA_Segment_Link> getIlaSegmentLinks(ObjectiveImpl obj)
        {
            //NOTE: Sequencings can hold segments and EOs, both of which are 'Cognitive Terminal'
            //we're relying on not finding these but if we have to adjust the EO heirarchy this may casue a problem
            //because the EO Heiarchy skips the segments since they don't fit into the EO heirarchy
            var sequencings = _sourceSequencing.Where(r => r.FkObjective == obj.FkObjective);

            List<ILA_Segment_Link> links = new List<ILA_Segment_Link>();

            foreach (var sequencing in sequencings)
            {
                var latestImp = sequencing.FkProgramNavigation.ProgramImpls.Where(r => r.FkExpiredBy == null).First();

                if (latestImp == null) continue;

                var ila = _targetIlas.Where(r => r.Number == latestImp.FkProgram.ToString()).FirstOrDefault();

                if (ila == null) continue;

                links.Add(new ILA_Segment_Link()
                {
                    ILAId = ila.Id,
                    DisplayOrder = Convert.ToInt32(sequencing.Sequence)
                });
            }

            return links;
        }

        private ICollection<SegmentObjective_Link> getObjectiveLinks(ObjectiveImpl obj)
        {
            List<SegmentObjective_Link> links = new List<SegmentObjective_Link>();

            var sequencingForProgram = _sourceSequencing.Where(r => r.FkObjective == obj.FkObjective).FirstOrDefault();

            if (sequencingForProgram == null) return links;

            var sourceSequencies = _sourceSequencing.Where(r => r.FkProgramNavigation == sequencingForProgram.FkProgramNavigation).OrderBy(r => r.Sequence);

            bool addObjective = false;

            foreach (var sourceLink in sourceSequencies)
            {
                var objective = sourceLink.FkObjectiveNavigation.ObjectiveImpls.Where(r => r.FkExpiredBy == null).First();
                var topicName = objective?.Topic ?? "";

                if (addObjective)
                {
                    if (topicName.ToUpper().Contains("SEGMENT"))
                    {
                        addObjective = false;
                        continue;
                    }

                    var targetTask = _targetTasks.Where(r => r.Description == objective.Text.RtfToPlainText()).FirstOrDefault();

                    if(targetTask != null)
                    {
                        links.Add(new SegmentObjective_Link()
                        {
                            Task = targetTask
                        });
                    }

                    var targetEo = _targetEnablingObjectives.Where(r => r.Description == objective.Text.RtfToPlainText()).FirstOrDefault();

                    if(targetEo != null)
                    {
                        links.Add(new SegmentObjective_Link()
                        {
                            EnablingObjective = targetEo
                        });
                    }
                }
                else
                {
                    if (topicName == obj.Topic)
                    {
                        addObjective = true;
                    }

                    continue;
                }
            }

            return links;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _sourceSegments.Count();
        }
        protected override void updateTarget(Segment record)
        {
            (_target as QTD2.Data.QTDContext).Segments.Add(record);
        }
    }
}
