using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegacyToQtd2Migrator.Extensions;
using QTD2.Domain.Exceptions;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class DocumentsMap : Common.MigrationMap<TblDocumentLink, Document>
    {
        List<TblDocumentLink> _sourceDocumentLinks;
        List<TblLinkedDocument> _sourceDocuments;
        List<TblTask> _sourceTasks;
        List<TblEmployee> _sourceEmployees;
        List<TblPosition> _sourcePositions;
        List<TblClass> _sourceClasses;

        List<DocumentType> _targetDocumentTypes;
        List<Employee> _targetEmployees;
        List<Task> _targetTasks;
        List<Position> _targetPositions;
        List<TaskQualification> _targetTaskQualifications;
        List<ClassSchedule> _targetClassSchedules;

        List<int> usedDocIds = new List<int>();

        public DocumentsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblDocumentLink> getSourceRecords()
        {
            _sourceDocumentLinks = (_source as EMP_DemoContext).TblDocumentLinks.ToList();
            _sourceDocuments = (_source as EMP_DemoContext).TblLinkedDocuments.ToList();
            _sourceTasks = (_source as EMP_DemoContext).TblTasks.Include("Da").ToList();
            _sourceEmployees = (_source as EMP_DemoContext).TblEmployees.ToList();
            _sourceClasses = (_source as EMP_DemoContext).TblClasses.ToList();
            _sourcePositions = (_source as EMP_DemoContext).TblPositions.ToList();

            _targetDocumentTypes = (_target as QTD2.Data.QTDContext).DocumentTypes.ToList();
            _targetTaskQualifications = (_target as QTD2.Data.QTDContext).TaskQualifications.ToList();
            _targetEmployees = (_target as QTD2.Data.QTDContext).Employees.Include("Person").ToList();
            _targetTasks = (_target as QTD2.Data.QTDContext).Tasks.ToList();
            _targetClassSchedules = (_target as QTD2.Data.QTDContext).ClassSchedules.ToList();
            _targetPositions = (_target as QTD2.Data.QTDContext).Positions.ToList();

            return _sourceDocumentLinks;
        }

        protected override Document mapRecord(TblDocumentLink obj)
        {
            if (usedDocIds.Contains(obj.LinkedDocId)) return null;

            usedDocIds.Add(obj.LinkedDocId);

            var sourceDocument = _sourceDocuments.Where(r => r.Ldid == obj.LinkedDocId).First();

            var targetDocumentType = getTargetDocumentType(obj);

            var linkedDatas = getLinkedDataId(obj);

            foreach (var linkedData in linkedDatas)
            {
                updateTarget(new Document()
                {
                    Active = true,
                    DateAdded = sourceDocument.LddateStamp.GetValueOrDefault().ToQtsTime(false),
                    Comments = obj.Comment,
                    Deleted = false,
                    DocumentTypeId = targetDocumentType.Id,
                    FileName = sourceDocument.LdfileName,
                    FilePath = sourceDocument.LdfileName,
                    LinkedDataId = linkedData
                });
            }

            return null;
        }

        private List<int> getLinkedDataId(TblDocumentLink obj)
        {
            switch (obj.DocTypeId)
            {
                //Completed Test (Provider > ILA > Date > Employee)
                case 1:
                    return getEmployee(obj);
                //Task Qualification (Duty Area > Sub Duty Area > Task > Employee)
                case 2:
                    return getTaskQualifications(obj);
                //Completed DIF Survey (Position > Date)
                case 3:
                    return getPosition(obj);
                //Completed Student Evaluation (Provider > ILA > Date)
                case 4:
                    return getStudentEvaluation(obj);
                //Sign-in Sheet (Provider > ILA > Date)
                case 5:
                    return getClassSchedule(obj);
                //Other (Employee > Date)
                case 6:
                    return getEmployee(obj);
                default:
                    throw new QTDServerException("Not found");
            }
        }

        private List<int> getPosition(TblDocumentLink obj)
        {
            var docLinks = _sourceDocumentLinks.Where(r => r.LinkedDocId == obj.LinkedDocId);
            var sourcePositionId = docLinks.Where(r => r.TypeId == 4).First();
            var sourcePosition = _sourcePositions.Where(r => r.Pid == sourcePositionId.LinkItemId).FirstOrDefault();

            if (sourcePosition == null) return new List<int>();

            var targetPosition = _targetPositions.FindPosition(sourcePosition);

            return new List<int>() { targetPosition.Id };
        }

        private List<int> getStudentEvaluation(TblDocumentLink obj)
        {
            var docLinks = _sourceDocumentLinks.Where(r => r.LinkedDocId == obj.LinkedDocId);
            var sourceClassId = docLinks.Where(r => r.TypeId == 5).First();
            var sourceClass = _sourceClasses.Where(r => r.Clid == sourceClassId.LinkItemId).First();

            var targetEmployee = _targetClassSchedules.FindClassSchedule(sourceClass);

            return new List<int>() { targetEmployee.Id };
        }

        private List<int> getEmployee(TblDocumentLink obj)
        {
            var docLinks = _sourceDocumentLinks.Where(r => r.LinkedDocId == obj.LinkedDocId);
            var sourceEmployeeId = docLinks.Where(r => r.TypeId == 1).First();
            var sourceEmployee = _sourceEmployees.Where(r => r.Eid == sourceEmployeeId.LinkItemId).FirstOrDefault();

            if (sourceEmployee == null) return new List<int>();

            var targetEmployee = _targetEmployees.FindEmployee(sourceEmployee);

            return new List<int>() { targetEmployee.Id };
        }

        private List<int> getClassSchedule(TblDocumentLink obj)
        {
            var docLinks = _sourceDocumentLinks.Where(r => r.LinkedDocId == obj.LinkedDocId);
            var sourceClassId = docLinks.Where(r => r.TypeId == 5).First();
            var sourceClass = _sourceClasses.Where(r => r.Clid == sourceClassId.LinkItemId).First();

            var targetClass = _targetClassSchedules.FindClassSchedule(sourceClass);

            return new List<int>() { targetClass.Id };
        }

        private List<int> getTaskQualifications(TblDocumentLink obj)
        {
            var docLinks = _sourceDocumentLinks.Where(r => r.LinkedDocId == obj.LinkedDocId);

            var sourceTaskId = docLinks.Where(r => r.TypeId == 3).First();
            var sourceEmployeeId = docLinks.Where(r => r.TypeId == 1).First();

            var sourceTask = _sourceTasks.Where(r => r.Tid == sourceTaskId.LinkItemId).First();
            var sourceEmployee = _sourceEmployees.Where(r => r.Eid == sourceEmployeeId.LinkItemId).FirstOrDefault();

            if (sourceEmployee == null) return new List<int>();

            var targetTask = _targetTasks.FindTask(sourceTask, sourceTask.Da, (_target as QTD2.Data.QTDContext));
            var targetEmployee = _targetEmployees.FindEmployee(sourceEmployee);

            var targetTaskQualifications = _targetTaskQualifications.Where(r => r.EmpId == targetEmployee.Id).Where(r => r.TaskId == targetTask.Id);

            return targetTaskQualifications.Select(r => r.Id).ToList();
        }

        private DocumentType getTargetDocumentType(TblDocumentLink obj)
        {
            switch (obj.DocTypeId)
            {
                case 1:
                    return _targetDocumentTypes.Where(r => r.Id == 9).First();
                case 2:
                    return _targetDocumentTypes.Where(r => r.Id == 8).First();
                case 3:
                    return _targetDocumentTypes.Where(r => r.Id == 3).First();
                case 4:
                    return _targetDocumentTypes.Where(r => r.Id == 7).First();
                case 5:
                    return _targetDocumentTypes.Where(r => r.Id == 6).First();
                case 6:
                    return _targetDocumentTypes.Where(r => r.Id == 5).First();
                default:
                    throw new QTDServerException("Not found");

            }
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _sourceDocumentLinks.Count();
        }

        protected override void updateTarget(Document record)
        {
            if (record != null)
                (_target as QTD2.Data.QTDContext).Documents.Add(record);
        }
    }
}
