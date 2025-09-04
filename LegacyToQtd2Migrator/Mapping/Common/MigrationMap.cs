using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using QTD2.Domain.Exceptions;

namespace LegacyToQtd2Migrator.Mapping.Common
{
    public class MigrationMap<S, T> : IMigrationMap
        where S : class
        where T : class
    {
        public int TotalRecordsToConvert { get; set; }
        public int RecordsConverted { get; set; }

        protected DbContext _source;
        protected DbContext _target;

        public MigrationMap(DbContext source, DbContext target)
        {
            _source = source;
            _target = target;
        }

        public void ConvertRecords()
        {
            var sourceRecords = getSourceRecords();
            setTotalRecordsToConvert();

            foreach (var sourceRecord in sourceRecords)
            {
                T targetRecord = mapRecord(sourceRecord);
                updateTarget(targetRecord);
                updateRecordsConverted();
            }

            try
            {
                _target.SaveChanges();
            }
            catch (Exception ex)
            {
                foreach (var entityEntry in _target.ChangeTracker.Entries().Where(et => et.State != EntityState.Unchanged))
                {
                    foreach (var entry in entityEntry.CurrentValues.Properties)
                    {
                        var prop = entityEntry.Property(entry.Name).Metadata;
                        var value = entry.PropertyInfo?.GetValue(entityEntry.Entity);
                        var valueLength = value?.ToString()?.Length;
                        var typemapping = prop.GetTypeMapping();
                        var typeSize = ((Microsoft.EntityFrameworkCore.Storage.RelationalTypeMapping)typemapping).Size;
                        if (typeSize.HasValue && valueLength > typeSize.Value)
                        {
                            string s = "";
                        }
                    }
                }
                throw ex;
            }
        }

        protected virtual void setTotalRecordsToConvert()
        {
            throw new NotImplementedException();
        }

        protected void updateRecordsConverted()
        {
            RecordsConverted++;
        }

        protected virtual void updateTarget(T record)
        {
            throw new NotImplementedException();
        }

        protected virtual T mapRecord(S obj)
        {
            throw new NotImplementedException();
        }

        protected virtual System.Collections.Generic.List<S> getSourceRecords()
        {
            throw new NotImplementedException();
        }

        protected string getQtd2TestItemTypeName(string testItemTypeName)
        {
            switch (testItemTypeName)
            {
                case "Fill-in-the-blank":
                    return "Fill in the Blank";

                case "Short Answer":
                    return "Short Answers";

                case "Matching":
                    return "Match the Column";

                case "True/False":
                    return "True / False";

                case "Multiple Choice":
                    return "Multiple Choice Questions";

                default: throw new QTDServerException("No test item type found");
            }
        }

        protected string getQtd2TestItemTypeName(int sourceTestItemType)
        {
            switch (sourceTestItemType)
            {
                case 6:
                    return "Fill in the Blank";

                case 3:
                    return "Short Answers";

                case 5:
                    return "Match the Column";

                case 7:
                    return "True / False";

                case 1:
                    return "Multiple Choice Questions";

                //not used
                case 2:
                    return "Scenario";

                case 4:
                    return "Short Answers";

                case 0:
                    return "None";

                default: throw new QTDServerException("No test item type found");
            }
        }
    }
}