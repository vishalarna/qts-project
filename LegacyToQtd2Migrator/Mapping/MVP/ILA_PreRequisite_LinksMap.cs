using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class ILA_PreRequisite_LinksMap : Common.MigrationMap<TblCourse, List<ILA_PreRequisite_Link>>
    {
        List<TblCourse> _courses;
        List<ILA> _ilas;

        public ILA_PreRequisite_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblCourse> getSourceRecords()
        {
            _courses = (_source as EMP_DemoContext).TblCourses.Where(r => !string.IsNullOrEmpty(r.Prerequisites)).ToList();
            _ilas = (_target as QTD2.Data.QTDContext).ILAs.ToList();

            return _courses;
        }

        protected override List<ILA_PreRequisite_Link> mapRecord(TblCourse obj)
        {
            List<ILA_PreRequisite_Link> links = new List<ILA_PreRequisite_Link>();

            List<ILA> usedIlas = new List<ILA>();

            string prereqs = obj.Prerequisites;

            var targetIla = _ilas.Where(r => r.Number == obj.Cornum).First();

            foreach (var ila in _ilas.OrderByDescending(r => r.Number.Length))
            {
                if (string.IsNullOrEmpty(prereqs)) break;

                string val = "";

                if (prereqs.IndexOf(ila.Number) >= 0)
                {
                    val = ila.Number;
                }
                if (prereqs.IndexOf(ila.Description) >= 0)
                {
                    if (ila.Description.Length > ila.Number.Length)
                    {
                        val = ila.Description;
                    }
                }

                if (string.IsNullOrEmpty(val)) continue;

                var targetPrereq = _ilas.Where(r => r.Number == val || r.Description == val).First();

                if (usedIlas.Contains(targetPrereq)) continue;

                usedIlas.Add(targetPrereq);

                links.Add(new ILA_PreRequisite_Link()
                {
                    ILAId = targetIla.Id,
                    PreRequisiteId = targetPrereq.Id,
                    //CreatedBy
                    //CreatedDate,
                    //ModifiedBy,
                    //ModifiedDate
                    Deleted = false,
                    Active = true
                });

                if(ila.Description.Length > ila.Number.Length)
                {
                    prereqs = prereqs.Replace(ila.Description, "").Replace(ila.Number, "").Trim();
                }
                else
                {
                    prereqs = prereqs.Replace(ila.Number, "").Replace(ila.Description, "").Trim();
                }
               
            }

            return links;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _courses.Count();
        }

        protected override void updateTarget(List<ILA_PreRequisite_Link> record)
        {
            (_target as QTD2.Data.QTDContext).ILA_PreRequisite_Links.AddRange(record);
        }
    }
}
