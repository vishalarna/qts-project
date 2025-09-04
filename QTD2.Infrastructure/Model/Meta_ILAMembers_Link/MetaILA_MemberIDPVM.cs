using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Meta_ILAMembers_Link
{
    public class MetaILA_MemberIDPVM
    {
        public int ILAId { get; set; }
        public string ILANumber { get; set; }
        public string ILAName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Grade { get; set; }
        public int? Score { get; set; }
        public bool? IsILACompleted { get; set; }

        public MetaILA_MemberIDPVM()
        {
        }

        public MetaILA_MemberIDPVM(int ilaId,string ilaNumber,string ilaName,DateTime? startDate,DateTime? completedDate,string grade,int? score, bool? isILACompleted)
        {
            ILAId = ilaId;
            ILANumber = ilaNumber;
            ILAName = ilaName;
            StartDate = startDate;
            CompletedDate = completedDate;
            Grade = grade;
            Score = score;
            IsILACompleted = isILACompleted;
        }
    }

}
