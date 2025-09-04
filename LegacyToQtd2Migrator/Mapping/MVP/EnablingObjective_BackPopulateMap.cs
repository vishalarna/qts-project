using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class EnablingObjective_BackPopulateMap : Common.MigrationMap<EnablingObjective, EnablingObjective>
    {
        List<EnablingObjective> _enablingObjectives; 
        List<TblCategory> _categories;

        public EnablingObjective_BackPopulateMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<EnablingObjective> getSourceRecords()
        {
            _enablingObjectives = (_target as QTD2.Data.QTDContext).EnablingObjectives.ToList();
            _categories = _categories = (_source as EMP_DemoContext).TblCategories.ToList();
            return _enablingObjectives;
        }
        protected override EnablingObjective mapRecord(EnablingObjective obj)
        {
            var topic = (_target as QTD2.Data.QTDContext).EnablingObjective_Topics.Where(r => r.Id == obj.TopicId).FirstOrDefault();

            if (topic == null)
            {
                var targetCategory = (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Where(r => r.Number == obj.CategoryId).First();

                if (obj.SubCategoryId == 0)
                {
                    obj.CategoryId = targetCategory.Id;   
                }
                else
                {
                    var targetSubCategory = (_target as QTD2.Data.QTDContext).EnablingObjective_SubCategories.Where(r => r.CategoryId == targetCategory.Id).Where(r => r.Number == obj.SubCategoryId ).First();
                    obj.CategoryId = targetCategory.Id;
                    obj.SubCategoryId = targetSubCategory.Id;
                }         
            }
            else
            {
                obj.SubCategoryId = topic.SubCategoryId;

                var subCategory = (_target as QTD2.Data.QTDContext).EnablingObjective_SubCategories.Where(r => r.Id == topic.SubCategoryId).First();
                obj.CategoryId = subCategory.CategoryId;
            }

            return obj;
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _enablingObjectives.Count();
        }
        protected override void updateTarget(EnablingObjective record)
        {
            (_target as QTD2.Data.QTDContext).EnablingObjectives.Update(record);
        }

    }
}
