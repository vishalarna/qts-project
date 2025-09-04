using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class RatingScalesMap : Common.MigrationMap<LktblRatingScale, RatingScale>
    {
        List<LktblRatingScale> _ratingscale;
        public RatingScalesMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<LktblRatingScale> getSourceRecords()
        {
            _ratingscale = (_source as EMP_DemoContext).LktblRatingScales.ToListAsync().Result;
            return _ratingscale;
        }

        protected override RatingScale mapRecord(LktblRatingScale obj)
        {
            var vals = obj.Rsinstruction.Split(';');

            return new RatingScale()
            {
                Position1Text = vals.Length >= 1 ? (vals[0].Split('=').Length > 1 ? vals[0].Split('=')[1].Trim() : vals[0].Split('=')[0].Trim()) : "",
                Position2Text = vals.Length >= 2 ? (vals[1].Split('=').Length > 1 ? vals[1].Split('=')[1].Trim() : vals[1].Split('=')[0].Trim()) : "",
                Position3Text = vals.Length >= 3 ? (vals[2].Split('=').Length > 1 ? vals[2].Split('=')[1].Trim() : vals[2].Split('=')[0].Trim()) : "",
                Position4Text = vals.Length >= 4 ? (vals[3].Split('=').Length > 1 ? vals[3].Split('=')[1].Trim() : vals[3].Split('=')[0].Trim()) : "",
                Position5Text = vals.Length >= 5 ? (vals[4].Split('=').Length > 1 ? vals[4].Split('=')[1].Trim() : vals[4].Split('=')[0].Trim()) : "",
                Deleted = false,
                Active = true,
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _ratingscale.Count();
        }

        protected override void updateTarget(RatingScale record)
        {
            (_target as QTD2.Data.QTDContext).RatingScales.Add(record);
        }

    }
}
