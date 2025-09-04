using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class SubdutyAreasMap : Common.MigrationMap<TblDutyArea, SubdutyArea>
    {
        List<TblDutyArea> _subDutyAreas;
        public SubdutyAreasMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblDutyArea> getSourceRecords()
        {
            _subDutyAreas = (_source as EMP_DemoContext).TblDutyAreas.ToListAsync().Result;
            return _subDutyAreas;
        }

        protected override SubdutyArea mapRecord(TblDutyArea obj)
        {
            return new SubdutyArea()
            {
                Active = true,
                DutyAreaId=obj.Daid,
                Description=obj.Dadesc,
                SubNumber=obj.DasubNum??-1,
                //Title,
                //EffectiveDate,
                //ReasonForRevision
                Deleted=false,
                Tasks= getTasks()
            };
        }

        private ICollection<Task> getTasks()
        {
            List<Task> task = new List<Task>();

            task.Add(new Task()
            {
                //SubdutyAreaId,
                //Description
                //Abreviation 
                //Number
                //Criteria,
                //TaskCriteriaUpload
                //RequalificationDueDate,
                //RequalificationRequired,
                //RequalificationNotes
                //Image
                //Critical 
                //References 
                //RequiredTime,
                //IsMeta,
                //IsReliability
                //Conditions
                //EffectiveDate
                Deleted = false,
                Active = true
            });

            return task;
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _subDutyAreas.Count();
        }

        protected override void updateTarget(SubdutyArea record)
        {
            (_target as QTD2.Data.QTDContext).SubdutyAreas.Add(record);
        }

    }
}
