using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TreeDataVMs
{
	public class TreeItemOptionViewModel
	{
		public int Id { get; set; }
		public string Display { get; set; }
		public TreeItemViewModel SubTreeItem { get; set; }
	}
}
