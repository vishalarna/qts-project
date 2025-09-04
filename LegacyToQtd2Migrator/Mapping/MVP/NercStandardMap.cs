using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class NercStandardMap : Common.MigrationMap<TblNercstandard, NercStandard>
    {
        List<TblNercstandard> _nercStandard;

        public NercStandardMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblNercstandard> getSourceRecords()
        {
            _nercStandard = (_source as EMP_DemoContext).TblNercstandards.ToListAsync().Result;
            return _nercStandard;
        }

        protected override NercStandard mapRecord(TblNercstandard obj)
        {
            return new NercStandard()
            {
                ILA_NercStandard_Links = getIlaLinks(obj),
                Name = obj.Nsname
            };
        }

        private ICollection<ILA_NercStandard_Link> getIlaLinks(TblNercstandard obj)
        {
            var links = new List<ILA_NercStandard_Link>();
            var sourceLinks = (_source as EMP_DemoContext).TblIlaNercstandards.Where(r => r.Nsid == obj.Nsid).ToList();

            foreach (var sourceLink in sourceLinks)
            {
                var sourceIla = (_source as EMP_DemoContext).TblCourses.Where(r => r.Corid == sourceLink.Corid).First();
                var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceIla.Cornum).First();

                links.Add(new ILA_NercStandard_Link()
                {
                    ILAId = targetIla.Id,
                    //StdId
                    //NERCStdMemberId
                    //CreditHoursByStd
                    //CreatedBy
                    //CreatedDate
                    //ModifiedBy
                    //ModifiedDate
                    Deleted = false,
                    Active = true
                });
            }

            return links;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _nercStandard.Count();
        }

        protected override void updateTarget(NercStandard record)
        {
            (_target as QTD2.Data.QTDContext).NercStandards.Add(record);
        }
    }
}
