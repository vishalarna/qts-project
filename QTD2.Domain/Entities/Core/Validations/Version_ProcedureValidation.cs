using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_ProcedureSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_ProcedureValidation : Validation<Version_Procedure>, IVersion_ProcedureValidation
    {
        public Version_ProcedureValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Procedure>(new VP_ProcedureNumberRequiredSpec(), _validationStringLocalizer["VP_ProcedureNumberRequired"]));
            AddRule(new ValidationRule<Version_Procedure>(new VP_TitleRequiredSpec(), _validationStringLocalizer["VP_TitleRequired"]));
            AddRule(new ValidationRule<Version_Procedure>(new VP_MajorVersionRequiredSpec(), _validationStringLocalizer["VP_MajorVersionRequired"]));
            AddRule(new ValidationRule<Version_Procedure>(new VP_MinorVersionRequiredSpec(), _validationStringLocalizer["VP_MinorVersionRequired"]));
        }
    }
}
