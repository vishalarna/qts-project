using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Test_Item_LinksMap : Common.MigrationMap<TblTestItem, Test_Item_Link>
    {
        List<TblTestItem> _tblTestItem;

        public Test_Item_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTestItem> getSourceRecords()
        {
            _tblTestItem = (_source as EMP_DemoContext).TblTestItems.ToListAsync().Result;
            return _tblTestItem;
        }

        protected override Test_Item_Link mapRecord(TblTestItem obj)
        {
            return new Test_Item_Link()
            {
                Active = true,
                TestItemId=obj.TestItemId,
                //TestId,
                //Sequence
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblTestItem.Count();
        }

        protected override void updateTarget(Test_Item_Link record)
        {
            (_target as QTD2.Data.QTDContext).Test_Item_Links.Add(record);
        }
    }
}
