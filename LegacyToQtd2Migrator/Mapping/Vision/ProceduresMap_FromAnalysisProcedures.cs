using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.Helpers;
using System;
using LegacyToQtd2Migrator.Vision.Data;
using QTD2.Data;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
   public class ProceduresMap_FromAnalysisProcedures : Common.MigrationMap<AnalysisProcedure, Procedure>
    {
        int _projectId;

        List<AnalysisProcedure> _analysisProcedures;
        List<AnalysisHierarchy> _analysisHierarchy;

        List<Task> _targetTasks;
        Procedure_IssuingAuthority _defaultIssuingAuthority;

        public ProceduresMap_FromAnalysisProcedures(DbContext source, DbContext target, int projectId) : base(source, target)
        {

        }

        protected override List<AnalysisProcedure> getSourceRecords()
        {
            _analysisProcedures = (_source as VisionContext).AnalysisProcedures.Include("FkAnalysisNavigation.AnalysisImpls").Where(r => r.FkExpiredBy == null).ToList();
            _analysisHierarchy= (_source as VisionContext).AnalysisHierarchies.Where(r => r.FkExpiredBy == null).ToList();

            _targetTasks = (_target as QTDContext).Tasks.ToList();
            _defaultIssuingAuthority = (_target as QTDContext).Procedure_IssuingAuthorities.First();

            return _analysisProcedures;
        }

        protected override Procedure mapRecord(AnalysisProcedure obj)
        {
            return new Procedure()
            {
                Number = obj.FkAnalysis.ToString(),
                Title = obj.FkAnalysisNavigation.AnalysisImpls.Where(r => r.FkExpiredBy == null).First().Text.RtfToPlainText(),
                Hyperlink = null,
                RevisionNumber = "1",
                Procedure_IssuingAuthority = _defaultIssuingAuthority,
                //Version_Procedures = getVersions(sourceProcedure),
                EffectiveDate = DateOnly.FromDateTime(obj.DateCreated),
                //Procedure_EnablingObjective_Links = getProcedureEnablingObjectiveLinks(sourceProcedure),
                //ILA_Procedure_Links = getISLAProcedureLinks(sourceProcedure),
                //Procedure_ILA_Links = getProcedureIlaLinks(sourceProcedure),
                Procedure_Task_Links = getProcedureTaskLinks(obj),
                //ProcedureReviews = getProcedureReviews(sourceProcedure),
                IsDeleted = false,
                IsPublished = true,
                Deleted = false,
                Active = true
            };
        }

        private ICollection<Procedure_Task_Link> getProcedureTaskLinks(AnalysisProcedure obj)
        {
            List<Procedure_Task_Link> taskLinks = new List<Procedure_Task_Link>();

            var hierarchies = _analysisHierarchy.Where(r => r.FkChild == obj.FkAnalysis);

            foreach(var hierarchy in hierarchies)
            {
                var targetTask = _targetTasks.Where(r => r.Description == obj.Text).FirstOrDefault();

                if (targetTask == null) continue;

                else taskLinks.Add(new Procedure_Task_Link()
                {
                    Active = true,
                    Task = targetTask
                });
            }

            return taskLinks;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _analysisProcedures.Count();
        }

        protected override void updateTarget(Procedure record)
        {
            (_target as QTD2.Data.QTDContext).Procedures.Add(record);
        }
    }
}
