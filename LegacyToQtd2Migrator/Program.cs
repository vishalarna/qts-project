using System;
using Microsoft.EntityFrameworkCore;

using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunVision(args);
            RunMvp(args);
            //RunScorm(args);
            //RunEmpAuth(args);
            //ConvertLicense("1151", "396475357929750");
        }

        static void ConvertLicense(string clientId, string activcationCode)
        {
            LicenseConverter converter = new LicenseConverter();
            var license = converter.ConvertLegacyLicense(activcationCode, clientId);
        }

        static void CreateDatabase(string[] args)
        {
            Console.WriteLine("Beginning Create Database " + DateTime.Now.ToShortTimeString());

            var sourceConnString = args[0];
            var targetConnString = args[1];

            var legacyOptionsBuilder = new DbContextOptionsBuilder<Legacy.Data.EMP_DemoContext>();
            legacyOptionsBuilder.UseSqlServer(sourceConnString);
            var legacyContext = new Legacy.Data.EMP_DemoContext(legacyOptionsBuilder.Options);

            var qtd2OptionsBuilder = new DbContextOptionsBuilder<QTD2.Data.QTDContext>();
            qtd2OptionsBuilder.UseSqlServer(targetConnString, providerOptions =>
            {
                providerOptions.CommandTimeout(180);
            });
            var qtd2Context = new QTD2.Data.QTDContext(qtd2OptionsBuilder.Options, null, null);

            qtd2Context.Database.Migrate();

            Console.WriteLine("Database created successfully " + DateTime.Now.ToShortTimeString());
        }

        static void RunEmpAuth(string[] args)
        {
            Console.WriteLine("Beginning EMP Auth Migration " + DateTime.Now.ToShortTimeString());

            var sourceConnString = args[0];
            var targetConnString = args[1];

            var legacyOptionsBuilder = new DbContextOptionsBuilder<Legacy.EmpAuth.EMPAuthenticationContext>();
            legacyOptionsBuilder.UseSqlServer(sourceConnString);
            var legacyContext = new Legacy.EmpAuth.EMPAuthenticationContext(legacyOptionsBuilder.Options);

            var qtd2OptionsBuilder = new DbContextOptionsBuilder<QTD2.Data.QTDAuthenticationContext>();
            qtd2OptionsBuilder.UseSqlServer(targetConnString, providerOptions =>
            {
                providerOptions.CommandTimeout(180);
            });

            var qtd2Context = new QTD2.Data.QTDAuthenticationContext(qtd2OptionsBuilder.Options);
            qtd2Context.Database.Migrate();

            Releases.EMPAuth empAuth = new Releases.EMPAuth(legacyContext, qtd2Context);
            empAuth.RunRelease();

            Console.WriteLine("EMP Auth Run Successfully " + DateTime.Now.ToShortTimeString());
        }

        static void RunScorm(string[] args)
        {
            Console.WriteLine("Beginning Scorm Migration " + DateTime.Now.ToShortTimeString());

            var sourceConnString = args[0];
            var targetConnString = args[1];

            var legacyOptionsBuilder = new DbContextOptionsBuilder<Legacy.Data.EMP_DemoContext>();
            legacyOptionsBuilder.UseSqlServer(sourceConnString);
            var legacyContext = new Legacy.Data.EMP_DemoContext(legacyOptionsBuilder.Options);

            var qtd2OptionsBuilder = new DbContextOptionsBuilder<QTD2ScormContext.MigrationTestContext>();
            qtd2OptionsBuilder.UseSqlServer(targetConnString, providerOptions =>
            {
                providerOptions.CommandTimeout(180);
            });
            var qtd2Context = new QTD2ScormContext.MigrationTestContext(qtd2OptionsBuilder.Options);

            Releases.Scorm scorm = new Releases.Scorm(legacyContext, qtd2Context);
            scorm.RunRelease();

            Console.WriteLine("Scorm Run Successfully " + DateTime.Now.ToShortTimeString());
        }

        static void RunVision(string[] args)
        {
            Console.WriteLine("Beginning Migration " + DateTime.Now.ToShortTimeString());

            var sourceConnString = args[0];
            var targetConnString = args[1];
            var projectId = 6;

            //TODO change to Vision Context
            var legacyOptionsBuilder = new DbContextOptionsBuilder<Vision.Data.VisionContext>();
            legacyOptionsBuilder.UseSqlServer(sourceConnString);
            //TODO change to Vision Context
            var legacyContext = new Vision.Data.VisionContext(legacyOptionsBuilder.Options);

            var qtd2OptionsBuilder = new DbContextOptionsBuilder<QTD2.Data.QTDContext>();
            qtd2OptionsBuilder.UseSqlServer(targetConnString, providerOptions =>
            {
                providerOptions.CommandTimeout(180);
            });
            var qtd2Context = new QTD2.Data.QTDContext(qtd2OptionsBuilder.Options, null, null);

            //qtd2Context.Database.Migrate();

            Releases.Vision mvp = new Releases.Vision(legacyContext, qtd2Context, projectId);
            mvp.RunRelease();

            using (var command = qtd2Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @" update ToolCategories 
                                            set Title = 'ATC HP TOOLS' 
                                                where Title = 'ATC Human Performance Tools'

                                            update tools
                                            set Name = d.Name from

                                            (
	                                            select t.id, tc.Title + ' - ' + t.Name as Name From tools t inner join ToolCategories tc on tc.id	 = t.toolCategoryId

                                            ) d

                                            where d.Id = Tools.Id

                                            update DutyAreas

                                            set Title = 
	
	                                            case
		                                            when Number = 1 then 'MONITORING COMMUNICATIONS / CONTROL / PROTECTION SYSTEMS'
		                                            when Number = 2 then 'CONTROL CENTER EMERGENCIES'
		                                            when Number = 4 then 'OUTAGE COORDINATION'
		                                            when Number = 6 then 'SWITCHING AND CLEARANCES'
		                                            when Number = 7 then 'SYSTEM EMERGENCIES'
		                                            when Number = 8 then 'SYSTEM MONITORING AND CONTROL'
		                                            when Number = 10 then 'SYSTEM RESTORATION'
		                                            when Number = 11 then 'VOLTAGE CONTROL'
		                                            when Number = 14 then 'FORCED AND URGENT OUTAGES'
		                                            when NUmber = 15 then 'LOGS, REPORTS, AND DOCUMENTATION'
		                                            when NUmber = 99 then 'Unknown'
	                                            end


                                            update SubDutyAreas
	                                            set Title = 'RRTs'";

                command.CommandType = System.Data.CommandType.Text;
                qtd2Context.Database.OpenConnection();
                var result = command.ExecuteNonQuery();
            }

            Console.WriteLine("MVP Release Run Successfully " + DateTime.Now.ToShortTimeString());
        }

        static void RunMvp(string[] args)
        {
            Console.WriteLine("Beginning Migration " + DateTime.Now.ToShortTimeString());

            var sourceConnString = args[0];
            var targetConnString = args[1];


            var legacyOptionsBuilder = new DbContextOptionsBuilder<Legacy.Data.EMP_DemoContext>();
            legacyOptionsBuilder.UseSqlServer(sourceConnString);
            var legacyContext = new Legacy.Data.EMP_DemoContext(legacyOptionsBuilder.Options);

            var qtd2OptionsBuilder = new DbContextOptionsBuilder<QTD2.Data.QTDContext>();
            qtd2OptionsBuilder.UseSqlServer(targetConnString, providerOptions =>
            {
                providerOptions.CommandTimeout(180);
            });
            var qtd2Context = new QTD2.Data.QTDContext(qtd2OptionsBuilder.Options, null, null);

            qtd2Context.Database.Migrate();

            Releases.MVP mvp = new Releases.MVP(legacyContext, qtd2Context);
            mvp.RunRelease();

            Console.WriteLine("MVP Release Run Successfully " + DateTime.Now.ToShortTimeString());
        }
    }
}
