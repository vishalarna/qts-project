using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILA_DesignSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
  public class InstructorWorkbook_ILA_DesignValidation : Validation<InstructorWorkbook_ILA_Design>, IInstructorWorkbook_ILA_DesignValidation
    {
        public InstructorWorkbook_ILA_DesignValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignILAIdRequiredSpecs(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignILAIdRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignIsOptionalTextRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignIsOptionalTextRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignDesignResultRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignDesignResultRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignOtherTypeAssesmentToolRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignOtherTypeAssesmentToolRequired"]));
            
            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignILADetailsStatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignILADetailsStatusRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignObjectivesAndSegmentsStatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignObjectivesAndSegmentsStatusRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignProceduresStatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignProceduresStatusRequired"]));
            
            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignTrainingPlanResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignTrainingPlanResponseRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignTrainingPlanStatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignTrainingPlanStatusRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignEvaluationMethodResponseRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignEvaluationMethodResponseRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignEvaluationMethodResponseStatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignEvaluationMethodResponseStatusRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignPrerequistiesStatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignPrerequistiesStatusRequired"]));
          
            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignResourcesStatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignResourcesStatusRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignILAApplicationStatusRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignILAApplicationStatusRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignPilotDataApplicableRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignPilotDataApplicableRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignInstructorCommentsRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignInstructorCommentsRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Design>(new InstructorWorkbook_ILA_DesignReviewerCommentsRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignReviewerCommentsRequired"]));
        }
    }
    
}
