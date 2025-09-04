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
   public class AspNetUserClaimsMap : Common.MigrationMap<TblEaUser, AppClaim>
    {
        List<TblEaUser> _eAUsers;
        List<TblEaCompany> _companies;

        List<AppUser> _users;

        public AspNetUserClaimsMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblEaUser> getSourceRecords()
        {
            _eAUsers = (_source as EMPAuthenticationContext).TblEaUsers.ToListAsync().Result;
            _companies = (_source as EMPAuthenticationContext).TblEaCompanies.ToList();

            _users = (_target as QTD2.Data.QTDAuthenticationContext).Users.ToList();

            return _eAUsers;
        }

        protected override AppClaim mapRecord(TblEaUser obj)
        {
            var company = _companies.Where(r => r.CompanyId == obj.CompanyId).First();
            var user = _users.Where(r => r.Email == obj.Email).First();

            return new AppClaim()
            {
                UserId = user.Id,
                ClaimType = "qtd/claims//instanceName",
                ClaimValue = company.Company
            };
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _eAUsers.Count();
        }

        protected override void updateTarget(AppClaim record)
        {
            (_target as QTD2.Data.QTDAuthenticationContext).UserClaims.Add(record);
        }
    }
}
