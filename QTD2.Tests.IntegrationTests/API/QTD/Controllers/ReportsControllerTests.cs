
using QTD2.Domain;
using QTD2.Infrastructure.Identity.Settings;
using QTD2.Infrastructure.Model.Reports;
using QTD2.Infrastructure.Reports.Export;
using QTD2.Test.Data.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Model.CertifyingBody;
using QTD2.Tests.IntegrationTests.Testing.Base;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using Xunit;

namespace QTD2.Tests.IntegrationTests.API.QTD.Controllers
{
    [Collection("QTD Collection")]
    public class ReportsControllerTests : QTDControllerBase
    {
        readonly QTDFixture _qtdFixture;
        public ReportsControllerTests(QTDFixture qtdFixture) : base(qtdFixture)
        {

        }

        public static IEnumerable<object[]> Data_ReportsController_GetReportFilterOptionsAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, "positions", HttpStatusCode.OK });

            return data;
        }


        public static IEnumerable<object[]> Data_ReportsController_GetAllAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ReportsController_CreateReportAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            var model = new ReportCreateOptions()
            {
                ReportSkeletonId = 1,
                InternalReportTitle = "Data_ReportsController_CreateReportAsync RS 1",
                Filters = new List<ReportFilter>()
                {
                    new ReportFilter()
                    {
                        Name = "Date Range",
                        Value= "01/01/2023,03/17/2023"
                    },
                     new ReportFilter()
                    {
                        Name = "Position",
                        Value= "1,2,3"
                    }
                },
                DisplayColumns = new List<string>()
                {
                    "DutyArea",
                    "SubDutyArea",
                    "Topic"
                }
            };

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, model, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ReportsController_GetAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, 1, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ReportsController_UpdateReportAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            var model = new ReportUpdateOptions
            {
                ReportSkeletonId = 1,
                InternalReportTitle = "Test update",
                Filters = new List<ReportFilter>()
                {
                    new ReportFilter(){Name="Test",Value="Update"},
                },
                DisplayColumns = new List<string>()
                {
                    "Test"
                }
            };

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, 1, model, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ReportsController_ExportReportAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, 1, new ExportReportModel() { ExportType = ReportExportType.Excel }, HttpStatusCode.OK });
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, 1, new ExportReportModel() { ExportType = ReportExportType.Excel, Options = new ReportCreateOrUpdateOptions() }, HttpStatusCode.OK });
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, -1, new ExportReportModel()
            {
                ExportType = ReportExportType.Excel,
                Options = new ReportCreateOrUpdateOptions(){
                    ReportSkeletonId = 1,
                    InternalReportTitle = "Data_ReportsController_CreateReportAsync RS 1",
                    Filters = new List<ReportFilter>()
                    {
                        new ReportFilter()
                        {
                            Name = "Date Range",
                            Value= "01/01/2023,03/17/2023"
                        },
                        new ReportFilter()
                        {
                            Name = "Positions",
                            Value= "1,2,3"
                        }
                    },
                    DisplayColumns = new List<string>()
                    {
                        "DutyArea",
                        "SubDutyArea",
                        "Topic"
                    }
                }
             },
                HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ReportsController_ExportReportsAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            var model = new ReportCreateOptions
            {
                ReportSkeletonId = 1,
                InternalReportTitle = "Data_ReportsController_CreateReportAsync RS 2",
                Filters = new List<ReportFilter>()
                {
                    new ReportFilter()
                    {
                        Name = "Date Range",
                        Value= "01/01/2023,03/17/2023"
                    },
                     new ReportFilter()
                    {
                        Name = "Position",
                        Value= "1,2,3"
                    }
                },
                DisplayColumns = new List<string>()
                {
                    "DutyArea",
                    "SubDutyArea",
                    "Topic"
                }
            };

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, model, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ReportsController_GenerateReportAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            var model = new ReportCreateOrUpdateOptions()
            {
                ReportSkeletonId = 1,
                InternalReportTitle = "Data_ReportsController_CreateReportAsync RS 1",
                Filters = new List<ReportFilter>()
                    {
                        new ReportFilter()
                        {
                            Name = "Date Range",
                            Value= "01/01/2023,03/17/2023"
                        },
                        new ReportFilter()
                        {
                            Name = "Positions",
                            Value= "1,2,3"
                        }
                    },
                DisplayColumns = new List<string>()
                    {
                        "DutyArea",
                        "SubDutyArea",
                        "Topic"
                    }
            };

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, model, -1, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ReportsController_GenerateReportsAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            var model = new ReportCreateOrUpdateOptions
            {
                ReportSkeletonId = 1,
                InternalReportTitle = "Test",
                Filters = new List<ReportFilter>()
                {

                },
                DisplayColumns = new List<string>()
                {

                },
            };

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, model, 1, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ReportsController_SendReportAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, 1, new SendReportModel() { Tos = new List<string>()
                {
                    "test@qts.com"
                }, ExportType = ReportExportType.Excel }, HttpStatusCode.OK });
            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, 1, new SendReportModel() { Tos = new List<string>()
                {
                    "test@qts.com"
                }, ExportType = ReportExportType.Excel, CreateOrUpdateOptions = new ReportCreateOrUpdateOptions() }, HttpStatusCode.OK });
            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, -1, new SendReportModel()
            {
                Tos = new List<string>()
                {
                    "test@qts.com"
                },
                ExportType = ReportExportType.Excel,
                CreateOrUpdateOptions = new ReportCreateOrUpdateOptions(){
                    ReportSkeletonId = 1,
                    InternalReportTitle = "Data_ReportsController_CreateReportAsync RS 1",
                    Filters = new List<ReportFilter>()
                    {
                        new ReportFilter()
                        {
                            Name = "Date Range",
                            Value= "01/01/2023,03/17/2023"
                        },
                        new ReportFilter()
                        {
                            Name = "Positions",
                            Value= "1,2,3"
                        }
                    },
                    DisplayColumns = new List<string>()
                    {
                        "DutyArea",
                        "SubDutyArea",
                        "Topic"
                    }
                }
             },
                HttpStatusCode.OK });

            return data;
        }

        [Theory]
        [MemberData(nameof(Data_ReportsController_GetReportFilterOptionsAsync))]
        public async void ReportsController_GetReportFilterOptionsAsync(ClaimsBuilderOptions options, string username, string filterName, HttpStatusCode httpStatusCode)
        {
            string url = $"/reports/options/{filterName}";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ReportsController_GetAllAsync))]
        public async void ReportsController_GetAllAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/reports";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ReportsController_CreateReportAsync))]
        public async void ReportsController_CreateReportAsync(ClaimsBuilderOptions options, string username, ReportCreateOptions model, HttpStatusCode httpStatusCode)
        {
            string url = $"/reports";
            await TestPostAsync(url, username, options, model, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ReportsController_GetAsync))]
        public async void ReportsController_GetAsync(ClaimsBuilderOptions options, string username, int reportId, HttpStatusCode httpStatusCode)
        {
            string url = $"/reports/{reportId}";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ReportsController_UpdateReportAsync))]
        public async void ReportsController_UpdateReportAsync(ClaimsBuilderOptions options, string username, int reportId, ReportUpdateOptions model, HttpStatusCode httpStatusCode)
        {
            string url = $"/reports/{reportId}";
            await TestPutAsync(url, username, options, model, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ReportsController_ExportReportAsync))]
        public async void ReportsController_ExportReportAsync(ClaimsBuilderOptions options, string username, int reportId, ExportReportModel model, HttpStatusCode httpStatusCode)
        {
            string url = $"/reports/export{(reportId < 1 ? "" : "/" + reportId)}";
            await TestPostAsync(url, username, options, model, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ReportsController_GenerateReportAsync))]
        public async void ReportsController_GenerateReportAsync(ClaimsBuilderOptions options, string username, int reportId, ReportCreateOrUpdateOptions model, HttpStatusCode httpStatusCode)
        {
            string url = $"/reports/generate{(reportId < 1 ? "" : "/" + reportId)}";
            await TestPostAsync(url, username, options, model, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ReportsController_GenerateReportsAsync))]
        public async void ReportsController_GenerateReportsAsync(ClaimsBuilderOptions options, string username, ReportCreateOrUpdateOptions model, int reportId, HttpStatusCode httpStatusCode)
        {
            string url = $"/reports/generate/{reportId}";
            await TestPostAsync(url, username, options, model, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ReportsController_SendReportAsync))]
        public async void ReportsController_SendReportAsync(ClaimsBuilderOptions options, string username, int reportId, SendReportModel model, HttpStatusCode httpStatusCode)
        {
            string url = $"/reports/send{(reportId < 1 ? "" : "/" + reportId)}";
            await TestPostAsync(url, username, options, model, httpStatusCode);
        }


        //public static IEnumerable<object[]> Data_ReportsController_SendReportAsync()
        //{
        //    var data = new List<object[]>();
        //    var model = new ReportSendOptions
        //    {

        //    };

        //    data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, model, HttpStatusCode.OK });

        //    return data;
        //}

        //[Theory]
        //[MemberData(nameof(Data_ReportsController_SendReportAsync))]
        //public async void ReportsController_SendReportAsync(ClaimsBuilderOptions options, string username, ReportSendOptions model,int reportId, HttpStatusCode httpStatusCode)
        //{
        //    string url = $"/reports/send/{reportId}";
        //    await TestPostAsync(url, username, options, model, httpStatusCode);
        //}


        //public static IEnumerable<object[]> Data_ReportsController_SendReportAsync()
        //{
        //    var data = new List<object[]>();
        //    var model = new ReportSendOptions
        //    {

        //    };

        //    data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, model, HttpStatusCode.OK });

        //    return data;
        //}
        //[Theory]
        //[MemberData(nameof(Data_ReportsController_SendReportAsync))]
        //public async void ReportsController_SendReportAsync(ClaimsBuilderOptions options, string username, ReportCreateOptions model, HttpStatusCode httpStatusCode)
        //{
        //    string url = $"/reports/send";
        //    await TestPostAsync(url, username, options, model, httpStatusCode);
        //}
    }
}
