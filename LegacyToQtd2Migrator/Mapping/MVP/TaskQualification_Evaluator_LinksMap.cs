using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TaskQualification_Evaluator_LinksMap : Common.MigrationMap<TblOjtevaluator, TaskQualification_Evaluator_Link>
    {
        List<TblOjtevaluator> _tblOjteEvaluator;

        public TaskQualification_Evaluator_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblOjtevaluator> getSourceRecords()
        {
            _tblOjteEvaluator = (_source as EMP_DemoContext).TblOjtevaluators.ToListAsync().Result;
            return _tblOjteEvaluator;
        }

        protected override TaskQualification_Evaluator_Link mapRecord(TblOjtevaluator obj)
        {
            //var sourceTaskQual = obj.TidNavigation;
            //var sourceEMp = obj.EidNavigation;

            //var targetTq = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceIla.Cornum).First();
            //var targetEmp = (_target as QTD2.Data.QTDContext).Tasks.Where(r => r.Number == sourceTask.Tnum).First();

            return new TaskQualification_Evaluator_Link()
            {
                //Active = true,
                //EvaluatorId = targetEmp.Id,
                //TaskQualificationId = targetTq.Id,
                //Number
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblOjteEvaluator.Count();
        }

        protected override void updateTarget(TaskQualification_Evaluator_Link record)
        {
            (_target as QTD2.Data.QTDContext).TaskQualification_Evaluator_Links.Add(record);
        }
    }
}
