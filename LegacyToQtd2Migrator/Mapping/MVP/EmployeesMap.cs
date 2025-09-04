using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class EmployeesMap : Common.MigrationMap<TblEmployee, Employee>
    {
        List<TblEmployee> _employees;
        List<LkTblCertificationType> _nercCertTypes;
        List<TblAddlCertsInfo> _additionalCerts;
        List<TblCertificationHistory> _certificationHistories;
        List<LktblAnnualTrainingRequirement> _regionalCertTypes;
        List<TblEmployeeAdditionalPosition> _employeePositions;
        List<TblEmployeePositionHistory> _employeePositionHistories;
        List<TblPosition> _positions;
        List<RsTblEmployeesTask> _employeetasks;
        List<TblQtsmgrOrganization> _managers;
        List<TblLabelReplacementText> _labelReplacements;
        List<TblPositionTrainingProgram> _trainingPrograms;
        List<TblEmployeeStatusRecord> _employeeStatusRecords;

        List<Certification> _certifications;

        public EmployeesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblEmployee> getSourceRecords()
        {
            _employees = (_source as EMP_DemoContext).TblEmployees.Include("OidNavigation").ToList();
            _nercCertTypes = (_source as EMP_DemoContext).LkTblCertificationTypes.ToList();
            _additionalCerts = (_source as EMP_DemoContext).TblAddlCertsInfos.ToList();
            _regionalCertTypes = (_source as EMP_DemoContext).LktblAnnualTrainingRequirements.ToList();
            _positions = (_source as EMP_DemoContext).TblPositions.ToList();
            _employeePositions = (_source as EMP_DemoContext).TblEmployeeAdditionalPositions.ToList();
            _employeetasks = (_source as EMP_DemoContext).RsTblEmployeesTasks.Where(r => r.TidNavigation.TsubNum == 0).ToList();
            _managers = (_source as EMP_DemoContext).TblQtsmgrOrganizations.ToList();
            _employeePositionHistories = (_source as EMP_DemoContext).TblEmployeePositionHistories.ToList();
            _certificationHistories = (_source as EMP_DemoContext).TblCertificationHistories.ToList();
            _labelReplacements = (_source as EMP_DemoContext).TblLabelReplacementTexts.ToList();
            _trainingPrograms = (_source as EMP_DemoContext).TblPositionTrainingPrograms.ToList();
            _employeeStatusRecords = (_source as EMP_DemoContext).TblEmployeeStatusRecords.ToList();

            _certifications = (_target as QTD2.Data.QTDContext).Certifications.ToList();

            return _employees;
        }

        protected override Employee mapRecord(TblEmployee obj)
        {
            int zipInt = 0;

            return new Employee()
            {
                Person = getPerson(obj),
                EmployeeNumber = obj.Enum,
                Address = obj.Estreet1 + " " + obj.Estreet2,
                InactiveDate = obj.EexpirationDate.HasValue ? DateOnly.FromDateTime(obj.EexpirationDate.Value) : DateOnly.FromDateTime(obj.EexpirationDate.GetValueOrDefault()),
                City = obj.Ecity,
                State = obj.Estate,
                ZipCode = obj.Ezip,
                PhoneNumber = obj.Ephone,
                WorkLocation = obj.EworkLoc,
                Notes = obj.Note1,
                Deleted = false,
                Active = !obj.InActive,
                // = obj.Qtsmanager,
                TQEqulator = obj.IsTaskEvaluator,
                EmployeeOrganizations = getEmployeeOrganizations(obj),

                //EmployeeCertifictaionHistorys = getEmployeeCertifictaionHistorys(obj),    //commenting this as foreign key is removed from the entity
                EmployeeCertifications = getEmployeeCertifications(obj),
                Employee_Tasks = getEmployee_Tasks(obj),
                EmployeeHistorys = getEmployeeHistorys(obj),
                EmployeePositions = getEmployeePositions(obj)
            };
        }

        private ICollection<EmployeePosition> getEmployeePositions(TblEmployee obj)
        {
            List<EmployeePosition> eps = new List<EmployeePosition>();

            var sourceEps = _employeePositions.Where(r => r.Eid == obj.Eid);

            foreach (var sourceEp in sourceEps)
            {
                var sourcePosition = _positions.Where(r => r.Pid == sourceEp.Pid).FirstOrDefault();
                if (sourcePosition == null) continue;

                var targetPosition = (_target as QTD2.Data.QTDContext).Positions.Where(r => r.PositionAbbreviation == sourcePosition.Pabbrev).First();

                bool willNotBeCertifed = obj.EnotCertified.GetValueOrDefault();
                bool willNotBeRecertied = obj.EwillNotBeRecertified.GetValueOrDefault();

                var trainingProgram = sourceEp.TrainingProgram.HasValue ? _trainingPrograms.Where(r => r.Ptpid == sourceEp.TrainingProgram).FirstOrDefault() : null;

                eps.Add(new EmployeePosition()
                {
                    Active = !sourceEp.EndDate.HasValue,
                    EndDate = sourceEp.EndDate.HasValue ? DateOnly.FromDateTime(sourceEp.EndDate.Value) : null,
                    IsCertificationNotRequired = willNotBeCertifed || willNotBeRecertied,
                    PositionId = targetPosition.Id,
                    QualificationDate = sourceEp.QualificationDate.HasValue ? DateOnly.FromDateTime(sourceEp.QualificationDate.Value) : null,
                    StartDate = sourceEp.StartDate.HasValue ? DateOnly.FromDateTime(sourceEp.StartDate.Value) : new DateOnly(1900, 1, 1),
                    Trainee = sourceEp.Trainee.GetValueOrDefault(),
                    TrainingProgramVersion = trainingProgram != null ? trainingProgram.Revision.ToString() : "N/A"
                });
            }

            foreach (var history in _employeePositionHistories.Where(r => r.Eid == obj.Eid))
            {
                var sourcePosition = _positions.Where(r => r.Pid == history.Pid).FirstOrDefault();

                if (sourcePosition == null) continue;

                var targetPosition = (_target as QTD2.Data.QTDContext).Positions.Where(r => r.PositionAbbreviation == sourcePosition.Pabbrev).First();

                eps.Add(new EmployeePosition()
                {
                    Active = false,
                    EndDate = history.EndDate.HasValue ? DateOnly.FromDateTime(history.EndDate.Value) : null,
                    PositionId = targetPosition.Id,
                    QualificationDate = history.QualificationDate.HasValue ? DateOnly.FromDateTime(history.QualificationDate.Value) : null,
                    StartDate = history.StartDate.HasValue ? DateOnly.FromDateTime(history.StartDate.Value) : new DateOnly(1900, 1, 1),
                    Trainee = history.Trainee.GetValueOrDefault(),
                    TrainingProgramVersion = history.TrainingProgram.HasValue ? history.TrainingProgram.Value.ToString() : "N/A"
                });
            }

            return eps;
        }

        private Person getPerson(TblEmployee obj)
        {
            string username = (obj.UserName ?? "").Contains("@") ? obj.UserName : "";

            return new Person()
            {
                FirstName = obj.EfirstName,
                LastName = obj.ElastName,
                MiddleName = obj.EmiddleInitial,
                Username = String.IsNullOrEmpty(username) ? (obj.Eemail ?? "") : username,
                ClientUser = new ClientUser()
                {
                    Active = true
                }
            };
        }

        private ICollection<EmployeeCertifictaionHistory> getEmployeeCertifictaionHistorys(TblEmployee obj)
        {
            List<EmployeeCertifictaionHistory> employeeCertifictaionHistories = new List<EmployeeCertifictaionHistory>();

            var certificationHistories = _certificationHistories.Where(r => r.Eid == obj.Eid).ToList();

            foreach (var history in certificationHistories)
            {
                var empCert = _nercCertTypes.Where(r => r.CertId == history.ChNerccertArea.Value).FirstOrDefault();

                if (empCert == null) continue;

                var employeeTableCertification = empCert == null ? null : _certifications.Where(r => r.CertAcronym == empCert.CertAbbrev).First();

                employeeCertifictaionHistories.Add(new EmployeeCertifictaionHistory()
                {
                    //NewCertificationID = employeeTableCertification.Id,       //commenting this as foreign key is removed from the entity
                    //OldCertificationID = employeeTableCertification.Id,
                    ChangeEffectiveDate = DateOnly.FromDateTime(history.ChNerccertIssueDate.GetValueOrDefault()),
                    DRADate = DateOnly.FromDateTime(history.ChDra.GetValueOrDefault()),
                    ExpirationDate = DateOnly.FromDateTime(history.ChNerccertExpDate.GetValueOrDefault()),
                    IssueDate = DateOnly.FromDateTime(history.ChNerccertIssueDate.GetValueOrDefault()),
                    //ChangeNotes = history.note
                    //ChangeNotes
                    Deleted = false,
                    Active = true
                });
            }

            //if (obj.NerccertAreaExisting.HasValue)
            //{
            //    var empCert = _nercCertTypes.Where(r => r.CertId == obj.NerccertAreaExisting.Value).FirstOrDefault();
            //    var employeeTableCertification = empCert == null ? null : _certifications.Where(r => r.CertAcronym == empCert.CertAbbrev).First();

            //    var expirationDate = obj.NerccertExpDate.HasValue ? obj.NerccertExpDate.Value.ToQtsTime(true) : obj.NerccertExpDate.ToQtsTime(true);


            //    if (expirationDate.HasValue && employeeTableCertification != null)
            //    {
            //        employeeCertifictaionHistories.Add(new EmployeeCertifictaionHistory()
            //        {
            //            NewCertificationID = employeeTableCertification.Id,
            //            OldCertificationID = employeeTableCertification.Id,
            //            ChangeEffectiveDate = obj.NerccertIssueDate.GetValueOrDefault().ToQtsTime(true),
            //            DRADate = obj.IssueDate.GetValueOrDefault().ToQtsTime(true),
            //            ExpirationDate = expirationDate.Value,
            //            IssueDate = obj.NerccertIssueDate.GetValueOrDefault().ToQtsTime(true),
            //            //ChangeNotes = history.note
            //            //ChangeNotes
            //            Deleted = false,
            //            Active = true
            //        });
            //    }
            //}

            return employeeCertifictaionHistories;
        }

        private ICollection<EmployeeCertification> getEmployeeCertifications(TblEmployee obj)
        {
            List<EmployeeCertification> employeeCertifications = new List<EmployeeCertification>();

            if (obj.NerccertAreaExisting.HasValue)
            {
                var empCert = _nercCertTypes.Where(r => r.CertId == obj.NerccertAreaExisting.Value).FirstOrDefault();
                var empCertHistories = _certificationHistories.Where(r => r.Eid == obj.Eid).Where(r => r.ChNerccertArea == obj.NerccertAreaExisting.Value).ToList();

                if (empCert != null)
                {
                    var employeeTableCertification = empCert == null ? null : _certifications.Where(r => r.CertAcronym == empCert.CertAbbrev).First();
                    employeeCertifications.Add(new EmployeeCertification()
                    {
                        CertificationId = employeeTableCertification.Id,
                        IssueDate = DateOnly.FromDateTime(obj.IssueDate ?? DateTime.Today),
                        ExpirationDate = obj.NerccertExpDate.HasValue ? DateOnly.FromDateTime(obj.NerccertExpDate.Value) : null,
                        RenewalDate = obj.NerccertIssueDate.HasValue ? DateOnly.FromDateTime(obj.NerccertIssueDate.Value) : null,
                        //RollOverHours,
                        CertificationNumber = obj.NerccertNum,
                        Deleted = false,
                        Active = (obj.NerccertExpDate.HasValue ? obj.NerccertExpDate.Value.ToQtsTime(true) : obj.NerccertExpDate.ToQtsTime(true)) > DateTime.UtcNow
                    });
                }

                foreach (var certHistory in _certificationHistories.Where(r => r.Eid == obj.Eid))
                {
                    var certificationType = _nercCertTypes.Where(r => r.CertId == certHistory.ChNerccertArea).FirstOrDefault();

                    if (certificationType == null)
                        continue;

                    var targetCertification = empCert == null ? null : _certifications.Where(r => r.CertAcronym == certificationType.CertAbbrev).First();

                    if (employeeCertifications.Where(r => r.CertificationId == targetCertification.Id).Count() > 0)
                        continue;

                    employeeCertifications.Add(new EmployeeCertification()
                    {
                        CertificationId = _certifications.Where(r => r.CertAcronym.ToUpper() == certificationType.CertAbbrev.ToUpper()).First().Id,
                        IssueDate = DateOnly.FromDateTime(certHistory.ChNerccertIssueDate ?? DateTime.Today),
                        ExpirationDate = certHistory.ChNerccertExpDate.HasValue ? DateOnly.FromDateTime(certHistory.ChNerccertExpDate.Value) : null,
                        RenewalDate = certHistory.ChNerccertIssueDate.HasValue ? DateOnly.FromDateTime(certHistory.ChNerccertIssueDate.Value) : null,
                        //RollOverHours,
                        CertificationNumber = certHistory.ChNerccertNum,
                        Deleted = false,
                        Active = (certHistory.ChNerccertExpDate.HasValue ? certHistory.ChNerccertExpDate.Value : new DateTime(1900, 1, 1)) < DateTime.Now
                    });
                }
            }

            string legacyRtName = getLegacyName("Reg");
            var rtLabelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == legacyRtName.ToUpper()).FirstOrDefault();

            string rtName = rtLabelreplacement == null ? "Reg" : rtLabelreplacement.ReplacementText;

            if (obj.RegCertNum != null)
            {
                employeeCertifications.Add(new EmployeeCertification()
                {
                    CertificationId = _certifications.Where(r => r.CertAcronym == rtName).First().Id,
                    IssueDate = DateOnly.FromDateTime(obj.RegCertIssueDate ?? DateTime.Today),
                    ExpirationDate = obj.RegCertExpDate.HasValue ? DateOnly.FromDateTime(obj.RegCertExpDate.Value) : null,
                    RenewalDate = obj.RegCertIssueDate.HasValue ? DateOnly.FromDateTime(obj.RegCertIssueDate.Value) : null,
                    //RollOverHours,
                    CertificationNumber = obj.RegCertNum,
                    Deleted = false,
                    Active = !obj.RegCertExpDate.HasValue || obj.RegCertExpDate.Value.ToQtsTime(true) > DateTime.Now
                });
            }

            string legacyRt2Name = getLegacyName("Reg2");
            var rt2Labelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == legacyRt2Name.ToUpper()).FirstOrDefault();

            string r2tName = rt2Labelreplacement == null ? "Reg2" : rt2Labelreplacement.ReplacementText;

            if (obj.RegCertNum2 != null)
            {
                employeeCertifications.Add(new EmployeeCertification()
                {
                    CertificationId = _certifications.Where(r => r.CertAcronym == r2tName).First().Id,
                    IssueDate = DateOnly.FromDateTime(obj.RegCertIssueDate2 ?? DateTime.Today),
                    ExpirationDate = obj.RegCertExpDate2.HasValue ? DateOnly.FromDateTime(obj.RegCertExpDate2.Value) : null,
                    RenewalDate = obj.RegCertIssueDate2.HasValue ? DateOnly.FromDateTime(obj.RegCertIssueDate2.Value) : null,
                    //RollOverHours,
                    CertificationNumber = obj.RegCertNum2,
                    Deleted = false,
                    Active = obj.RegCertIssueDate2.GetValueOrDefault().ToQtsTime(true) > DateTime.UtcNow
                });
            }

            foreach (var addtCert in _additionalCerts.Where(r => r.Eid == obj.Eid))
            {
                var trainingType = _regionalCertTypes.Where(r => r.TrainingTypeId == addtCert.TrainingTypeId).First();

                string legacyName = getLegacyName(trainingType.TrainingType);
                var labelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == legacyName.ToUpper()).FirstOrDefault();

                string name = labelreplacement == null ? legacyName : labelreplacement.ReplacementText;

                employeeCertifications.Add(new EmployeeCertification()
                {
                    CertificationId = _certifications.Where(r => r.CertAcronym == trainingType.TrainingType).First().Id,
                    IssueDate = DateOnly.FromDateTime(addtCert.AddlCertIssueDate ?? DateTime.Today),
                    ExpirationDate = addtCert.AddlCertExpDate.HasValue ? DateOnly.FromDateTime(addtCert.AddlCertExpDate.Value) : null,
                    RenewalDate = addtCert.AddlCertIssueDate.HasValue ? DateOnly.FromDateTime(addtCert.AddlCertIssueDate.Value) : null,
                    //RollOverHours,
                    CertificationNumber = addtCert.AddlCertNum,
                    Deleted = false,
                    Active = !addtCert.AddlCertExpDate.HasValue || addtCert.AddlCertExpDate.Value > DateTime.Now
                });
            }

            foreach (var employeeCertification in employeeCertifications)
            {
                var issueDate = employeeCertifications
                        .Where(r => r.CertificationNumber == employeeCertification.CertificationNumber)
                        .Where(r => r.CertificationId == employeeCertification.CertificationId)
                        .Min(r => r.IssueDate);
                employeeCertification.IssueDate = issueDate;
            }

            return employeeCertifications;
        }

        private string getLegacyName(string trainingType)
        {
            switch (trainingType.ToUpper())
            {
                case "EMERGENCYRESPONCE":
                    return "Emergency Response";
                case "REGIONALTRAINING":
                    return "Regional Training";
                case "REGIONALTRAINING2":
                    return "Regional Training 2";
                default: return trainingType;
            }
        }

        private ICollection<Employee_Task> getEmployee_Tasks(TblEmployee obj)
        {
            List<Employee_Task> employee_Tasks = new List<Employee_Task>();

            foreach (var employeeTask in _employeetasks.Where(r => r.Eid == obj.Eid))
            {
                var sourceTask = employeeTask.TidNavigation;
                var sourceDutyArea = employeeTask.TidNavigation.Da;
                var targetTask = (_target as QTD2.Data.QTDContext)
                    .DutyAreas
                    .Where(r => r.Number == sourceDutyArea.Danum)
                    .Where(r => r.Letter == sourceDutyArea.Daletter).First()
                    .SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
                    .Tasks.Where(r => r.Number == sourceTask.Tnum).First();


                employee_Tasks.Add(new Employee_Task()
                {
                    //EmployeeId,
                    TaskId = targetTask.Id,
                    //MajorVersion,
                    //MinorVersion,
                    //Archived
                    Deleted = false,
                    Active = true
                });
            }

            return employee_Tasks;
        }

        private ICollection<EmployeeHistory> getEmployeeHistorys(TblEmployee obj)
        {
            List<EmployeeHistory> employeeHistories = new List<EmployeeHistory>();

            var statusRecords = _employeeStatusRecords.Where(r => r.Eid == obj.Eid);

            foreach (var statusRecord in statusRecords)
            {
                employeeHistories.Add(new EmployeeHistory()
                {
                    CurrentActiveStatus = statusRecord.NewStatus.GetValueOrDefault(),
                    ChangeEffectiveDate = statusRecord.EffectiveDate.GetValueOrDefault(),
                    ChangeNotes = statusRecord.Comments,
                    OperationType = statusRecord.NewStatus.GetValueOrDefault() ? EmployeeHistoryOperationType.Activate : EmployeeHistoryOperationType.Deactivate,
                    Deleted = false,
                    Active = true
                });
            }

            return employeeHistories;
        }

        private ICollection<EmployeeOrganization> getEmployeeOrganizations(TblEmployee obj)
        {
            List<EmployeeOrganization> employeeOrganizations = new List<EmployeeOrganization>();

            foreach (var managerRecord in obj.TblQtsmgrOrganizations)
            {
                var loopTargetOrg = (_target as QTD2.Data.QTDContext).Organizations.Where(r => r.Name == managerRecord.Org.Oname).First();

                employeeOrganizations.Add(new EmployeeOrganization()
                {
                    IsManager = true,
                    OrganizationId = loopTargetOrg.Id
                });
            }

            if (obj.OidNavigation != null && obj.TblQtsmgrOrganizations.Where(r => r.OrgId == obj.Oid).Count() == 0)
            {
                var empTargetOrg = (_target as QTD2.Data.QTDContext).Organizations.Where(r => r.Name == obj.OidNavigation.Oname).First();
                if (employeeOrganizations.Where(r => r.Id == empTargetOrg.Id).Count() == 0)
                {
                    employeeOrganizations.Add(new EmployeeOrganization()
                    {
                        IsManager = false,
                        OrganizationId = empTargetOrg.Id,
                        Deleted = false,
                        Active = true
                    });
                }
            }


            return employeeOrganizations;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _employees.Count();
        }

        protected override void updateTarget(Employee record)
        {
            (_target as QTD2.Data.QTDContext).Employees.Add(record);
        }
    }
}
