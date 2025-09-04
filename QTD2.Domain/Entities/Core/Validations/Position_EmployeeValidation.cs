using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Position_EmployeeSpecs;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Position_EmployeeValidation : Validation<Position_Employee>, IPosition_EmployeeValidation
    {
        public Position_EmployeeValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Position_Employee>(new Position_EmployeePositionIdRequiredSpec(), _validationStringLocalizer["PositionIdRequired"]));
            AddRule(new ValidationRule<Position_Employee>(new Position_Employee_EmployeeIdRequiredSpec(), _validationStringLocalizer["EmployeeIdRequired"]));
        }
    }
}
