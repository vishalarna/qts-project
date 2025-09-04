using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Procedure_IssuingAuthoritiesMap : Common.MigrationMap<LktblIssuingAuthority, Procedure_IssuingAuthority>
    {
        List<LktblIssuingAuthority> _issuingauthority;
        List<LktblIssuingAuthority> _procIssuingAuthorityHistory;
        List<TblProcedure> _procedures;
        List<TblTask> _tasks;
        List<RsTblCoursesProcedure> _courseProcedureLinks;
        List<TblProcedureResource> _procedureResources;
        List<TblProcReleaseEmpSummary> _procedureReviews;
        List<TblProceduresHistory> _procedureHistories;

        public Procedure_IssuingAuthoritiesMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<LktblIssuingAuthority> getSourceRecords()
        {
            _issuingauthority = (_source as EMP_DemoContext).LktblIssuingAuthorities.ToList();
            _procIssuingAuthorityHistory = (_source as EMP_DemoContext).LktblIssuingAuthorities.ToList();
            _procedures = (_source as EMP_DemoContext).TblProcedures.ToList();
            _tasks = (_source as EMP_DemoContext).TblTasks.ToList();
            _courseProcedureLinks = (_source as EMP_DemoContext).RsTblCoursesProcedures.ToList();
            _procedureResources = (_source as EMP_DemoContext).TblProcedureResources.ToList();
            _procedureReviews = (_source as EMP_DemoContext).TblProcReleaseEmpSummaries.ToList();
            _procedureHistories = (_source as EMP_DemoContext).TblProceduresHistories.ToList();

            return _issuingauthority;
        }

        protected override Procedure_IssuingAuthority mapRecord(LktblIssuingAuthority obj)
        {
            return new Procedure_IssuingAuthority()
            {
                Title = obj.Iatitle,
                Website = "",
                Deleted = false,
                Active = true,
                EffectiveDate = DateOnly.FromDateTime(DateTime.Now),
                IsActive = true,
                IsDeleted = false,
                //IssuingAuthorityStatusHistories= getIssuingAuthorityStatusHistories(obj),
                Procedures = getProcedures(obj)
            };
        }

        private ICollection<Proc_IssuingAuthority_History> getIssuingAuthorityStatusHistories(LktblIssuingAuthority obj)
        {
            List<Proc_IssuingAuthority_History> proc_IssuingAuthority_Histories = new List<Proc_IssuingAuthority_History>();

            proc_IssuingAuthority_Histories.Add(new Proc_IssuingAuthority_History()
            {
                //ProcedureIssuingAuthorityId,
                //OldStatus,
                //NewStatus,
                //ChangeEffectiveDate,
                //ChangeNotes
                Deleted = false,
                Active = true
            });

            return proc_IssuingAuthority_Histories;
        }

        private ICollection<Procedure> getProcedures(LktblIssuingAuthority obj)
        {
            List<Procedure> procedures = new List<Procedure>();

            var sourceProcedures = _procedures.Where(r => r.Iaid == obj.Iaid);

            foreach (var sourceProcedure in sourceProcedures)
            {
                var procedureResource = _procedureResources.Where(r => r.Prid == sourceProcedure.Prid).FirstOrDefault();

                procedures.Add(new Procedure()
                {
                    Number = (sourceProcedure.Prseries ?? "") + (sourceProcedure.Prnum.HasValue ? " " + sourceProcedure.Prnum.ToString() : ""),
                    Title = sourceProcedure.Prtitle,
                    Hyperlink = procedureResource == null? null : procedureResource.PrrhyperlinkText,
                    RevisionNumber = sourceProcedure.Prrevision.ToString(),
                    Version_Procedures = getVersions(sourceProcedure),
                    EffectiveDate = DateOnly.FromDateTime(sourceProcedure.PrrevDate.GetValueOrDefault()),
                    Procedure_EnablingObjective_Links = getProcedureEnablingObjectiveLinks(sourceProcedure),
                    ILA_Procedure_Links = getISLAProcedureLinks(sourceProcedure),
                    Procedure_ILA_Links = getProcedureIlaLinks(sourceProcedure),
                    Procedure_Task_Links = getProcedureTaskLinks(sourceProcedure),
                    ProcedureReviews = getProcedureReviews(sourceProcedure),
                    IsDeleted = false,
                    IsPublished = true,
                    Deleted = false,
                    Active = !sourceProcedure.Prinactive
                });
            }

            return procedures;
        }

        private DateTime getProcedureEffectiveDate(TblProcedure sourceProcedure)
        {
            var dateByProcedureTable = sourceProcedure.PrStartDate.HasValue ? sourceProcedure.PrStartDate.Value : new System.DateTime(1900, 1, 1);
            var dateByRevisions = _procedureHistories.Where(r => r.Prid == sourceProcedure.Prid).Count() == 0 ? new System.DateTime(1900, 1, 1) : _procedureHistories.Where(r => r.Prid == sourceProcedure.Prid).Max(r => r.PrrevDate.GetValueOrDefault());

            return dateByProcedureTable > dateByRevisions ? dateByProcedureTable : dateByRevisions;
        }

        private ICollection<Version_Procedure> getVersions(TblProcedure sourceProcedure)
        {
            List<Version_Procedure> version_Procedures = new List<Version_Procedure>();

            var sourceHistories = _procedureHistories.Where(r => r.Prid == sourceProcedure.Prid);

            foreach(var sourceHistory in sourceHistories)
            {
                version_Procedures.Add(new Version_Procedure()
                {
                    VersionNumber = sourceHistory.Prrevision.ToString(),
                    Title = sourceHistory.Prtitle,
                    ProcedureNumber = sourceHistory.Prnum.GetValueOrDefault().ToString(),
                    Active = sourceHistory.Prrevision == sourceProcedure.Prrevision
                });
            }

            return version_Procedures;
        }

        private ICollection<ProcedureReview> getProcedureReviews(TblProcedure sourceProcedure)
        {
            List<ProcedureReview> procedureReviews = new List<ProcedureReview>();
            var sourceProcedureReviews = _procedureReviews
                .Where(r => r.Prid == sourceProcedure.Prid)
                .Select(r => new { r.ProcStartDateAtRel, r.ProcEndDateAtRel, r.Prid  }).Distinct().ToList();

            foreach(var sourceProcedureReview in sourceProcedureReviews)
            {
                string reviewInstructions = (sourceProcedure.PrEmpReleaseText ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(sourceProcedure.PrEmpReleaseText ?? "") : sourceProcedure.PrEmpReleaseText;

                procedureReviews.Add(new ProcedureReview()
                {
                     ProcedureReviewTitle = null,
                     IsEmployeeShowResponses = true,
                     StartDateTime = sourceProcedureReview.ProcStartDateAtRel.GetValueOrDefault().ToQtsTime(false),
                     EndDateTime = sourceProcedureReview.ProcEndDateAtRel.GetValueOrDefault().ToQtsTime(false),
                     IsPublished = true,
                     ProcedureReviewInstructions = reviewInstructions,
                     ProcedureReview_Employee = getProcedureReviewEmployees(_procedureReviews.Where(r => r.Prid == sourceProcedureReview.Prid &&  r.ProcEndDateAtRel == sourceProcedureReview.ProcEndDateAtRel && r.ProcStartDateAtRel == sourceProcedureReview.ProcStartDateAtRel).ToList())
                });
            }

            return procedureReviews;
        }

        private ICollection<ProcedureReview_Employee> getProcedureReviewEmployees(List<TblProcReleaseEmpSummary> sourceProcedureReviews)
        {
            List<ProcedureReview_Employee> employees = new List<ProcedureReview_Employee>();

            foreach(var sourceProcedureReview in sourceProcedureReviews)
            {
                var sourceEmployee = (_source as EMP_DemoContext).TblEmployees.Where(r => r.Eid == sourceProcedureReview.Eid).FirstOrDefault();
                if (sourceEmployee == null) continue;

                var targetEmployee = (_target as QTD2.Data.QTDContext).Employees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();


                employees.Add(new ProcedureReview_Employee()
                {
                    EmployeeId = targetEmployee.Id,
                    IsCompleted = sourceProcedureReview.Complete,
                    IsStarted = sourceProcedureReview.Started,
                    ProcedureReviewResponse = sourceProcedureReview.EmployeeProcResponse.GetValueOrDefault(),
                    CompletedDate = sourceProcedureReview.CompletedDate,
                    Comments = sourceProcedureReview.PosComments,
                    Active = true
                });
            }

            return employees;
        }

        private ICollection<Procedure_Task_Link> getProcedureTaskLinks(TblProcedure sourceProcedure)
        {
            List<Procedure_Task_Link> links = new List<Procedure_Task_Link>();

            var sources = (_source as EMP_DemoContext).RsTblProceduresTasks.Where(r => r.Prid == sourceProcedure.Prid).ToList();

            foreach (var taskLink in sources)
            {
                var sourceTask = (_source as EMP_DemoContext).TblTasks.Where(r => r.Tid == taskLink.Tid).FirstOrDefault();

                if (sourceTask == null) continue;

                var sourceDutyArea = sourceTask.Da;
                var targetTask = (_target as QTD2.Data.QTDContext)
                    .DutyAreas.Where(r => r.Number == sourceDutyArea.Danum).Where(r => r.Letter == sourceDutyArea.Daletter).First()
                    .SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
                    .Tasks.Where(r => r.Number == sourceTask.Tnum).First();

                links.Add(new Procedure_Task_Link()
                {
                    TaskId = targetTask.Id,
                    Active = true,
                    Deleted = false
                });
            }

            return links;
        }

        private ICollection<Procedure_ILA_Link> getProcedureIlaLinks(TblProcedure sourceProcedure)
        {
            List<Procedure_ILA_Link> links = new List<Procedure_ILA_Link>();

            var sources = _courseProcedureLinks.Where(r => r.Prid == sourceProcedure.Prid);

            foreach (var source in sources)
            {

                var sourceIla = (_source as EMP_DemoContext).TblCourses.Where(r => r.Corid == source.Corid).FirstOrDefault();

                if (sourceIla == null) continue;

                var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceIla.Cornum).First();

                links.Add(new Procedure_ILA_Link()
                {
                    ILAId = targetIla.Id
                });
            }

            return links;
        }

        private ICollection<ILA_Procedure_Link> getISLAProcedureLinks(TblProcedure sourceProcedure)
        {
            List<ILA_Procedure_Link> links = new List<ILA_Procedure_Link>();

            var sources = _courseProcedureLinks.Where(r => r.Prid == sourceProcedure.Prid);

            foreach (var source in sources)
            {
                var sourceIla = (_source as EMP_DemoContext).TblCourses.Where(r => r.Corid == source.Corid).FirstOrDefault();

                if (sourceIla == null) continue;

                var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceIla.Cornum).First();

                links.Add(new ILA_Procedure_Link()
                {
                    ILAId = targetIla.Id
                });
            }

            return links;
        }

        private ICollection<Procedure_EnablingObjective_Link> getProcedureEnablingObjectiveLinks(TblProcedure sourceProcedure)
        {
            List<Procedure_EnablingObjective_Link> links = new List<Procedure_EnablingObjective_Link>();
            var source = (_source as EMP_DemoContext).TblSkProcedures.Where(r => r.ProcId == sourceProcedure.Prid);

            foreach (var s in source)
            {
                var sourceEo = s.Sk;
                EnablingObjective targetEo = new EnablingObjective();
                if (sourceEo.Sknum == 0)
                {
                    targetEo = (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Where(r => r.Number == sourceEo.CidNavigation.Cnum).First()
                                                    .EnablingObjectives.Where(r => r.Number == sourceEo.SksubNum.Value.ToString()).First();
                }
                else
                {
                    targetEo = (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Where(r => r.Number == sourceEo.CidNavigation.Cnum).First()
                                .EnablingObjective_SubCategories.Where(r => r.Number == sourceEo.CidNavigation.CsubNum).First()
                                .EnablingObjective_Topics.Where(r => r.Number == sourceEo.Sknum).First()
                                .EnablingObjectives.Where(r => r.Number == sourceEo.SksubNum.Value.ToString()).First();
                }

                links.Add(new Procedure_EnablingObjective_Link()
                {
                    Active = true,
                    EnablingObjectiveId = targetEo.Id
                });
            }

            var proceduresTasksHistoriesFromPTasks = (_source as EMP_DemoContext).TblProceduresTasksHistories.ToList();        

            foreach (var procedure in proceduresTasksHistoriesFromPTasks)
            {

                var sourceTask = _tasks.Where(r => r.Tid == procedure.Tid).First();
                var sourceDutyArea = sourceTask.Da;

                var targetEo = (_target as QTD2.Data.QTDContext)
                 .EnablingObjective_SubCategories.Where(r => r.Title == sourceDutyArea.Daletter + " " + sourceDutyArea.Danum + " " + sourceDutyArea.DasubNum).First()
                 .EnablingObjectives.Where(r => r.Description == (sourceTask.TsubNum.HasValue ? sourceTask.TsubNum.Value.ToString() : "NoNum")).First();

                links.Add(new Procedure_EnablingObjective_Link()
                {
                    Active = true,
                    EnablingObjectiveId = targetEo.Id
                });
            }

            return links;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _issuingauthority.Count();
        }

        protected override void updateTarget(Procedure_IssuingAuthority record)
        {
            (_target as QTD2.Data.QTDContext).Procedure_IssuingAuthorities.Add(record);
        }
    }
}
