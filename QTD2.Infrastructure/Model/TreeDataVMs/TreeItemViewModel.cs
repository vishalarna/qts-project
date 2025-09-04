using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TreeDataVMs
{
	public class TreeItemViewModel
	{
		public string Label { get; set; }
		public List<TreeItemOptionViewModel> TreeItemOptions { get; set; } = new List<TreeItemOptionViewModel>();
		public bool Searchable { get; set; }
	}
}
