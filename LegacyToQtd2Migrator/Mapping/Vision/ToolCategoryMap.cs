using LegacyToQtd2Migrator.Helpers;
using LegacyToQtd2Migrator.Legacy.Data;
using LegacyToQtd2Migrator.Vision.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ProspectiveILASpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class ToolCategoryMap : Common.MigrationMap<XrefLibImpl, ToolCategory>
    {
        int _projectId;
        List<XrefLibImpl> _toolCategories;
        List<XrefLibImpl> _atcTools;
        List<XrefLibImpl> _humanPerformanceTools;

        public ToolCategoryMap(DbContext source, DbContext target, int projectId) : base(source, target)
        {
            _projectId = projectId;
        }

        protected override List<XrefLibImpl> getSourceRecords()
        {
            _toolCategories = (_source as VisionContext).XrefLibImpls
                    .Where(r => r.FkXrefLib == 320 || r.FkXrefLib == 6208)
                    .Where(r => r.FkExpiredBy == null)
                    .ToList();

            _atcTools = (_source as VisionContext).XrefLibLinks
                    .Where(r => r.FkParent == 320).Where(r => r.FkExpiredBy == null)
                    .SelectMany(r => r.FkItemNavigation.XrefLibImplFkXrefLibNavigations)
                    .Distinct()
                    .Where(r => r.FkExpiredBy == null).ToList();

            _humanPerformanceTools = (_source as VisionContext).XrefLibLinks
                   .Where(r => r.FkParent == 6208).Where(r => r.FkExpiredBy == null)
                   .SelectMany(r => r.FkItemNavigation.XrefLibImplFkXrefLibNavigations)
                   .Distinct()
                   .Where(r => r.FkExpiredBy == null).ToList();


            return _toolCategories;
        }

        protected override ToolCategory mapRecord(XrefLibImpl obj)
        {
            return new ToolCategory()
            {
                Active = true,
                Description = obj.Text.RtfToPlainText(),
                Title = obj.Text.RtfToPlainText(),
                Tools = getTools(obj)
            };
        }

        private List<Tool> getTools(XrefLibImpl obj)
        {
            var listTools = new List<Tool>();

            var sourceTools = obj.FkXrefLib == 320 ? _atcTools : _humanPerformanceTools;

            foreach (var sourceTool in sourceTools)
            {
                listTools.Add(new Tool()
                {
                    Active = true,
                    Deleted = false,
                    Number = (sourceTools.IndexOf(sourceTool) + 1).ToString(),
                    Name = sourceTool.Text.RtfToPlainText(),
                    //Hyperlink,
                    //EffectiveDate,
                    Description = sourceTool.Text.RtfToPlainText()
                    //Upload
                });
            }

            return listTools;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _toolCategories.Count();
        }

        protected override void updateTarget(ToolCategory record)
        {
            (_target as QTD2.Data.QTDContext).ToolCategories.Add(record);
        }
    }
}
