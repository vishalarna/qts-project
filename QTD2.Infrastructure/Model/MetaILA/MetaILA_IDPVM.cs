using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.MetaILA
{
    public class MetaILA_IDPVM
    {
        public int MetaILAId { get; set; }
        public string MetaILATitle { get; set; }
        public string CompletedLinkedCourses { get; set; }
        public string MetaStatus { get; set; }
        public DateTime? MetaTestCompletedDate { get; set; }
        public string MetaTestGrade { get; set; }
        public int? MetaTestScore { get; set; }
        public string MetaStudentEvaluationStatus { get; set; }

        public MetaILA_IDPVM()
        {
        }

        public MetaILA_IDPVM(int metaILAId,string metaILATitle,string completedLinkedCourses,string metaStatus,DateTime? metaTestCompletedDate,string metaTestGrade,int? metaTestScore,string metaStudentEvaluationStatus)
        {
            MetaILAId = metaILAId;
            MetaILATitle = metaILATitle;
            CompletedLinkedCourses = completedLinkedCourses;
            MetaStatus = metaStatus;
            MetaTestCompletedDate = metaTestCompletedDate;
            MetaTestGrade = metaTestGrade;
            MetaTestScore = metaTestScore;
            MetaStudentEvaluationStatus = metaStudentEvaluationStatus;
        }
    }

}
