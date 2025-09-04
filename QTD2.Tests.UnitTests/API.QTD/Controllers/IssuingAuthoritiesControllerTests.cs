using QTD2.API.QTD.Controllers;
using QTD2.Test.Data.Infrastructure.Model.Procedure_IssuingAuthority;
using QTD2.Test.Mock.Application.Interfaces.Services.Shared;
using System.Collections.Generic;


namespace QTD2.Tests.UnitTests.API.QTD.Controllers
{
    public partial class IssuingAuthoritiesControllerTests
    {
        readonly IssuingAuthoritiesController _controller;
        readonly Mock_IProcedureService _procedureService = new Mock_IProcedureService();

        public IssuingAuthoritiesControllerTests()
        {
            // _controller = new IssuingAuthoritiesController(_procedureService.Object);
        }

        public static List<object[]> Create_StatusCodeCorrect()
        {
            return new List<object[]>()
            {
                new object[] { Procedure_IssuingAuthorityCreateOptionsTestData.Null(), 422 },
                new object[] { Procedure_IssuingAuthorityCreateOptionsTestData.Empty(), 422 },
                new object[] { Procedure_IssuingAuthorityCreateOptionsTestData.NameTooLong(), 422 },
                new object[] { Procedure_IssuingAuthorityCreateOptionsTestData.PriorityIA(), 200 },
                new object[] { Procedure_IssuingAuthorityCreateOptionsTestData.NonPriorityIA(), 200 },
            };
        }
    }
}
