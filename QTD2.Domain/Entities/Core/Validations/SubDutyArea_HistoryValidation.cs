using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SubDutyArea_HistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SubDutyArea_HistoryValidation : Validation<SubDutyArea_History>, ISubDutyArea_HistoryValidation
    {
        public SubDutyArea_HistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SubDutyArea_History>(new SubDutyAreaIdRequiredSpecs(), _validationStringLocalizer["SubDutyAreaIdRequiredSpecs"]));
        }
    }
}
