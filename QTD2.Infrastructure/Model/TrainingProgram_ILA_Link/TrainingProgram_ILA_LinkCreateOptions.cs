using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingProgram_ILA_Link
{
    public class TrainingProgram_ILA_LinkCreateOptions
    {
        public int TrainingProgramId { get; set; }

        public int[] ILAIds { get; set; }
    }
}
