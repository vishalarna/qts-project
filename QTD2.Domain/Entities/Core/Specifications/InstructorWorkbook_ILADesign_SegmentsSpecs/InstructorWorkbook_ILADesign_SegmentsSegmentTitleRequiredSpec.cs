using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_SegmentsSpecs
{
    class InstructorWorkbook_ILADesign_SegmentsSegmentTitleRequiredSpec : ISpecification<InstructorWorkbook_ILADesign_Segments>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILADesign_Segments entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.SegmentTitle);
        }
    }
}