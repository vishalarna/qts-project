using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class RatingScalesNMap : Common.MigrationMap<LktblRatingScale, RatingScaleN>
    {
        List<LktblRatingScale> _ratingscale;
        public RatingScalesNMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<LktblRatingScale> getSourceRecords()
        {
            _ratingscale = (_source as EMP_DemoContext).LktblRatingScales.Where(r => !string.IsNullOrEmpty(r.Rsinstruction)).ToList();

            return _ratingscale;
        }

        protected override RatingScaleN mapRecord(LktblRatingScale obj)
        {
            return new RatingScaleN()
            {
                RatingScaleDescription = obj.Rsinstruction,
                RatingScaleExpanded = getRatingScaleExpanded(obj)
            };
        }

        private ICollection<RatingScaleExpanded> getRatingScaleExpanded(LktblRatingScale obj)
        {
            List<RatingScaleExpanded> ratingScaleExpandeds = new List<RatingScaleExpanded>();

            var vals = obj.Rsinstruction.Split(';');

            foreach (var val in vals)
            {
                var parts = val.Split("=");

                int rating;
                bool isRating = int.TryParse(parts[0], out rating);

                ratingScaleExpandeds.Add(new RatingScaleExpanded()
                {
                    Description = parts.Length > 1 ? parts[1].Trim() : parts[0].Trim(),
                    Ratings = isRating ? rating : 0
                });
            }

            return ratingScaleExpandeds;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _ratingscale.Count();
        }

        protected override void updateTarget(RatingScaleN record)
        {
            var rsn = (_target as QTD2.Data.QTDContext).RatingScaleNs.Where(r => r.RatingScaleDescription == record.RatingScaleDescription).FirstOrDefault();

            if (rsn == null)
                (_target as QTD2.Data.QTDContext).RatingScaleNs.Add(record);
        }

    }
}
