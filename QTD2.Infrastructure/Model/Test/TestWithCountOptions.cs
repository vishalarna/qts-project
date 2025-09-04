using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Test
{
    public class TestWithCountOptions
    {
        public string TestNum { get; set; }

        public string TestTitle { get; set; }

        public string NumberOfQuestions { get; set; }

        public string TestType { get; set; }

        public string TestStatus { get; set; }

        public bool Active { get; set; }

        public int Id { get; set; }

        public bool isPublished { get; set; }

        public bool isReleased { get; set; }
    }
}
