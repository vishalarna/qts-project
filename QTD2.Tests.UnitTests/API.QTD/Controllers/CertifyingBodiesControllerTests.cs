using QTD2.API.QTD.Controllers;
using QTD2.Test.Mock.Application.Interfaces.Services.Shared;

namespace QTD2.Tests.UnitTests.API.QTD.Controllers
{
    public class CertifyingBodiesControllerTests
    {
        readonly CertifyingBodiesController _controller;
        readonly Mock_ICertifyingBodiesService _certifyingBodyService = new Mock_ICertifyingBodiesService();

        public CertifyingBodiesControllerTests()
        {
            //  _controller = new CertifyingBodiesController(_certifyingBodyService.Object);
        }
    }
}
