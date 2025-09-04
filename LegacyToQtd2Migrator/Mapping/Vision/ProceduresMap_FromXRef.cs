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
    public class ProceduresMap_FromXRef : Common.MigrationMap<XrefLibImpl, Procedure>
    {
        int _projectId;

        List<XrefLibImpl> _sourceProcedureParents;
        List<XrefLibImpl> _sourceProcedures;
        List<XrefLibLink> _sourceProcedureLinks;
        List<AnalysisImpl> _sourceTasks;

        Procedure_IssuingAuthority _defaultIssuingAuthority;
        List<Task> _targetTasks;

        public ProceduresMap_FromXRef(DbContext source, DbContext target, int projectId) : base(source, target)
        {

        }

        protected override List<XrefLibImpl> getSourceRecords()
        {
            _sourceProcedureParents = (_source as VisionContext).XrefLibImpls.Where(r => r.Id == 6051).ToList();

            List<decimal> parentIds = _sourceProcedureParents.Select(r => r.FkXrefLib).ToList();

            _sourceProcedures = (_source as VisionContext).XrefLibImpls.Where(r => r.FkExpiredBy == null).Where(r => parentIds.Contains(r.FkParent ?? -1)).ToList();

            List<decimal> procedureIds = _sourceProcedures.Select(r => r.FkXrefLib).ToList();

            _sourceProcedureLinks = (_source as VisionContext).XrefLibLinks
                .Where(r => r.FkExpiredBy == null)
                .Where(r => procedureIds.Contains(r.FkItem)).ToList();


            _defaultIssuingAuthority = (_target as QTDContext).Procedure_IssuingAuthorities.First();

            _targetTasks = (_target as QTDContext).Tasks.ToList();

            return _sourceProcedures;
        }

        protected override Procedure mapRecord(XrefLibImpl obj)
        {
            return new Procedure()
            {
                Number = obj.Text.RtfToPlainText(),
                Title = obj.Text.RtfToPlainText(),
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

        private ICollection<Procedure_Task_Link> getProcedureTaskLinks(XrefLibImpl obj)
        {
            List<Procedure_Task_Link> taskLinks = new List<Procedure_Task_Link>();

            List<decimal> sourceTaskIds = _sourceProcedureLinks
                    .Where(r => r.FkItem == obj.FkXrefLib)
                    .Where(r => r.FkExpiredBy == null)
                    .Select(r => r.FkLinkTo).ToList();

            foreach(var sourceTaskId in sourceTaskIds)
            {
                var sourceTask = _sourceTasks.Where(r => r.FkAnalysis == sourceTaskId);
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
            TotalRecordsToConvert = _sourceProcedures.Count();
        }

        protected override void updateTarget(Procedure record)
        {
            (_target as QTD2.Data.QTDContext).Procedures.Add(record);
        }
    }
}
