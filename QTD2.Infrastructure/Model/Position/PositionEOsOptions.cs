using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Position
{
    public class PositionEOsOptions 
    {
        public QTD2.Domain.Entities.Core.Position Position { get; set; }
        public List<QTD2.Domain.Entities.Core.EnablingObjective> EnablingObjectives { get; set; } = new List<Domain.Entities.Core.EnablingObjective>();

        public PositionEOsOptions(QTD2.Domain.Entities.Core.Position position,List<QTD2.Domain.Entities.Core.EnablingObjective> enablingObjectives)
        {
            Position = position;
            EnablingObjectives = enablingObjectives;
        } 

        public PositionEOsOptions(){}
    }
}
