using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class SaftyHazard_CategoriesMap : Common.MigrationMap<TblSafetyHazardCategory, SaftyHazard_Category>
    {
        List<TblSafetyHazardCategory> _safetyhazardcategory;
        List<TblSafetyHazard> _saftyhazard;
        List<TblSafetyHazardAbatement> _saftyhazardabatment;
        List<TblSafetyHazardControl> _saftyhazardcontrol;

        public SaftyHazard_CategoriesMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblSafetyHazardCategory> getSourceRecords()
        {
            _safetyhazardcategory = (_source as EMP_DemoContext).TblSafetyHazardCategories.ToListAsync().Result;
            _saftyhazard = (_source as EMP_DemoContext).TblSafetyHazards.ToListAsync().Result;
            _saftyhazardabatment = (_source as EMP_DemoContext).TblSafetyHazardAbatements.ToListAsync().Result;
            _saftyhazardcontrol = (_source as EMP_DemoContext).TblSafetyHazardControls.ToListAsync().Result;
            return _safetyhazardcategory;
        }

        protected override SaftyHazard_Category mapRecord(TblSafetyHazardCategory obj)
        {
            return new SaftyHazard_Category()
            {
                Active = true,
                Title = obj.Hzcategory,
                Number = obj.Hzcid,
                //Notes = obj.n
                //EffectiveDate
                Description = obj.Hzcategory,
                Deleted = false,
                SaftyHazards = getSaftyHazards(obj)
            };
        }

        protected ICollection<SaftyHazard> getSaftyHazards(TblSafetyHazardCategory obj)
        {
            List<SaftyHazard> saftyHazards = new List<SaftyHazard>();

            foreach (var saftyhazard in obj.TblSafetyHazards)
            {
                saftyHazards.Add(new SaftyHazard()
                {
                    Title = saftyhazard.Shztitle,
                    Number = saftyhazard.Shznum,
                    //EffectiveDate,F
                    Text = saftyhazard.Shzdesc,
                    Deleted = false,
                    Active = true,
                    SafetyHazard_Set_Links = getSaftyHazardSetLinks(saftyhazard),
                    SaftyHazard_Abatements = getSaftyHazard_Abatements(saftyhazard),
                    SaftyHazard_Controls = getSaftyHazard_Controls(saftyhazard),
                    SafetyHazard_EO_Links = getEnablingObjectiveLinks(saftyhazard),
                    SafetyHazard_Task_Links = getTaskLinks(saftyhazard)
                });
            }
            return saftyHazards;
        }

        private ICollection<SafetyHazard_Set_Link> getSaftyHazardSetLinks(TblSafetyHazard saftyhazard)
        {
            List<SafetyHazard_Set_Link> links = new List<SafetyHazard_Set_Link>();

            links.Add(new SafetyHazard_Set_Link()
            {
                SafetyHazardSet = new SafetyHazard_Set()
                {
                    SafetyHzAbatementText = string.Join(",", saftyhazard.TblSafetyHazardAbatements.Select(r => r.Shzabatement)),
                    SafetyHzControlsText = string.Join(",", saftyhazard.TblSafetyHazardControls.Select(r => r.Shzcontrol))
                }
            });


            return links;
        }

        private ICollection<SafetyHazard_Task_Link> getTaskLinks(TblSafetyHazard saftyhazard)
        {
            List<SafetyHazard_Task_Link> links = new List<SafetyHazard_Task_Link>();

            var sourceLinks = (_source as EMP_DemoContext).TblSafetyHazardTasks.Where(r => r.Shzid == saftyhazard.Shzid).ToList();

            foreach (var sourceLink in sourceLinks)
            {
                var sourceTask = (_source as EMP_DemoContext).TblTasks.Where(r => r.Tid == sourceLink.Tid).First();
                var sourceDutyArea = (_source as EMP_DemoContext).TblDutyAreas.Where(r => r.Daid == sourceTask.Daid).First();
                var targetTask = (_target as QTD2.Data.QTDContext)
                    .DutyAreas.Where(r => r.Letter == sourceDutyArea.Daletter && r.Number == sourceDutyArea.Danum).First()
                    .SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
                    .Tasks.Where(r => r.Number == sourceTask.Tnum).First();

                links.Add(new SafetyHazard_Task_Link()
                {
                    TaskId = targetTask.Id
                });
            }

            return links;
        }

        private ICollection<SafetyHazard_EO_Link> getEnablingObjectiveLinks(TblSafetyHazard saftyhazard)
        {
            List<SafetyHazard_EO_Link> links = new List<SafetyHazard_EO_Link>();

            var sourceLinks = (_source as EMP_DemoContext).TblSafetyHazardEos.Where(r => r.Shzid == saftyhazard.Shzid).ToListAsync().Result;

            foreach (var sourceLink in sourceLinks)
            {
                var sourceEo = (_source as EMP_DemoContext).TblSkillsKnowledges.Where(r => r.Skid == sourceLink.Skid).First();
                var sourceCateogry = (_source as EMP_DemoContext).TblCategories.Where(r => r.Cid == sourceEo.Cid).First();
                var targetEo = (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Where(r => r.Number == sourceCateogry.Cnum).First()
                                .EnablingObjective_SubCategories.Where(r => r.Number == sourceCateogry.CsubNum).First()
                                .EnablingObjective_Topics.Where(r => r.Number == sourceEo.Sknum).First()
                                .EnablingObjectives.Where(r => r.Number == sourceEo.SksubNum.Value.ToString()).First();

                links.Add(new SafetyHazard_EO_Link()
                {
                    EOID = targetEo.Id
                });
            }

            return links;
        }

        private ICollection<SaftyHazard_Abatement> getSaftyHazard_Abatements(TblSafetyHazard obj)
        {
            List<SaftyHazard_Abatement> saftyHazard_Abatements = new List<SaftyHazard_Abatement>();

            foreach (var abatement in obj.TblSafetyHazardAbatements)
            {
                saftyHazard_Abatements.Add(new SaftyHazard_Abatement()
                {
                    Active = true,
                    Deleted = false,
                    SaftyHazardId = abatement.Shzid,
                    Number = abatement.Anum,
                    Description = abatement.Shzabatement
                });
            }

            return saftyHazard_Abatements;
        }
        private ICollection<SaftyHazard_Control> getSaftyHazard_Controls(TblSafetyHazard obj)
        {
            List<SaftyHazard_Control> saftyHazard_Controls = new List<SaftyHazard_Control>();

            foreach (var control in obj.TblSafetyHazardControls)
            {
                saftyHazard_Controls.Add(new SaftyHazard_Control()
                {
                    Active = true,
                    Deleted = false,
                    Number = control.Cnum,
                    SaftyHazardId = control.Shzid,
                    Description = control.Shzcontrol
                });
            }

            return saftyHazard_Controls;
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _safetyhazardcategory.Count();
        }

        protected override void updateTarget(SaftyHazard_Category record)
        {
            (_target as QTD2.Data.QTDContext).SaftyHazard_Categories.Add(record);
        }
    }
}
