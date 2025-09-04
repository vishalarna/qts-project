using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILACustomObjective_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILACustomObjective_LinkValidation : Validation<ILACustomObjective_Link>, IILACustomObjective_LinkValidation
    {
        public ILACustomObjective_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILACustomObjective_Link>(new ILACustomObjective_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILACustomObjective_LinkILAIdRequiredSpec"]));
            AddRule(new ValidationRule<ILACustomObjective_Link>(new ILACustObj_LinkCustomObjIdRequiredSpec(), _validationStringLocalizer["ILACustObj_LinkCustomObjIdRequiredSpec"]));
        }
    }
}
