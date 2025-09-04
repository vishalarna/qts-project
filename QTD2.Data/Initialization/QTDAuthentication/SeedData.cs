using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace QTD2.Data.Initialization.QTDAuthentication
{
    public partial class SeedData
    {
        private readonly MigrationBuilder _migrationBuilder;
        private readonly Environments _environment;
        private readonly string _path;

        private string prefix
        {
            get { return _environment.ToString() + "_"; }
        }

        public SeedData(string environment, MigrationBuilder migrationBuilder)
        {
            _environment = (Environments)Enum.Parse(typeof(Environments), environment);
            _migrationBuilder = migrationBuilder;
            _path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Initialization\\QTDAuthentication\\Data\\" + _environment;
        }

        public void AddIdentity()
        {
            callMethod(prefix + "AddIdentity");
        }

        public void AddClientsTable()
        {
            callMethod(prefix + "AddClientsTable");
        }

        public void AddInstancesTable()
        {
            callMethod(prefix + "AddInstancesTable");
        }

        public void AddInstanceSettingsTable()
        {
            callMethod(prefix + "AddInstanceSettingsTable");
        }

        public void AddTableData_IdentityProviderAndLinks()
        {
            callMethod(prefix + "AddTableData_IdentityProviderAndLinks");
        }
        public void SetLockoutForAllUsers()
        {
            callMethod(prefix + "SetLockoutForAllUsers");
        }
        public void SetTwoFactorEnabledForAllUsers()
        {
            callMethod(prefix + "SetTwoFactorEnabledForAllUsers");
        }
        public void SetEmailConfirmedForAllUsers()
        {
            callMethod(prefix + "SetEmailConfirmedForAllUsers");
        }
        public void SetPublicUrlToClassesForAllUsers()
        {
            callMethod(prefix + "SetPublicUrlToClassesForAllUsers");
        }
        public void AddAuthenticationSettings()
        {
            callMethod(prefix + "AddAuthenticationSettings");
        }
        protected void callMethod(string method)
        {
            Type thisType = GetType();
            MethodInfo theMethod = thisType.GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance);
            theMethod.Invoke(this, null);
        }

        protected object[,] toRectangular(object[][] arrayOfArrays)
        {
            int height = arrayOfArrays.Length;
            int width = arrayOfArrays[0].Length;

            object[,] result = new object[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result[i, j] = arrayOfArrays[i][j];
                }
            }
            return result;
        }
    }
}
