using LegacyToQtd2Migrator.Legacy.EmpAuth;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LegacyToQtd2Migrator.Mapping.EMPAuth
{
   public class AspNetUsersMap : Common.MigrationMap<TblEaUser, AppUser>
    {
        List<TblEaUser> _eAUsers;
        List<TblEaCompany> _companies;

        public AspNetUsersMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblEaUser> getSourceRecords()
        {
            _eAUsers = (_source as EMPAuthenticationContext).TblEaUsers.ToList();
            _companies = (_source as EMPAuthenticationContext).TblEaCompanies.ToList();
            return _eAUsers;
        }

        protected override AppUser mapRecord(TblEaUser obj)
        {
            var hasher = new PasswordHasher<AppUser>();

            var appUser =  new AppUser()
            {
                UserName = obj.Email,
                NormalizedUserName = obj.Email.ToUpper().Normalize(),
                Email = obj.Email,
                NormalizedEmail = obj.Email.ToUpper().Normalize(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                //ConcurrencyStamp
                //PhoneNumber
                //PhoneNumberConfirmed
                TwoFactorEnabled = false,
                LockoutEnd =obj.LockOut,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            //force everyone to reset passwords
            appUser.PasswordHash = hasher.HashPassword(appUser, Guid.NewGuid().ToString());

            return appUser;
        }

        private List<AppClaim> getUserClaims(TblEaUser obj)
        {
            var claims = new List<AppClaim>();

            var company = _companies.Where(r => r.CompanyId == obj.CompanyId).First();

            claims.Add(new AppClaim()
            {
                ClaimType = "qtd/claims//instanceName",
                ClaimValue = company.Company
            });

            return claims;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _eAUsers.Count();
        }

        protected override void updateTarget(AppUser record)
        {
            (_target as QTD2.Data.QTDAuthenticationContext).Users.Add(record);
        }
    }
}
