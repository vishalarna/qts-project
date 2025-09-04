using Moq;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Procedure_IssuingAuthority;
using System;
using System.ComponentModel.DataAnnotations;

namespace QTD2.Test.Mock.Application.Interfaces.Services.Shared
{
    public class Mock_IProcedureService : Mock<IProcedureService>
    {
        public Mock_IProcedureService()
        {
            Setup(service => service.CreateProcedure_IssuingAuthorityAsync(It.IsAny<Procedure_IssuingAuthorityCreateOptions>())).Returns<Procedure_IssuingAuthorityCreateOptions>(input => System.Threading.Tasks.Task.FromResult(new Procedure_IssuingAuthority(input.Description, input.Title, input.Website, DateOnly.FromDateTime(input.EffectiveDate), input.Notes, input.IsActive, input.IsDeleted)));

            Setup(service => service.CreateProcedure_IssuingAuthorityAsync(It.Is<Procedure_IssuingAuthorityCreateOptions>(o => o.Description.Length > 250))).Throws(new ValidationException());
            Setup(service => service.CreateProcedure_IssuingAuthorityAsync(It.Is<Procedure_IssuingAuthorityCreateOptions>(o => string.IsNullOrEmpty(o.Description)))).Throws(new ValidationException());
            Setup(service => service.CreateProcedure_IssuingAuthorityAsync(It.Is<Procedure_IssuingAuthorityCreateOptions>(o => o == null))).Throws(new Exception());
        }
    }
}
