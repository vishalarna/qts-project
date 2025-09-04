using QTD2.API.QTD;
using QTD2.Tests.IntegrationTests.Testing.Fixures;

namespace QTD2.Tests.IntegrationTests.Testing.Base
{
    public class QTDControllerBase : ControllerBaseClass<Startup>
    {
        public QTDControllerBase(QTDFixture qtdFixture) : base(qtdFixture)
        {

        }
    }
}
