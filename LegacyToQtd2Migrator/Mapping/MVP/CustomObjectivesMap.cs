using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class CustomObjectivesMap : Common.MigrationMap<TblObjectivesUserAdd, CustomEnablingObjective>
    {
        List<TblObjectivesUserAdd> _objectivesuseradd;

        public CustomObjectivesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblObjectivesUserAdd> getSourceRecords()
        {
            _objectivesuseradd = (_source as EMP_DemoContext).TblObjectivesUserAdds.ToListAsync().Result;
            return _objectivesuseradd;
        }

        protected override CustomEnablingObjective mapRecord(TblObjectivesUserAdd obj)
        {
            var sourceIla = (_source as EMP_DemoContext).TblCourses.Where(r => r.Corid == obj.ObjCorid).First();
            //var sourceTopic = sourceCustomObjective

            var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceIla.Cornum).First();
            //var targetEoTopic = 

            return new CustomEnablingObjective()
            {
                IsAddtoEO = obj.ObjIsAdded.GetValueOrDefault(),
                CustomEONumber = obj.ObjId,
                Description = obj.ObjText,
                Active = true,
                //EO_TopicId = 1,
                ILACustomObjective_Links = targetIla == null ? null : new List<ILACustomObjective_Link>()
                 {
                     new ILACustomObjective_Link()
                     {
                         ILAId = targetIla.Id
                     }
                 }
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _objectivesuseradd.Count();
        }

        protected override void updateTarget(CustomEnablingObjective record)
        {
            (_target as QTD2.Data.QTDContext).CustomEnablingObjectives.Add(record);
        }
    }
}
