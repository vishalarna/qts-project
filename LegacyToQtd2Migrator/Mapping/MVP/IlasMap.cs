using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.Helpers;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class IlasMap : Common.MigrationMap<TblCourse, ILA>
    {
        List<TblCourse> _course;
        public IlasMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblCourse> getSourceRecords()
        {
            _course = (_source as EMP_DemoContext).TblCourses.ToListAsync().Result;
            return _course;
        }

        protected override ILA mapRecord(TblCourse obj)
        {
            return new ILA()
            {
                //Name
                //NickName
                //image
                //ProviderId
                //TopicId 
                //IsPublished
                //PublishDate
                //HasPilotData
                //IsProgramManual
                //SubmissionDate
                //EffectiveDate
                //CreatedBy
                //CreatedDate,
                //ModifiedBy,
                //ModifiedDate
                Number =obj.Cornum,
                Description=obj.Cordesc,
                TrainingPlan=obj.TrainingPlan,
                IsSelfPaced=obj.SelfPased,
                IsOptional= obj.IsOptional ?? false,
                DeliveryMethodId=obj.DeliveryMethodId,
                ApprovalDate= obj.CehApprovalDate.HasValue ? DateOnly.FromDateTime(obj.CehApprovalDate.Value): null,
                ExpirationDate = obj.CorexpDate.HasValue ? DateOnly.FromDateTime(obj.CorexpDate.Value) : null,
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _course.Count();
        }

        protected override void updateTarget(ILA record)
        {
            (_target as QTD2.Data.QTDContext).ILAs.Add(record);
        }
    }
}
