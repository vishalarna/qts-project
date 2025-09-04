using Microsoft.AspNetCore.Identity;
using QTD2.Domain.Entities.Authentication;

namespace QTD2.Data.Initialization.QTDAuthentication
{
    public partial class SeedData
    {
        protected void Production_AddIdentity()
        {
            var hasher = new PasswordHasher<AppUser>();

            var admin = new AppUser()
            {
                Id = System.Guid.NewGuid().ToString(),
                Email = "admin@qualitytrainingsystems.com",
                UserName = "admin@qualitytrainingsystems.com",
                NormalizedEmail = "admin@qualitytrainingsystems.com",
                NormalizedUserName = "admin@qualitytrainingsystems.com",
                EmailConfirmed = true,
                TwoFactorEnabled = true,
                SecurityStamp = System.Guid.NewGuid().ToString(),
            };

            admin.PasswordHash = hasher.HashPassword(admin, "QTDPassword1");

            _migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "Email", "UserName", "NormalizedEmail", "NormalizedUserName", "EmailConfirmed", "TwoFactorEnabled", "AccessFailedCount", "PhoneNumberConfirmed", "LockoutEnabled", "PasswordHash", "SecurityStamp" },
                values: new object[,]
                {
                    { admin.Id, admin.Email, admin.UserName, admin.NormalizedEmail, admin.NormalizedUserName, admin.EmailConfirmed, admin.TwoFactorEnabled, 0, false, true, admin.PasswordHash, admin.SecurityStamp },
                });

            _migrationBuilder.InsertData(
               table: "AspNetUserClaims",
               columns: new[] { "UserId", "ClaimType", "ClaimValue" },
               values: new object[,]
               {
                    { admin.Id, Domain.CustomClaimTypes.IsAdmin, "true" },
               });
        }

        protected void Production_AddClientsTable()
        {
        }

        protected void Production_AddInstancesTable()
        {
        }

        protected void Production_AddInstanceSettingsTable()
        {
        }

        protected void Production_AddTableData_IdentityProviderAndLinks()
        {
            _migrationBuilder.Sql(@"
                        IF NOT EXISTS (SELECT 1 FROM IdentityProviders WHERE Name = 'Default Password Provider')
                        BEGIN
                            INSERT INTO IdentityProviders (Name, SubType,Deleted,Active)
                            VALUES ('Default Password Provider', 'Password',0,1);
                        END
                    ");

            _migrationBuilder.Sql(@"
                        INSERT INTO Instance_IdentityProvider_Links (InstanceId, IdentityProviderId, Deleted, Active)
                        SELECT i.Id, 
                               (SELECT TOP 1 Id FROM IdentityProviders ORDER BY Id) AS IdentityProviderId, 
                               0, 
                               1
                        FROM Instances i
                        WHERE NOT EXISTS (
                            SELECT 1
                            FROM Instance_IdentityProvider_Links l
                            WHERE l.InstanceId = i.Id 
                              AND l.IdentityProviderId = (SELECT TOP 1 Id FROM IdentityProviders ORDER BY Id));
                    ");
        }

        protected void Production_SetLockoutForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[AspNetUsers]
                            SET [LockoutEnabled] = 1;");
        }
        protected void Production_SetTwoFactorEnabledForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[AspNetUsers]
                            SET [TwoFactorEnabled] = 1;");
        }
        protected void Production_SetEmailConfirmedForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[AspNetUsers]
                            SET [EmailConfirmed] = 1;");
        }
        protected void Production_SetPublicUrlToClassesForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[InstanceSettings] 
                            SET [PublicUrl] = 'Classes';");
        }

        protected void Production_AddAuthenticationSettings()
        {
            _migrationBuilder.InsertData(
                table: "AuthenticationSettings",
                columns: new[] { "VersionMajor", "VersionMinor", "VersionPatch", "Deleted", "Active" },
                values: new object[,]
                {
                    { 2, 1, 0, false, true }
                });
        }
    }
}
