using Microsoft.AspNetCore.Identity;
using QTD2.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;


namespace QTD2.Data.Initialization.QTDAuthentication
{
    public partial class SeedData
    {
        protected void UAT_AddIdentity()
        {
            var hasher = new PasswordHasher<AppUser>();

            string jsonString = System.IO.File.ReadAllText(_path + "\\app-users.json");
            List<AppUser> appUsers = JsonSerializer.Deserialize<List<AppUser>>(jsonString);

            foreach (var user in appUsers)
            {
                user.Email = user.UserName;
                user.NormalizedEmail = user.UserName.ToUpper().Normalize();
                user.NormalizedUserName = user.UserName.ToUpper().Normalize();
                user.EmailConfirmed = true;
                user.TwoFactorEnabled = false;
                user.SecurityStamp = System.Guid.NewGuid().ToString();
                user.PasswordHash = hasher.HashPassword(user, "Password1");
                user.LockoutEnabled = false;
                user.AccessFailedCount = 0;
                user.PhoneNumberConfirmed = true;
            }

            _migrationBuilder.InsertData(
               table: "AspNetUsers",
               columns: new[] { "Id", "Email", "UserName", "NormalizedEmail", "NormalizedUserName", "EmailConfirmed", "TwoFactorEnabled", "SecurityStamp", "PasswordHash", "AccessFailedCount", "PhoneNumberConfirmed", "LockoutEnabled" },
               values: toRectangular(appUsers.Select(r => new object[] { r.Id, r.Email, r.UserName, r.NormalizedEmail, r.NormalizedUserName, r.EmailConfirmed, r.TwoFactorEnabled, r.SecurityStamp, r.PasswordHash, r.AccessFailedCount, r.PhoneNumberConfirmed, r.LockoutEnabled }).ToArray()));


            foreach (var user in appUsers)
            {
                if (adminUsers.Contains(user.Id))
                {
                    _migrationBuilder.InsertData(
                           table: "AspNetUserClaims",
                           columns: new[] { "UserId", "ClaimType", "ClaimValue" },
                           values: new object[,]
                           {
                                { user.Id, Domain.CustomClaimTypes.IsAdmin, "true" }
                           });
                }
                else
                {
                    _migrationBuilder.InsertData(
                        table: "AspNetUserClaims",
                        columns: new[] { "UserId", "ClaimType", "ClaimValue" },
                        values: new object[,]
                        {
                            { user.Id, Domain.CustomClaimTypes.InstanceName, "QTD2" },
                            { user.Id, Domain.CustomClaimTypes.InstanceName, "QTD2Training" }
                        });
                }
            }
        }

        protected void UAT_AddClientsTable()
        {
            _migrationBuilder.InsertData(
               table: "Clients",
               columns: new[] { "Id", "Name" },
               values: new object[,]
               {
                    { 1, "QTD" },
               });
        }

        protected void UAT_AddInstancesTable()
        {
            _migrationBuilder.InsertData(
               table: "Instances",
               columns: new[] { "Id", "ClientId", "Name" },
               values: new object[,]
               {
                    { 1, 1, "QTD2" },
                    { 2, 1, "QTDTraining" },
               });
        }

        protected void UAT_AddInstanceSettingsTable()
        {
            _migrationBuilder.InsertData(
                table: "InstanceSettings",
                columns: new[] { "Id", "InstanceId", "DatabaseName" },
                values: new object[,]
                {
                    { 1, 1, "QTD2" },
                    { 2, 2, "QTD2Training" },
                });
        }

        protected void UAT_AddTableData_IdentityProviderAndLinks()
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
        protected void UAT_SetLockoutForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[AspNetUsers]
                            SET [LockoutEnabled] = 1;");
        }

        protected void UAT_SetTwoFactorEnabledForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[AspNetUsers]
                            SET [TwoFactorEnabled] = 1;");
        }
        protected void UAT_SetEmailConfirmedForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[AspNetUsers]
                            SET [EmailConfirmed] = 1;");
        }
        protected void UAT_SetPublicUrlToClassesForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[InstanceSettings] 
                            SET [PublicUrl] = 'Classes';");
        }
        protected void UAT_AddAuthenticationSettings()
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
