using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class EnablingObjective_CategoriesMap : Common.MigrationMap<TblCategory, EnablingObjective_Category>
    {
        List<TblCategory> _categories;
        List<TblSkillsKnowledge> _skillsknowledge;
        List<TblCourseSegmentLearningObjective> _segments;
        List<RsTblTasksSkillsKnowledge> _taskSkillKnowledgeLinks;

        public EnablingObjective_CategoriesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblCategory> getSourceRecords()
        {
            _skillsknowledge = (_source as EMP_DemoContext).TblSkillsKnowledges.ToListAsync().Result;
            _categories = (_source as EMP_DemoContext).TblCategories.Where(r => r.CsubNum == 0).ToListAsync().Result;
            _segments = (_source as EMP_DemoContext).TblCourseSegmentLearningObjectives.Where(r => r.ObjType == "EO").ToListAsync().Result;
            _taskSkillKnowledgeLinks = (_source as EMP_DemoContext).RsTblTasksSkillsKnowledges.ToList();

            return _categories;
        }

        protected override EnablingObjective_Category mapRecord(TblCategory obj)
        {

            var category =  new EnablingObjective_Category()
            {
                Title = obj.Cdesc.Substring(0, Math.Min(obj.Cdesc.Length, 255)),
                Number = obj.Cnum ?? 0,
                Description = obj.Cdesc,
                Deleted = false,
                Active = true,              
                EnablingObjectives = getEnablingObjectiveToCategoriesDirectly(obj)
            };

            category.EnablingObjective_SubCategories = getEnablingObjective_SubCategories(obj, category);

            return category;
        }

        private ICollection<EnablingObjective> getEnablingObjectiveToCategoriesDirectly(TblCategory obj)
        {
            List<EnablingObjective> enablingObjectives = new List<EnablingObjective>();

            if (obj.CsubNum != 0) return enablingObjectives;

            foreach (var sk in obj.TblSkillsKnowledges.Where(r => r.Sknum.GetValueOrDefault() == 0))
            {
                if (!sk.SksubNum.HasValue) continue;

                enablingObjectives.Add(new EnablingObjective()
                {
                    Active = !sk.Inactive,
                    Deleted = false,
                    Description = sk.Skdesc,
                    Number = sk.SksubNum.Value.ToString(),
                    //isMetaEO
                    //IsSkillQualification
                    //EffectiveDate
                    CategoryId = obj.Cnum.GetValueOrDefault(),
                    Task_EnablingObjective_Links = getTaskLinks(sk)
                });
            }

            return enablingObjectives;
        }

        private ICollection<Task_EnablingObjective_Link> getTaskLinks(TblSkillsKnowledge sk)
        {
            List<Task_EnablingObjective_Link> links = new List<Task_EnablingObjective_Link>();

            var taskLinks = _taskSkillKnowledgeLinks.Where(r => r.Skid == sk.Skid).ToList();

            foreach (var taskLink in taskLinks)
            {
                var sourceTask = taskLink.TidNavigation;
                var sourceDutyArea = sourceTask.Da;
                var targetTask = (_target as QTD2.Data.QTDContext)
                    .DutyAreas.Where(r => r.Number == sourceDutyArea.Danum).Where(r => r.Letter == sourceDutyArea.Daletter).First()
                    .SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
                    .Tasks.Where(r => r.Number == sourceTask.Tnum).First();

                if (links.Where(r => r.TaskId == targetTask.Id).Count() > 0) continue;

                links.Add(new Task_EnablingObjective_Link()
                {
                    TaskId = targetTask.Id,
                    Active = true,
                    Deleted = false
                });
            }

            return links;
        }

        private ICollection<EnablingObjective_SubCategory> getEnablingObjective_SubCategories(TblCategory obj, EnablingObjective_Category category)
        {
            List<EnablingObjective_SubCategory> enablingObjective_SubCategories = new List<EnablingObjective_SubCategory>();

            var subCategories = (_source as EMP_DemoContext).TblCategories.Where(r => r.Cnum == obj.Cnum).Where(r => r.CsubNum != 0).ToList();

            foreach (var sc in subCategories)
            {
                var subCategory = new EnablingObjective_SubCategory()
                {
                    Active = true,
                    Deleted = false,
                    Title = sc.Cdesc.Substring(0, Math.Min(sc.Cdesc.Length, 50)),
                    Description = sc.Cdesc,
                    Number = sc.CsubNum,                    
                    EnablingObjectives = getEnablingObjectiveToSubcategoriesDirectly(sc)
                };

                subCategory.EnablingObjective_Topics = getEnablingObjective_Topics(sc, category, subCategory);

                enablingObjective_SubCategories.Add(subCategory);
            }

            return enablingObjective_SubCategories;
        }

        private ICollection<EnablingObjective> getEnablingObjectiveToSubcategoriesDirectly(TblCategory obj)
        {
            List<EnablingObjective> enablingObjectives = new List<EnablingObjective>();

            foreach (var sk in obj.TblSkillsKnowledges.Where(r => r.Sknum.HasValue).Where(r => r.Sknum.GetValueOrDefault() == 0))
            {
                enablingObjectives.Add(new EnablingObjective()
                {
                    Active = !sk.Inactive,
                    Deleted = false,
                    Description = sk.Skdesc,
                    Number = sk.SksubNum.HasValue ? sk.SksubNum.Value.ToString() : "NoNum",
                    //isMetaEO
                    //IsSkillQualification
                    //EffectiveDate
                    //CategoryId = obj.Cnum.GetValueOrDefault(),
                    //SubCategoryId = obj.CsubNum.GetValueOrDefault(),
                    Task_EnablingObjective_Links = getTaskLinks(sk)
                });
            }

            return enablingObjectives;
        }

        private ICollection<EnablingObjective_Topic> getEnablingObjective_Topics(TblCategory obj, EnablingObjective_Category category, EnablingObjective_SubCategory subCategory)
        {
            List<EnablingObjective_Topic> enablingObjective_Topics = new List<EnablingObjective_Topic>();

            var eots = _skillsknowledge.Where(r => r.Cid == obj.Cid).Where(r => !r.SksubNum.HasValue || r.SksubNum == 0);

            foreach (var eot in eots)
            {
                enablingObjective_Topics.Add(new EnablingObjective_Topic()
                {
                    Active = true,
                    Deleted = false,
                    Description = eot.Skdesc,
                    Number = eot.Sknum.HasValue ? eot.Sknum.Value : 0,
                    EnablingObjectives = getEnablingObjectives(_skillsknowledge.Where(r => r.Cid == eot.Cid).Where(r => r.Sknum == eot.Sknum).Where(r => r.SksubNum.HasValue && r.SksubNum != 0).ToList(), category, subCategory),
                    Title = eot.Skdesc.Substring(0, Math.Min(eot.Skdesc.Length, 255)),
                });
            }

            return enablingObjective_Topics;
        }

        private ICollection<EnablingObjective> getEnablingObjectives(List<TblSkillsKnowledge> skList, EnablingObjective_Category category, EnablingObjective_SubCategory subCategory)
        {
            List<EnablingObjective> enablingObjectives = new List<EnablingObjective>();

            foreach (var sk in skList.Where(r => r.Sknum.HasValue))
            {
                enablingObjectives.Add(new EnablingObjective()
                {
                    Active = !sk.Inactive,
                    Deleted = false,
                    Description = sk.Skdesc,
                    Number = sk.SksubNum.HasValue ? sk.SksubNum.Value.ToString() : "NoNum",
                    EnablingObjective_Employee_Links = getEnablingObjectiveEmployeeLinks(sk),
                    Task_EnablingObjective_Links = getTaskLinks(sk),
                    EnablingObjective_Category = category,
                    EnablingObjective_SubCategory = subCategory
                    //isMetaEO
                    //IsSkillQualification
                    //EffectiveDate
                });
            }

            return enablingObjectives;
        }

        private ICollection<EnablingObjective_Employee_Link> getEnablingObjectiveEmployeeLinks(TblSkillsKnowledge sk)
        {
            return new List<EnablingObjective_Employee_Link>();
        }

        private ICollection<EnablingObjective_Question> getEnablingObjective_Questions(TblSkillsKnowledge eot)
        {
            List<EnablingObjective_Question> enablingObjective_Questions = new List<EnablingObjective_Question>();

            enablingObjective_Questions.Add(new EnablingObjective_Question()
            {
                Active = true,
                Deleted = false,
                //EnablingObjectiveId
                //Question
                //Answer
                //QuestionNumber,
                //CreatedDate,
                //CreatedBy
                //ModifiedBy
                //ModifiedDate
            });

            return enablingObjective_Questions;
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _skillsknowledge.Count();
        }

        protected override void updateTarget(EnablingObjective_Category record)
        {
            (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Add(record);
        }
    }
}
