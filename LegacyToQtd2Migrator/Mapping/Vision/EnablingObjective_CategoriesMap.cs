using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.Vision.Data;
using LegacyToQtd2Migrator.Helpers;
using System;
using RtfPipe.Tokens;
using QTD2.Data;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class EnablingObjective_CategoriesMap : Common.MigrationMap<ObjectiveImpl, EnablingObjective_Category>
    {
        int _projectId;

        List<ObjectiveHierarchy> _sourceObjectiveHierarchies;

        List<ObjectiveImpl> _sourceEnablingObjectiveCategories;
        List<ObjectiveImpl> _sourceEnablingObjectiveSubCategories;
        List<ObjectiveImpl> _sourceEnablingObjectives;
        List<ObjectiveImpl> _sourceEosFromTasks;

        List<Task> _targetTasks;

        public EnablingObjective_CategoriesMap(DbContext source, DbContext target, int projectId) : base(source, target)
        {
            _projectId = projectId;
        }

        protected override List<ObjectiveImpl> getSourceRecords()
        {
            _sourceObjectiveHierarchies = (_source as VisionContext).ObjectiveHierarchies.ToList();
            _sourceEnablingObjectiveCategories = (_source as VisionContext).GetVisionObjectiveImps(_projectId, "Organizer", new List<System.Func<ObjectiveImpl, bool>>() { r => r.Id == 8731 }).ToList();

            List<decimal> subCategoriesIds = _sourceObjectiveHierarchies.Where(r => r.FkParent == _sourceEnablingObjectiveCategories.First().FkObjective).Select(r => r.FkChild).ToList();

            _sourceEnablingObjectiveSubCategories = (_source as VisionContext).GetVisionObjectiveImps(_projectId, "Organizer", new List<System.Func<ObjectiveImpl, bool>>() { r => subCategoriesIds.Contains(r.FkObjective) }).ToList();

            List<decimal> eoIds = _sourceObjectiveHierarchies.Where(r => subCategoriesIds.Contains(r.FkParent)).Select(r => r.FkChild).ToList();

            _sourceEnablingObjectives = (_source as VisionContext).GetVisionObjectiveImps(_projectId, "Cognitive Terminal", new List<System.Func<ObjectiveImpl, bool>>() { r => eoIds.Contains(r.FkObjective) }).ToList();
            _sourceEnablingObjectives.AddRange((_source as VisionContext).GetVisionObjectiveImps(_projectId, "Cognitive Enabler", new List<System.Func<ObjectiveImpl, bool>>() { r => eoIds.Contains(r.FkObjective) }).ToList());
            _sourceEnablingObjectives.AddRange((_source as VisionContext).GetVisionObjectiveImps(_projectId, "Performance Enabler", new List<System.Func<ObjectiveImpl, bool>>() { r => eoIds.Contains(r.FkObjective) }).ToList());

            _sourceEosFromTasks = (_source as VisionContext).GetVisionObjectiveImps(_projectId, "Performance Terminal", null).ToList();

            _targetTasks = (_target as QTDContext).Tasks.ToList();

            return _sourceEnablingObjectiveCategories;
        }

        protected override EnablingObjective_Category mapRecord(ObjectiveImpl obj)
        {
            var category = new EnablingObjective_Category()
            {
                Title = obj.Text.RtfToPlainText(),
                Number = 1,
                Description = obj.Text.RtfToPlainText(),
                Deleted = false,
                Active = true
            };


            category.EnablingObjective_SubCategories = getEnablingObjective_SubCategories(obj, category);

            return category;
        }

        private ICollection<EnablingObjective_SubCategory> getEnablingObjective_SubCategories(ObjectiveImpl obj, EnablingObjective_Category category)
        {
            List<EnablingObjective_SubCategory> enablingObjective_SubCategories = new List<EnablingObjective_SubCategory>();

            var subCategories = _sourceEnablingObjectiveSubCategories;

            foreach (var sc in subCategories)
            {
                int idex = subCategories.IndexOf(sc);

                var subCategory = new EnablingObjective_SubCategory()
                {
                    Active = true,
                    Deleted = false,
                    Title = sc.Text.RtfToPlainText(),
                    Description = sc.Text.RtfToPlainText(),
                    Number = subCategories.IndexOf(sc) + 1,
                    EnablingObjectives = getEnablingObjectiveToSubcategoriesDirectly(sc, category)
                };

                enablingObjective_SubCategories.Add(subCategory);
            }

            return enablingObjective_SubCategories;
        }

        private ICollection<EnablingObjective> getEnablingObjectiveToSubcategoriesDirectly(ObjectiveImpl obj, EnablingObjective_Category category)
        {
            List<EnablingObjective> enablingObjectives = new List<EnablingObjective>();

            var sourceHeirarchies = _sourceObjectiveHierarchies.Where(r => r.FkParent == obj.FkObjective);
            var souceEos = _sourceEnablingObjectives
                                .Where(r => sourceHeirarchies.Select(r => r.FkChild).ToList().Contains(r.FkObjective))
                                .ToList();

            foreach (var sourceEo in souceEos)
            {
                enablingObjectives.Add(new EnablingObjective()
                {
                    Active = true,
                    Deleted = false,
                    Description = sourceEo.Text.RtfToPlainText(),
                    Number = (souceEos.IndexOf(sourceEo) + 1).ToString(),
                    EnablingObjective_Category = category,
                    Task_EnablingObjective_Links = getTaskLinks(sourceEo)
                });
            }

            return enablingObjectives;
        }

        private ICollection<Task_EnablingObjective_Link> getTaskLinks(ObjectiveImpl obj)
        {
            List<Task_EnablingObjective_Link> links = new List<Task_EnablingObjective_Link>();

            var sourceHeirarchies = _sourceObjectiveHierarchies.Where(r => r.FkParent == obj.FkObjective);
            var sourceTaskLinks = _sourceEosFromTasks
                                    .Where(r => sourceHeirarchies.Select(r => r.FkChild).ToList().Contains(r.FkObjective))
                                .ToList();

            foreach (var sourceTaskLink in sourceTaskLinks)
            {
                var targetTasks = _targetTasks.Where(r => r.Description.Trim() == sourceTaskLink.Text.RtfToPlainText()).ToList();

                if (targetTasks.Count() > 1)
                {
                    string s = "stop";
                }

                var targetTask = targetTasks.FirstOrDefault();

                if (targetTask == null) continue;

                links.Add(new Task_EnablingObjective_Link()
                {
                    TaskId = targetTask.Id,
                    Active = true,
                    Deleted = false
                });
            }

            return links;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _sourceEnablingObjectiveCategories.Count();
        }

        protected override void updateTarget(EnablingObjective_Category record)
        {
            (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Add(record);
        }
    }
}
