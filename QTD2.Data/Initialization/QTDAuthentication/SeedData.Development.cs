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
        List<string> adminUsers = new List<string>()
        {
            "F0BB40EC-58BC-2B5E-C42F-B6E59D1D8F03",
            "7E6FF1FA-3118-F5A4-60FC-1F4E2CFE927C",
            "B954A2C3-0454-5FA6-F738-6EFA93054C38",
            "D7295A14-3C4E-299A-47C9-C94F3D088DDC",
            "A215E526-CD3A-8443-EA42-4BE47A5D66CD",
            "05771032-9E02-967A-7BFE-17C500D1C7EB",
            "C88953A9-F5F9-9952-34AD-7409769D0672",
            "EB720182-03AE-1898-3B53-C377B25DC5FE",
            "7ADB1DCF-A192-56A5-39BF-C7C0DF83FDFD",
            "23F693DC-FDD4-FA75-13B2-1682827B597E",
            "7FD255C1-249B-0819-28E2-74F15DD51ECA",
            "EFE15415-8F04-B8EA-BBF3-B469D3925DA4",
            "6D1BF27B-EAE6-7CAB-AB42-60A1DEE3751E",
            "28EA22F6-5FE9-B4E0-FD82-9AE7808CE0F5"
        };

        protected void Development_AddIdentity()
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

        protected void Development_AddClientsTable()
        {
            _migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "QTD" },
                });
        }

        protected void Development_AddInstancesTable()
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

        protected void Development_AddInstanceSettingsTable()
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

        protected void Development_AddTableData_IdentityProviderAndLinks()
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

        protected void Development_SetLockoutForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[AspNetUsers]
                            SET [LockoutEnabled] = 1;");
        }

        protected void Development_SetTwoFactorEnabledForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[AspNetUsers]
                            SET [TwoFactorEnabled] = 1;");
        }
        protected void Development_SetEmailConfirmedForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[AspNetUsers]
                            SET [EmailConfirmed] = 1;");
        }
        protected void Development_SetPublicUrlToClassesForAllUsers()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[InstanceSettings] 
                            SET [PublicUrl] = 'Classes';");
        }
        protected void Development_AddAuthenticationSettings()
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
