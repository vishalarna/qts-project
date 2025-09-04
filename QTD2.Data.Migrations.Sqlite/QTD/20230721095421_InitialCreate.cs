using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QTD2.Data.Migrations.Sqlite.QTD
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentTools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentTools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertificationIssuingAuthorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationIssuingAuthorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertifyingBodies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Desc = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsNERC = table.Column<bool>(type: "INTEGER", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertifyingBodies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassSchedule_Roster_Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedule_Roster_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientSettings_GeneralSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CompanyLogo = table.Column<string>(type: "TEXT", nullable: false),
                    DateFormat = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ClassStartEndTimeFormat = table.Column<string>(type: "TEXT", nullable: false),
                    CompanySpecificCoursePassingScore = table.Column<decimal>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSettings_GeneralSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientSettings_LabelReplacements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DefaultLabel = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    LabelReplacement = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSettings_LabelReplacements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientSettings_Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActivationCode = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSettings_Licenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientSettings_Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    TimingText = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSettings_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coversheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoversheetTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CoversheetTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CoversheetInstructions = table.Column<string>(type: "TEXT", nullable: false),
                    CoversheetFileUpload = table.Column<byte[]>(type: "BLOB", nullable: true),
                    CoversheetImageUpload = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coversheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoverSheetTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverSheetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DashboardSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    GroupName = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatorIlaId = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsUserDefined = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsAvailableForAllIlas = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsNerc = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DutyAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Letter = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReasonForRevision = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IDP_ReviewStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDP_ReviewStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ILA_Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsPriority = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructor_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ICategoryTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ICategoryDescription = table.Column<string>(type: "TEXT", nullable: true),
                    ICategoryUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocCategoryTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    LocCategoryDesc = table.Column<string>(type: "TEXT", nullable: true),
                    LocCategoryWebsite = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetaILA_Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaILA_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetaILAAssessments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaILAAssessments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetaILAConfigurationPublishOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaILAConfigurationPublishOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NercStandards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IsNercStandard = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsUserDefined = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NercStandards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NERCTargetAudiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IsOther = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    OtherName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NERCTargetAudiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PositionNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionAbbreviation = table.Column<string>(type: "TEXT", nullable: true),
                    PositionTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PositionDescription = table.Column<string>(type: "TEXT", nullable: true),
                    HyperLink = table.Column<string>(type: "TEXT", maxLength: 400, nullable: true),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    PositionsFileUpload = table.Column<byte[]>(type: "BLOB", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Procedure_IssuingAuthorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Website = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure_IssuingAuthorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProviderLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Stem = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatingScaleNs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RatingScaleDescription = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingScaleNs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatingScales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Position1Text = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Position2Text = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Position3Text = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Position4Text = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Position5Text = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingScales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportSkeletonId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    InternalReportTitle = table.Column<string>(type: "TEXT", nullable: false),
                    OfficialReportTitle = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportSkeletons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DefaultTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSkeletons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RR_IssuingAuthorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RR_IssuingAuthorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SafetyHazard_Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SafetyHzAbatementText = table.Column<string>(type: "TEXT", nullable: true),
                    SafetyHzAbatementFiles = table.Column<string>(type: "TEXT", nullable: true),
                    SafetyHzAbatementImage = table.Column<string>(type: "TEXT", nullable: true),
                    SafetyHzControlsText = table.Column<string>(type: "TEXT", nullable: true),
                    SafetyHzControlsFiles = table.Column<string>(type: "TEXT", nullable: true),
                    SafetyHzControlsImage = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyHazard_Sets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaftyHazard_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaftyHazard_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    IsNercStandard = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsNercOperatingTopics = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsNercSimulation = table.Column<bool>(type: "INTEGER", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Uploads = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimulationScenarioSpecLookUps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SimScenarioSpecHeading = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationScenarioSpecLookUps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimulatorScenarioDifficultyLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SimulatorScenarioDiffLevel = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorScenarioDifficultyLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentEvaluationAudiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvaluationAudiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentEvaluationAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvaluationAvailabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task_CollaboratorInvitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvitedByEId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvitedForTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    InviteeEId = table.Column<int>(type: "INTEGER", nullable: true),
                    InviteeEmail = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    InviteDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_CollaboratorInvitations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task_References",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_References", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskQualificationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskQualificationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxonomyLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxonomyLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestItemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsOverride = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToolCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToolGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TraineeEvaluationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeEvaluationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingGroup_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingGroup_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrainingProgramTypeTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgramTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingTopic_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTopic_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CertifyingBodyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CertNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    CertAcronym = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CertDesc = table.Column<string>(type: "TEXT", nullable: true),
                    RenewalTimeFrame = table.Column<bool>(type: "INTEGER", nullable: true),
                    RenewalInterval = table.Column<int>(type: "INTEGER", nullable: true),
                    CreditHrsReq = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreditHrs = table.Column<float>(type: "REAL", nullable: true),
                    CertSubReq = table.Column<bool>(type: "INTEGER", nullable: true),
                    CertSubReqName = table.Column<string>(type: "TEXT", nullable: true),
                    CertSubReqHours = table.Column<float>(type: "REAL", nullable: true),
                    CertMemberNum = table.Column<bool>(type: "INTEGER", nullable: true),
                    CertifiedDate = table.Column<bool>(type: "INTEGER", nullable: true),
                    RenewalDate = table.Column<bool>(type: "INTEGER", nullable: true),
                    ExpirationDate = table.Column<bool>(type: "INTEGER", nullable: true),
                    AllowRolloverHours = table.Column<bool>(type: "INTEGER", nullable: true),
                    AdditionalHours = table.Column<float>(type: "REAL", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certifications_CertifyingBodies_CertifyingBodyId",
                        column: x => x.CertifyingBodyId,
                        principalTable: "CertifyingBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CertifyingBody_History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CertifyingBodyId = table.Column<int>(type: "INTEGER", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertifyingBody_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertifyingBody_History_CertifyingBodies_CertifyingBodyId",
                        column: x => x.CertifyingBodyId,
                        principalTable: "CertifyingBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSettings_Notification_AvailableCustomSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientSettingsNotificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Setting = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSettings_Notification_AvailableCustomSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSettings_Notification_AvailableCustomSettings_ClientSettings_Notifications_ClientSettingsNotificationId",
                        column: x => x.ClientSettingsNotificationId,
                        principalTable: "ClientSettings_Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSettings_Notification_CustomSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientSettingsNotificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Key = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSettings_Notification_CustomSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSettings_Notification_CustomSettings_ClientSettings_Notifications_ClientSettingsNotificationId",
                        column: x => x.ClientSettingsNotificationId,
                        principalTable: "ClientSettings_Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSettings_Notification_Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientSettingsNotificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Template = table.Column<string>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSettings_Notification_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSettings_Notification_Steps_ClientSettings_Notifications_ClientSettingsNotificationId",
                        column: x => x.ClientSettingsNotificationId,
                        principalTable: "ClientSettings_Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DutyArea_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DutyAreaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyArea_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DutyArea_Histories_DutyAreas_DutyAreaId",
                        column: x => x.DutyAreaId,
                        principalTable: "DutyAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubdutyAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DutyAreaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    SubNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReasonForRevision = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubdutyAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubdutyAreas_DutyAreas_DutyAreaId",
                        column: x => x.DutyAreaId,
                        principalTable: "DutyAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_CategoryHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnablingObjectiveCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_CategoryHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_CategoryHistories_EnablingObjective_Categories_EnablingObjectiveCategoryId",
                        column: x => x.EnablingObjectiveCategoryId,
                        principalTable: "EnablingObjective_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_SubCategories_EnablingObjective_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "EnablingObjective_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructor_CategoryHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ICategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ICategoryNotes = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor_CategoryHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructor_CategoryHistories_Instructor_Categories_ICategoryId",
                        column: x => x.ICategoryId,
                        principalTable: "Instructor_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ICategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    InstructorNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    InstructorName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    InstructorEmail = table.Column<string>(type: "TEXT", nullable: true),
                    InstructorDescription = table.Column<string>(type: "TEXT", nullable: true),
                    IsWorkBookAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Instructor_Categories_ICategoryId",
                        column: x => x.ICategoryId,
                        principalTable: "Instructor_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location_CategoryHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocCategoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location_CategoryHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_CategoryHistories_Location_Categories_LocCategoryID",
                        column: x => x.LocCategoryID,
                        principalTable: "Location_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocCategoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    LocNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LocName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    LocDescription = table.Column<string>(type: "TEXT", nullable: true),
                    LocAddress = table.Column<string>(type: "TEXT", nullable: true),
                    LocCity = table.Column<string>(type: "TEXT", nullable: true),
                    LocState = table.Column<string>(type: "TEXT", nullable: true),
                    LocZipCode = table.Column<string>(type: "TEXT", nullable: true),
                    LocPhone = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Location_Categories_LocCategoryID",
                        column: x => x.LocCategoryID,
                        principalTable: "Location_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetaILAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    MetaILAStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MetaILAAssessmentID = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaILAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetaILAs_MetaILA_Statuses_MetaILAStatusId",
                        column: x => x.MetaILAStatusId,
                        principalTable: "MetaILA_Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetaILAs_MetaILAAssessments_MetaILAAssessmentID",
                        column: x => x.MetaILAAssessmentID,
                        principalTable: "MetaILAAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NercStandardMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StdId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NercStandardMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NercStandardMembers_NercStandards_StdId",
                        column: x => x.StdId,
                        principalTable: "NercStandards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientUsers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ZipCode = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    WorkLocation = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    TQEqulator = table.Column<bool>(type: "INTEGER", nullable: false),
                    InactiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Reason = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QTDUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QTDUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QTDUsers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Position_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Position_Histories_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PositionNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionAbbreviation = table.Column<string>(type: "TEXT", nullable: true),
                    PositionTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PositionDescription = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    HyperLink = table.Column<string>(type: "TEXT", maxLength: 400, nullable: true),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    PositionsFileUpload = table.Column<byte[]>(type: "BLOB", nullable: true),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Positions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proc_IssuingAuthority_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcedureIssuingAuthorityId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proc_IssuingAuthority_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proc_IssuingAuthority_Histories_Procedure_IssuingAuthorities_ProcedureIssuingAuthorityId",
                        column: x => x.ProcedureIssuingAuthorityId,
                        principalTable: "Procedure_IssuingAuthorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IssuingAuthorityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RevisionNumber = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProceduresFileUpload = table.Column<byte[]>(type: "BLOB", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    Hyperlink = table.Column<string>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedures_Procedure_IssuingAuthorities_IssuingAuthorityId",
                        column: x => x.IssuingAuthorityId,
                        principalTable: "Procedure_IssuingAuthorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ProviderLevelId = table.Column<int>(type: "INTEGER", nullable: true),
                    ContactName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ContactTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ContactPhone = table.Column<string>(type: "TEXT", nullable: true),
                    ContactExt = table.Column<int>(type: "INTEGER", nullable: true),
                    ContactMobile = table.Column<string>(type: "TEXT", nullable: true),
                    ContactEmail = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CompanyWebsite = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    RepName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    RepTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    RepPhone = table.Column<string>(type: "TEXT", nullable: true),
                    RepEmail = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    RepSignature = table.Column<string>(type: "TEXT", nullable: true),
                    IsPriority = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsNERC = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Providers_ProviderLevels_ProviderLevelId",
                        column: x => x.ProviderLevelId,
                        principalTable: "ProviderLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionBankHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionBankId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionBankNotes = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBankHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionBankHistories_QuestionBanks_QuestionBankId",
                        column: x => x.QuestionBankId,
                        principalTable: "QuestionBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingScaleExpanded",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Ratings = table.Column<int>(type: "INTEGER", nullable: false),
                    RatingScaleNId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingScaleExpanded", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingScaleExpanded_RatingScaleNs_RatingScaleNId",
                        column: x => x.RatingScaleNId,
                        principalTable: "RatingScaleNs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentEvaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RatingScaleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Instructions = table.Column<string>(type: "TEXT", nullable: true),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsAvailableForAllILAs = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsAvailableForSelectedILAs = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsIncludeCommentSections = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsAllowNAOption = table.Column<bool>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEvaluations_RatingScaleNs_RatingScaleId",
                        column: x => x.RatingScaleId,
                        principalTable: "RatingScaleNs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentEvaluationForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RatingScaleId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsShared = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsAvailableForAllILAs = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsNAOption = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IncludeComments = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvaluationForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEvaluationForms_RatingScales_RatingScaleId",
                        column: x => x.RatingScaleId,
                        principalTable: "RatingScales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportDisplayColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportId = table.Column<int>(type: "INTEGER", nullable: false),
                    ColumnName = table.Column<string>(type: "TEXT", nullable: false),
                    Display = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDisplayColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportDisplayColumns_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportFilters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    PropertyType = table.Column<string>(type: "TEXT", nullable: false),
                    ValueType = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportFilters_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportSkeletonColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportSkeletonId = table.Column<int>(type: "INTEGER", maxLength: 200, nullable: false),
                    ColumnName = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSkeletonColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportSkeletonColumns_ReportSkeletons_ReportSkeletonId",
                        column: x => x.ReportSkeletonId,
                        principalTable: "ReportSkeletons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportSkeletonFilters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportSkeletonId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    PropertyType = table.Column<string>(type: "TEXT", nullable: false),
                    ValueType = table.Column<string>(type: "TEXT", nullable: false),
                    MinOption = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MaxOption = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FilterOption = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSkeletonFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportSkeletonFilters_ReportSkeletons_ReportSkeletonId",
                        column: x => x.ReportSkeletonId,
                        principalTable: "ReportSkeletons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegulatoryRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IssuingAuthorityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RevisionNumber = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Uploads = table.Column<byte[]>(type: "BLOB", nullable: true),
                    HyperLink = table.Column<string>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulatoryRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegulatoryRequirements_RR_IssuingAuthorities_IssuingAuthorityId",
                        column: x => x.IssuingAuthorityId,
                        principalTable: "RR_IssuingAuthorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RR_IssuingAuthority_StatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RRIssuingAuthorityId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RR_IssuingAuthority_StatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RR_IssuingAuthority_StatusHistories_RR_IssuingAuthorities_RRIssuingAuthorityId",
                        column: x => x.RRIssuingAuthorityId,
                        principalTable: "RR_IssuingAuthorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafetyHazard_CategoryHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SafetyHazardCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyHazard_CategoryHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_CategoryHistories_SaftyHazard_Categories_SafetyHazardCategoryId",
                        column: x => x.SafetyHazardCategoryId,
                        principalTable: "SaftyHazard_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaftyHazards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SaftyHazardCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    RevisionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HyperLinks = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Text = table.Column<string>(type: "TEXT", nullable: true),
                    Files = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaftyHazards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaftyHazards_SaftyHazard_Categories_SaftyHazardCategoryId",
                        column: x => x.SaftyHazardCategoryId,
                        principalTable: "SaftyHazard_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulatorScenarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SimScenarioDiffID = table.Column<int>(type: "INTEGER", nullable: false),
                    SimScenarioTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    SimScenarioDesc = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    SimScenarioDurationHours = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    SimScenarioDurationMins = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    SimScenarioUpload = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorScenarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatorScenarios_SimulatorScenarioDifficultyLevels_SimScenarioDiffID",
                        column: x => x.SimScenarioDiffID,
                        principalTable: "SimulatorScenarioDifficultyLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    RandomizeDistractors = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_TestStatuses_TestStatusId",
                        column: x => x.TestStatusId,
                        principalTable: "TestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_TestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Version_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_TestStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_TestStatuses_TestStatuses_TestStatusId",
                        column: x => x.TestStatusId,
                        principalTable: "TestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ToolCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Hyperlink = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Upload = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tools_ToolCategories_ToolCategoryId",
                        column: x => x.ToolCategoryId,
                        principalTable: "ToolCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupName = table.Column<string>(type: "TEXT", nullable: false),
                    GroupDescription = table.Column<string>(type: "TEXT", nullable: true),
                    HyperLink = table.Column<string>(type: "TEXT", nullable: true),
                    PDF = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingGroups_TrainingGroup_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TrainingGroup_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingProgramTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TPVersionNo = table.Column<string>(type: "TEXT", nullable: true),
                    ProgramTitle = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Year = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Publish = table.Column<bool>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPrograms_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingPrograms_TrainingProgramTypes_TrainingProgramTypeId",
                        column: x => x.TrainingProgramTypeId,
                        principalTable: "TrainingProgramTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingTopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrainingTopic_CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingTopics_TrainingTopic_Categories_TrainingTopic_CategoryId",
                        column: x => x.TrainingTopic_CategoryId,
                        principalTable: "TrainingTopic_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certification_History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CertId = table.Column<int>(type: "INTEGER", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    CertificationId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certification_History_Certifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificationSubRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CertificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReqName = table.Column<string>(type: "TEXT", nullable: true),
                    ReqHour = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationSubRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificationSubRequirements_Certifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSettings_Notification_Step_AvailableCustomSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientSettingsNotificationStepId = table.Column<int>(type: "INTEGER", nullable: false),
                    Setting = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSettings_Notification_Step_AvailableCustomSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSettings_Notification_Step_AvailableCustomSettings_ClientSettings_Notification_Steps_ClientSettingsNotificationStepId",
                        column: x => x.ClientSettingsNotificationStepId,
                        principalTable: "ClientSettings_Notification_Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSettings_Notification_Step_CustomSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientSettingsNotificationStepId = table.Column<int>(type: "INTEGER", nullable: false),
                    Key = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSettings_Notification_Step_CustomSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSettings_Notification_Step_CustomSettings_ClientSettings_Notification_Steps_ClientSettingsNotificationStepId",
                        column: x => x.ClientSettingsNotificationStepId,
                        principalTable: "ClientSettings_Notification_Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSettings_Notification_Step_ModelItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientSettingsNotificationStepId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Template = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSettings_Notification_Step_ModelItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSettings_Notification_Step_ModelItems_ClientSettings_Notification_Steps_ClientSettingsNotificationStepId",
                        column: x => x.ClientSettingsNotificationStepId,
                        principalTable: "ClientSettings_Notification_Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSettings_Notification_Step_Recipients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientSettingsNotificationStepId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSettings_Notification_Step_Recipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSettings_Notification_Step_Recipients_ClientSettings_Notification_Steps_ClientSettingsNotificationStepId",
                        column: x => x.ClientSettingsNotificationStepId,
                        principalTable: "ClientSettings_Notification_Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubDutyArea_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubDutyAreaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubDutyArea_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubDutyArea_Histories_SubdutyAreas_SubDutyAreaId",
                        column: x => x.SubDutyAreaId,
                        principalTable: "SubdutyAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubdutyAreaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Abreviation = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Criteria = table.Column<string>(type: "TEXT", nullable: true),
                    TaskCriteriaUpload = table.Column<string>(type: "TEXT", nullable: true),
                    RequalificationDueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RequalificationRequired = table.Column<bool>(type: "INTEGER", nullable: true),
                    RequalificationNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Critical = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    References = table.Column<string>(type: "TEXT", nullable: true),
                    RequiredTime = table.Column<int>(type: "INTEGER", nullable: false),
                    IsMeta = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsReliability = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Conditions = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_SubdutyAreas_SubdutyAreaId",
                        column: x => x.SubdutyAreaId,
                        principalTable: "SubdutyAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_SubCategoryHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnablingObjectiveSubCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_SubCategoryHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_SubCategoryHistories_EnablingObjective_SubCategories_EnablingObjectiveSubCategoryId",
                        column: x => x.EnablingObjectiveSubCategoryId,
                        principalTable: "EnablingObjective_SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    SubCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_Topics_EnablingObjective_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "EnablingObjective_SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructor_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstructorId = table.Column<int>(type: "INTEGER", nullable: false),
                    InstructorNotes = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructor_Histories_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Histories_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_MetaILAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MetaILAId = table.Column<int>(type: "INTEGER", nullable: true),
                    MetaILAName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    MetaILADesc = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    MetaILAStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Reason = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    MetaILAAssessmentID = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_MetaILAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_MetaILAs_MetaILA_Statuses_MetaILAStatusId",
                        column: x => x.MetaILAStatusId,
                        principalTable: "MetaILA_Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_MetaILAs_MetaILAAssessments_MetaILAAssessmentID",
                        column: x => x.MetaILAAssessmentID,
                        principalTable: "MetaILAAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_MetaILAs_MetaILAs_MetaILAId",
                        column: x => x.MetaILAId,
                        principalTable: "MetaILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientUserSettings_DashboardSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    DashboardSettingId = table.Column<int>(type: "INTEGER", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientUserSettings_DashboardSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientUserSettings_DashboardSettings_ClientUsers_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientUserSettings_DashboardSettings_DashboardSettings_DashboardSettingId",
                        column: x => x.DashboardSettingId,
                        principalTable: "DashboardSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorInvitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvitedByEID = table.Column<int>(type: "INTEGER", nullable: false),
                    InviteeEID = table.Column<int>(type: "INTEGER", nullable: false),
                    InviteeEmailID = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    InviteDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InvitedMessage = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorInvitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorInvitations_Employees_InvitedByEID",
                        column: x => x.InvitedByEID,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaboratorInvitations_Employees_InviteeEID",
                        column: x => x.InviteeEID,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeActivityNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ActivityNotificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeActivityNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeActivityNotifications_ActivityNotifications_ActivityNotificationId",
                        column: x => x.ActivityNotificationId,
                        principalTable: "ActivityNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeActivityNotifications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCertifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CertificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RenewalDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RollOverHours = table.Column<int>(type: "INTEGER", nullable: true),
                    CertificationNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCertifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCertifications_Certifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeCertifications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCertifictaionHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeID = table.Column<int>(type: "INTEGER", nullable: false),
                    NewCertificationID = table.Column<int>(type: "INTEGER", nullable: false),
                    OldCertificationID = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IssueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DRADate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCertifictaionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCertifictaionHistories_Certifications_NewCertificationID",
                        column: x => x.NewCertificationID,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeCertifictaionHistories_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeID = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    FileSize = table.Column<string>(type: "TEXT", nullable: true),
                    FileType = table.Column<string>(type: "TEXT", nullable: true),
                    FileAsBase64 = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDocuments_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeID = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeHistories_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeOrganizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrganizationId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsManager = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeOrganizations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeOrganizations_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Trainee = table.Column<bool>(type: "INTEGER", nullable: false),
                    QualificationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ManagerName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    TrainingProgramVersion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IsSignificant = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCertificationRequired = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePositions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDP_Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Instructions = table.Column<string>(type: "TEXT", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    IsStarted = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IDP_ReviewStatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDP_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDP_Review_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDP_Review_IDP_ReviewStatuses_IDP_ReviewStatusId",
                        column: x => x.IDP_ReviewStatusId,
                        principalTable: "IDP_ReviewStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Position_Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Trainee = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Position_Employees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Position_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Version_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Employees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedure_StatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure_StatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedure_StatusHistories_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcedureReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcedureReviewTitle = table.Column<string>(type: "TEXT", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProcedureReviewInstructions = table.Column<string>(type: "TEXT", nullable: true),
                    IsEmployeeShowResponses = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcedureReviews_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Procedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcedureNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    MajorVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    MinorVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Procedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Procedures_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    NickName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    OtherAssesmentTool = table.Column<string>(type: "TEXT", nullable: true),
                    OtherNercTargetAudience = table.Column<string>(type: "TEXT", nullable: true),
                    TrainingPlan = table.Column<string>(type: "TEXT", nullable: true),
                    ProviderId = table.Column<int>(type: "INTEGER", nullable: false),
                    TopicId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsSelfPaced = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsOptional = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeliveryMethodId = table.Column<int>(type: "INTEGER", nullable: true),
                    HasPilotData = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: false),
                    IsProgramManual = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    SubmissionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CBTRequiredForCourse = table.Column<bool>(type: "INTEGER", nullable: false),
                    TrainingEvalMethods = table.Column<string>(type: "TEXT", nullable: true),
                    UseForEMP = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILAs_DeliveryMethods_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalTable: "DeliveryMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ILAs_ILA_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "ILA_Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ILAs_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentEvaluation_Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentEvaluationId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionBankId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvaluation_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEvaluation_Questions_QuestionBanks_QuestionBankId",
                        column: x => x.QuestionBankId,
                        principalTable: "QuestionBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEvaluation_Questions_StudentEvaluations_StudentEvaluationId",
                        column: x => x.StudentEvaluationId,
                        principalTable: "StudentEvaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentEvaluationHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentEvaluationId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentEvaluationNotes = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvaluationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEvaluationHistories_StudentEvaluations_StudentEvaluationId",
                        column: x => x.StudentEvaluationId,
                        principalTable: "StudentEvaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentEvaluationQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EvalFormID = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionText = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    QuestionNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionImage = table.Column<string>(type: "TEXT", nullable: true),
                    QuestionFiles = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvaluationQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEvaluationQuestions_StudentEvaluationForms_EvalFormID",
                        column: x => x.EvalFormID,
                        principalTable: "StudentEvaluationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedure_RR_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    RegulatoryRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure_RR_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedure_RR_Links_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedure_RR_Links_RegulatoryRequirements_RegulatoryRequirementId",
                        column: x => x.RegulatoryRequirementId,
                        principalTable: "RegulatoryRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RR_StatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegulatoryRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RR_StatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RR_StatusHistories_RegulatoryRequirements_RegulatoryRequirementId",
                        column: x => x.RegulatoryRequirementId,
                        principalTable: "RegulatoryRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_RegulatoryRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegulatoryRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    IssuingAuthorityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    RevisionNumber = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Uploads = table.Column<byte[]>(type: "BLOB", nullable: true),
                    HyperLink = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_RegulatoryRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_RegulatoryRequirements_RegulatoryRequirements_RegulatoryRequirementId",
                        column: x => x.RegulatoryRequirementId,
                        principalTable: "RegulatoryRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedure_SaftyHazard_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    SaftyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure_SaftyHazard_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedure_SaftyHazard_Links_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedure_SaftyHazard_Links_SaftyHazards_SaftyHazardId",
                        column: x => x.SaftyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafetyHazard_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SafetyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyHazard_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_Histories_SaftyHazards_SafetyHazardId",
                        column: x => x.SafetyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafetyHazard_Set_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SafetyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    SafetyHazardSetId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyHazard_Set_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_Set_Links_SafetyHazard_Sets_SafetyHazardSetId",
                        column: x => x.SafetyHazardSetId,
                        principalTable: "SafetyHazard_Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_Set_Links_SaftyHazards_SafetyHazardId",
                        column: x => x.SafetyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaftyHazard_Abatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SaftyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaftyHazard_Abatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaftyHazard_Abatements_SaftyHazards_SaftyHazardId",
                        column: x => x.SaftyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaftyHazard_Controls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SaftyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaftyHazard_Controls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaftyHazard_Controls_SaftyHazards_SaftyHazardId",
                        column: x => x.SaftyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaftyHazard_RR_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegulatoryRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    SafetyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaftyHazard_RR_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaftyHazard_RR_Links_RegulatoryRequirements_RegulatoryRequirementId",
                        column: x => x.RegulatoryRequirementId,
                        principalTable: "RegulatoryRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaftyHazard_RR_Links_SaftyHazards_SafetyHazardId",
                        column: x => x.SafetyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_SaftyHazards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SaftyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    PersonalProtectiveEquipment = table.Column<string>(type: "TEXT", nullable: true),
                    MinorVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    MajorVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_SaftyHazards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_SaftyHazards_SaftyHazards_SaftyHazardId",
                        column: x => x.SaftyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulatorScenarioPositon_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SimulatorScenarioID = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionID = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorScenarioPositon_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatorScenarioPositon_Links_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimulatorScenarioPositon_Links_SimulatorScenarios_SimulatorScenarioID",
                        column: x => x.SimulatorScenarioID,
                        principalTable: "SimulatorScenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Test_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_Histories_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestTitle = table.Column<string>(type: "TEXT", nullable: true),
                    TestId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_Number = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    IsInUse = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Tests_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Version_Tests_TestStatuses_TestStatusId",
                        column: x => x.TestStatusId,
                        principalTable: "TestStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SafetyHazard_Tool_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SafetyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyHazard_Tool_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_Tool_Links_SaftyHazards_SafetyHazardId",
                        column: x => x.SafetyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_Tool_Links_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tool_StatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tool_StatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tool_StatusHistories_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToolGroup_Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToolGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolGroup_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToolGroup_Tools_ToolGroups_ToolGroupId",
                        column: x => x.ToolGroupId,
                        principalTable: "ToolGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToolGroup_Tools_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    MinorVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    MajorVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Tools_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_TrainingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_TrainingGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupName = table.Column<string>(type: "TEXT", nullable: false),
                    GroupDescription = table.Column<string>(type: "TEXT", nullable: true),
                    HyperLink = table.Column<string>(type: "TEXT", nullable: true),
                    PDF = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_TrainingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_TrainingGroups_TrainingGroups_Version_TrainingGroupId",
                        column: x => x.Version_TrainingGroupId,
                        principalTable: "TrainingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_TrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingProgramTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TPVersionNo = table.Column<string>(type: "TEXT", nullable: true),
                    ProgramTitle = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Year = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: true),
                    IsInUse = table.Column<bool>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_TrainingPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_TrainingPrograms_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Version_TrainingPrograms_TrainingPrograms_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee_Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    MajorVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    MinorVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    Archived = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Tasks_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Tasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Position_Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Position_Tasks_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Position_Tasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedure_Task_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure_Task_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedure_Task_Links_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedure_Task_Links_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RR_Task_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RR_Task_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RR_Task_Links_RegulatoryRequirements_RegRequirementId",
                        column: x => x.RegRequirementId,
                        principalTable: "RegulatoryRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RR_Task_Links_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafetyHazard_Task_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SaftyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyHazard_Task_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_Task_Links_SaftyHazards_SaftyHazardId",
                        column: x => x.SaftyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_Task_Links_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulatorScenarioTaskObjectives_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SimulatorScenarioID = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskID = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorScenarioTaskObjectives_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatorScenarioTaskObjectives_Links_SimulatorScenarios_SimulatorScenarioID",
                        column: x => x.SimulatorScenarioID,
                        principalTable: "SimulatorScenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimulatorScenarioTaskObjectives_Links_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task_Collaborator_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskCollabInviteId = table.Column<int>(type: "INTEGER", nullable: false),
                    isTaskCreator = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_Collaborator_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Collaborator_Links_Task_CollaboratorInvitations_TaskCollabInviteId",
                        column: x => x.TaskCollabInviteId,
                        principalTable: "Task_CollaboratorInvitations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Collaborator_Links_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task_MetaTask_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Meta_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_MetaTask_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_MetaTask_Links_Tasks_Meta_TaskId",
                        column: x => x.Meta_TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_MetaTask_Links_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Task_Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Positions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Positions_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task_Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", nullable: false),
                    QuestionNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Questions_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task_Reference_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskReferenceId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_Reference_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Reference_Links_Task_References_TaskReferenceId",
                        column: x => x.TaskReferenceId,
                        principalTable: "Task_References",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Reference_Links_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task_Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: true),
                    ParentStepId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Steps_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task_Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Suggestions_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task_Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Tools_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Tools_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task_TrainingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_TrainingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_TrainingGroups_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_TrainingGroups_TrainingGroups_TrainingGroupId",
                        column: x => x.TrainingGroupId,
                        principalTable: "TrainingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskQualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpId = table.Column<int>(type: "INTEGER", nullable: false),
                    EvaluationId = table.Column<int>(type: "INTEGER", nullable: true),
                    TaskQualificationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TaskQualificationEvaluator = table.Column<string>(type: "TEXT", nullable: true),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CriteriaMet = table.Column<bool>(type: "INTEGER", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    IsReleasedToEMP = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    TQStatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    EvaluatorId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskQualifications_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskQualifications_Employees_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskQualifications_EvaluationMethods_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "EvaluationMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskQualifications_TaskQualificationStatuses_TQStatusId",
                        column: x => x.TQStatusId,
                        principalTable: "TaskQualificationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskQualifications_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskNumber = table.Column<string>(type: "TEXT", nullable: true),
                    VersionNumber = table.Column<string>(type: "TEXT", nullable: true),
                    RequalificationDueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RequalificationRequired = table.Column<bool>(type: "INTEGER", nullable: true),
                    RequalificationNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Conditions = table.Column<string>(type: "TEXT", nullable: true),
                    Standards = table.Column<string>(type: "TEXT", nullable: true),
                    Critical = table.Column<bool>(type: "INTEGER", nullable: false),
                    Tools = table.Column<string>(type: "TEXT", nullable: true),
                    References = table.Column<string>(type: "TEXT", nullable: true),
                    RequiredTime = table.Column<int>(type: "INTEGER", nullable: false),
                    Abbreviation = table.Column<string>(type: "TEXT", nullable: true),
                    TaskCriteriaUpload = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    IsMeta = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsReliability = table.Column<bool>(type: "INTEGER", nullable: false),
                    Criteria = table.Column<string>(type: "TEXT", nullable: true),
                    IsInUse = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Tasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomEnablingObjectives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EO_TopicId = table.Column<int>(type: "INTEGER", nullable: true),
                    EO_CatId = table.Column<int>(type: "INTEGER", nullable: true),
                    EO_SubCatId = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsAddtoEO = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CustomEONumber = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomEnablingObjectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomEnablingObjectives_EnablingObjective_Categories_EO_CatId",
                        column: x => x.EO_CatId,
                        principalTable: "EnablingObjective_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomEnablingObjectives_EnablingObjective_SubCategories_EO_SubCatId",
                        column: x => x.EO_SubCatId,
                        principalTable: "EnablingObjective_SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomEnablingObjectives_EnablingObjective_Topics_EO_TopicId",
                        column: x => x.EO_TopicId,
                        principalTable: "EnablingObjective_Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_TopicHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnablingObjectiveTopicId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_TopicHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_TopicHistories_EnablingObjective_Topics_EnablingObjectiveTopicId",
                        column: x => x.EnablingObjectiveTopicId,
                        principalTable: "EnablingObjective_Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjectives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    TopicId = table.Column<int>(type: "INTEGER", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    isMetaEO = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSkillQualification = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    References = table.Column<string>(type: "TEXT", nullable: true),
                    Criteria = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Conditions = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EnablingObjective_CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    EnablingObjective_SubCategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjectives_EnablingObjective_Categories_EnablingObjective_CategoryId",
                        column: x => x.EnablingObjective_CategoryId,
                        principalTable: "EnablingObjective_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnablingObjectives_EnablingObjective_SubCategories_EnablingObjective_SubCategoryId",
                        column: x => x.EnablingObjective_SubCategoryId,
                        principalTable: "EnablingObjective_SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnablingObjectives_EnablingObjective_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "EnablingObjective_Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcedureReview_Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcedureReviewId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcedureReviewResponse = table.Column<string>(type: "TEXT", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsStarted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureReview_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcedureReview_Employees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedureReview_Employees_ProcedureReviews_ProcedureReviewId",
                        column: x => x.ProcedureReviewId,
                        principalTable: "ProcedureReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CBTs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    Availablity = table.Column<int>(type: "INTEGER", nullable: false),
                    CBTLearningContractInstructions = table.Column<string>(type: "TEXT", nullable: true),
                    DueDateAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    DueDateInterval = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CBTs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CBTs_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecurrenceId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsRecurring = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    ProviderID = table.Column<int>(type: "INTEGER", nullable: true),
                    ILAID = table.Column<int>(type: "INTEGER", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InstructorId = table.Column<int>(type: "INTEGER", nullable: true),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: true),
                    SpecialInstructions = table.Column<string>(type: "TEXT", nullable: true),
                    WebinarLink = table.Column<string>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSchedules_ClassSchedules_RecurrenceId",
                        column: x => x.RecurrenceId,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSchedules_ILAs_ILAID",
                        column: x => x.ILAID,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassSchedules_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassSchedules_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassSchedules_Providers_ProviderID",
                        column: x => x.ProviderID,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationReleaseEMPSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    EvaluationRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    EvaluationUsedToDeployStudentEvaluation = table.Column<bool>(type: "INTEGER", nullable: false),
                    jobDetails = table.Column<string>(type: "TEXT", nullable: true),
                    EvaluationAvailableOnStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
                    EvaluationAvailableOnEndDate = table.Column<bool>(type: "INTEGER", nullable: false),
                    FinalGradeRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseOnSpecificTimeAfterClassEndDate = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseAfterEndTime = table.Column<int>(type: "INTEGER", nullable: true),
                    ReleasePrior = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseAfterGradeAssigned = table.Column<bool>(type: "INTEGER", nullable: false),
                    EvaluationDueDate = table.Column<int>(type: "INTEGER", nullable: true),
                    HasWeek = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationReleaseEMPSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationReleaseEMPSettings_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    IDPYear = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Grade = table.Column<string>(type: "TEXT", nullable: true),
                    Score = table.Column<string>(type: "TEXT", nullable: true),
                    GradeUpdateReason = table.Column<string>(type: "TEXT", nullable: true),
                    taskQualificationCompleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    completionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDPs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDPs_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_AssessmentTool_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssessmentToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_AssessmentTool_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_AssessmentTool_Links_AssessmentTools_AssessmentToolId",
                        column: x => x.AssessmentToolId,
                        principalTable: "AssessmentTools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_AssessmentTool_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_Evaluator_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    EvaluatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_Evaluator_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_Evaluator_Links_Employees_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_Evaluator_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_NERCAudience_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    NERCAudienceID = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_NERCAudience_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_NERCAudience_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_NERCAudience_Links_NERCTargetAudiences_NERCAudienceID",
                        column: x => x.NERCAudienceID,
                        principalTable: "NERCTargetAudiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_NercStandard_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    StdId = table.Column<int>(type: "INTEGER", nullable: false),
                    NERCStdMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditHoursByStd = table.Column<float>(type: "REAL", nullable: false, defaultValue: 0f),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_NercStandard_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_NercStandard_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_NercStandard_Links_NercStandardMembers_NERCStdMemberId",
                        column: x => x.NERCStdMemberId,
                        principalTable: "NercStandardMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ILA_NercStandard_Links_NercStandards_StdId",
                        column: x => x.StdId,
                        principalTable: "NercStandards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_PerformTraineeEvals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_PerformTraineeEvals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_PerformTraineeEvals_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_Position_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_Position_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_Position_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_Position_Links_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_PreRequisite_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    PreRequisiteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_PreRequisite_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_PreRequisite_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ILA_PreRequisite_Links_ILAs_PreRequisiteId",
                        column: x => x.PreRequisiteId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_Procedure_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_Procedure_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_Procedure_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_Procedure_Links_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_RegRequirement_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    RegulatoryRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_RegRequirement_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_RegRequirement_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_RegRequirement_Links_RegulatoryRequirements_RegulatoryRequirementId",
                        column: x => x.RegulatoryRequirementId,
                        principalTable: "RegulatoryRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_SafetyHazard_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    SafetyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_SafetyHazard_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_SafetyHazard_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_SafetyHazard_Links_SaftyHazards_SafetyHazardId",
                        column: x => x.SafetyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_Segment_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    SegmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_Segment_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_Segment_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_Segment_Links_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_StudentEvaluation_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    studentEvalFormID = table.Column<int>(type: "INTEGER", nullable: false),
                    studentEvalAvailabilityID = table.Column<int>(type: "INTEGER", nullable: true),
                    studentEvalAudienceID = table.Column<int>(type: "INTEGER", nullable: true),
                    isAllQuestionMandatory = table.Column<bool>(type: "INTEGER", nullable: true),
                    StudentEvaluationAvailabilityId = table.Column<int>(type: "INTEGER", nullable: true),
                    StudentEvaluationAudienceId = table.Column<int>(type: "INTEGER", nullable: true),
                    StudentEvaluationFormId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_StudentEvaluation_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_StudentEvaluation_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_StudentEvaluation_Links_StudentEvaluationAudiences_StudentEvaluationAudienceId",
                        column: x => x.StudentEvaluationAudienceId,
                        principalTable: "StudentEvaluationAudiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ILA_StudentEvaluation_Links_StudentEvaluationAvailabilities_StudentEvaluationAvailabilityId",
                        column: x => x.StudentEvaluationAvailabilityId,
                        principalTable: "StudentEvaluationAvailabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ILA_StudentEvaluation_Links_StudentEvaluationForms_StudentEvaluationFormId",
                        column: x => x.StudentEvaluationFormId,
                        principalTable: "StudentEvaluationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ILA_StudentEvaluation_Links_StudentEvaluations_studentEvalFormID",
                        column: x => x.studentEvalFormID,
                        principalTable: "StudentEvaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_TaskObjective_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    UseForTQ = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_TaskObjective_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_TaskObjective_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_TaskObjective_Links_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_TrainingTopic_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrTopicId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_TrainingTopic_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_TrainingTopic_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_TrainingTopic_Links_TrainingTopics_TrTopicId",
                        column: x => x.TrTopicId,
                        principalTable: "TrainingTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_Uploads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    FileSize = table.Column<string>(type: "TEXT", nullable: true),
                    FileType = table.Column<string>(type: "TEXT", nullable: true),
                    FileAsBase64 = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_Uploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_Uploads_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILACertificationLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CertificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalHours = table.Column<double>(type: "REAL", nullable: false),
                    IsIncludeSimulation = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEmergencyOpHours = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPartialCreditHours = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILACertificationLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILACertificationLinks_Certifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILACertificationLinks_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILACollaborators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    CollaboratorInviteId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILACollaborators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILACollaborators_CollaboratorInvitations_CollaboratorInviteId",
                        column: x => x.CollaboratorInviteId,
                        principalTable: "CollaboratorInvitations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILACollaborators_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ILACollaborators_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILAHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILAHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILAHistories_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILATraineeEvaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestId = table.Column<int>(type: "INTEGER", nullable: false),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestTypeId = table.Column<int>(type: "INTEGER", nullable: true),
                    EvaluationTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestTitle = table.Column<string>(type: "TEXT", nullable: true),
                    TestInstruction = table.Column<string>(type: "TEXT", nullable: true),
                    TestTimeLimitHours = table.Column<int>(type: "INTEGER", nullable: false),
                    TestTimeLimitMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingEvaluationMethod = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILATraineeEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILATraineeEvaluations_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILATraineeEvaluations_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILATraineeEvaluations_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ILATraineeEvaluations_TraineeEvaluationTypes_EvaluationTypeId",
                        column: x => x.EvaluationTypeId,
                        principalTable: "TraineeEvaluationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meta_ILAMembers_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MetaILAID = table.Column<int>(type: "INTEGER", nullable: false),
                    ILAID = table.Column<int>(type: "INTEGER", nullable: false),
                    MetaILAConfigPublishOptionID = table.Column<int>(type: "INTEGER", nullable: false),
                    SequenceNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meta_ILAMembers_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meta_ILAMembers_Links_ILAs_ILAID",
                        column: x => x.ILAID,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meta_ILAMembers_Links_MetaILAConfigurationPublishOptions_MetaILAConfigPublishOptionID",
                        column: x => x.MetaILAConfigPublishOptionID,
                        principalTable: "MetaILAConfigurationPublishOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meta_ILAMembers_Links_MetaILAs_MetaILAID",
                        column: x => x.MetaILAID,
                        principalTable: "MetaILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedure_ILA_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure_ILA_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedure_ILA_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedure_ILA_Links_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegRequirement_ILA_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegulatoryRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    ILAID = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegRequirement_ILA_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegRequirement_ILA_Links_ILAs_ILAID",
                        column: x => x.ILAID,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegRequirement_ILA_Links_RegulatoryRequirements_RegulatoryRequirementId",
                        column: x => x.RegulatoryRequirementId,
                        principalTable: "RegulatoryRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafetyHazard_ILA_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    SafetyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyHazard_ILA_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_ILA_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_ILA_Links_SaftyHazards_SafetyHazardId",
                        column: x => x.SafetyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelfRegistrationOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    MakeAvailableForSelfReg = table.Column<bool>(type: "INTEGER", nullable: false),
                    RequireAdminApproval = table.Column<bool>(type: "INTEGER", nullable: false),
                    AcknowledgeRegDisclaimer = table.Column<bool>(type: "INTEGER", nullable: false),
                    RegDisclaimer = table.Column<string>(type: "TEXT", nullable: true),
                    LimitForLinkedPositions = table.Column<bool>(type: "INTEGER", nullable: false),
                    CloseRegOnStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
                    ClassSize = table.Column<int>(type: "INTEGER", nullable: true),
                    EnableWaitlist = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfRegistrationOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelfRegistrationOptions_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulatorScenarioILA_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SimulatorScenarioID = table.Column<int>(type: "INTEGER", nullable: false),
                    ILAID = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorScenarioILA_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatorScenarioILA_Links_ILAs_ILAID",
                        column: x => x.ILAID,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimulatorScenarioILA_Links_SimulatorScenarios_SimulatorScenarioID",
                        column: x => x.SimulatorScenarioID,
                        principalTable: "SimulatorScenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task_ILA_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_ILA_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_ILA_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_ILA_Links_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestReleaseEMPSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    FinalTestId = table.Column<int>(type: "INTEGER", nullable: true),
                    PreTestId = table.Column<int>(type: "INTEGER", nullable: true),
                    UsePreTestAndTest = table.Column<bool>(type: "INTEGER", nullable: false),
                    PreTestRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    jobDetails = table.Column<string>(type: "TEXT", nullable: true),
                    PreTestAvailableOnEnrollment = table.Column<bool>(type: "INTEGER", nullable: false),
                    PreTestAvailableOneStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
                    PreTestScore = table.Column<int>(type: "INTEGER", nullable: false),
                    ShowStudentSubmittedPreTestAnswers = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowCorrectIncorrectPreTestAnswers = table.Column<bool>(type: "INTEGER", nullable: false),
                    MakeAvailableBeforeDays = table.Column<int>(type: "INTEGER", nullable: true),
                    FinalTestPassingScore = table.Column<string>(type: "TEXT", nullable: true),
                    MakeFinalTestAvailableImmediatelyAfterStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
                    MakeFinalTestAvailableOnClassEndDate = table.Column<bool>(type: "INTEGER", nullable: false),
                    MakeFinalTestAvailableAfterCBTCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    MakeFinalTestAvailableOnSpecificTime = table.Column<int>(type: "INTEGER", nullable: true),
                    FinalTestSpecificTimePrior = table.Column<bool>(type: "INTEGER", nullable: false),
                    FinalTestDueDate = table.Column<int>(type: "INTEGER", nullable: false),
                    ShowStudentSubmittedFinalTestAnswers = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowStudentSubmittedRetakeTestAnswers = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowCorrectIncorrectFinalTestAnswers = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowCorrectIncorrectRetakeTestAnswers = table.Column<bool>(type: "INTEGER", nullable: false),
                    AutoReleaseRetake = table.Column<bool>(type: "INTEGER", nullable: false),
                    RetakeEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    NumberOfRetakes = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestReleaseEMPSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestReleaseEMPSettings_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestReleaseEMPSettings_Tests_FinalTestId",
                        column: x => x.FinalTestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestReleaseEMPSettings_Tests_PreTestId",
                        column: x => x.PreTestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TQILAEmpSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    TQRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseAtOnce = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseOneAtTime = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseOnClassStart = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseOnClassEnd = table.Column<bool>(type: "INTEGER", nullable: false),
                    SpecificTime = table.Column<int>(type: "INTEGER", nullable: true),
                    PriorToSpecificTime = table.Column<bool>(type: "INTEGER", nullable: false),
                    OneSignOffRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    MultipleSignOffRequired = table.Column<int>(type: "INTEGER", nullable: true),
                    TQDueDate = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TQILAEmpSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TQILAEmpSettings_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPrograms_ILA_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrainingProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPrograms_ILA_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPrograms_ILA_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingPrograms_ILA_Links_TrainingPrograms_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_ILAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    NickName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    TrainingPlan = table.Column<string>(type: "TEXT", nullable: true),
                    ProviderId = table.Column<int>(type: "INTEGER", nullable: false),
                    TopicId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsSelfPaced = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsOptional = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeliveryMethodId = table.Column<int>(type: "INTEGER", nullable: true),
                    HasPilotData = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: false),
                    IsProgramManual = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    SubmissionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OtherAssesmentTool = table.Column<string>(type: "TEXT", nullable: true),
                    OtherNercTargetAudience = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ChangeReason = table.Column<string>(type: "TEXT", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_ILAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_ILAs_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Procedure_SaftyHazard_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_SaftyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Procedure_SaftyHazard_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Procedure_SaftyHazard_Links_Version_Procedures_Version_ProcedureId",
                        column: x => x.Version_ProcedureId,
                        principalTable: "Version_Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Procedure_SaftyHazard_Links_Version_SaftyHazards_Version_SaftyHazardId",
                        column: x => x.Version_SaftyHazardId,
                        principalTable: "Version_SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_SaftyHazard_Abatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_SaftyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_SaftyHazard_Abatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_SaftyHazard_Abatements_Version_SaftyHazards_Version_SaftyHazardId",
                        column: x => x.Version_SaftyHazardId,
                        principalTable: "Version_SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_SaftyHazard_Controls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_SaftyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_SaftyHazard_Controls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_SaftyHazard_Controls_Version_SaftyHazards_Version_SaftyHazardId",
                        column: x => x.Version_SaftyHazardId,
                        principalTable: "Version_SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Procedure_Tool_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_ToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Procedure_Tool_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Procedure_Tool_Links_Version_Procedures_Version_ProcedureId",
                        column: x => x.Version_ProcedureId,
                        principalTable: "Version_Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Procedure_Tool_Links_Version_Tools_Version_ToolId",
                        column: x => x.Version_ToolId,
                        principalTable: "Version_Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgram_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrainingProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingProgramVersionId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Version_TrainingProgramId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgram_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingProgram_Histories_TrainingPrograms_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainingProgram_Histories_Version_TrainingPrograms_Version_TrainingProgramId",
                        column: x => x.Version_TrainingProgramId,
                        principalTable: "Version_TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Timesheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MethodId = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheets_Employee_Tasks_EmployeeTaskId",
                        column: x => x.EmployeeTaskId,
                        principalTable: "Employee_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskQualification_Evaluator_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EvaluatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskQualificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskQualification_Evaluator_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskQualification_Evaluator_Links_Employees_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskQualification_Evaluator_Links_TaskQualifications_TaskQualificationId",
                        column: x => x.TaskQualificationId,
                        principalTable: "TaskQualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskReQualificationEmp_QuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskQualificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskQuestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    EvaluatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TraineeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskReQualificationEmp_QuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_QuestionAnswers_Employees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_QuestionAnswers_Task_Questions_TaskQuestionId",
                        column: x => x.TaskQuestionId,
                        principalTable: "Task_Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_QuestionAnswers_TaskQualifications_TaskQualificationId",
                        column: x => x.TaskQualificationId,
                        principalTable: "TaskQualifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskReQualificationEmp_SignOffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskQualificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCriteriaMet = table.Column<bool>(type: "INTEGER", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    EvaluatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    EvaluationMethodId = table.Column<int>(type: "INTEGER", nullable: true),
                    TaskQualificationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TraineeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsStarted = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: true),
                    SignOffDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskReQualificationEmp_SignOffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_SignOffs_Employees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_SignOffs_EvaluationMethods_EvaluationMethodId",
                        column: x => x.EvaluationMethodId,
                        principalTable: "EvaluationMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_SignOffs_TaskQualifications_TaskQualificationId",
                        column: x => x.TaskQualificationId,
                        principalTable: "TaskQualifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskReQualificationEmp_Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskQualificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskStepId = table.Column<int>(type: "INTEGER", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    EvaluatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TraineeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskReQualificationEmp_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_Steps_Employees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_Steps_Task_Steps_TaskStepId",
                        column: x => x.TaskStepId,
                        principalTable: "Task_Steps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_Steps_TaskQualifications_TaskQualificationId",
                        column: x => x.TaskQualificationId,
                        principalTable: "TaskQualifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskReQualificationEmp_Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskQualificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskSuggestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    EvaluatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TraineeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskReQualificationEmp_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_Suggestions_Employees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_Suggestions_Task_Suggestions_TaskSuggestionId",
                        column: x => x.TaskSuggestionId,
                        principalTable: "Task_Suggestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskReQualificationEmp_Suggestions_TaskQualifications_TaskQualificationId",
                        column: x => x.TaskQualificationId,
                        principalTable: "TaskQualifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TQEmpSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskQualificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReleaseToAllSingleSignOff = table.Column<bool>(type: "INTEGER", nullable: false),
                    MultipleSignOff = table.Column<int>(type: "INTEGER", nullable: true),
                    ReleaseOnReleaseDate = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseInSpecificOrder = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowTaskSuggestions = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowTaskQuestions = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TQEmpSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TQEmpSettings_TaskQualifications_TaskQualificationId",
                        column: x => x.TaskQualificationId,
                        principalTable: "TaskQualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Histories_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Histories_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_MetaTask_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_MetaTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_MetaTask_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_MetaTask_Links_Version_Tasks_Version_MetaTaskId",
                        column: x => x.Version_MetaTaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Task_MetaTask_Links_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_Position_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_Position_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_Position_Links_Version_Positions_Version_PositionId",
                        column: x => x.Version_PositionId,
                        principalTable: "Version_Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Task_Position_Links_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_Procedure_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_Procedure_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_Procedure_Links_Version_Procedures_Version_ProcedureId",
                        column: x => x.Version_ProcedureId,
                        principalTable: "Version_Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Task_Procedure_Links_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskQuestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", nullable: false),
                    QuestionNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_Questions_Task_Questions_TaskQuestionId",
                        column: x => x.TaskQuestionId,
                        principalTable: "Task_Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Version_Task_Questions_Version_Tasks_VersionTaskId",
                        column: x => x.VersionTaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_RR_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_RegulatoryRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_RR_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_RR_Links_Version_RegulatoryRequirements_Version_RegulatoryRequirementId",
                        column: x => x.Version_RegulatoryRequirementId,
                        principalTable: "Version_RegulatoryRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Task_RR_Links_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_SaftyHazard_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_SaftyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_SaftyHazard_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_SaftyHazard_Links_Version_SaftyHazards_Version_SaftyHazardId",
                        column: x => x.Version_SaftyHazardId,
                        principalTable: "Version_SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Task_SaftyHazard_Links_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskStepId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_Steps_Task_Steps_TaskStepId",
                        column: x => x.TaskStepId,
                        principalTable: "Task_Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Task_Steps_Version_Tasks_VersionTaskId",
                        column: x => x.VersionTaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Task_SuggestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_Suggestions_Task_Suggestions_Task_SuggestionId",
                        column: x => x.Task_SuggestionId,
                        principalTable: "Task_Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Task_Suggestions_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_Tool_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_ToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_Tool_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_Tool_Links_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Task_Tool_Links_Version_Tools_Version_ToolId",
                        column: x => x.Version_ToolId,
                        principalTable: "Version_Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_TrainingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_TrainingGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_TrainingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_TrainingGroups_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Task_TrainingGroups_Version_TrainingGroups_Version_TrainingGroupId",
                        column: x => x.Version_TrainingGroupId,
                        principalTable: "Version_TrainingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILACustomObjective_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomObjId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILACustomObjective_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILACustomObjective_Links_CustomEnablingObjectives_CustomObjId",
                        column: x => x.CustomObjId,
                        principalTable: "CustomEnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILACustomObjective_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_Employee_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EOID = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_Employee_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_Employee_Links_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_Employee_Links_EnablingObjectives_EOID",
                        column: x => x.EOID,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_MetaEO_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MetaEOId = table.Column<int>(type: "INTEGER", nullable: false),
                    EOID = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_MetaEO_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_MetaEO_Links_EnablingObjectives_EOID",
                        column: x => x.EOID,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_MetaEO_Links_EnablingObjectives_MetaEOId",
                        column: x => x.MetaEOId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", nullable: false),
                    QuestionNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_Questions_EnablingObjectives_EnablingObjectiveId",
                        column: x => x.EnablingObjectiveId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EOId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: true),
                    ParentStepId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_Steps_EnablingObjectives_EOId",
                        column: x => x.EOId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EOId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_Suggestions_EnablingObjectives_EOId",
                        column: x => x.EOId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjective_Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EOId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjective_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_Tools_EnablingObjectives_EOId",
                        column: x => x.EOId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnablingObjective_Tools_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILA_EnablingObjective_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILA_EnablingObjective_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILA_EnablingObjective_Links_EnablingObjectives_EnablingObjectiveId",
                        column: x => x.EnablingObjectiveId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILA_EnablingObjective_Links_ILAs_ILAId",
                        column: x => x.ILAId,
                        principalTable: "ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions_SQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    EOId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions_SQs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_SQs_EnablingObjectives_EOId",
                        column: x => x.EOId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_SQs_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedure_EnablingObjective_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure_EnablingObjective_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedure_EnablingObjective_Links_EnablingObjectives_EnablingObjectiveId",
                        column: x => x.EnablingObjectiveId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedure_EnablingObjective_Links_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegRequirement_EO_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegulatoryRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    EOID = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegRequirement_EO_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegRequirement_EO_Links_EnablingObjectives_EOID",
                        column: x => x.EOID,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegRequirement_EO_Links_RegulatoryRequirements_RegulatoryRequirementId",
                        column: x => x.RegulatoryRequirementId,
                        principalTable: "RegulatoryRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafetyHazard_EO_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SafetyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    EOID = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyHazard_EO_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_EO_Links_EnablingObjectives_EOID",
                        column: x => x.EOID,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafetyHazard_EO_Links_SaftyHazards_SafetyHazardId",
                        column: x => x.SafetyHazardId,
                        principalTable: "SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SegmentObjective_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SegmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: true),
                    EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentObjective_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SegmentObjective_Links_EnablingObjectives_EnablingObjectiveId",
                        column: x => x.EnablingObjectiveId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SegmentObjective_Links_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SegmentObjective_Links_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SimulatorScenario_EnablingObjectives_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SimulatorScenarioID = table.Column<int>(type: "INTEGER", nullable: false),
                    EnablingObjectiveID = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorScenario_EnablingObjectives_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatorScenario_EnablingObjectives_Links_EnablingObjectives_EnablingObjectiveID",
                        column: x => x.EnablingObjectiveID,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimulatorScenario_EnablingObjectives_Links_SimulatorScenarios_SimulatorScenarioID",
                        column: x => x.SimulatorScenarioID,
                        principalTable: "SimulatorScenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task_EnablingObjective_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_EnablingObjective_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_EnablingObjective_Links_EnablingObjectives_EnablingObjectiveId",
                        column: x => x.EnablingObjectiveId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_EnablingObjective_Links_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestItemTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxonomyId = table.Column<int>(type: "INTEGER", nullable: false),
                    EOId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestItems_EnablingObjectives_EOId",
                        column: x => x.EOId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestItems_TaxonomyLevels_TaxonomyId",
                        column: x => x.TaxonomyId,
                        principalTable: "TaxonomyLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestItems_TestItemTypes_TestItemTypeId",
                        column: x => x.TestItemTypeId,
                        principalTable: "TestItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjectives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    TopicId = table.Column<int>(type: "INTEGER", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    isMetaEO = table.Column<bool>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    IsInUse = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjectives_EnablingObjectives_EnablingObjectiveId",
                        column: x => x.EnablingObjectiveId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CBT_ScormUpload",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CbtId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ScormStatus = table.Column<string>(type: "TEXT", nullable: true),
                    ConnectedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DisconnectedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CBT_ScormUpload", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CBT_ScormUpload_CBTs_CbtId",
                        column: x => x.CbtId,
                        principalTable: "CBTs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSchedule_Evaluation_Roster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ClassScheduleId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentEvaluationId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsStarted = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAllowed = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsReleased = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedule_Evaluation_Roster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSchedule_Evaluation_Roster_ClassSchedules_ClassScheduleId",
                        column: x => x.ClassScheduleId,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSchedule_Evaluation_Roster_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSchedule_Evaluation_Roster_StudentEvaluations_StudentEvaluationId",
                        column: x => x.StudentEvaluationId,
                        principalTable: "StudentEvaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSchedule_Recurrences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClassId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecurrenceType = table.Column<string>(type: "TEXT", nullable: true),
                    RecurrencePattern = table.Column<string>(type: "TEXT", nullable: true),
                    Mon = table.Column<bool>(type: "INTEGER", nullable: true),
                    Tue = table.Column<bool>(type: "INTEGER", nullable: true),
                    Wed = table.Column<bool>(type: "INTEGER", nullable: true),
                    Thu = table.Column<bool>(type: "INTEGER", nullable: true),
                    Fri = table.Column<bool>(type: "INTEGER", nullable: true),
                    Sat = table.Column<bool>(type: "INTEGER", nullable: true),
                    Sun = table.Column<bool>(type: "INTEGER", nullable: true),
                    RecurrenceStartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RecurrenceEndDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DaysForWeeklyDailyOrMonthly = table.Column<int>(type: "INTEGER", nullable: true),
                    Month = table.Column<int>(type: "INTEGER", nullable: true),
                    NthDayMonthly = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedule_Recurrences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSchedule_Recurrences_ClassSchedules_ClassId",
                        column: x => x.ClassId,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSchedule_Roster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClassScheduleId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpId = table.Column<int>(type: "INTEGER", nullable: false),
                    Disclaimer = table.Column<bool>(type: "INTEGER", nullable: false),
                    Grade = table.Column<string>(type: "TEXT", nullable: true),
                    Interrupted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Restarted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Score = table.Column<int>(type: "INTEGER", nullable: true),
                    IsReleased = table.Column<bool>(type: "INTEGER", nullable: true),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    RetakeOrder = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedule_Roster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSchedule_Roster_ClassSchedules_ClassScheduleId",
                        column: x => x.ClassScheduleId,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSchedule_Roster_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSchedule_Roster_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSchedule_Roster_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSchedule_StudentEvaluations_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentEvaluationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassScheduleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedule_StudentEvaluations_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSchedule_StudentEvaluations_Links_ClassSchedules_ClassScheduleId",
                        column: x => x.ClassScheduleId,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSchedule_StudentEvaluations_Links_StudentEvaluations_StudentEvaluationId",
                        column: x => x.StudentEvaluationId,
                        principalTable: "StudentEvaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassScheduleEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClassScheduleId = table.Column<int>(type: "INTEGER", nullable: false),
                    PreTestStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    RetakeStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    CBTStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    FinalScore = table.Column<int>(type: "INTEGER", nullable: false),
                    FinalGrade = table.Column<string>(type: "TEXT", nullable: true),
                    GradeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    EnrolledDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PlannedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsEnrolled = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsWaitlisted = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDropped = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDenied = table.Column<bool>(type: "INTEGER", nullable: false),
                    TestId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassScheduleEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassScheduleEmployees_ClassSchedule_Roster_Statuses_CBTStatusId",
                        column: x => x.CBTStatusId,
                        principalTable: "ClassSchedule_Roster_Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassScheduleEmployees_ClassSchedule_Roster_Statuses_PreTestStatusId",
                        column: x => x.PreTestStatusId,
                        principalTable: "ClassSchedule_Roster_Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassScheduleEmployees_ClassSchedule_Roster_Statuses_RetakeStatusId",
                        column: x => x.RetakeStatusId,
                        principalTable: "ClassSchedule_Roster_Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassScheduleEmployees_ClassSchedule_Roster_Statuses_TestStatusId",
                        column: x => x.TestStatusId,
                        principalTable: "ClassSchedule_Roster_Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassScheduleEmployees_ClassSchedules_ClassScheduleId",
                        column: x => x.ClassScheduleId,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassScheduleEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassScheduleEmployees_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassScheduleHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClassScheduleID = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassScheduleHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassScheduleHistories_ClassSchedules_ClassScheduleID",
                        column: x => x.ClassScheduleID,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentEvaluationWithoutEmp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentEvaluationId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: true),
                    ClassScheduleId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataMode = table.Column<string>(type: "TEXT", nullable: true),
                    RatingScale = table.Column<int>(type: "INTEGER", nullable: true),
                    High = table.Column<double>(type: "REAL", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Low = table.Column<double>(type: "REAL", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    AdditionalComments = table.Column<string>(type: "TEXT", nullable: true),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    StudentEvaluationQuestionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvaluationWithoutEmp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEvaluationWithoutEmp_ClassSchedules_ClassScheduleId",
                        column: x => x.ClassScheduleId,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEvaluationWithoutEmp_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentEvaluationWithoutEmp_QuestionBanks_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEvaluationWithoutEmp_StudentEvaluationQuestions_StudentEvaluationQuestionId",
                        column: x => x.StudentEvaluationQuestionId,
                        principalTable: "StudentEvaluationQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentEvaluationWithoutEmp_StudentEvaluations_StudentEvaluationId",
                        column: x => x.StudentEvaluationId,
                        principalTable: "StudentEvaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDPSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IDPId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassScheduleId = table.Column<int>(type: "INTEGER", nullable: false),
                    startDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    endDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    plannedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDPSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDPSchedules_ClassSchedules_ClassScheduleId",
                        column: x => x.ClassScheduleId,
                        principalTable: "ClassSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDPSchedules_IDPs_IDPId",
                        column: x => x.IDPId,
                        principalTable: "IDPs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILACertificationSubRequirementLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILACertificationLinkId = table.Column<int>(type: "INTEGER", nullable: false),
                    CertificationSubRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReqHour = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILACertificationSubRequirementLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILACertificationSubRequirementLinks_CertificationSubRequirements_CertificationSubRequirementId",
                        column: x => x.CertificationSubRequirementId,
                        principalTable: "CertificationSubRequirements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ILACertificationSubRequirementLinks_ILACertificationLinks_ILACertificationLinkId",
                        column: x => x.ILACertificationLinkId,
                        principalTable: "ILACertificationLinks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiscussionQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ILATraineeEvaluationId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionText = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    QuestionFileUpload = table.Column<string>(type: "TEXT", nullable: true),
                    QuestionImageUpload = table.Column<string>(type: "TEXT", nullable: true),
                    QuestionLinksUpload = table.Column<string>(type: "TEXT", nullable: true),
                    AnswerKeywords = table.Column<string>(type: "TEXT", nullable: true),
                    AnswerImageUpload = table.Column<string>(type: "TEXT", nullable: true),
                    AnswerFileUpload = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscussionQuestions_ILATraineeEvaluations_ILATraineeEvaluationId",
                        column: x => x.ILATraineeEvaluationId,
                        principalTable: "ILATraineeEvaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestReleaseEMPSetting_Retake_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestReleaseSettingId = table.Column<int>(type: "INTEGER", nullable: false),
                    RetakeTestId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestReleaseEMPSetting_Retake_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestReleaseEMPSetting_Retake_Links_TestReleaseEMPSettings_TestReleaseSettingId",
                        column: x => x.TestReleaseSettingId,
                        principalTable: "TestReleaseEMPSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestReleaseEMPSetting_Retake_Links_Tests_RetakeTestId",
                        column: x => x.RetakeTestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_ILA_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_ILA_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_ILA_Links_Version_ILAs_Version_ILAId",
                        column: x => x.Version_ILAId,
                        principalTable: "Version_ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Task_ILA_Links_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_TrainingProgram_ILA_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_TrainingProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: true),
                    IsInUse = table.Column<bool>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_TrainingProgram_ILA_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_TrainingProgram_ILA_Links_Version_ILAs_Version_ILAId",
                        column: x => x.Version_ILAId,
                        principalTable: "Version_ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_TrainingProgram_ILA_Links_Version_TrainingPrograms_Version_TrainingProgramId",
                        column: x => x.Version_TrainingProgramId,
                        principalTable: "Version_TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Test_Item_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Sequence = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test_Item_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_Item_Links_TestItems_TestItemId",
                        column: x => x.TestItemId,
                        principalTable: "TestItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Test_Item_Links_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestItem_Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestItem_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestItem_Histories_TestItems_TestItemId",
                        column: x => x.TestItemId,
                        principalTable: "TestItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestItemFillBlanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    CorrectIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    Correct = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestItemFillBlanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestItemFillBlanks_TestItems_TestItemId",
                        column: x => x.TestItemId,
                        principalTable: "TestItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestItemMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChoiceDescription = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    MatchDescription = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    MatchValue = table.Column<char>(type: "TEXT", maxLength: 1, nullable: false),
                    CorrectValue = table.Column<char>(type: "TEXT", maxLength: 1, nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestItemMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestItemMatches_TestItems_TestItemId",
                        column: x => x.TestItemId,
                        principalTable: "TestItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestItemMCQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChoiceDescription = table.Column<string>(type: "TEXT", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestItemMCQs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestItemMCQs_TestItems_TestItemId",
                        column: x => x.TestItemId,
                        principalTable: "TestItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestItemShortAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Responses = table.Column<string>(type: "TEXT", nullable: false),
                    IsCaseSensitive = table.Column<bool>(type: "INTEGER", nullable: false),
                    AcceptableResponses = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestItemShortAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestItemShortAnswers_TestItems_TestItemId",
                        column: x => x.TestItemId,
                        principalTable: "TestItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestItemTrueFalses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Choices = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestItemTrueFalses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestItemTrueFalses_TestItems_TestItemId",
                        column: x => x.TestItemId,
                        principalTable: "TestItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnablingObjectiveHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangeNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnablingObjectiveHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnablingObjectiveHistories_EnablingObjectives_EnablingObjectiveId",
                        column: x => x.EnablingObjectiveId,
                        principalTable: "EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnablingObjectiveHistories_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_Employee_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Version_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_Employee_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Employee_Links_Version_Employees_Version_EmployeeId",
                        column: x => x.Version_EmployeeId,
                        principalTable: "Version_Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Employee_Links_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_ILALinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_ILAId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_ILALinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_ILALinks_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_ILALinks_Version_ILAs_Version_ILAId",
                        column: x => x.Version_ILAId,
                        principalTable: "Version_ILAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_MetaEOLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_MetaEOId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_MetaEOLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_MetaEOLinks_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_MetaEOLinks_Version_EnablingObjectives_Version_MetaEOId",
                        column: x => x.Version_MetaEOId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_Position_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_Position_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Position_Links_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Position_Links_Version_Positions_Version_PositionId",
                        column: x => x.Version_PositionId,
                        principalTable: "Version_Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_Procedure_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_Procedure_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Procedure_Links_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Procedure_Links_Version_Procedures_Version_ProcedureId",
                        column: x => x.Version_ProcedureId,
                        principalTable: "Version_Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EOQuestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", nullable: false),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Questions_EnablingObjective_Questions_EOQuestionId",
                        column: x => x.EOQuestionId,
                        principalTable: "EnablingObjective_Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Questions_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_RRLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_RegulatoryRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_RRLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_RRLinks_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_RRLinks_Version_RegulatoryRequirements_Version_RegulatoryRequirementId",
                        column: x => x.Version_RegulatoryRequirementId,
                        principalTable: "Version_RegulatoryRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_SaftyHazard_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_SaftyHazardId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_SaftyHazard_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_SaftyHazard_Links_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_SaftyHazard_Links_Version_SaftyHazards_Version_SaftyHazardId",
                        column: x => x.Version_SaftyHazardId,
                        principalTable: "Version_SaftyHazards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EOStepId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Steps_EnablingObjective_Steps_EOStepId",
                        column: x => x.EOStepId,
                        principalTable: "EnablingObjective_Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Steps_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnablingObjective_SuugestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_EOId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Suggestions_EnablingObjective_Suggestions_EnablingObjective_SuugestionId",
                        column: x => x.EnablingObjective_SuugestionId,
                        principalTable: "EnablingObjective_Suggestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Suggestions_Version_EnablingObjectives_Version_EOId",
                        column: x => x.Version_EOId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Tasks_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Tasks_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_EnablingObjective_Tool_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_ToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_EnablingObjective_Tool_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Tool_Links_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_EnablingObjective_Tool_Links_Version_Tools_Version_ToolId",
                        column: x => x.Version_ToolId,
                        principalTable: "Version_Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Procedure_EnablingObjective_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Procedure_EnablingObjective_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Procedure_EnablingObjective_Links_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Procedure_EnablingObjective_Links_Version_Procedures_Version_ProcedureId",
                        column: x => x.Version_ProcedureId,
                        principalTable: "Version_Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_Task_EnablingObjective_Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version_TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_Task_EnablingObjective_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_Task_EnablingObjective_Links_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_Task_EnablingObjective_Links_Version_Tasks_Version_TaskId",
                        column: x => x.Version_TaskId,
                        principalTable: "Version_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Version_TestItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version_EnablingObjectiveId = table.Column<int>(type: "INTEGER", nullable: true),
                    TestItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestItemTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxonomyId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Version_Number = table.Column<string>(type: "TEXT", nullable: true),
                    TaxonomyLevelId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version_TestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Version_TestItems_TaxonomyLevels_TaxonomyLevelId",
                        column: x => x.TaxonomyLevelId,
                        principalTable: "TaxonomyLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Version_TestItems_TestItems_TestItemId",
                        column: x => x.TestItemId,
                        principalTable: "TestItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Version_TestItems_TestItemTypes_TestItemTypeId",
                        column: x => x.TestItemTypeId,
                        principalTable: "TestItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Version_TestItems_Version_EnablingObjectives_Version_EnablingObjectiveId",
                        column: x => x.Version_EnablingObjectiveId,
                        principalTable: "Version_EnablingObjectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RosterId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestItemTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserAnswer = table.Column<string>(type: "TEXT", nullable: true),
                    MatchValue = table.Column<string>(type: "TEXT", nullable: true),
                    CorrectIndex = table.Column<int>(type: "INTEGER", nullable: true),
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpTests_ClassSchedule_Roster_RosterId",
                        column: x => x.RosterId,
                        principalTable: "ClassSchedule_Roster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpTests_TestItemTypes_TestItemTypeId",
                        column: x => x.TestItemTypeId,
                        principalTable: "TestItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CBT_ScormRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CBTScormUploadId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassScheduleEmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    LaunchLink = table.Column<string>(type: "TEXT", nullable: true),
                    RegistrationCompletion = table.Column<int>(type: "INTEGER", nullable: false),
                    RegistrationSuccess = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<double>(type: "REAL", nullable: false),
                    ScormUploadId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                     Active = table.Column<bool>(type: "INTEGER", nullable: false , defaultValue: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CBT_ScormRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CBT_ScormRegistration_CBT_ScormUpload_ScormUploadId",
                        column: x => x.ScormUploadId,
                        principalTable: "CBT_ScormUpload",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CBT_ScormRegistration_ClassScheduleEmployees_ClassScheduleEmployeeId",
                        column: x => x.ClassScheduleEmployeeId,
                        principalTable: "ClassScheduleEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CBT_ScormRegistration_ClassScheduleEmployeeId",
                table: "CBT_ScormRegistration",
                column: "ClassScheduleEmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CBT_ScormRegistration_ScormUploadId",
                table: "CBT_ScormRegistration",
                column: "ScormUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_CBT_ScormUpload_CbtId",
                table: "CBT_ScormUpload",
                column: "CbtId");

            migrationBuilder.CreateIndex(
                name: "IX_CBTs_ILAId",
                table: "CBTs",
                column: "ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_History_CertificationId",
                table: "Certification_History",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Certifications_CertifyingBodyId",
                table: "Certifications",
                column: "CertifyingBodyId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationSubRequirements_CertificationId",
                table: "CertificationSubRequirements",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_CertifyingBodies_Name",
                table: "CertifyingBodies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CertifyingBody_History_CertifyingBodyId",
                table: "CertifyingBody_History",
                column: "CertifyingBodyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_Evaluation_Roster_ClassScheduleId",
                table: "ClassSchedule_Evaluation_Roster",
                column: "ClassScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_Evaluation_Roster_EmployeeId",
                table: "ClassSchedule_Evaluation_Roster",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_Evaluation_Roster_StudentEvaluationId",
                table: "ClassSchedule_Evaluation_Roster",
                column: "StudentEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_Recurrences_ClassId",
                table: "ClassSchedule_Recurrences",
                column: "ClassId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_Roster_ClassScheduleId",
                table: "ClassSchedule_Roster",
                column: "ClassScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_Roster_EmpId",
                table: "ClassSchedule_Roster",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_Roster_TestId",
                table: "ClassSchedule_Roster",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_Roster_TestTypeId",
                table: "ClassSchedule_Roster",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_StudentEvaluations_Links_ClassScheduleId",
                table: "ClassSchedule_StudentEvaluations_Links",
                column: "ClassScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedule_StudentEvaluations_Links_StudentEvaluationId",
                table: "ClassSchedule_StudentEvaluations_Links",
                column: "StudentEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleEmployees_CBTStatusId",
                table: "ClassScheduleEmployees",
                column: "CBTStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleEmployees_ClassScheduleId",
                table: "ClassScheduleEmployees",
                column: "ClassScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleEmployees_EmployeeId",
                table: "ClassScheduleEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleEmployees_PreTestStatusId",
                table: "ClassScheduleEmployees",
                column: "PreTestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleEmployees_RetakeStatusId",
                table: "ClassScheduleEmployees",
                column: "RetakeStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleEmployees_TestId",
                table: "ClassScheduleEmployees",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleEmployees_TestStatusId",
                table: "ClassScheduleEmployees",
                column: "TestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScheduleHistories_ClassScheduleID",
                table: "ClassScheduleHistories",
                column: "ClassScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_ILAID",
                table: "ClassSchedules",
                column: "ILAID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_InstructorId",
                table: "ClassSchedules",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_LocationId",
                table: "ClassSchedules",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_ProviderID",
                table: "ClassSchedules",
                column: "ProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_RecurrenceId",
                table: "ClassSchedules",
                column: "RecurrenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSettings_Notification_AvailableCustomSettings_ClientSettingsNotificationId",
                table: "ClientSettings_Notification_AvailableCustomSettings",
                column: "ClientSettingsNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSettings_Notification_CustomSettings_ClientSettingsNotificationId",
                table: "ClientSettings_Notification_CustomSettings",
                column: "ClientSettingsNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSettings_Notification_Step_AvailableCustomSettings_ClientSettingsNotificationStepId",
                table: "ClientSettings_Notification_Step_AvailableCustomSettings",
                column: "ClientSettingsNotificationStepId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSettings_Notification_Step_CustomSettings_ClientSettingsNotificationStepId",
                table: "ClientSettings_Notification_Step_CustomSettings",
                column: "ClientSettingsNotificationStepId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSettings_Notification_Step_ModelItems_ClientSettingsNotificationStepId",
                table: "ClientSettings_Notification_Step_ModelItems",
                column: "ClientSettingsNotificationStepId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSettings_Notification_Step_Recipients_ClientSettingsNotificationStepId",
                table: "ClientSettings_Notification_Step_Recipients",
                column: "ClientSettingsNotificationStepId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSettings_Notification_Steps_ClientSettingsNotificationId",
                table: "ClientSettings_Notification_Steps",
                column: "ClientSettingsNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUsers_PersonId",
                table: "ClientUsers",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserSettings_DashboardSettings_ClientUserId",
                table: "ClientUserSettings_DashboardSettings",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserSettings_DashboardSettings_DashboardSettingId",
                table: "ClientUserSettings_DashboardSettings",
                column: "DashboardSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorInvitations_InvitedByEID",
                table: "CollaboratorInvitations",
                column: "InvitedByEID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorInvitations_InviteeEID",
                table: "CollaboratorInvitations",
                column: "InviteeEID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEnablingObjectives_EO_CatId",
                table: "CustomEnablingObjectives",
                column: "EO_CatId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEnablingObjectives_EO_SubCatId",
                table: "CustomEnablingObjectives",
                column: "EO_SubCatId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomEnablingObjectives_EO_TopicId",
                table: "CustomEnablingObjectives",
                column: "EO_TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionQuestions_ILATraineeEvaluationId",
                table: "DiscussionQuestions",
                column: "ILATraineeEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyArea_Histories_DutyAreaId",
                table: "DutyArea_Histories",
                column: "DutyAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Tasks_EmployeeId_TaskId_MajorVersion",
                table: "Employee_Tasks",
                columns: new[] { "EmployeeId", "TaskId", "MajorVersion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Tasks_TaskId",
                table: "Employee_Tasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeActivityNotifications_ActivityNotificationId",
                table: "EmployeeActivityNotifications",
                column: "ActivityNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeActivityNotifications_EmployeeId_ActivityNotificationId",
                table: "EmployeeActivityNotifications",
                columns: new[] { "EmployeeId", "ActivityNotificationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCertifications_CertificationId",
                table: "EmployeeCertifications",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCertifications_EmployeeId",
                table: "EmployeeCertifications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCertifictaionHistories_EmployeeID",
                table: "EmployeeCertifictaionHistories",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCertifictaionHistories_NewCertificationID",
                table: "EmployeeCertifictaionHistories",
                column: "NewCertificationID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_EmployeeID",
                table: "EmployeeDocuments",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistories_EmployeeID",
                table: "EmployeeHistories",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOrganizations_EmployeeId",
                table: "EmployeeOrganizations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOrganizations_OrganizationId",
                table: "EmployeeOrganizations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePositions_EmployeeId",
                table: "EmployeePositions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePositions_PositionId",
                table: "EmployeePositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonId",
                table: "Employees",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpTests_RosterId",
                table: "EmpTests",
                column: "RosterId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpTests_TestItemTypeId",
                table: "EmpTests",
                column: "TestItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_CategoryHistories_EnablingObjectiveCategoryId",
                table: "EnablingObjective_CategoryHistories",
                column: "EnablingObjectiveCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_Employee_Links_EmployeeId",
                table: "EnablingObjective_Employee_Links",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_Employee_Links_EOID_EmployeeId",
                table: "EnablingObjective_Employee_Links",
                columns: new[] { "EOID", "EmployeeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_MetaEO_Links_EOID",
                table: "EnablingObjective_MetaEO_Links",
                column: "EOID");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_MetaEO_Links_MetaEOId_EOID",
                table: "EnablingObjective_MetaEO_Links",
                columns: new[] { "MetaEOId", "EOID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_Questions_EnablingObjectiveId",
                table: "EnablingObjective_Questions",
                column: "EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_Steps_EOId",
                table: "EnablingObjective_Steps",
                column: "EOId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_SubCategories_CategoryId_Number",
                table: "EnablingObjective_SubCategories",
                columns: new[] { "CategoryId", "Number" },
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_SubCategoryHistories_EnablingObjectiveSubCategoryId",
                table: "EnablingObjective_SubCategoryHistories",
                column: "EnablingObjectiveSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_Suggestions_EOId",
                table: "EnablingObjective_Suggestions",
                column: "EOId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_Tools_EOId_ToolId",
                table: "EnablingObjective_Tools",
                columns: new[] { "EOId", "ToolId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_Tools_ToolId",
                table: "EnablingObjective_Tools",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_TopicHistories_EnablingObjectiveTopicId",
                table: "EnablingObjective_TopicHistories",
                column: "EnablingObjectiveTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjective_Topics_SubCategoryId_Number",
                table: "EnablingObjective_Topics",
                columns: new[] { "SubCategoryId", "Number" },
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjectiveHistories_EnablingObjectiveId",
                table: "EnablingObjectiveHistories",
                column: "EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjectiveHistories_Version_EnablingObjectiveId",
                table: "EnablingObjectiveHistories",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjectives_EnablingObjective_CategoryId",
                table: "EnablingObjectives",
                column: "EnablingObjective_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjectives_EnablingObjective_SubCategoryId",
                table: "EnablingObjectives",
                column: "EnablingObjective_SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EnablingObjectives_TopicId",
                table: "EnablingObjectives",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationReleaseEMPSettings_ILAId",
                table: "EvaluationReleaseEMPSettings",
                column: "ILAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IDP_Review_EmployeeId",
                table: "IDP_Review",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_IDP_Review_IDP_ReviewStatusId",
                table: "IDP_Review",
                column: "IDP_ReviewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_IDPs_EmployeeId",
                table: "IDPs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_IDPs_ILAId",
                table: "IDPs",
                column: "ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_IDPSchedules_ClassScheduleId",
                table: "IDPSchedules",
                column: "ClassScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_IDPSchedules_IDPId",
                table: "IDPSchedules",
                column: "IDPId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_AssessmentTool_Links_AssessmentToolId",
                table: "ILA_AssessmentTool_Links",
                column: "AssessmentToolId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_AssessmentTool_Links_ILAId_AssessmentToolId",
                table: "ILA_AssessmentTool_Links",
                columns: new[] { "ILAId", "AssessmentToolId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_EnablingObjective_Links_EnablingObjectiveId",
                table: "ILA_EnablingObjective_Links",
                column: "EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_EnablingObjective_Links_ILAId_EnablingObjectiveId",
                table: "ILA_EnablingObjective_Links",
                columns: new[] { "ILAId", "EnablingObjectiveId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_Evaluator_Links_EvaluatorId",
                table: "ILA_Evaluator_Links",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_Evaluator_Links_ILAId",
                table: "ILA_Evaluator_Links",
                column: "ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_NERCAudience_Links_ILAId_NERCAudienceID",
                table: "ILA_NERCAudience_Links",
                columns: new[] { "ILAId", "NERCAudienceID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_NERCAudience_Links_NERCAudienceID",
                table: "ILA_NERCAudience_Links",
                column: "NERCAudienceID");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_NercStandard_Links_ILAId_StdId_NERCStdMemberId",
                table: "ILA_NercStandard_Links",
                columns: new[] { "ILAId", "StdId", "NERCStdMemberId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_NercStandard_Links_NERCStdMemberId",
                table: "ILA_NercStandard_Links",
                column: "NERCStdMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_NercStandard_Links_StdId",
                table: "ILA_NercStandard_Links",
                column: "StdId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_PerformTraineeEvals_ILAId",
                table: "ILA_PerformTraineeEvals",
                column: "ILAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_Position_Links_ILAId_PositionId",
                table: "ILA_Position_Links",
                columns: new[] { "ILAId", "PositionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_Position_Links_PositionId",
                table: "ILA_Position_Links",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_PreRequisite_Links_ILAId_PreRequisiteId",
                table: "ILA_PreRequisite_Links",
                columns: new[] { "ILAId", "PreRequisiteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_PreRequisite_Links_PreRequisiteId",
                table: "ILA_PreRequisite_Links",
                column: "PreRequisiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_Procedure_Links_ILAId_ProcedureId",
                table: "ILA_Procedure_Links",
                columns: new[] { "ILAId", "ProcedureId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_Procedure_Links_ProcedureId",
                table: "ILA_Procedure_Links",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_RegRequirement_Links_ILAId_RegulatoryRequirementId",
                table: "ILA_RegRequirement_Links",
                columns: new[] { "ILAId", "RegulatoryRequirementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_RegRequirement_Links_RegulatoryRequirementId",
                table: "ILA_RegRequirement_Links",
                column: "RegulatoryRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_SafetyHazard_Links_ILAId_SafetyHazardId",
                table: "ILA_SafetyHazard_Links",
                columns: new[] { "ILAId", "SafetyHazardId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_SafetyHazard_Links_SafetyHazardId",
                table: "ILA_SafetyHazard_Links",
                column: "SafetyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_Segment_Links_ILAId_SegmentId",
                table: "ILA_Segment_Links",
                columns: new[] { "ILAId", "SegmentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_Segment_Links_SegmentId",
                table: "ILA_Segment_Links",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_StudentEvaluation_Links_ILAId",
                table: "ILA_StudentEvaluation_Links",
                column: "ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_StudentEvaluation_Links_studentEvalFormID",
                table: "ILA_StudentEvaluation_Links",
                column: "studentEvalFormID");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_StudentEvaluation_Links_StudentEvaluationAudienceId",
                table: "ILA_StudentEvaluation_Links",
                column: "StudentEvaluationAudienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_StudentEvaluation_Links_StudentEvaluationAvailabilityId",
                table: "ILA_StudentEvaluation_Links",
                column: "StudentEvaluationAvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_StudentEvaluation_Links_StudentEvaluationFormId",
                table: "ILA_StudentEvaluation_Links",
                column: "StudentEvaluationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_TaskObjective_Links_ILAId_TaskId",
                table: "ILA_TaskObjective_Links",
                columns: new[] { "ILAId", "TaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_TaskObjective_Links_TaskId",
                table: "ILA_TaskObjective_Links",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_TrainingTopic_Links_ILAId_TrTopicId",
                table: "ILA_TrainingTopic_Links",
                columns: new[] { "ILAId", "TrTopicId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILA_TrainingTopic_Links_TrTopicId",
                table: "ILA_TrainingTopic_Links",
                column: "TrTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ILA_Uploads_ILAId",
                table: "ILA_Uploads",
                column: "ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_ILACertificationLinks_CertificationId",
                table: "ILACertificationLinks",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ILACertificationLinks_ILAId",
                table: "ILACertificationLinks",
                column: "ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_ILACertificationSubRequirementLinks_CertificationSubRequirementId",
                table: "ILACertificationSubRequirementLinks",
                column: "CertificationSubRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_ILACertificationSubRequirementLinks_ILACertificationLinkId",
                table: "ILACertificationSubRequirementLinks",
                column: "ILACertificationLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_ILACollaborators_CollaboratorInviteId",
                table: "ILACollaborators",
                column: "CollaboratorInviteId");

            migrationBuilder.CreateIndex(
                name: "IX_ILACollaborators_EmployeeId",
                table: "ILACollaborators",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ILACollaborators_ILAId_CollaboratorInviteId",
                table: "ILACollaborators",
                columns: new[] { "ILAId", "CollaboratorInviteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILACustomObjective_Links_CustomObjId",
                table: "ILACustomObjective_Links",
                column: "CustomObjId");

            migrationBuilder.CreateIndex(
                name: "IX_ILACustomObjective_Links_ILAId_CustomObjId",
                table: "ILACustomObjective_Links",
                columns: new[] { "ILAId", "CustomObjId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ILAHistories_ILAId",
                table: "ILAHistories",
                column: "ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_ILAs_DeliveryMethodId",
                table: "ILAs",
                column: "DeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ILAs_ProviderId",
                table: "ILAs",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ILAs_TopicId",
                table: "ILAs",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ILATraineeEvaluations_EvaluationTypeId",
                table: "ILATraineeEvaluations",
                column: "EvaluationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ILATraineeEvaluations_ILAId",
                table: "ILATraineeEvaluations",
                column: "ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_ILATraineeEvaluations_TestId",
                table: "ILATraineeEvaluations",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_ILATraineeEvaluations_TestTypeId",
                table: "ILATraineeEvaluations",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_CategoryHistories_ICategoryId",
                table: "Instructor_CategoryHistories",
                column: "ICategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_Histories_InstructorId",
                table: "Instructor_Histories",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_ICategoryId",
                table: "Instructors",
                column: "ICategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CategoryHistories_LocCategoryID",
                table: "Location_CategoryHistories",
                column: "LocCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Histories_LocationId",
                table: "Location_Histories",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocCategoryID",
                table: "Locations",
                column: "LocCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Meta_ILAMembers_Links_ILAID_MetaILAID_MetaILAConfigPublishOptionID",
                table: "Meta_ILAMembers_Links",
                columns: new[] { "ILAID", "MetaILAID", "MetaILAConfigPublishOptionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meta_ILAMembers_Links_MetaILAConfigPublishOptionID",
                table: "Meta_ILAMembers_Links",
                column: "MetaILAConfigPublishOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Meta_ILAMembers_Links_MetaILAID",
                table: "Meta_ILAMembers_Links",
                column: "MetaILAID");

            migrationBuilder.CreateIndex(
                name: "IX_MetaILAs_MetaILAAssessmentID",
                table: "MetaILAs",
                column: "MetaILAAssessmentID");

            migrationBuilder.CreateIndex(
                name: "IX_MetaILAs_MetaILAStatusId",
                table: "MetaILAs",
                column: "MetaILAStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_NercStandardMembers_StdId",
                table: "NercStandardMembers",
                column: "StdId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_Employees_EmployeeId",
                table: "Position_Employees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_Employees_PositionId",
                table: "Position_Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_Histories_PositionId",
                table: "Position_Histories",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_Tasks_PositionId",
                table: "Position_Tasks",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_Tasks_TaskId",
                table: "Position_Tasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SQs_EOId",
                table: "Positions_SQs",
                column: "EOId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SQs_PositionId",
                table: "Positions_SQs",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Proc_IssuingAuthority_Histories_ProcedureIssuingAuthorityId",
                table: "Proc_IssuingAuthority_Histories",
                column: "ProcedureIssuingAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_EnablingObjective_Links_EnablingObjectiveId_ProcedureId",
                table: "Procedure_EnablingObjective_Links",
                columns: new[] { "EnablingObjectiveId", "ProcedureId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_EnablingObjective_Links_ProcedureId",
                table: "Procedure_EnablingObjective_Links",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_ILA_Links_ILAId_ProcedureId",
                table: "Procedure_ILA_Links",
                columns: new[] { "ILAId", "ProcedureId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_ILA_Links_ProcedureId",
                table: "Procedure_ILA_Links",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_RR_Links_ProcedureId",
                table: "Procedure_RR_Links",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_RR_Links_RegulatoryRequirementId_ProcedureId",
                table: "Procedure_RR_Links",
                columns: new[] { "RegulatoryRequirementId", "ProcedureId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_SaftyHazard_Links_ProcedureId",
                table: "Procedure_SaftyHazard_Links",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_SaftyHazard_Links_SaftyHazardId_ProcedureId",
                table: "Procedure_SaftyHazard_Links",
                columns: new[] { "SaftyHazardId", "ProcedureId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_StatusHistories_ProcedureId",
                table: "Procedure_StatusHistories",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_Task_Links_ProcedureId_TaskId",
                table: "Procedure_Task_Links",
                columns: new[] { "ProcedureId", "TaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_Task_Links_TaskId",
                table: "Procedure_Task_Links",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureReview_Employees_EmployeeId",
                table: "ProcedureReview_Employees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureReview_Employees_ProcedureReviewId",
                table: "ProcedureReview_Employees",
                column: "ProcedureReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureReviews_ProcedureId",
                table: "ProcedureReviews",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_IssuingAuthorityId_Number",
                table: "Procedures",
                columns: new[] { "IssuingAuthorityId", "Number" },
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_ProviderLevelId",
                table: "Providers",
                column: "ProviderLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_QTDUsers_PersonId",
                table: "QTDUsers",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBankHistories_QuestionBankId",
                table: "QuestionBankHistories",
                column: "QuestionBankId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingScaleExpanded_RatingScaleNId",
                table: "RatingScaleExpanded",
                column: "RatingScaleNId");

            migrationBuilder.CreateIndex(
                name: "IX_RegRequirement_EO_Links_EOID_RegulatoryRequirementId",
                table: "RegRequirement_EO_Links",
                columns: new[] { "EOID", "RegulatoryRequirementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegRequirement_EO_Links_RegulatoryRequirementId",
                table: "RegRequirement_EO_Links",
                column: "RegulatoryRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_RegRequirement_ILA_Links_ILAID_RegulatoryRequirementId",
                table: "RegRequirement_ILA_Links",
                columns: new[] { "ILAID", "RegulatoryRequirementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegRequirement_ILA_Links_RegulatoryRequirementId",
                table: "RegRequirement_ILA_Links",
                column: "RegulatoryRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulatoryRequirements_IssuingAuthorityId",
                table: "RegulatoryRequirements",
                column: "IssuingAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDisplayColumns_ReportId",
                table: "ReportDisplayColumns",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFilters_ReportId",
                table: "ReportFilters",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSkeletonColumns_ReportSkeletonId",
                table: "ReportSkeletonColumns",
                column: "ReportSkeletonId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSkeletonFilters_ReportSkeletonId",
                table: "ReportSkeletonFilters",
                column: "ReportSkeletonId");

            migrationBuilder.CreateIndex(
                name: "IX_RR_IssuingAuthority_StatusHistories_RRIssuingAuthorityId",
                table: "RR_IssuingAuthority_StatusHistories",
                column: "RRIssuingAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_RR_StatusHistories_RegulatoryRequirementId",
                table: "RR_StatusHistories",
                column: "RegulatoryRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_RR_Task_Links_RegRequirementId_TaskId",
                table: "RR_Task_Links",
                columns: new[] { "RegRequirementId", "TaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RR_Task_Links_TaskId",
                table: "RR_Task_Links",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_CategoryHistories_SafetyHazardCategoryId",
                table: "SafetyHazard_CategoryHistories",
                column: "SafetyHazardCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_EO_Links_EOID",
                table: "SafetyHazard_EO_Links",
                column: "EOID");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_EO_Links_SafetyHazardId_EOID",
                table: "SafetyHazard_EO_Links",
                columns: new[] { "SafetyHazardId", "EOID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_Histories_SafetyHazardId",
                table: "SafetyHazard_Histories",
                column: "SafetyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_ILA_Links_ILAId_SafetyHazardId",
                table: "SafetyHazard_ILA_Links",
                columns: new[] { "ILAId", "SafetyHazardId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_ILA_Links_SafetyHazardId",
                table: "SafetyHazard_ILA_Links",
                column: "SafetyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_Set_Links_SafetyHazardId",
                table: "SafetyHazard_Set_Links",
                column: "SafetyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_Set_Links_SafetyHazardSetId_SafetyHazardId",
                table: "SafetyHazard_Set_Links",
                columns: new[] { "SafetyHazardSetId", "SafetyHazardId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_Task_Links_SaftyHazardId",
                table: "SafetyHazard_Task_Links",
                column: "SaftyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_Task_Links_TaskId_SaftyHazardId",
                table: "SafetyHazard_Task_Links",
                columns: new[] { "TaskId", "SaftyHazardId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_Tool_Links_SafetyHazardId",
                table: "SafetyHazard_Tool_Links",
                column: "SafetyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyHazard_Tool_Links_ToolId",
                table: "SafetyHazard_Tool_Links",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_SaftyHazard_Abatements_SaftyHazardId_Number",
                table: "SaftyHazard_Abatements",
                columns: new[] { "SaftyHazardId", "Number" },
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SaftyHazard_Controls_SaftyHazardId_Number",
                table: "SaftyHazard_Controls",
                columns: new[] { "SaftyHazardId", "Number" },
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SaftyHazard_RR_Links_RegulatoryRequirementId",
                table: "SaftyHazard_RR_Links",
                column: "RegulatoryRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_SaftyHazard_RR_Links_SafetyHazardId_RegulatoryRequirementId",
                table: "SaftyHazard_RR_Links",
                columns: new[] { "SafetyHazardId", "RegulatoryRequirementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaftyHazards_SaftyHazardCategoryId",
                table: "SaftyHazards",
                column: "SaftyHazardCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentObjective_Links_EnablingObjectiveId",
                table: "SegmentObjective_Links",
                column: "EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentObjective_Links_SegmentId",
                table: "SegmentObjective_Links",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentObjective_Links_TaskId",
                table: "SegmentObjective_Links",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfRegistrationOptions_ILAId",
                table: "SelfRegistrationOptions",
                column: "ILAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_EnablingObjectives_Links_EnablingObjectiveID_SimulatorScenarioID",
                table: "SimulatorScenario_EnablingObjectives_Links",
                columns: new[] { "EnablingObjectiveID", "SimulatorScenarioID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenario_EnablingObjectives_Links_SimulatorScenarioID",
                table: "SimulatorScenario_EnablingObjectives_Links",
                column: "SimulatorScenarioID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenarioILA_Links_ILAID_SimulatorScenarioID",
                table: "SimulatorScenarioILA_Links",
                columns: new[] { "ILAID", "SimulatorScenarioID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenarioILA_Links_SimulatorScenarioID",
                table: "SimulatorScenarioILA_Links",
                column: "SimulatorScenarioID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenarioPositon_Links_PositionID_SimulatorScenarioID",
                table: "SimulatorScenarioPositon_Links",
                columns: new[] { "PositionID", "SimulatorScenarioID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenarioPositon_Links_SimulatorScenarioID",
                table: "SimulatorScenarioPositon_Links",
                column: "SimulatorScenarioID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenarios_SimScenarioDiffID",
                table: "SimulatorScenarios",
                column: "SimScenarioDiffID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenarioTaskObjectives_Links_SimulatorScenarioID",
                table: "SimulatorScenarioTaskObjectives_Links",
                column: "SimulatorScenarioID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorScenarioTaskObjectives_Links_TaskID_SimulatorScenarioID",
                table: "SimulatorScenarioTaskObjectives_Links",
                columns: new[] { "TaskID", "SimulatorScenarioID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluation_Questions_QuestionBankId",
                table: "StudentEvaluation_Questions",
                column: "QuestionBankId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluation_Questions_StudentEvaluationId",
                table: "StudentEvaluation_Questions",
                column: "StudentEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationForms_RatingScaleId",
                table: "StudentEvaluationForms",
                column: "RatingScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationHistories_StudentEvaluationId",
                table: "StudentEvaluationHistories",
                column: "StudentEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationQuestions_EvalFormID",
                table: "StudentEvaluationQuestions",
                column: "EvalFormID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluations_RatingScaleId",
                table: "StudentEvaluations",
                column: "RatingScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationWithoutEmp_ClassScheduleId",
                table: "StudentEvaluationWithoutEmp",
                column: "ClassScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationWithoutEmp_EmployeeId",
                table: "StudentEvaluationWithoutEmp",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationWithoutEmp_QuestionId",
                table: "StudentEvaluationWithoutEmp",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationWithoutEmp_StudentEvaluationId",
                table: "StudentEvaluationWithoutEmp",
                column: "StudentEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationWithoutEmp_StudentEvaluationQuestionId",
                table: "StudentEvaluationWithoutEmp",
                column: "StudentEvaluationQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubDutyArea_Histories_SubDutyAreaId",
                table: "SubDutyArea_Histories",
                column: "SubDutyAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubdutyAreas_DutyAreaId",
                table: "SubdutyAreas",
                column: "DutyAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Collaborator_Links_TaskCollabInviteId_TaskId",
                table: "Task_Collaborator_Links",
                columns: new[] { "TaskCollabInviteId", "TaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_Collaborator_Links_TaskId",
                table: "Task_Collaborator_Links",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_EnablingObjective_Links_EnablingObjectiveId_TaskId",
                table: "Task_EnablingObjective_Links",
                columns: new[] { "EnablingObjectiveId", "TaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_EnablingObjective_Links_TaskId",
                table: "Task_EnablingObjective_Links",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Histories_TaskId",
                table: "Task_Histories",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Histories_Version_TaskId",
                table: "Task_Histories",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ILA_Links_ILAId_TaskId",
                table: "Task_ILA_Links",
                columns: new[] { "ILAId", "TaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_ILA_Links_TaskId",
                table: "Task_ILA_Links",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_MetaTask_Links_Meta_TaskId_TaskId",
                table: "Task_MetaTask_Links",
                columns: new[] { "Meta_TaskId", "TaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_MetaTask_Links_TaskId",
                table: "Task_MetaTask_Links",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Positions_PositionId",
                table: "Task_Positions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Positions_TaskId",
                table: "Task_Positions",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Questions_TaskId",
                table: "Task_Questions",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Reference_Links_TaskId",
                table: "Task_Reference_Links",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Reference_Links_TaskReferenceId_TaskId",
                table: "Task_Reference_Links",
                columns: new[] { "TaskReferenceId", "TaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_Steps_TaskId",
                table: "Task_Steps",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Suggestions_TaskId",
                table: "Task_Suggestions",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Tools_TaskId_ToolId",
                table: "Task_Tools",
                columns: new[] { "TaskId", "ToolId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_Tools_ToolId",
                table: "Task_Tools",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TrainingGroups_TaskId",
                table: "Task_TrainingGroups",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TrainingGroups_TrainingGroupId_TaskId",
                table: "Task_TrainingGroups",
                columns: new[] { "TrainingGroupId", "TaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskQualification_Evaluator_Links_EvaluatorId",
                table: "TaskQualification_Evaluator_Links",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskQualification_Evaluator_Links_TaskQualificationId",
                table: "TaskQualification_Evaluator_Links",
                column: "TaskQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskQualifications_EmpId",
                table: "TaskQualifications",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskQualifications_EvaluationId",
                table: "TaskQualifications",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskQualifications_EvaluatorId",
                table: "TaskQualifications",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskQualifications_TaskId",
                table: "TaskQualifications",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskQualifications_TQStatusId",
                table: "TaskQualifications",
                column: "TQStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_QuestionAnswers_TaskQualificationId",
                table: "TaskReQualificationEmp_QuestionAnswers",
                column: "TaskQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_QuestionAnswers_TaskQuestionId",
                table: "TaskReQualificationEmp_QuestionAnswers",
                column: "TaskQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_QuestionAnswers_TraineeId",
                table: "TaskReQualificationEmp_QuestionAnswers",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_SignOffs_EvaluationMethodId",
                table: "TaskReQualificationEmp_SignOffs",
                column: "EvaluationMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_SignOffs_TaskQualificationId",
                table: "TaskReQualificationEmp_SignOffs",
                column: "TaskQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_SignOffs_TraineeId",
                table: "TaskReQualificationEmp_SignOffs",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_Steps_TaskQualificationId",
                table: "TaskReQualificationEmp_Steps",
                column: "TaskQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_Steps_TaskStepId",
                table: "TaskReQualificationEmp_Steps",
                column: "TaskStepId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_Steps_TraineeId",
                table: "TaskReQualificationEmp_Steps",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_Suggestions_TaskQualificationId",
                table: "TaskReQualificationEmp_Suggestions",
                column: "TaskQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_Suggestions_TaskSuggestionId",
                table: "TaskReQualificationEmp_Suggestions",
                column: "TaskSuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReQualificationEmp_Suggestions_TraineeId",
                table: "TaskReQualificationEmp_Suggestions",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SubdutyAreaId_Number",
                table: "Tasks",
                columns: new[] { "SubdutyAreaId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Test_Histories_TestId",
                table: "Test_Histories",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Item_Links_TestId_TestItemId",
                table: "Test_Item_Links",
                columns: new[] { "TestId", "TestItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Test_Item_Links_TestItemId",
                table: "Test_Item_Links",
                column: "TestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TestItem_Histories_TestItemId",
                table: "TestItem_Histories",
                column: "TestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TestItemFillBlanks_TestItemId",
                table: "TestItemFillBlanks",
                column: "TestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TestItemMatches_TestItemId",
                table: "TestItemMatches",
                column: "TestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TestItemMCQs_TestItemId",
                table: "TestItemMCQs",
                column: "TestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TestItems_EOId",
                table: "TestItems",
                column: "EOId");

            migrationBuilder.CreateIndex(
                name: "IX_TestItems_TaxonomyId",
                table: "TestItems",
                column: "TaxonomyId");

            migrationBuilder.CreateIndex(
                name: "IX_TestItems_TestItemTypeId",
                table: "TestItems",
                column: "TestItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestItemShortAnswers_TestItemId",
                table: "TestItemShortAnswers",
                column: "TestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TestItemTrueFalses_TestItemId",
                table: "TestItemTrueFalses",
                column: "TestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TestReleaseEMPSetting_Retake_Links_RetakeTestId",
                table: "TestReleaseEMPSetting_Retake_Links",
                column: "RetakeTestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestReleaseEMPSetting_Retake_Links_TestReleaseSettingId",
                table: "TestReleaseEMPSetting_Retake_Links",
                column: "TestReleaseSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_TestReleaseEMPSettings_FinalTestId",
                table: "TestReleaseEMPSettings",
                column: "FinalTestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestReleaseEMPSettings_ILAId",
                table: "TestReleaseEMPSettings",
                column: "ILAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestReleaseEMPSettings_PreTestId",
                table: "TestReleaseEMPSettings",
                column: "PreTestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestStatusId",
                table: "Tests",
                column: "TestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_EmployeeTaskId",
                table: "Timesheets",
                column: "EmployeeTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tool_StatusHistories_ToolId",
                table: "Tool_StatusHistories",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolGroup_Tools_ToolGroupId",
                table: "ToolGroup_Tools",
                column: "ToolGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolGroup_Tools_ToolId_ToolGroupId",
                table: "ToolGroup_Tools",
                columns: new[] { "ToolId", "ToolGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tools_ToolCategoryId",
                table: "Tools",
                column: "ToolCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TQEmpSettings_TaskQualificationId",
                table: "TQEmpSettings",
                column: "TaskQualificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TQILAEmpSettings_ILAId",
                table: "TQILAEmpSettings",
                column: "ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingGroups_CategoryId_GroupNumber",
                table: "TrainingGroups",
                columns: new[] { "CategoryId", "GroupNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgram_Histories_TrainingProgramId",
                table: "TrainingProgram_Histories",
                column: "TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgram_Histories_Version_TrainingProgramId",
                table: "TrainingProgram_Histories",
                column: "Version_TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_PositionId_Id",
                table: "TrainingPrograms",
                columns: new[] { "PositionId", "Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_TrainingProgramTypeId",
                table: "TrainingPrograms",
                column: "TrainingProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_ILA_Links_ILAId",
                table: "TrainingPrograms_ILA_Links",
                column: "ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_ILA_Links_TrainingProgramId",
                table: "TrainingPrograms_ILA_Links",
                column: "TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTopics_TrainingTopic_CategoryId",
                table: "TrainingTopics",
                column: "TrainingTopic_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Employees_EmployeeId",
                table: "Version_Employees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Employee_Links_Version_EmployeeId",
                table: "Version_EnablingObjective_Employee_Links",
                column: "Version_EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Employee_Links_Version_EnablingObjectiveId",
                table: "Version_EnablingObjective_Employee_Links",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_ILALinks_Version_EnablingObjectiveId",
                table: "Version_EnablingObjective_ILALinks",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_ILALinks_Version_ILAId",
                table: "Version_EnablingObjective_ILALinks",
                column: "Version_ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_MetaEOLinks_Version_EnablingObjectiveId",
                table: "Version_EnablingObjective_MetaEOLinks",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_MetaEOLinks_Version_MetaEOId",
                table: "Version_EnablingObjective_MetaEOLinks",
                column: "Version_MetaEOId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Position_Links_Version_EnablingObjectiveId",
                table: "Version_EnablingObjective_Position_Links",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Position_Links_Version_PositionId",
                table: "Version_EnablingObjective_Position_Links",
                column: "Version_PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Procedure_Links_Version_EnablingObjectiveId",
                table: "Version_EnablingObjective_Procedure_Links",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Procedure_Links_Version_ProcedureId",
                table: "Version_EnablingObjective_Procedure_Links",
                column: "Version_ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Questions_EOQuestionId",
                table: "Version_EnablingObjective_Questions",
                column: "EOQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Questions_Version_EnablingObjectiveId",
                table: "Version_EnablingObjective_Questions",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_RRLinks_Version_EnablingObjectiveId",
                table: "Version_EnablingObjective_RRLinks",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_RRLinks_Version_RegulatoryRequirementId",
                table: "Version_EnablingObjective_RRLinks",
                column: "Version_RegulatoryRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_SaftyHazard_Links_Version_EnablingObjectiveId",
                table: "Version_EnablingObjective_SaftyHazard_Links",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_SaftyHazard_Links_Version_SaftyHazardId",
                table: "Version_EnablingObjective_SaftyHazard_Links",
                column: "Version_SaftyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Steps_EOStepId",
                table: "Version_EnablingObjective_Steps",
                column: "EOStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Steps_Version_EnablingObjectiveId",
                table: "Version_EnablingObjective_Steps",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Suggestions_EnablingObjective_SuugestionId",
                table: "Version_EnablingObjective_Suggestions",
                column: "EnablingObjective_SuugestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Suggestions_Version_EOId",
                table: "Version_EnablingObjective_Suggestions",
                column: "Version_EOId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Tasks_Version_EnablingObjectiveId",
                table: "Version_EnablingObjective_Tasks",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Tasks_Version_TaskId",
                table: "Version_EnablingObjective_Tasks",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Tool_Links_Version_EnablingObjectiveId",
                table: "Version_EnablingObjective_Tool_Links",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjective_Tool_Links_Version_ToolId",
                table: "Version_EnablingObjective_Tool_Links",
                column: "Version_ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_EnablingObjectives_EnablingObjectiveId",
                table: "Version_EnablingObjectives",
                column: "EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_ILAs_ILAId",
                table: "Version_ILAs",
                column: "ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_MetaILAs_MetaILAAssessmentID",
                table: "Version_MetaILAs",
                column: "MetaILAAssessmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Version_MetaILAs_MetaILAId",
                table: "Version_MetaILAs",
                column: "MetaILAId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_MetaILAs_MetaILAStatusId",
                table: "Version_MetaILAs",
                column: "MetaILAStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Positions_PositionId",
                table: "Version_Positions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Procedure_EnablingObjective_Links_Version_EnablingObjectiveId",
                table: "Version_Procedure_EnablingObjective_Links",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Procedure_EnablingObjective_Links_Version_ProcedureId",
                table: "Version_Procedure_EnablingObjective_Links",
                column: "Version_ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Procedure_SaftyHazard_Links_Version_ProcedureId",
                table: "Version_Procedure_SaftyHazard_Links",
                column: "Version_ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Procedure_SaftyHazard_Links_Version_SaftyHazardId",
                table: "Version_Procedure_SaftyHazard_Links",
                column: "Version_SaftyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Procedure_Tool_Links_Version_ProcedureId",
                table: "Version_Procedure_Tool_Links",
                column: "Version_ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Procedure_Tool_Links_Version_ToolId",
                table: "Version_Procedure_Tool_Links",
                column: "Version_ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Procedures_ProcedureId",
                table: "Version_Procedures",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_RegulatoryRequirements_RegulatoryRequirementId",
                table: "Version_RegulatoryRequirements",
                column: "RegulatoryRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_SaftyHazard_Abatements_Version_SaftyHazardId",
                table: "Version_SaftyHazard_Abatements",
                column: "Version_SaftyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_SaftyHazard_Controls_Version_SaftyHazardId",
                table: "Version_SaftyHazard_Controls",
                column: "Version_SaftyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_SaftyHazards_SaftyHazardId",
                table: "Version_SaftyHazards",
                column: "SaftyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_EnablingObjective_Links_Version_EnablingObjectiveId",
                table: "Version_Task_EnablingObjective_Links",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_EnablingObjective_Links_Version_TaskId",
                table: "Version_Task_EnablingObjective_Links",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_ILA_Links_Version_ILAId",
                table: "Version_Task_ILA_Links",
                column: "Version_ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_ILA_Links_Version_TaskId",
                table: "Version_Task_ILA_Links",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_MetaTask_Links_Version_MetaTaskId",
                table: "Version_Task_MetaTask_Links",
                column: "Version_MetaTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_MetaTask_Links_Version_TaskId",
                table: "Version_Task_MetaTask_Links",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Position_Links_Version_PositionId",
                table: "Version_Task_Position_Links",
                column: "Version_PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Position_Links_Version_TaskId",
                table: "Version_Task_Position_Links",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Procedure_Links_Version_ProcedureId",
                table: "Version_Task_Procedure_Links",
                column: "Version_ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Procedure_Links_Version_TaskId",
                table: "Version_Task_Procedure_Links",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Questions_TaskQuestionId",
                table: "Version_Task_Questions",
                column: "TaskQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Questions_VersionTaskId",
                table: "Version_Task_Questions",
                column: "VersionTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_RR_Links_Version_RegulatoryRequirementId_Version_TaskId",
                table: "Version_Task_RR_Links",
                columns: new[] { "Version_RegulatoryRequirementId", "Version_TaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_RR_Links_Version_TaskId",
                table: "Version_Task_RR_Links",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_SaftyHazard_Links_Version_SaftyHazardId",
                table: "Version_Task_SaftyHazard_Links",
                column: "Version_SaftyHazardId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_SaftyHazard_Links_Version_TaskId",
                table: "Version_Task_SaftyHazard_Links",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Steps_TaskStepId",
                table: "Version_Task_Steps",
                column: "TaskStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Steps_VersionTaskId",
                table: "Version_Task_Steps",
                column: "VersionTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Suggestions_Task_SuggestionId",
                table: "Version_Task_Suggestions",
                column: "Task_SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Suggestions_Version_TaskId",
                table: "Version_Task_Suggestions",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Tool_Links_Version_TaskId",
                table: "Version_Task_Tool_Links",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_Tool_Links_Version_ToolId",
                table: "Version_Task_Tool_Links",
                column: "Version_ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_TrainingGroups_Version_TaskId",
                table: "Version_Task_TrainingGroups",
                column: "Version_TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Task_TrainingGroups_Version_TrainingGroupId_Version_TaskId",
                table: "Version_Task_TrainingGroups",
                columns: new[] { "Version_TrainingGroupId", "Version_TaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Version_Tasks_TaskId",
                table: "Version_Tasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_TestItems_TaxonomyLevelId",
                table: "Version_TestItems",
                column: "TaxonomyLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_TestItems_TestItemId",
                table: "Version_TestItems",
                column: "TestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_TestItems_TestItemTypeId",
                table: "Version_TestItems",
                column: "TestItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_TestItems_Version_EnablingObjectiveId",
                table: "Version_TestItems",
                column: "Version_EnablingObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Tests_TestId",
                table: "Version_Tests",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Tests_TestStatusId",
                table: "Version_Tests",
                column: "TestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_TestStatuses_TestStatusId",
                table: "Version_TestStatuses",
                column: "TestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_Tools_ToolId",
                table: "Version_Tools",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_TrainingGroups_Version_TrainingGroupId",
                table: "Version_TrainingGroups",
                column: "Version_TrainingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_TrainingProgram_ILA_Links_Version_ILAId",
                table: "Version_TrainingProgram_ILA_Links",
                column: "Version_ILAId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_TrainingProgram_ILA_Links_Version_TrainingProgramId",
                table: "Version_TrainingProgram_ILA_Links",
                column: "Version_TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_TrainingPrograms_PositionId",
                table: "Version_TrainingPrograms",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Version_TrainingPrograms_TrainingProgramId",
                table: "Version_TrainingPrograms",
                column: "TrainingProgramId");
            
            Initialization.QTDContext.SeedData seed =
                new Initialization.QTDContext.SeedData(
                System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                migrationBuilder);

            seed.AddIDP_ReviewStatusTable();
            seed.AddPersonsTable();
            seed.AddClientUsers();
            seed.AddEmployeeTable();
            seed.AddCertifyingBodyTable();
            seed.AddCertificationTable();
            seed.AddPositionTable();
            seed.AddOrganizationTable();
            seed.AddEmployeeCertificationsTable();
            seed.AddEmployeePositionTable();
            seed.AddEmployeeOrganizationTable();
            seed.AddDutyAreaTable();
            seed.AddSubdutyAreaTable();
            seed.AddTaskTable();
            seed.AddEnablingObjective_CategoryTable();
            seed.AddEnablingObjective_SubCategoryTable();
            seed.AddEnablingObjective_TopicTable();
            seed.AddEnablingObjectiveTable();
            seed.AddEnablingObjective_Employee_LinksTable();
            seed.AddProcedure_IssuingAuthorityTable();
            seed.AddProcedureTable();
            seed.AddProcedureReviewsTable();
            seed.AddProcedureReview_EmployeesTable();
            seed.AddSaftyHazard_categoryTable();
            seed.AddSaftyHazardTable();
            seed.AddSaftyHazard_ControlTable();
            seed.AddSaftyHazard_AbatementTable();
            seed.AddProcedure_SaftyHazard_LinkTable();
            seed.AddProcedure_EnablingObjective_LinkTable();
            seed.AddEnablingObjective_SaftyHazard_LinkTable();
            seed.AddEnablingObjective_Procedure_LinkTable();
            seed.AddTask_EnablingObjective_LinkTable();
            seed.AddTask_Procedure_LinkTable();
            seed.AddTask_SaftyHazard_LinkTable();
            seed.AddTask_StepTable();
            seed.AddTask_ToolTable();
            seed.AddToolGroupsTable();
            seed.AddToolTable();
            seed.AddToolGroup_ToolsTable();
            seed.AddVersion_TaskTable();
            seed.AddVersion_Task_StepTable();
            seed.AddVersion_Task_QuestionTable();
            seed.AddVersion_ProcedureTable();
            seed.AddVersion_Task_Procedure_LinkTable();
            seed.AddVersion_ToolTable();
            seed.AddVersion_Task_Tool_LinkTable();
            seed.AddVersion_Procedure_Tool_LinkTable();
            seed.AddVersion_Task_EnablingObjective_LinkTable();
            seed.AddVersion_EnablingObjective_Tool_LinkTable();
            seed.AddVersion_SaftyHazardTable();
            seed.AddVersion_SaftyHazard_AbatementTable();
            seed.AddVersion_SaftyHazard_ControlTable();
            seed.AddVersion_Task_SaftyHazard_LinkTable();
            seed.AddVersion_Procedure_SaftyHazard_LinkTable();
            seed.AddVersion_EnablingObjective_SaftyHazard_LinkTable();
            seed.AddVersion_EnablingObjectiveTable();
            seed.AddVersion_EnablingObjective_Procedure_LinkTable();
            seed.AddVersion_Procedure_EnablingObjective_LinkTable();
            seed.AddEmployee_TaskTable();
            seed.AddTimesheetTable();
            seed.AddTask_QuestionTable();
            seed.AddTask_PositionTable();
            seed.AddProviderLevelTable();
            seed.AddProviderTable();
            seed.AddILA_TopicTable();
            seed.AddDeliveryMethodTable();
            //seed.AddTrainingTopicTable();
            seed.AddNercStandardTable();
            seed.AddNercStandardMemberTable();
            seed.AddTraineeEvaluationTypeTable();
            seed.AddMetaILATable();
            seed.AddSegmentTable();
            seed.AddAssessmentToolTable();
            seed.AddRR_IssuingAuthorityTable();
            seed.AddRegulatoryRequirementTable();
            seed.AddRR_SH_LinkTable();
            seed.AddILATable();
            //seed.AddTrainingTopic_CategoryTable();
            seed.AddNERCTargetAudienceTable();
            seed.AddRatingScaleTable();
            seed.AddStudentEvaluationAvailabilityTable();
            seed.AddILA_NERCAudience_LinkTable();
            seed.AddStudentEvaluationFormsTable();
            seed.AddCoverSheetTypeTable();
            seed.AddStudentEvaluationQuestionTable();
            seed.AddCollaboratorInvitationTable();
            seed.AddCoversheetTable();
            seed.AddCustomEnablingObjectiveTable();
            seed.AddStudentEvaluationAudienceTable();
            seed.AddTaxonomyLevelTable();
            seed.AddTestStatusTable();
            seed.AddTestTable();
            seed.AddTestTypeTable();
            seed.AddTestSettingTable();
            seed.AddTestItemTypeTable();
            seed.AddILATraineeEvaluationTable();
            seed.AddTestItemMatchTable();
            seed.AddDiscussionQuestionTable();
            seed.AddToolCategoryTable();
            seed.AddSimulatorScenarioDifficultyLevelTable();
            seed.AddSimulationScenarioSpecLookUpTable();
            seed.AddSimulatorScenarioTable();
            seed.AddTrainingGroup_CategoryTable();
            seed.AddTrainingGroupTable();
            seed.AddTask_TrainingGroupTable();
            seed.AddVersion_EmployeeTable();
            seed.AddActivityNotificationTable();
            seed.AddVersion_TrainingGroupTable();
            seed.AddSettings_EmailNotifications();
            seed.AddTrainingProgramTypeTable();
            seed.AddTrainingProgramTable();
            seed.AddTrainingPrograms_ILA_LinksTable();
            seed.AddTaskQualificationStatusTable();
            seed.AddIDP_ReviewStatusTable();
            seed.AddIDP_ReviewTable();
            seed.AddRatingScaleNTable();
            seed.AddEvaluationMethodTable();
            seed.AddTaskQualificationTable();
            seed.AddDatabaseSettings();
            seed.AddTask_PositionTable();
            seed.AddClassSchedule_Roster_StatusesTable();
            seed.AddInstructor_Category();
            seed.AddInstructorTable();
            seed.AddLocationCategories();
            seed.AddLocation();
            seed.AddRatingScaleExpandedTable();
            seed.AddPositionSQsTable();
            seed.AddPositionHistoriesTable();
            seed.AddClassScheduleTable();
            seed.AddStudentEvaluationTable();
            seed.AddILA_Evaluator_LinksTable();
            seed.AddILA_Position_LinksTable();
            seed.AddILA_PreRequisite_LinksTable();
            
            //seed.AddProcedureReviewsTable();
            seed.AddClassScheduleEmployeesTable();
            seed.AddSelfRegistrationOptionsTable();
            seed.AddClassSchedule_Evaluation_RosterTable();
            seed.AddILA_SafetyHazard_LinksTable();
            seed.AddILA_Procedure_LinksTable();
            seed.AddILA_Segment_LinksTable();
            seed.AddILA_EnablingObjective_LinksTable();
            seed.AddILA_TaskObjective_LinksTable();
            seed.AddTestItemTable();
            seed.AddTestItemMCQTable();
            seed.AddTestItemTrueFalseTable();
            seed.AddTestItemFillBlankTable();
            seed.AddTestItemShortAnswerTable();
            seed.AddTestItemLinksTable();
            seed.AddAddQuestionBankTable();
            seed.AddStudentEvaluationWithoutEmpTable();
            seed.AddTaskQualification_Evaluator_LinksTable();
            seed.AddStudentEvaluation_QuestionTable();
            seed.AddPosition_TasksTable();
            seed.AddILA_UploadsTable();
            seed.AddILA_NERCAudience_LinksTable();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CBT_ScormRegistration");

            migrationBuilder.DropTable(
                name: "Certification_History");

            migrationBuilder.DropTable(
                name: "CertificationIssuingAuthorities");

            migrationBuilder.DropTable(
                name: "CertifyingBody_History");

            migrationBuilder.DropTable(
                name: "ClassSchedule_Evaluation_Roster");

            migrationBuilder.DropTable(
                name: "ClassSchedule_Recurrences");

            migrationBuilder.DropTable(
                name: "ClassSchedule_StudentEvaluations_Links");

            migrationBuilder.DropTable(
                name: "ClassScheduleHistories");

            migrationBuilder.DropTable(
                name: "ClientSettings_GeneralSettings");

            migrationBuilder.DropTable(
                name: "ClientSettings_LabelReplacements");

            migrationBuilder.DropTable(
                name: "ClientSettings_Licenses");

            migrationBuilder.DropTable(
                name: "ClientSettings_Notification_AvailableCustomSettings");

            migrationBuilder.DropTable(
                name: "ClientSettings_Notification_CustomSettings");

            migrationBuilder.DropTable(
                name: "ClientSettings_Notification_Step_AvailableCustomSettings");

            migrationBuilder.DropTable(
                name: "ClientSettings_Notification_Step_CustomSettings");

            migrationBuilder.DropTable(
                name: "ClientSettings_Notification_Step_ModelItems");

            migrationBuilder.DropTable(
                name: "ClientSettings_Notification_Step_Recipients");

            migrationBuilder.DropTable(
                name: "ClientUserSettings_DashboardSettings");

            migrationBuilder.DropTable(
                name: "Coversheets");

            migrationBuilder.DropTable(
                name: "CoverSheetTypes");

            migrationBuilder.DropTable(
                name: "DiscussionQuestions");

            migrationBuilder.DropTable(
                name: "DutyArea_Histories");

            migrationBuilder.DropTable(
                name: "EmployeeActivityNotifications");

            migrationBuilder.DropTable(
                name: "EmployeeCertifications");

            migrationBuilder.DropTable(
                name: "EmployeeCertifictaionHistories");

            migrationBuilder.DropTable(
                name: "EmployeeDocuments");

            migrationBuilder.DropTable(
                name: "EmployeeHistories");

            migrationBuilder.DropTable(
                name: "EmployeeOrganizations");

            migrationBuilder.DropTable(
                name: "EmployeePositions");

            migrationBuilder.DropTable(
                name: "EmpTests");

            migrationBuilder.DropTable(
                name: "EnablingObjective_CategoryHistories");

            migrationBuilder.DropTable(
                name: "EnablingObjective_Employee_Links");

            migrationBuilder.DropTable(
                name: "EnablingObjective_MetaEO_Links");

            migrationBuilder.DropTable(
                name: "EnablingObjective_SubCategoryHistories");

            migrationBuilder.DropTable(
                name: "EnablingObjective_Tools");

            migrationBuilder.DropTable(
                name: "EnablingObjective_TopicHistories");

            migrationBuilder.DropTable(
                name: "EnablingObjectiveHistories");

            migrationBuilder.DropTable(
                name: "EvaluationReleaseEMPSettings");

            migrationBuilder.DropTable(
                name: "IDP_Review");

            migrationBuilder.DropTable(
                name: "IDPSchedules");

            migrationBuilder.DropTable(
                name: "ILA_AssessmentTool_Links");

            migrationBuilder.DropTable(
                name: "ILA_EnablingObjective_Links");

            migrationBuilder.DropTable(
                name: "ILA_Evaluator_Links");

            migrationBuilder.DropTable(
                name: "ILA_NERCAudience_Links");

            migrationBuilder.DropTable(
                name: "ILA_NercStandard_Links");

            migrationBuilder.DropTable(
                name: "ILA_PerformTraineeEvals");

            migrationBuilder.DropTable(
                name: "ILA_Position_Links");

            migrationBuilder.DropTable(
                name: "ILA_PreRequisite_Links");

            migrationBuilder.DropTable(
                name: "ILA_Procedure_Links");

            migrationBuilder.DropTable(
                name: "ILA_RegRequirement_Links");

            migrationBuilder.DropTable(
                name: "ILA_SafetyHazard_Links");

            migrationBuilder.DropTable(
                name: "ILA_Segment_Links");

            migrationBuilder.DropTable(
                name: "ILA_StudentEvaluation_Links");

            migrationBuilder.DropTable(
                name: "ILA_TaskObjective_Links");

            migrationBuilder.DropTable(
                name: "ILA_TrainingTopic_Links");

            migrationBuilder.DropTable(
                name: "ILA_Uploads");

            migrationBuilder.DropTable(
                name: "ILACertificationSubRequirementLinks");

            migrationBuilder.DropTable(
                name: "ILACollaborators");

            migrationBuilder.DropTable(
                name: "ILACustomObjective_Links");

            migrationBuilder.DropTable(
                name: "ILAHistories");

            migrationBuilder.DropTable(
                name: "Instructor_CategoryHistories");

            migrationBuilder.DropTable(
                name: "Instructor_Histories");

            migrationBuilder.DropTable(
                name: "Location_CategoryHistories");

            migrationBuilder.DropTable(
                name: "Location_Histories");

            migrationBuilder.DropTable(
                name: "Meta_ILAMembers_Links");

            migrationBuilder.DropTable(
                name: "Position_Employees");

            migrationBuilder.DropTable(
                name: "Position_Histories");

            migrationBuilder.DropTable(
                name: "Position_Tasks");

            migrationBuilder.DropTable(
                name: "Positions_SQs");

            migrationBuilder.DropTable(
                name: "Proc_IssuingAuthority_Histories");

            migrationBuilder.DropTable(
                name: "Procedure_EnablingObjective_Links");

            migrationBuilder.DropTable(
                name: "Procedure_ILA_Links");

            migrationBuilder.DropTable(
                name: "Procedure_RR_Links");

            migrationBuilder.DropTable(
                name: "Procedure_SaftyHazard_Links");

            migrationBuilder.DropTable(
                name: "Procedure_StatusHistories");

            migrationBuilder.DropTable(
                name: "Procedure_Task_Links");

            migrationBuilder.DropTable(
                name: "ProcedureReview_Employees");

            migrationBuilder.DropTable(
                name: "QTDUsers");

            migrationBuilder.DropTable(
                name: "QuestionBankHistories");

            migrationBuilder.DropTable(
                name: "RatingScaleExpanded");

            migrationBuilder.DropTable(
                name: "RegRequirement_EO_Links");

            migrationBuilder.DropTable(
                name: "RegRequirement_ILA_Links");

            migrationBuilder.DropTable(
                name: "ReportDisplayColumns");

            migrationBuilder.DropTable(
                name: "ReportFilters");

            migrationBuilder.DropTable(
                name: "ReportSkeletonColumns");

            migrationBuilder.DropTable(
                name: "ReportSkeletonFilters");

            migrationBuilder.DropTable(
                name: "RR_IssuingAuthority_StatusHistories");

            migrationBuilder.DropTable(
                name: "RR_StatusHistories");

            migrationBuilder.DropTable(
                name: "RR_Task_Links");

            migrationBuilder.DropTable(
                name: "SafetyHazard_CategoryHistories");

            migrationBuilder.DropTable(
                name: "SafetyHazard_EO_Links");

            migrationBuilder.DropTable(
                name: "SafetyHazard_Histories");

            migrationBuilder.DropTable(
                name: "SafetyHazard_ILA_Links");

            migrationBuilder.DropTable(
                name: "SafetyHazard_Set_Links");

            migrationBuilder.DropTable(
                name: "SafetyHazard_Task_Links");

            migrationBuilder.DropTable(
                name: "SafetyHazard_Tool_Links");

            migrationBuilder.DropTable(
                name: "SaftyHazard_Abatements");

            migrationBuilder.DropTable(
                name: "SaftyHazard_Controls");

            migrationBuilder.DropTable(
                name: "SaftyHazard_RR_Links");

            migrationBuilder.DropTable(
                name: "SegmentObjective_Links");

            migrationBuilder.DropTable(
                name: "SelfRegistrationOptions");

            migrationBuilder.DropTable(
                name: "SimulationScenarioSpecLookUps");

            migrationBuilder.DropTable(
                name: "SimulatorScenario_EnablingObjectives_Links");

            migrationBuilder.DropTable(
                name: "SimulatorScenarioILA_Links");

            migrationBuilder.DropTable(
                name: "SimulatorScenarioPositon_Links");

            migrationBuilder.DropTable(
                name: "SimulatorScenarioTaskObjectives_Links");

            migrationBuilder.DropTable(
                name: "StudentEvaluation_Questions");

            migrationBuilder.DropTable(
                name: "StudentEvaluationHistories");

            migrationBuilder.DropTable(
                name: "StudentEvaluationWithoutEmp");

            migrationBuilder.DropTable(
                name: "SubDutyArea_Histories");

            migrationBuilder.DropTable(
                name: "Task_Collaborator_Links");

            migrationBuilder.DropTable(
                name: "Task_EnablingObjective_Links");

            migrationBuilder.DropTable(
                name: "Task_Histories");

            migrationBuilder.DropTable(
                name: "Task_ILA_Links");

            migrationBuilder.DropTable(
                name: "Task_MetaTask_Links");

            migrationBuilder.DropTable(
                name: "Task_Positions");

            migrationBuilder.DropTable(
                name: "Task_Reference_Links");

            migrationBuilder.DropTable(
                name: "Task_Tools");

            migrationBuilder.DropTable(
                name: "Task_TrainingGroups");

            migrationBuilder.DropTable(
                name: "TaskQualification_Evaluator_Links");

            migrationBuilder.DropTable(
                name: "TaskReQualificationEmp_QuestionAnswers");

            migrationBuilder.DropTable(
                name: "TaskReQualificationEmp_SignOffs");

            migrationBuilder.DropTable(
                name: "TaskReQualificationEmp_Steps");

            migrationBuilder.DropTable(
                name: "TaskReQualificationEmp_Suggestions");

            migrationBuilder.DropTable(
                name: "Test_Histories");

            migrationBuilder.DropTable(
                name: "Test_Item_Links");

            migrationBuilder.DropTable(
                name: "TestItem_Histories");

            migrationBuilder.DropTable(
                name: "TestItemFillBlanks");

            migrationBuilder.DropTable(
                name: "TestItemMatches");

            migrationBuilder.DropTable(
                name: "TestItemMCQs");

            migrationBuilder.DropTable(
                name: "TestItemShortAnswers");

            migrationBuilder.DropTable(
                name: "TestItemTrueFalses");

            migrationBuilder.DropTable(
                name: "TestReleaseEMPSetting_Retake_Links");

            migrationBuilder.DropTable(
                name: "TestSettings");

            migrationBuilder.DropTable(
                name: "Timesheets");

            migrationBuilder.DropTable(
                name: "Tool_StatusHistories");

            migrationBuilder.DropTable(
                name: "ToolGroup_Tools");

            migrationBuilder.DropTable(
                name: "TQEmpSettings");

            migrationBuilder.DropTable(
                name: "TQILAEmpSettings");

            migrationBuilder.DropTable(
                name: "TrainingProgram_Histories");

            migrationBuilder.DropTable(
                name: "TrainingPrograms_ILA_Links");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_Employee_Links");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_ILALinks");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_MetaEOLinks");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_Position_Links");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_Procedure_Links");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_Questions");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_RRLinks");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_SaftyHazard_Links");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_Steps");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_Suggestions");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_Tasks");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjective_Tool_Links");

            migrationBuilder.DropTable(
                name: "Version_MetaILAs");

            migrationBuilder.DropTable(
                name: "Version_Procedure_EnablingObjective_Links");

            migrationBuilder.DropTable(
                name: "Version_Procedure_SaftyHazard_Links");

            migrationBuilder.DropTable(
                name: "Version_Procedure_Tool_Links");

            migrationBuilder.DropTable(
                name: "Version_SaftyHazard_Abatements");

            migrationBuilder.DropTable(
                name: "Version_SaftyHazard_Controls");

            migrationBuilder.DropTable(
                name: "Version_Task_EnablingObjective_Links");

            migrationBuilder.DropTable(
                name: "Version_Task_ILA_Links");

            migrationBuilder.DropTable(
                name: "Version_Task_MetaTask_Links");

            migrationBuilder.DropTable(
                name: "Version_Task_Position_Links");

            migrationBuilder.DropTable(
                name: "Version_Task_Procedure_Links");

            migrationBuilder.DropTable(
                name: "Version_Task_Questions");

            migrationBuilder.DropTable(
                name: "Version_Task_RR_Links");

            migrationBuilder.DropTable(
                name: "Version_Task_SaftyHazard_Links");

            migrationBuilder.DropTable(
                name: "Version_Task_Steps");

            migrationBuilder.DropTable(
                name: "Version_Task_Suggestions");

            migrationBuilder.DropTable(
                name: "Version_Task_Tool_Links");

            migrationBuilder.DropTable(
                name: "Version_Task_TrainingGroups");

            migrationBuilder.DropTable(
                name: "Version_TestItems");

            migrationBuilder.DropTable(
                name: "Version_Tests");

            migrationBuilder.DropTable(
                name: "Version_TestStatuses");

            migrationBuilder.DropTable(
                name: "Version_TrainingProgram_ILA_Links");

            migrationBuilder.DropTable(
                name: "CBT_ScormUpload");

            migrationBuilder.DropTable(
                name: "ClassScheduleEmployees");

            migrationBuilder.DropTable(
                name: "ClientSettings_Notification_Steps");

            migrationBuilder.DropTable(
                name: "ClientUsers");

            migrationBuilder.DropTable(
                name: "DashboardSettings");

            migrationBuilder.DropTable(
                name: "ILATraineeEvaluations");

            migrationBuilder.DropTable(
                name: "ActivityNotifications");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "ClassSchedule_Roster");

            migrationBuilder.DropTable(
                name: "IDP_ReviewStatuses");

            migrationBuilder.DropTable(
                name: "IDPs");

            migrationBuilder.DropTable(
                name: "AssessmentTools");

            migrationBuilder.DropTable(
                name: "NERCTargetAudiences");

            migrationBuilder.DropTable(
                name: "NercStandardMembers");

            migrationBuilder.DropTable(
                name: "StudentEvaluationAudiences");

            migrationBuilder.DropTable(
                name: "StudentEvaluationAvailabilities");

            migrationBuilder.DropTable(
                name: "TrainingTopics");

            migrationBuilder.DropTable(
                name: "CertificationSubRequirements");

            migrationBuilder.DropTable(
                name: "ILACertificationLinks");

            migrationBuilder.DropTable(
                name: "CollaboratorInvitations");

            migrationBuilder.DropTable(
                name: "CustomEnablingObjectives");

            migrationBuilder.DropTable(
                name: "MetaILAConfigurationPublishOptions");

            migrationBuilder.DropTable(
                name: "ProcedureReviews");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "ReportSkeletons");

            migrationBuilder.DropTable(
                name: "SafetyHazard_Sets");

            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropTable(
                name: "SimulatorScenarios");

            migrationBuilder.DropTable(
                name: "QuestionBanks");

            migrationBuilder.DropTable(
                name: "StudentEvaluationQuestions");

            migrationBuilder.DropTable(
                name: "StudentEvaluations");

            migrationBuilder.DropTable(
                name: "Task_CollaboratorInvitations");

            migrationBuilder.DropTable(
                name: "Task_References");

            migrationBuilder.DropTable(
                name: "TestReleaseEMPSettings");

            migrationBuilder.DropTable(
                name: "Employee_Tasks");

            migrationBuilder.DropTable(
                name: "ToolGroups");

            migrationBuilder.DropTable(
                name: "TaskQualifications");

            migrationBuilder.DropTable(
                name: "Version_Employees");

            migrationBuilder.DropTable(
                name: "EnablingObjective_Questions");

            migrationBuilder.DropTable(
                name: "EnablingObjective_Steps");

            migrationBuilder.DropTable(
                name: "EnablingObjective_Suggestions");

            migrationBuilder.DropTable(
                name: "MetaILAs");

            migrationBuilder.DropTable(
                name: "Version_Positions");

            migrationBuilder.DropTable(
                name: "Version_Procedures");

            migrationBuilder.DropTable(
                name: "Task_Questions");

            migrationBuilder.DropTable(
                name: "Version_RegulatoryRequirements");

            migrationBuilder.DropTable(
                name: "Version_SaftyHazards");

            migrationBuilder.DropTable(
                name: "Task_Steps");

            migrationBuilder.DropTable(
                name: "Task_Suggestions");

            migrationBuilder.DropTable(
                name: "Version_Tools");

            migrationBuilder.DropTable(
                name: "Version_Tasks");

            migrationBuilder.DropTable(
                name: "Version_TrainingGroups");

            migrationBuilder.DropTable(
                name: "TestItems");

            migrationBuilder.DropTable(
                name: "Version_EnablingObjectives");

            migrationBuilder.DropTable(
                name: "Version_ILAs");

            migrationBuilder.DropTable(
                name: "Version_TrainingPrograms");

            migrationBuilder.DropTable(
                name: "CBTs");

            migrationBuilder.DropTable(
                name: "ClassSchedule_Roster_Statuses");

            migrationBuilder.DropTable(
                name: "ClientSettings_Notifications");

            migrationBuilder.DropTable(
                name: "TraineeEvaluationTypes");

            migrationBuilder.DropTable(
                name: "ClassSchedules");

            migrationBuilder.DropTable(
                name: "TestTypes");

            migrationBuilder.DropTable(
                name: "NercStandards");

            migrationBuilder.DropTable(
                name: "TrainingTopic_Categories");

            migrationBuilder.DropTable(
                name: "Certifications");

            migrationBuilder.DropTable(
                name: "SimulatorScenarioDifficultyLevels");

            migrationBuilder.DropTable(
                name: "StudentEvaluationForms");

            migrationBuilder.DropTable(
                name: "RatingScaleNs");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "EvaluationMethods");

            migrationBuilder.DropTable(
                name: "TaskQualificationStatuses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "MetaILA_Statuses");

            migrationBuilder.DropTable(
                name: "MetaILAAssessments");

            migrationBuilder.DropTable(
                name: "Procedures");

            migrationBuilder.DropTable(
                name: "RegulatoryRequirements");

            migrationBuilder.DropTable(
                name: "SaftyHazards");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TrainingGroups");

            migrationBuilder.DropTable(
                name: "TaxonomyLevels");

            migrationBuilder.DropTable(
                name: "TestItemTypes");

            migrationBuilder.DropTable(
                name: "EnablingObjectives");

            migrationBuilder.DropTable(
                name: "TrainingPrograms");

            migrationBuilder.DropTable(
                name: "ILAs");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "CertifyingBodies");

            migrationBuilder.DropTable(
                name: "RatingScales");

            migrationBuilder.DropTable(
                name: "TestStatuses");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Procedure_IssuingAuthorities");

            migrationBuilder.DropTable(
                name: "RR_IssuingAuthorities");

            migrationBuilder.DropTable(
                name: "SaftyHazard_Categories");

            migrationBuilder.DropTable(
                name: "ToolCategories");

            migrationBuilder.DropTable(
                name: "SubdutyAreas");

            migrationBuilder.DropTable(
                name: "TrainingGroup_Categories");

            migrationBuilder.DropTable(
                name: "EnablingObjective_Topics");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "TrainingProgramTypes");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");

            migrationBuilder.DropTable(
                name: "ILA_Topics");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Instructor_Categories");

            migrationBuilder.DropTable(
                name: "Location_Categories");

            migrationBuilder.DropTable(
                name: "DutyAreas");

            migrationBuilder.DropTable(
                name: "EnablingObjective_SubCategories");

            migrationBuilder.DropTable(
                name: "ProviderLevels");

            migrationBuilder.DropTable(
                name: "EnablingObjective_Categories");
        }
    }
}
