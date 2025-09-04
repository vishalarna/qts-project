using QTD2.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace QTD2.Domain.Entities.Core
{
    public class Employee : Common.Entity
    {
        public int PersonId { get; set; }

        public string? EmployeeNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNumber { get; set; }

        public string WorkLocation { get; set; }

        public string Notes { get; set; }

        public string Password { get; set; }

        public bool TQEqulator { get; set; }

        //public bool? IsCertificationRequired { get; set; }

        public DateOnly? InactiveDate { get; set; }

        public string? Reason { get; set; }

        public string? IDPReviewInformation { get; set; }
        public bool PublicUser { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<MetaILA_Employee> MetaILA_Employees { get; set; } = new List<MetaILA_Employee>();
        public virtual ICollection<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();
        public virtual ICollection<StudentEvaluationWithoutEmp> StudentEvaluationWithoutEmps { get; set; } = new List<StudentEvaluationWithoutEmp>();

        public virtual ICollection<ClassSchedule_Evaluation_Roster> ClassSchedule_Evaluation_Rosters { get; set; } = new List<ClassSchedule_Evaluation_Roster>();

        public virtual ICollection<EmployeeOrganization> EmployeeOrganizations { get; set; } = new List<EmployeeOrganization>();

        public virtual ICollection<EmployeeCertification> EmployeeCertifications { get; set; } = new List<EmployeeCertification>();

        public virtual ICollection<Employee_Task> Employee_Tasks { get; set; } = new List<Employee_Task>();

        public virtual ICollection<ILACollaborator> ILACollaborators { get; set; } = new List<ILACollaborator>();

        public virtual ICollection<CollaboratorInvitation> CollaboratorInvitations { get; set; } = new List<CollaboratorInvitation>();

        public virtual ICollection<Position_Employee> Position_Employees { get; set; } = new List<Position_Employee>();

        public virtual ICollection<EnablingObjective_Employee_Link> EnablingObjective_Employee_Links { get; set; } = new List<EnablingObjective_Employee_Link>();

        public virtual ICollection<Version_Employee> Version_Employees { get; set; } = new List<Version_Employee>();

        public virtual ICollection<EmployeeDocument> EmployeeDocuments { get; set; } = new List<EmployeeDocument>();

        public virtual ICollection<EmployeeHistory> EmployeeHistorys { get; set; } = new List<EmployeeHistory>();

        public virtual ICollection<TaskQualification> TaskQualifications { get; set; } = new List<TaskQualification>();

        public virtual ICollection<TaskQualification_Evaluator_Link> TaskQualification_Evaluator_Links { get; set; } = new List<TaskQualification_Evaluator_Link>();
        
        public virtual ICollection<ClassSchedule_Employee> ClassSchedule_Employee { get; set; } = new List<ClassSchedule_Employee>();

        public virtual ICollection<ClassSchedule_Roster> ClassSchedule_Rosters { get; set; } = new List<ClassSchedule_Roster>();

        public virtual ICollection<ILA_Evaluator_Link> ILA_Evaluator_Links { get; set; } = new List<ILA_Evaluator_Link>();
        //public virtual ICollection<EmpTest> EmpTests { get; set; } = new List<EmpTest>();

        
        public virtual ICollection<IDP> IDPs { get; set; } = new List<IDP>();
        
        public virtual ICollection<IDP_Review> IDP_Reviews { get; set; } = new List<IDP_Review>();

        public virtual ICollection<ProcedureReview_Employee> ProcedureReview_Employee { get; set; } = new List<ProcedureReview_Employee>();

        public virtual ICollection<TaskReQualificationEmp_Suggestion> TaskReQualificationEmp_SuggestionsAsEvaluator { get; set; } = new List<TaskReQualificationEmp_Suggestion>();
        public virtual ICollection<TaskReQualificationEmp_Suggestion> TaskReQualificationEmp_SuggestionsAsTrainee { get; set; } = new List<TaskReQualificationEmp_Suggestion>();

        public virtual ICollection<TaskReQualificationEmp_Steps> TaskReQualificationEmp_StepsAsEvaluator { get; set; } = new List<TaskReQualificationEmp_Steps>();
        public virtual ICollection<TaskReQualificationEmp_Steps> TaskReQualificationEmp_StepsAsTrainee { get; set; } = new List<TaskReQualificationEmp_Steps>();

        public virtual ICollection<TaskReQualificationEmp_QuestionAnswer> TaskReQualificationEmp_QuestionAnswersAsEvaluator { get; set; } = new List<TaskReQualificationEmp_QuestionAnswer>();
        public virtual ICollection<TaskReQualificationEmp_QuestionAnswer> TaskReQualificationEmp_QuestionAnswersAsTrainee { get; set; } = new List<TaskReQualificationEmp_QuestionAnswer>();

        public virtual ICollection<TaskReQualificationEmp_SignOff> TaskReQualificationEmp_SignOffAsEvaluator { get; set; } = new List<TaskReQualificationEmp_SignOff>();
        public virtual ICollection<TaskReQualificationEmp_SignOff> TaskReQualificationEmp_SignOffAsTrainee { get; set; } = new List<TaskReQualificationEmp_SignOff>();
        
        public virtual ICollection<DIFSurvey_Task> DIFSurvey_Tasks { get; set; } = new List<DIFSurvey_Task>();
        public virtual ICollection<DIFSurvey_Employee> DIFSurvey_Employees { get; set; } = new List<DIFSurvey_Employee>();
        public virtual ICollection<ClassSchedule_Evaluator_Link> ClassSchedule_Evaluator_Links { get; set; } = new List<ClassSchedule_Evaluator_Link>();
        public virtual ICollection<SkillQualification> SkillQualifications { get; set; } = new List<SkillQualification>();
        public virtual ICollection<SkillQualification_Evaluator_Link> SkillQualification_Evaluator_Links { get; set; } = new List<SkillQualification_Evaluator_Link>();
        public Employee(int personId, string employeeNumber, string address, string city, string state, string zipCode, string phoneNumber, string workLocation, string notes, bool tQEqulator, string password, DateOnly? inactiveDate, string? reason, bool publicUser)
        {
            PersonId = personId;
            EmployeeNumber = employeeNumber;
            Address = address;
            City = city;
            State = state;
            ZipCode = zipCode;
            PhoneNumber = phoneNumber;
            WorkLocation = workLocation;
            Notes = notes;
            TQEqulator = tQEqulator;
            Password = password;
            //IsCertificationRequired = isCertificationRequired;
            InactiveDate = inactiveDate;
            Reason = reason;
            PublicUser = publicUser;
        }

        public Employee(int personId, string employeeNumber, string phone)
        {
            PersonId = personId;
            EmployeeNumber = employeeNumber;
            PhoneNumber = phone;
            Deleted = false;
            Active = true;
        }

        public Employee(int personId, string employeeNumber, bool isTQEqulator)
        {
            PersonId = personId;
            EmployeeNumber = employeeNumber;
            TQEqulator = isTQEqulator;
            Deleted = false;
            Active = true;
        }

        public Employee()
        {
        }
        public void SetAddress(string address)
        {
            Address = address;
        }
        public void SetCity(string city)
        {
            City = city;
        }
        public void SetState(string state)
        {
            State = state;
        }
        public void SetZipCode(string zipCode)
        {
            ZipCode = zipCode;
        }
        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        public void SetWorkLocation(string workLocation)
        {
            WorkLocation = workLocation;
        }
        public void SetNotes(string notes)
        {
            Notes = notes;
        }
        public void SetTQEqulator(bool tQEqulator)
        {
            TQEqulator = tQEqulator;
        }
        public void SetEmployeeNumber(string employeeNumber)
        {
            EmployeeNumber = employeeNumber;
        }
        public void SetPublicUser(bool publicUser)
        {
            PublicUser = publicUser;
        }
        public override void Create(string username)
        {
            base.Create(username);
            AddDomainEvent(new Domain.Events.Core.OnNewEmployeeAdded(this));            
        }
        public EmployeePosition AddPosition(Position position, int employeeId, DateOnly startDate, DateOnly? posQualDate, DateOnly? endDate, bool isTrainee, string managerName, string trainingProgramVersion, bool isSignificant,bool? isCertificationRequired = false)
        {
            //var employeePosition = EmployeePositions.FirstOrDefault(p => p.PositionId == position.Id);
            //if (employeePosition != null)
            //{
            //    return employeePosition;
            //}

            var employeePosition = new EmployeePosition(employeeId, position.Id, startDate, endDate, isTrainee, posQualDate, managerName, trainingProgramVersion, isSignificant, isCertificationRequired);

            AddEntityToNavigationProperty<EmployeePosition>(employeePosition);

            return employeePosition;
        }

        public void UnlinkEmployee(Position position)
        {
            EmployeePosition pos_emp_link = EmployeePositions.FirstOrDefault(x => x.EmployeeId == this.Id && x.PositionId == position.Id);
            if (pos_emp_link != null)
            {
                RemoveEntityFromNavigationProperty<EmployeePosition>(pos_emp_link);
            }
        }

        public EmployeePosition Qualify(Position position, DateOnly qualificationDate)
        {
            var employeePosition = EmployeePositions.FirstOrDefault(x => x.PositionId == position.Id && x.Active);
            if (employeePosition == null)
            {
                throw new QTDServerException("PositionNotFound");
            }

            return employeePosition.SetAsQualified(qualificationDate);
        }

        public EmployeePosition LeavePosition(Position position, DateOnly endDate)
        {
            var employeePosition = EmployeePositions.FirstOrDefault(x => x.PositionId == position.Id && x.Active);
            if (employeePosition == null)
            {
                throw new QTDServerException("PositionNotFound");
            }

            return employeePosition.SetEndDate(endDate);
        }

        public EmployeeOrganization JoinOrganization(Organization organization, int employeeId, bool isManager)
        {
            var employeeOrganization = EmployeeOrganizations.FirstOrDefault(p => p.OrganizationId == organization.Id);
            if (employeeOrganization != null)
            {
                return employeeOrganization;
            }

            if (PublicUser && !organization.PublicOrganization)
            {
                throw new QTDServerException("PositionNotFound");
            }

            employeeOrganization = new EmployeeOrganization(employeeId, organization.Id, isManager);

            AddEntityToNavigationProperty(employeeOrganization);

            return employeeOrganization;
        }

        public void LeaveOrganization(Organization organization, int employeeId)
        {
            EmployeeOrganization employeeOrganization = EmployeeOrganizations.FirstOrDefault(p => p.OrganizationId == organization.Id && this.Id == employeeId);

            if (employeeOrganization != null)
            {
                RemoveEntityFromNavigationProperty<EmployeeOrganization>(employeeOrganization);
            }
            

           // RemoveEntityFromNavigationProperty(employeeOrganization);
        }

        public void AddCertification(Certification certification, EmployeeCertification employeeCertification)
        {
            //var empCert = EmployeeCertifications.FirstOrDefault(x => x.CertificationId == certification.Id && x.EmployeeId == this.Id);
            //if (empCert == null)
            //{
                AddEntityToNavigationProperty<EmployeeCertification>(employeeCertification);
            //}
        }

        public void DeleteCertification(Certification certification)
        {
            var empCert = EmployeeCertifications.FirstOrDefault(x => x.CertificationId == certification.Id);
            if (empCert != null)
            {
                RemoveEntityFromNavigationProperty<EmployeeCertification>(empCert);
            }
        }
       
        public EmployeeOrganization LinkOrganization(Organization org)
        {
            EmployeeOrganization org_emp_link = EmployeeOrganizations.FirstOrDefault(x => x.OrganizationId == org.Id && x.EmployeeId == this.Id);
            if (org_emp_link != null)
            {
                return org_emp_link;
            }

            org_emp_link = new EmployeeOrganization(this.Id,org.Id,false);
            AddEntityToNavigationProperty<EmployeeOrganization>(org_emp_link);
            return org_emp_link;
        }

        public EmployeePosition LinkPosition(Position pos)
        {
            EmployeePosition pos_emp_link = EmployeePositions.FirstOrDefault(x => x.PositionId == pos.Id && x.EmployeeId == this.Id);
            if (pos_emp_link != null)
            {
                return pos_emp_link;
            }

            pos_emp_link = new EmployeePosition(this.Id, pos.Id);
            AddEntityToNavigationProperty<EmployeePosition>(pos_emp_link);
            return pos_emp_link;
        }

        public EmployeeCertification LinkCerificate(Certification cer, string certificationNumber, DateOnly issueDate, DateOnly? renewalDate, DateOnly? certExpirationDate)
        {
            EmployeeCertification cer_emp_link = EmployeeCertifications.FirstOrDefault(x => x.CertificationId == cer.Id && x.EmployeeId == this.Id);
            if (cer_emp_link != null)
            {
                return cer_emp_link;
            }

            cer_emp_link = new EmployeeCertification(this.Id, cer.Id, issueDate, certExpirationDate, renewalDate, null, certificationNumber);
            AddEntityToNavigationProperty<EmployeeCertification>(cer_emp_link);
            return cer_emp_link;
        }
        public void DeleteEmpCertification(EmployeeCertification empcertification)
        {
            var empCert = EmployeeCertifications.FirstOrDefault(x => x.CertificationId == empcertification.CertificationId && x.EmployeeId == empcertification.EmployeeId);
            if (empCert != null)
            {
                RemoveEntityFromNavigationProperty<EmployeeCertification>(empCert);
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();
            AddDomainEvent(new Domain.Events.Core.OnEmployee_Deactivated(this));
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnEmployee_Deleted(this));
        }

    }
}
