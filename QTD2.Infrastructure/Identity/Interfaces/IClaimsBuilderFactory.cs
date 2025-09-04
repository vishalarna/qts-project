using QTD2.Infrastructure.Identity.Settings;

namespace QTD2.Infrastructure.Identity.Interfaces
{
    public interface IClaimsBuilderFactory
    {
        IClaimsBuilder GetBuilder(ClaimsBuilderOptions options);
    }
}
