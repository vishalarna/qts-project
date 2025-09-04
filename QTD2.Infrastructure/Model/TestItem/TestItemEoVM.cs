using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItem
{
    public class TestItemEoVM
    {
        public int Id { get; set; }
        public int EoId { get; set; }
        public int TestItemTypeId { get; set; }
        public string TaxonomyDescription { get; set; }
        public string TestItemtypeDescription { get; set; }
        public string QuestionDescription { get; set; }
        public string Number { get; set; }
        public int TaxonomyId { get; set; }
        public string EoNumber { get; set; }
        public string EoDescription { get; set; }
    }
}
