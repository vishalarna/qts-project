using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Infrastructure.Authorization.Operations.Core
{
    public static class SimulatorScenario_Script_CriteriaOperations
    {
        public static readonly OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement { Name = nameof(Create) };
        public static readonly OperationAuthorizationRequirement Read = new OperationAuthorizationRequirement { Name = nameof(Read) };
        public static readonly OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement { Name = nameof(Update) };
        public static readonly OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = nameof(Delete) };
    }
}
