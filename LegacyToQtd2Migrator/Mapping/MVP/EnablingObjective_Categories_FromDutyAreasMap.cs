using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    //this class is for pseudo tasks to EOs
    public class EnablingObjective_Categories_FromDutyAreasMap : Common.MigrationMap<TblDutyArea, EnablingObjective_Category>
    {
        List<TblDutyArea> _dutyAreas;
        List<TblDutyArea> _subDutyAreas;

        public EnablingObjective_Categories_FromDutyAreasMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblDutyArea> getSourceRecords()
        {
            _dutyAreas = (_source as EMP_DemoContext).TblDutyAreas.Where(r => r.Daletter == "P").Where(r => !r.DasubNum.HasValue || r.DasubNum.Value == 0).ToList();
            _subDutyAreas = (_source as EMP_DemoContext).TblDutyAreas.Where(r => r.Daletter == "P").Where(r => r.DasubNum.HasValue && r.DasubNum.Value != 0).ToList();

            return _dutyAreas;
        }

        protected override EnablingObjective_Category mapRecord(TblDutyArea obj)
        {
            var nextCategoryNumber = (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Max(r => r.Number) + 1;
            List<TblDutyArea> others = _subDutyAreas.Where(r => r.Daletter == obj.Daletter).Where(r => r.Danum == obj.Danum).ToList();

            return new EnablingObjective_Category()
            {
                Title = obj.Dadesc,
                Number = nextCategoryNumber,
                Description = obj.Dadesc,
                Deleted = false,
                Active = true,
                EnablingObjective_SubCategories = getEnablingObjective_SubCategories(others)
            };
        }

        private ICollection<EnablingObjective_SubCategory> getEnablingObjective_SubCategories(List<TblDutyArea> subCategories)
        {
            List<EnablingObjective_SubCategory> enablingObjective_SubCategories = new List<EnablingObjective_SubCategory>();

            foreach (var sc in subCategories)
            {
                var tasks = (_source as EMP_DemoContext).TblTasks.Where(r => r.Daid == sc.Daid).ToList();
                enablingObjective_SubCategories.Add(new EnablingObjective_SubCategory()
                {
                    Active = true,
                    Deleted = false,
                    Title = sc.Daletter + " " + sc.Danum + " " + sc.DasubNum,
                    Description = sc.Dadesc,
                    Number = sc.DasubNum,
                    EnablingObjectives = getEnablingObjectives(tasks)
                });
            }

            return enablingObjective_SubCategories;
        }

        private ICollection<EnablingObjective> getEnablingObjectives(List<TblTask> sourceTasks)
        {
            List<EnablingObjective> enablingObjectives = new List<EnablingObjective>();

            foreach (var sourceTask in sourceTasks)
            {
                enablingObjectives.Add(new EnablingObjective()
                {
                    Active = !sourceTask.Inactive.GetValueOrDefault(),
                    Deleted = false,
                    Description = sourceTask.Tdesc,
                    Number = sourceTask.TsubNum.HasValue ? sourceTask.TsubNum.Value.ToString() : "NoNum",
                    //isMetaEO
                    //IsSkillQualification
                    //EffectiveDate
                });
            }

            return enablingObjectives;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _dutyAreas.Count();
        }

        protected override void updateTarget(EnablingObjective_Category record)
        {
            (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Add(record);
        }
    }
}
