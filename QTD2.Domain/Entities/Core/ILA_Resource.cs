using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_Resource : Entity
    {
        public int ILAId { get; set; }
        public int? ResourceNumber { get; set; }

        public string Title { get; set; }
        public string Section { get; set; }
        public string Chapter { get; set; }
        public string Hyperlink { get; set; }
        public string HyperlinkText { get; set; }
        public string Comments { get; set; }

        public virtual ILA ILA { get; set; }

        public ILA_Resource() { }
        public ILA_Resource(int ilaId, string title , string comments)
        {
            ILAId = ilaId;
            Title = title;
            Comments = comments;
        }
    }
}
