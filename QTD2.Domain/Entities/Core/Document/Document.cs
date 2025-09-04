using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
	public class Document : Entity
	{
		public string FileName { get; set; }
		public string FilePath { get; set; }
		public DateTime DateAdded { get; set; }
		public string? Comments { get; set; }
		public int DocumentTypeId { get; set; }
		public virtual DocumentType DocumentType { get; set; }
		public int LinkedDataId { get; set; }

		public Document()
		{

		}

		public Document(string fileName, string filePath, DateTime dateAdded, int documentTypeId, int linkedDataId, string? comments)
		{
			FileName = fileName;
			FilePath = filePath;
			DateAdded = dateAdded;
			DocumentTypeId = documentTypeId;
			LinkedDataId = linkedDataId;
			Comments = comments;
		}

		public void SetDocumentTypeId(int documentTypeId)
		{
			DocumentTypeId = documentTypeId;
		}

		public void SetLinkedDataId(int linkedDataId)
		{
			LinkedDataId = linkedDataId;
		}

		public void SetComments(string comments)
		{
			Comments = comments;
		}
	}
}
