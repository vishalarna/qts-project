using LegacyToQtd2Migrator.Legacy.EmpAuth;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Mapping.EMPAuth
{
   public class InstancesMap : Common.MigrationMap<TblEaCompany, Instance>
    {
        List<TblEaCompany> _Companies;
        public InstancesMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblEaCompany> getSourceRecords()
        {
            _Companies = (_source as EMPAuthenticationContext).TblEaCompanies.ToListAsync().Result;
            return _Companies;
        }

        protected override Instance mapRecord(TblEaCompany obj)
        {
            return new Instance()
            {
                //ClientId
                //Name
                Deleted=false,
                Active=true
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
            };
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _Companies.Count();
        }
        protected override void updateTarget(Instance record)
        {
            //(_target as QTD2.Data.QTDAuthenticationContext).Instance.Add(record);
        }
        
    }
}
