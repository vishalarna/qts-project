using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.QTD2ScormContext;

namespace LegacyToQtd2Migrator.Mapping.Scorm
{
    public class ScormLearnerMap : Common.MigrationMap<ScormApiLearner, ScormApiLearner>
    {
        List<ScormApiLearner> _scormApiLearners;

        public ScormLearnerMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<ScormApiLearner> getSourceRecords()
        {
            _scormApiLearners = (_target as MigrationTestContext).ScormApiLearners.ToList();
            return _scormApiLearners;
        }

        protected override ScormApiLearner mapRecord(ScormApiLearner obj)
        {
            int apiLearnerId = Convert.ToInt32(obj.ApiLearnerId);

            var sourceEmployee = (_source as Legacy.Data.EMP_DemoContext).TblEmployees.Where(r => r.Eid == apiLearnerId).First();
            var targetEmployee = (_target as MigrationTestContext).Employees.Where(r => r.Person.FirstName == sourceEmployee.EfirstName).Where(r => r.Person.LastName == sourceEmployee.ElastName).Include("Person").First();

            obj.FirstName = targetEmployee.Person.FirstName;
            obj.LastName = targetEmployee.Person.LastName;
            obj.ApiLearnerId = targetEmployee.Id.ToString();

            foreach (var link in obj.ScormApiRegToLearners)
            {
                link.ApiLearnerId = targetEmployee.Id.ToString();
            }

            return obj;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _scormApiLearners.Count();
        }

        protected override void updateTarget(ScormApiLearner record)
        {
            (_target as MigrationTestContext).ScormApiLearners.Update(record);
        }
    }
}
