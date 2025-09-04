using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TDTImagesMap : Common.MigrationMap<TblTdtimage, Image>
    {
        List<TblTdtimage> _images;

        public TDTImagesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTdtimage> getSourceRecords()
        {
            _images = (_source as EMP_DemoContext).TblTdtimages.ToList();
            return _images;
        }

        protected override Image mapRecord(TblTdtimage obj)
        {
            return new Image()
            {
                Active = true,
                Body = obj.Image,
                Deleted = false,
                Description = obj.ImageId.ToString()
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _images.Count();
        }

        protected override void updateTarget(Image record)
        {
            (_target as QTD2.Data.QTDContext).Images.Add(record);
        }
    }
}