using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.PositionHistory;

namespace QTD2.Infrastructure.Model.PositionTask
{
	public class UpdateMarkAsR6Model
	{
		public List<int> PositionTaskIds{ get; set; }
		public Position_HistoryCreateOptions Position_HistoryCreateOptions { get; set; }

	}
}
