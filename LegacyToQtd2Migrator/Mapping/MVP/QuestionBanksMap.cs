using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
  public  class QuestionBanksMap : Common.MigrationMap<TblFormQuestion, QuestionBank>
    {
        List<TblFormQuestion> _tblFormQuestions;
        public QuestionBanksMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblFormQuestion> getSourceRecords()
        {
            _tblFormQuestions = (_source as EMP_DemoContext).TblFormQuestions.ToList();
            return _tblFormQuestions;
        }
        protected override QuestionBank mapRecord(TblFormQuestion obj)
        {
            return new QuestionBank()
            {
                Stem = (obj.Fqdesc ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(obj.Fqdesc ?? "") : obj.Fqdesc,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Deleted = false,
                Active = !obj.Fqinactive,

            };
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblFormQuestions.Count();
        }
        protected override void updateTarget(QuestionBank record)
        {
            (_target as QTD2.Data.QTDContext).QuestionBanks.Add(record);
        }

    }
}
