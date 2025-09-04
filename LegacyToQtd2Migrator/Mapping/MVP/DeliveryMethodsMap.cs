using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class DeliveryMethodsMap : Common.MigrationMap<TblCourse, DeliveryMethod>
    {
        List<TblCourse> _delieveryMethod;
        List<DeliveryMethod> _targetDeliveryMethods = new List<DeliveryMethod>();

        public DeliveryMethodsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblCourse> getSourceRecords()
        {
            _delieveryMethod = (_source as EMP_DemoContext).TblCourses.Where(r => !String.IsNullOrEmpty(r.TypeOtherSpecify)).ToListAsync().Result;
            return _delieveryMethod;
        }

        protected override DeliveryMethod mapRecord(TblCourse obj)
        {
            if (_targetDeliveryMethods.Where(r => r.Name.ToUpper() == obj.TypeOtherSpecify.ToUpper()).Count() > 0)
                return null;

            var dm = new DeliveryMethod()
            {
                Active = true,
                Name = obj.TypeOtherSpecify.Trim(),
                DisplayName = obj.TypeOtherSpecify.Trim(),
                IsAvailableForAllIlas = true,
                IsNerc = true
            };

            _targetDeliveryMethods.Add(dm);

            return dm;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _delieveryMethod.Count();
        }

        protected override void updateTarget(DeliveryMethod record)
        {
            if (record == null) return;

            (_target as QTD2.Data.QTDContext).DeliveryMethods.Add(record);
        }
    }
}
