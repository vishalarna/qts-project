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
    public class ClientsMap : Common.MigrationMap<TblEaCompany, Client>
    {
        List<TblEaCompany> _Companies;
        public ClientsMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblEaCompany> getSourceRecords()
        {
            _Companies = (_source as EMPAuthenticationContext).TblEaCompanies.ToListAsync().Result;
            return _Companies;
        }

        protected override Client mapRecord(TblEaCompany obj)
        {
            return new Client()
            {
                Name = obj.Company,
                Deleted = false,
                Active = true,
                Instances = getInstances(obj)
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
            };
        }

        private ICollection<Instance> getInstances(TblEaCompany obj)
        {
            List<Instance> instances = new List<Instance>();

            instances.Add(new Instance()
            {
                Active = true,
                Deleted = false,
                Name = obj.Company,
                InstanceSetting = getInstanceSetting(obj)
            });

            return instances;
        }

        private InstanceSetting getInstanceSetting(TblEaCompany obj)
        {
            return new InstanceSetting()
            {
                DatabaseName = obj.Company,
                DataBaseVersion = "1.0",
                ScormTenant = obj.Company
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _Companies.Count();
        }

        protected override void updateTarget(Client record)
        {
            (_target as QTD2.Data.QTDAuthenticationContext).Clients.Add(record);
        }
    }
}
