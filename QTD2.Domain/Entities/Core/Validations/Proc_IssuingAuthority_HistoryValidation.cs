using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.IssuingAuthorityStatusHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Proc_IssuingAuthority_HistoryValidation : Validation<Proc_IssuingAuthority_History>, IProc_IssuingAuthority_HistoryValidation
    {
        public Proc_IssuingAuthority_HistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Proc_IssuingAuthority_History>(new Proc_IssuingAuthority_HistoryIssuAuthIdReqSpec(), _validationStringLocalizer["IssuAuthStatusHistoryIssuAuthIdReqSpec"]));
        }
    }
}
