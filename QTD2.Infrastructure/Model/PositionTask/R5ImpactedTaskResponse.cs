using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.PositionTask
{
	public class R5ImpactedTaskResponse
	{
		public int Id { get; set; }
		public bool Active { get; set; }
		public int PositionTaskId { get; set; }
		public int ImpactedTaskId { get; set; }
		public string ImpactedTaskDescription { get; set; }
		public string ImpactedTaskFullNumber { get; set; }
	}
}
