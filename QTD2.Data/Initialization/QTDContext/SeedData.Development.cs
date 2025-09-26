using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using QTD2.Domain.Entities.Core;
using System.Linq;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QTD2.Data.Initialization.QTDContext
{
    public partial class SeedData
    {
        protected void Development_AddInstructor_Category()
        {
            _migrationBuilder.InsertData(
                 table: "Instructor_Categories",
                 columns: new[] { "ICategoryTitle", "ICategoryDescription", "IEffectiveDate", "Active" },
                 values: new object[,]
                  {
                       { "Default1", "The Default Instructor Category1", System.DateTime.Now, true },
                       { "Default2", "The Default Instructor Category2", System.DateTime.Now, true },
                       { "Default3", "The Default Instructor Category3", System.DateTime.Now, true },
                       { "Default4", "The Default Instructor Category4", System.DateTime.Now, true },
                       { "Default5", "The Default Instructor Category5", System.DateTime.Now, true }
                  });
        }

        protected void Development_AddInstructorTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\instructors.json");
            List<Instructor> persons = JsonSerializer.Deserialize<List<Instructor>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "ICategoryId", "InstructorNumber", "InstructorName", "IsWorkBookAdmin", "EffectiveDate", "Deleted", "Active" },
                values: toRectangular(persons.Select(r => new object[] { r.ICategoryId, r.InstructorNumber, r.InstructorName, r.IsWorkBookAdmin, r.EffectiveDate, r.Deleted, r.Active }).ToArray()));
        }
        protected void Development_AddPersonsTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\persons.json");
            List<Person> persons = JsonSerializer.Deserialize<List<Person>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "FirstName", "LastName", "Username", "Active", "Deleted" },
                values: toRectangular(persons.Select(r => new object[] { r.FirstName, r.LastName, r.Username, r.Active, r.Deleted }).ToArray()));
        }

        protected void Development_AddEmployeeTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\employees.json");
            List<Employee> objs = JsonSerializer.Deserialize<List<Employee>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "PersonId", "Active", "EmployeeNumber", "ZipCode", "PhoneNumber", "TQEqulator", "Deleted" },
                values: toRectangular(objs.Select(r => new object[] { r.PersonId, r.Active, r.EmployeeNumber, r.ZipCode, r.PhoneNumber, r.TQEqulator, false }).ToArray()));
        }

        protected void Development_AddCertificationsHistoryTable()
        {
            List<Certification_History> objs = new List<Certification_History>();
            objs.Add(new Certification_History
            {
                CertId = 1,
                EffectiveDate = new DateTime(),
                Notes = "test"
            });
            _migrationBuilder.InsertData(
             table: "CertificationHistory",
             columns: new[] { "CertId", "EffectiveDate", "Notes", "Active", "Deleted" },
             values: toRectangular(objs.Select(r => new object[] { r.CertId, r.EffectiveDate, r.Notes, true, false }).ToArray()));
        }

        protected void Development_AddEmployeeCertificationsTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\employee_certifications.json");
            List<EmployeeCertification> objs = JsonSerializer.Deserialize<List<EmployeeCertification>>(jsonString);
            _migrationBuilder.InsertData(
             table: "EmployeeCertifications",
             columns: new[] { "EmployeeId", "CertificationId", "CertificationNumber", "IssueDate", "RenewalDate", "ExpirationDate", "RollOverHours", "Active", "Deleted" },
             values: toRectangular(objs.Select(r => new object[] { r.EmployeeId, r.CertificationId, r.CertificationNumber, DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), r.RollOverHours, true, false }).ToArray()));

        }

        protected void Development_AddEmployeePositionTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\employee-positions.json");
            List<EmployeePosition> objs = JsonSerializer.Deserialize<List<EmployeePosition>>(jsonString);

            _migrationBuilder.InsertData(
                table: "EmployeePositions",
                columns: new[] { "EmployeeId", "PositionId", "StartDate", "EndDate", "Trainee", "QualificationDate", "IsSignificant", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.EmployeeId, r.PositionId, DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), r.Trainee, DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), true, true }).ToArray()));
        }

        protected void Development_AddPositionTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\positions.json");
            List<Position> objs = JsonSerializer.Deserialize<List<Position>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "PositionNumber", "PositionAbbreviation", "PositionDescription", "PositionTitle", "IsPublished", "EffectiveDate", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.PositionNumber.ToString(), r.PositionAbbreviation, r.PositionDescription, r.PositionTitle, r.IsPublished, DateTime.Now, true }).ToArray()));

        }
        protected void Development_AddPositionSQsTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\position_SQ.json");
            List<Positions_SQ> objs = JsonSerializer.Deserialize<List<Positions_SQ>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Positions_SQs",
                columns: new[] { "PositionId", "EOId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.PositionId, r.EOId, r.Deleted, r.Active }).ToArray()));

        }
        protected void Development_AddILA_Position_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila_Position_Links.json");
            List<ILA_Position_Link> objs = JsonSerializer.Deserialize<List<ILA_Position_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_Position_Links",
                columns: new[] { "PositionId", "ILAId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.PositionId, r.ILAId, r.Deleted, r.Active }).ToArray()));

        }
        protected void Development_AddILA_PreRequisite_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila_PreRequisite_Links.json");
            List<ILA_PreRequisite_Link> objs = JsonSerializer.Deserialize<List<ILA_PreRequisite_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_PreRequisite_Links",
                columns: new[] { "ILAId", "PreRequisiteId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ILAId, r.PreRequisiteId, r.Deleted, r.Active }).ToArray()));

        }
        protected void Development_AddILA_UploadsTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila_Uploads.json");
            List<ILA_Upload> objs = JsonSerializer.Deserialize<List<ILA_Upload>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_Uploads",
                columns: new[] { "ILAId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ILAId, r.Deleted, r.Active }).ToArray()));

        }
        protected void Development_AddILA_NERCAudience_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila_NERCAudience_Links.json");
            List<ILA_NERCAudience_Link> objs = JsonSerializer.Deserialize<List<ILA_NERCAudience_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_NERCAudience_Links",
                columns: new[] { "ILAId", "NERCAudienceID", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ILAId, r.NERCAudienceID, r.Deleted, r.Active }).ToArray()));

        }
        protected void Development_AddILA_TrainingTopic_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila_TrainingTopic_Links.json");
            List<ILA_TrainingTopic_Link> objs = JsonSerializer.Deserialize<List<ILA_TrainingTopic_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_TrainingTopic_Links",
                columns: new[] { "ILAId", "TrTopicId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ILAId, r.TrTopicId, r.Deleted, r.Active }).ToArray()));

        }
        protected void Development_AddILA_TaskObjective_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila_TaskObjective_Links.json");
            List<ILA_TaskObjective_Link> objs = JsonSerializer.Deserialize<List<ILA_TaskObjective_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_TaskObjective_Links",
                columns: new[] { "ILAId", "TaskId", "UseForTQ", "ILAObjectiveOrder", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ILAId, r.TaskId, r.UseForTQ, r.ILAObjectiveOrder, r.Deleted, r.Active }).ToArray()));

        }

        protected void Development_AddClientUsers()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\persons.json");
            List<Person> persons = JsonSerializer.Deserialize<List<Person>>(jsonString);

            List<ClientUser> clientUsers = new List<ClientUser>();

            foreach (var person in persons)
            {
                clientUsers.Add(new ClientUser(persons.IndexOf(person) + 1));
            }

            _migrationBuilder.InsertData(
              table: "ClientUsers",
              columns: new[] { "PersonId", "Active" },
              values: toRectangular(clientUsers.Select(r => new object[] { r.PersonId, true }).ToArray()));
        }

        protected void Development_AddVersion_EmployeeTable()
        {
            _migrationBuilder.InsertData(
               table: "Version_Employees",
               columns: new[] { "EmployeeId", "PersonId", "EmployeeNumber", "Version_Number", "Active" },
               values: new object[,]
                {
                    { 1, 1, "1", "1.0", true },
                    { 2, 2, "2", "1.0", true },
                });
        }

        protected void Development_AddEmployeeOrganizationTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\employee_organization.json");
            List<EmployeeOrganization> objs = JsonSerializer.Deserialize<List<EmployeeOrganization>>(jsonString);

            _migrationBuilder.InsertData(
                table: "EmployeeOrganizations",
                columns: new[] { "EmployeeId", "OrganizationId", "IsManager", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.EmployeeId, r.OrganizationId, r.IsManager, true }).ToArray()));
        }

        protected void Development_AddOrganizationTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\organizations.json");
            List<Organization> objs = JsonSerializer.Deserialize<List<Organization>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Name", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.Name, true }).ToArray()));
        }

        protected void Development_AddTrainingProgramTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\trainingPrograms.json");
            List<TrainingProgram> objs = JsonSerializer.Deserialize<List<TrainingProgram>>(jsonString);

            _migrationBuilder.InsertData(
                table: "TrainingPrograms",
                columns: new[] { "PositionId", "TrainingProgramTypeId", "ProgramTitle", "StartDate", "Year", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.PositionId, r.TrainingProgramTypeId, r.ProgramTitle, r.StartDate, r.Year, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddTrainingPrograms_ILA_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\trainingPrograms_ILA_Links.json");
            List<TrainingPrograms_ILA_Link> objs = JsonSerializer.Deserialize<List<TrainingPrograms_ILA_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "TrainingPrograms_ILA_Links",
                columns: new[] { "TrainingProgramId", "ILAId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.TrainingProgramId, r.ILAId, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddCertifyingBodyTable()
        {
            _migrationBuilder.InsertData(
                 table: "CertifyingBodies",
                 columns: new[] { "Id", "Name", "IsNERC", "Active", "Deleted" },
                 values: new object[,]
                {
                    { 1, "NERC", true, true, false },
                    { 2, "Regional", false, true, false },
                });
        }

        protected void Development_AddCertificationTable()
        {
            _migrationBuilder.InsertData(
            table: "Certifications",
            columns: new[] { "Id", "Name", "CertifyingBodyId", "RenewalTimeFrame", "RenewalInterval", "CreditHrsReq", "CreditHrs", "CertSubReq", "CertSubReqName", "CertSubReqHours", "CertMemberNum", "CertifiedDate", "RenewalDate", "ExpirationDate", "AllowRolloverHours", "AdditionalHours", "EffectiveDate", "Active", "Deleted" },
            values: new object[,]
            {
                { 1, "Reliability Coordinator", 1, false, 1, false, 1, false, "CertSubReqName", 1.00, false, false, false, false, false, 0.00, DateTime.Now, true, false },
                { 2, "Balancing and Interchange/Transmission Operator", 1, false, 1, false, 1, false, "CertSubReqName", 1.00, false, false, false, false, false, 0.00, DateTime.Now, true, false },
                { 3, "Balancing and Interchange Operator", 1, false, 1, false, 1, false, "CertSubReqName", 1.00, false, false, false, false, false, 0.00, DateTime.Now, true, false },
                { 4, "Transmission Operator", 1, false, 1, false, 1, false, "CertSubReqName", 1.00, false, false, false, false, false, 0.00, DateTime.Now, true, false },
            });
        }

        protected void Development_AddDutyAreaTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\dutyArea.json");
            List<DutyArea> objs = JsonSerializer.Deserialize<List<DutyArea>>(jsonString);

            _migrationBuilder.InsertData(
                table: "DutyAreas",
                columns: new[] { "Title", "Description", "Letter", "Number", "EffectiveDate", "ReasonForRevision", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.Title, r.Description, r.Letter, r.Number, DateTime.Now, r.ReasonForRevision, true }).ToArray()));
        }

        protected void Development_AddSubdutyAreaTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\sub_duty_area.json");
            List<SubdutyArea> objs = JsonSerializer.Deserialize<List<SubdutyArea>>(jsonString);

            _migrationBuilder.InsertData(
                table: "SubdutyAreas",
                columns: new[] { "DutyAreaId", "Description", "SubNumber", "Title", "EffectiveDate", "ReasonForRevision", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.DutyAreaId, r.Description, r.SubNumber, r.Title, DateTime.Now, r.ReasonForRevision, true }).ToArray()));
        }

        protected void Development_AddTaskTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\tasks.json");
            List<Task> objs = JsonSerializer.Deserialize<List<Task>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "SubdutyAreaId", "Description", "Abreviation", "Number", "Criteria", "TaskCriteriaUpload", "Image", "Critical", "References", "RequiredTime", "IsMeta", "IsReliability", "Conditions", "EffectiveDate", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.SubdutyAreaId, r.Description, r.Abreviation, r.Number, r.Criteria, r.TaskCriteriaUpload, r.Image, r.Critical, r.References, r.RequiredTime, r.IsMeta, r.IsReliability, r.Conditions, DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), true }).ToArray()));
        }

        protected void Development_AddEnablingObjectiveTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\enabling_objectives.json");
            List<EnablingObjective> objs = JsonSerializer.Deserialize<List<EnablingObjective>>(jsonString);

            _migrationBuilder.InsertData(
                table: "EnablingObjectives",
                columns: new[] { "CategoryId", "SubCategoryId", "TopicId", "Number", "Description", "isMetaEO", "IsSkillQualification", "References", "Criteria", "Conditions", "EffectiveDate", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.CategoryId, r.SubCategoryId, r.TopicId, r.Number, r.Description, r.isMetaEO, r.IsSkillQualification, r.References, r.Criteria, r.Conditions, r.EffectiveDate, true }).ToArray()));
        }
        protected void Development_AddILA_EnablingObjective_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila_EnablingObjective_Links.json");
            List<ILA_EnablingObjective_Link> objs = JsonSerializer.Deserialize<List<ILA_EnablingObjective_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_EnablingObjective_Links",
                columns: new[] { "ILAId", "EnablingObjectiveId", "ILAObjectiveOrder", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ILAId, r.EnablingObjectiveId, r.ILAObjectiveOrder, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddEnablingObjective_TopicTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\enabling_objectives_topic.json");
            List<EnablingObjective_Topic> objs = JsonSerializer.Deserialize<List<EnablingObjective_Topic>>(jsonString);

            _migrationBuilder.InsertData(
                table: "EnablingObjective_Topics",
                columns: new[] { "SubCategoryId", "Title", "Description", "Number", "EffectiveDate", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.SubCategoryId, r.Title, r.Description, r.Number, r.EffectiveDate, true }).ToArray()));
        }

        protected void Development_AddEnablingObjective_CategoryTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\enabling_objectives_category.json");
            List<EnablingObjective_Category> objs = JsonSerializer.Deserialize<List<EnablingObjective_Category>>(jsonString);

            _migrationBuilder.InsertData(
                table: "EnablingObjective_Categories",
                columns: new[] { "Title", "Description", "Number", "EffectiveDate", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.Title, r.Description, r.Number, r.EffectiveDate, true }).ToArray()));
        }

        protected void Development_AddEnablingObjective_SubCategoryTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\enabling_objectives_sub_category.json");
            List<EnablingObjective_SubCategory> objs = JsonSerializer.Deserialize<List<EnablingObjective_SubCategory>>(jsonString);

            _migrationBuilder.InsertData(
                table: "EnablingObjective_SubCategories",
                columns: new[] { "CategoryId", "Title", "Description", "Number", "EffectiveDate", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.CategoryId, r.Title, r.Description, r.Number, r.EffectiveDate, true }).ToArray()));
        }

        protected void Development_AddEnablingObjective_Employee_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\enabling_objective_employee_links.json");
            List<EnablingObjective_Employee_Link> objs = JsonSerializer.Deserialize<List<EnablingObjective_Employee_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "EnablingObjective_Employee_Links",
                columns: new[] { "EOID", "EmployeeId", "StartDate", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.EOID, r.EmployeeId, r.StartDate, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddProcedure_IssuingAuthorityTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\issuing_authority_procedures.json");
            List<Procedure_IssuingAuthority> objs = JsonSerializer.Deserialize<List<Procedure_IssuingAuthority>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Procedure_IssuingAuthorities",
                columns: new[] { "Title", "Website", "EffectiveDate", "Notes", "IsActive", "IsDeleted", "Description", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.Title, r.Website, DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), r.Notes, r.IsActive, r.IsDeleted, r.Description, true }).ToArray()));
        }

        protected void Development_AddProcedureTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\procedures.json");
            List<Procedure> objs = JsonSerializer.Deserialize<List<Procedure>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Procedures",
                columns: new[] { "IssuingAuthorityId", "Number", "Title", "Description", "RevisionNumber", "EffectiveDate", "ProceduresFileUpload", "IsDeleted", "IsPublished", "Hyperlink", "FileName", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.IssuingAuthorityId, r.Number, r.Title, r.Description, r.RevisionNumber, DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), r.ProceduresFileUpload, r.IsDeleted, r.IsPublished, r.Hyperlink, r.FileName, true }).ToArray()));
        }

        protected void Development_AddProcedureReviewsTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\procedureReviews.json");
            List<ProcedureReview> objs = JsonSerializer.Deserialize<List<ProcedureReview>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ProcedureReviews",
                columns: new[] { "ProcedureId", "StartDateTime", "EndDateTime", "IsEmployeeShowResponses", "IsPublished", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ProcedureId, r.StartDateTime, r.EndDateTime, r.IsEmployeeShowResponses, r.IsPublished, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddILA_Procedure_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila_Procedure_Links.json");
            List<ILA_Procedure_Link> objs = JsonSerializer.Deserialize<List<ILA_Procedure_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_Procedure_Links",
                columns: new[] { "ILAId", "ProcedureId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ILAId, r.ProcedureId, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddProcedureReview_EmployeesTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\procedureReview_Employees.json");
            List<ProcedureReview_Employee> objs = JsonSerializer.Deserialize<List<ProcedureReview_Employee>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ProcedureReview_Employees",
                columns: new[] { "ProcedureReviewId", "EmployeeId", "IsCompleted", "IsStarted", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ProcedureReviewId, r.EmployeeId, r.IsCompleted, r.IsStarted, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddSaftyHazard_categoryTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\category_sh.json");
            List<SaftyHazard_Category> objs = JsonSerializer.Deserialize<List<SaftyHazard_Category>>(jsonString);

            _migrationBuilder.InsertData(
                table: "SaftyHazard_Categories",
                columns: new[] { "Description", "Number", "Title", "Notes", "EffectiveDate", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.Description, r.Number, r.Title, r.Notes, r.EffectiveDate, true }).ToArray()));
        }

        protected void Development_AddSaftyHazardTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\safety_hazards.json");
            List<SaftyHazard> objs = JsonSerializer.Deserialize<List<SaftyHazard>>(jsonString);

            _migrationBuilder.InsertData(
                table: "SaftyHazards",
                columns: new[] { "SaftyHazardCategoryId", "Number", "Title", "RevisionNumber", "EffectiveDate", "HyperLinks", "Text", "Files", "Image", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.SaftyHazardCategoryId, r.Number, r.Title, r.RevisionNumber, r.EffectiveDate, r.HyperLinks, r.Text, r.Files, r.Image, true }).ToArray()));
        }
        protected void Development_AddILA_SafetyHazard_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila_SafetyHazard_Links.json");
            List<ILA_SafetyHazard_Link> objs = JsonSerializer.Deserialize<List<ILA_SafetyHazard_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_SafetyHazard_Links",
                columns: new[] { "ILAId", "SafetyHazardId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ILAId, r.SafetyHazardId, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddClassSchedule_SelfRegistrationTable()
        {

        }

        protected void Development_AddSaftyHazard_ControlTable()
        {
            //_migrationBuilder.InsertData(
            //   table: "SaftyHazard_Controls",
            //   columns: new[] { "Id", "SaftyHazardId", "Number", "Description" },
            //   values: new object[,]
            //    {
            //        { 1, 1, 1, "Replace the hazard" },
            //        { 2, 1, 2, "Physically remove the hazard" },
            //        { 3, 2, 1, "remove the hazard" },
            //        { 4, 2, 2, "New Safety Hazard control" },
            //    });
        }

        protected void Development_AddSaftyHazard_AbatementTable()
        {
            //_migrationBuilder.InsertData(
            //   table: "SaftyHazard_Abatements",
            //   columns: new[] { "Id", "SaftyHazardId", "Number", "Description" },
            //   values: new object[,]
            //    {
            //        { 1, 1, 1, "Place cones around the hazard" },
            //        { 2, 1, 2, "Contact maintenance for spills" },
            //        { 3, 2, 1, "Notify employees" },
            //        { 4, 2, 2, "hazard abatement details" },
            //    });
        }

        protected void Development_AddProcedure_SaftyHazard_LinkTable()
        {
        }

        protected void Development_AddProcedure_EnablingObjective_LinkTable()
        {
        }

        protected void Development_AddEnablingObjective_SaftyHazard_LinkTable()
        {
        }

        protected void Development_AddEnablingObjective_Procedure_LinkTable()
        {
        }

        protected void Development_AddTask_EnablingObjective_LinkTable()
        {
        }

        protected void Development_AddTask_Procedure_LinkTable()
        {
        }

        protected void Development_AddTask_SaftyHazard_LinkTable()
        {
        }

        protected void Development_AddTask_StepTable()
        {
            //_migrationBuilder.InsertData(
            //   table: "Task_Steps",
            //   columns: new[] { "Id", "TaskId", "Description", "Number", "Active" },
            //   values: new object[,]
            //    {
            //            { 1, 1, "test system Voltage Alarms", 1, true },
            //    });
        }

        protected void Development_AddTask_ToolTable()
        {
            //_migrationBuilder.InsertData(
            //   table: "Task_Tools",
            //   columns: new[] { "Id", "TaskId", "ToolId" },
            //   values: new object[,]
            //    {
            //            { 1, 1, 1 },
            //    });
        }

        protected void Development_AddToolGroupsTable()
        {
            //_migrationBuilder.InsertData(
            //  table: "ToolGroups",
            //  columns: new[] { "Id", "Description", "Active" },
            //  values: new object[,]
            //    {
            //        { 1, "Saws", true },
            //    });
        }

        protected void Development_AddToolTable()
        {
            //_migrationBuilder.InsertData(
            //     table: "Tools",
            //     columns: new[] { "Id", "ToolCategoryId", "Number", "Name" },
            //     values: new object[,]
            //     {
            //            { 1, 1, "1_T", "TableSaw"},
            //            { 2, 1, "1_D", "Dovetail"},
            //            { 3, 1, "1_B", "Band"},
            //            { 4, 1, "1_CL", "Channel Locks"},
            //            { 5, 1, "1W", "Wrench"},
            //     });
        }

        protected void Development_AddToolGroup_ToolsTable()
        {
            //_migrationBuilder.InsertData(
            //   table: "ToolGroup_Tools",
            //   columns: new[] { "Id", "ToolId", "ToolGroupId" },
            //   values: new object[,]
            //    {
            //            { 1, 1, 1 },
            //            { 2, 2, 1 },
            //            { 3, 3, 1 },
            //    });
        }

        protected void Development_AddVersion_TaskTable()
        {
        }

        protected void Development_AddVersion_Task_StepTable()
        {
        }

        protected void Development_AddVersion_Task_QuestionTable()
        {
        }

        protected void Development_AddVersion_ProcedureTable()
        {
        }

        protected void Development_AddVersion_Task_Procedure_LinkTable()
        {
        }

        protected void Development_AddVersion_ToolTable()
        {
        }

        protected void Development_AddVersion_Task_Tool_LinkTable()
        {
        }

        protected void Development_AddVersion_Procedure_Tool_LinkTable()
        {
        }

        protected void Development_AddVersion_Task_EnablingObjective_LinkTable()
        {
        }

        protected void Development_AddVersion_EnablingObjective_Tool_LinkTable()
        {
        }

        protected void Development_AddVersion_SaftyHazardTable()
        {
        }

        protected void Development_AddVersion_SaftyHazard_AbatementTable()
        {
        }

        protected void Development_AddVersion_SaftyHazard_ControlTable()
        {
        }

        protected void Development_AddVersion_Task_SaftyHazard_LinkTable()
        {
        }

        protected void Development_AddVersion_Procedure_SaftyHazard_LinkTable()
        {
        }

        protected void Development_AddVersion_EnablingObjective_SaftyHazard_LinkTable()
        {
        }

        protected void Development_AddVersion_EnablingObjectiveTable()
        {
        }

        protected void Development_AddVersion_EnablingObjective_Procedure_LinkTable()
        {
        }

        protected void Development_AddVersion_Procedure_EnablingObjective_LinkTable()
        {
        }

        protected void Development_AddEmployee_TaskTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\employee_tasks.json");
            List<Employee_Task> objs = JsonSerializer.Deserialize<List<Employee_Task>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Employee_Tasks",
                columns: new[] { "Id", "TaskId", "EmployeeId", "MajorVersion", "MinorVersion", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.Id, r.TaskId, r.EmployeeId, r.MajorVersion, r.MinorVersion, true }).ToArray()));
        }

        protected void Development_AddTimesheetTable()
        {
        }

        protected void Development_AddTask_QuestionTable()
        {
        }

        protected void Development_AddPositionHistoriesTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\position_histories.json");
            List<Position_History> objs = JsonSerializer.Deserialize<List<Position_History>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Position_Histories",
                columns: new[] { "PositionId", "ChangeNotes", "ChangeEffectiveDate", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.PositionId, r.ChangeNotes, r.ChangeEffectiveDate, r.Deleted, r.Active }).ToArray()));

        }

        protected void Development_AddTask_PositionTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\tasks_position.json");
            List<Task_Position> objs = JsonSerializer.Deserialize<List<Task_Position>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Task_Positions",
                columns: new[] { "TaskId", "PositionId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.TaskId, r.PositionId, false, true }).ToArray()));

        }

        protected void Development_AddProviderLevelTable()
        {
            _migrationBuilder.InsertData(
                table: "ProviderLevels",
                columns: new[] { "Id", "Name", "Active" },
                values: new object[,]
                  {
                    { 1, "Level 1 - Transcript Reviewer", true },
                    { 2, "Level 2 - ILA Provider", true },
                    { 3, "Level 3 - NERC Continuing Education Provider", true },
                  });
        }
        protected void Development_AddProviderTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\providers.json");
            List<Provider> objs = JsonSerializer.Deserialize<List<Provider>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Name", "Number", "ProviderLevelId", "ContactName", "ContactTitle", "ContactEmail", "IsPriority", "IsNERC", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.Name, r.Number, r.ProviderLevelId, r.ContactName, r.ContactTitle, r.ContactEmail, r.IsPriority, r.IsNERC, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddILA_TopicTable()
        {
        }
        protected void Development_AddILA_Evaluator_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila_Evaluator_Links.json");
            List<ILA_Evaluator_Link> objs = JsonSerializer.Deserialize<List<ILA_Evaluator_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_Evaluator_Links",
                columns: new[] { "ILAId", "EvaluatorId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ILAId, r.EvaluatorId, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddDeliveryMethodTable()
        {
            _migrationBuilder.InsertData(
                 table: "DeliveryMethods",
                 columns: new[] { "Name", "DisplayName", "IsNerc", "Active", "CreatorIlaId" },
                 values: new object[,]
                   {
                    { "Backup Drill Facility", "Backup Drill Facility", false, true, 0 },
                    { "Classroom", "Classroom", false, true, 0 },
                    { "Computer Based Training (CBT)", "Computer Based Training (CBT)", false, true, 0 },
                    { "External Conference/Seminar/Workshop", "External Conference/Seminar/Workshop", false, true, 0 },
                    { "Field Visit", "Field Visit", false, true, 0 },
                    { "On-the-Job Training (OJT)", "On-the-Job Training (OJT)", false, true, 0 },
                    { "One-on-One Training", "One-on-One Training", false, true, 0 },
                    { "Self-Study (e.g., paperbased)", "Self-Study (e.g., paperbased)", false, true, 0 },
                    { "Simulator Training", "Simulator Training", false, true, 0 },
                    { "Table Top Simulation", "Table Top Simulation", false, true, 0 },
                    { "Task Qualification", "Task Qualification", false, true, 0 },
                    { "Vendor Supplied Training", "Vendor Supplied Training", false, true, 0 },
                    { "Virtual Instructor Led Training", "Virtual Instructor Led Training", false, true, 0 },
                    { "Workshop", "Workshop", true, true, 0 },
                    { "Traditional Classroom Activities", "Traditional Classroom Activities", true, true, 0 },
                    { "Conferences", "Conferences", true, true, 0 },
                    { "Seminars", "Seminars", true, true, 0 },
                    { "In-house training curricula", "In-house training curricula", true, true, 0 },
                    { "Structured self-study activities", "Structured self-study activities", true, true, 0 },
                    { "Online and distance - learning activities", "Online and distance - learning activities", true, true, 0 },
                    { "Operator Training Simulations", "Operator Training Simulations", true, true, 0 },
                    { "Computer- based training activities", "Computer- based training activities", true, true, 0 },
                   });
        }

        protected void Development_AddTrainingTopicTable()
        {
            _migrationBuilder.InsertData(
                    table: "TrainingTopics",
                    columns: new[] { "TrainingTopic_CategoryId", "Name", "Active" },
                    values: new object[,]
                    {
                        { 1,"Capacitance", true },
                        { 1,"Inductance", true },
                        { 1,"Impedance", true },
                        { 1,"Real and reactive power", true },
                        { 1,"Electrical circuits", true },
                        { 1,"Magnetism", true },
                        { 2,"Basic trigonometry", true },
                        { 2,"Ratios" , true},
                        { 2,"Per unit values", true },
                        { 2,"Pythagorean Theorem", true },
                        { 2,"Ohm’s Law" , true},
                        { 2,"Kirchoff’s Laws" , true},
                        { 3,"Transmission lines", true },
                        { 3,"Transformers", true },
                        { 3,"Substations" , true},
                        { 3,"Power plants" , true},
                        { 3,"Protection", true },
                        { 3,"Introduction to power system operations and interconnected operations" , true},
                        { 3,"Frequency" , true},
                        { 4,"Transmission lines", true },
                        { 4,"Transformers", true },
                        { 4,"Busses" , true},
                        { 4,"Generators" , true},
                        { 4,"Relays and protection schemes", true},
                        { 4,"Power system faults", true },
                        { 4,"Synchronizing equipment", true },
                        { 4,"Under-frequency load shedding" , true},
                        { 4,"Under-voltage load shedding", true },
                        { 4,"Communication systems utilized", true },
                        { 5,"Voltage control", true },
                        { 5,"Frequency control", true },
                        { 5,"Power system stability", true },
                        { 5,"Facility outage both planned and unplanned" , true},
                        { 5,"Energy accounting", true },
                        { 5,"Inadvertent energy" , true},
                        { 5,"Time error control", true },
                        { 5,"Balancing of load and resources", true },
                        { 6,"Loss of generation resource(s)", true },
                        { 6,"Loss of transmission element(s)", true },
                        { 6,"Operating reserves", true },
                        { 6,"Contingency reserves", true },
                        { 6,"Line loading relief", true },
                        { 6,"Load shedding", true },
                        { 6,"Voltage and reactive flows during emergencies", true },
                        { 6,"Loss of EMS", true },
                        { 6,"Loss of primary control center", true },
                        { 7,"Restoration philosophies", true },
                        { 7,"Facility restoration priorities", true },
                        { 7,"Blackstart restoration", true },
                        { 7,"Stability (angle and voltage)", true },
                        { 7,"Islanding and Synchronizing", true },
                        { 8,"NAESB standards", true },
                        { 8,"Standards of Conduct", true },
                        { 8,"Tariffs", true },
                        { 8,"OASIS applications (Transmission Reservations)", true },
                        { 8,"E-Tag application", true },
                        { 8,"Transaction scheduling", true },
                        { 8,"Market applications", true },
                        { 8,"Interchange", true },
                        { 9,"Supervisory Control and Data Acquisition (SCADA)", true },
                        { 9,"Automatic Generation Control (AGC) application", true },
                        { 9,"Power flow application", true },
                        { 9,"State Estimator application", true },
                        { 9,"Contingency analysis application", true },
                        { 9,"P-V Curves", true },
                        { 9,"Load forecasting application", true },
                        { 9,"Energy accounting application", true },
                        { 9,"Voice and data communication systems", true },
                        { 9,"Demand-side management programs" , true},
                        { 10,"Identifying loss of facilities", true},
                        { 10,"Recognizing loss of communication facilities", true },
                        { 10,"Recognizing telemetry problems", true },
                        { 10,"Recognizing and identifying contingency problems", true },
                        { 10,"Proper communications (three-part)", true },
                        { 10,"Communication with appropriate entities including the RC", true },
                        { 10,"Cyber and physical security and threats", true },
                        { 10,"Reducing System Operator errors through the use of HPI Tools (self-checking, peer checking, Place Keeping and Procedure Use", true },
                        { 11,"ISO/RTO operational and emergency policies and procedures", true },
                        { 11,"Regional operational and emergency policies and procedures", true },
                        { 11,"Company-specific operational and emergency policies and procedures", true },
                        { 12,"Application and/or implementation of NERC Reliability Standards", true },
                    }
                );
        }

        protected void Development_AddNercStandardTable()
        {
            _migrationBuilder.InsertData(
                       table: "NercStandards",
                       columns: new[] { "Name", "IsUserDefined", "IsNercStandard", "Active" },
                       values: new object[,]
                       {
                        { "PJM", false, false, true },
                        { "NERC", false, true, true },
                        { "Forklift", true, false, true },
                        { "Internal Training Only - No Certificate Training Credit", true, false, true },
                       });
        }

        protected void Development_AddNercStandardMemberTable()
        {
            _migrationBuilder.InsertData(
                       table: "NercStandardMembers",
                       columns: new[] { "StdId", "Name", "Type", "Active" },
                       values: new object[,]
                       {
                        { 1, "PJM Total Training Hours", "if", true},
                        { 2, "Standard related hours", "if", true },
                        { 2, "Operating Topic/CEHs", "if",  true},
                        { 2, "Simulation Hours",  "if", true },
                        { 2, "PER-005 Emergency Op Hours", "if", true },
                        { 2, "Include simulations", "cb" , true},
                        { 2, "Count toward Emergency Operations training", "cb",true },
                        { 2, "Partial credit allowed", "cb", true },
                       });
        }

        protected void Development_AddTraineeEvaluationTypeTable()
        {
            _migrationBuilder.InsertData(
                 table: "TraineeEvaluationTypes",
                 columns: new[] { "Name", "Active" },
                 values: new object[,]
                   {
                    { "Written", true },
                    { "Perform", true },
                    { "Discuss", true },
                    { "Simulations", true },
                   });
        }

        protected void Development_AddMetaILATable()
        {
        }

        protected void Development_AddSegmentTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\segments.json");
            List<Segment> objs = JsonSerializer.Deserialize<List<Segment>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Segments",
                columns: new[] { "Title", "Duration", "IsNercStandard", "IsNercOperatingTopics", "IsNercSimulation", "Content", "Deleted", "Active", "IsPartialCredit" },
                values: toRectangular(objs.Select(r => new object[] { r.Title, r.Duration, r.IsNercStandard, r.IsNercOperatingTopics, r.IsNercSimulation, r.Content, r.Deleted, r.Active, false }).ToArray()));

        }

        protected void Development_AddILA_Segment_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila_Segment_Links.json");
            List<ILA_Segment_Link> objs = JsonSerializer.Deserialize<List<ILA_Segment_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_Segment_Links",
                columns: new[] { "ILAId", "SegmentId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ILAId, r.SegmentId, r.Deleted, r.Active }).ToArray()));

        }
        protected void Development_AddAssessmentToolTable()
        {

        }

        protected void Development_AddRR_IssuingAuthorityTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\issuing_authority_req.json");
            List<RR_IssuingAuthority> objs = JsonSerializer.Deserialize<List<RR_IssuingAuthority>>(jsonString);

            _migrationBuilder.InsertData(
                table: "RR_IssuingAuthorities",
                columns: new[] { "Title", "Description", "Website", "EffectiveDate", "Notes", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.Title, r.Description, r.Website, r.EffectiveDate, r.Notes, true }).ToArray()));

        }

        protected void Development_AddSelfRegistrationOptionsTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\selfRegistrationOptions.json");
            List<ILA_SelfRegistrationOptions> objs = JsonSerializer.Deserialize<List<ILA_SelfRegistrationOptions>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ILA_SelfRegistrationOptions",
                columns: new[] { "ILAId", "MakeAvailableForSelfReg", "RequireAdminApproval", "AcknowledgeRegDisclaimer", "LimitForLinkedPositions", "CloseRegOnStartDate", "EnableWaitlist", "Deleted", "Active", "SendApprovedEmail" },
                values: toRectangular(objs.Select(r => new object[] { r.ILAId, r.MakeAvailableForSelfReg, r.RequireAdminApproval, r.AcknowledgeRegDisclaimer, r.LimitForLinkedPositions, r.CloseRegOnStartDate, r.EnableWaitlist, r.Deleted, r.Active, false }).ToArray()));

        }

        protected void Development_AddRegulatoryRequirementTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\regulatory_requirements.json");
            List<RegulatoryRequirement> objs = JsonSerializer.Deserialize<List<RegulatoryRequirement>>(jsonString);

            _migrationBuilder.InsertData(
                table: "RegulatoryRequirements",
                columns: new[] { "IssuingAuthorityId", "Number", "Title", "Description", "RevisionNumber", "EffectiveDate", "Uploads", "HyperLink", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.IssuingAuthorityId, r.Number, r.Title, r.Description, r.RevisionNumber, r.EffectiveDate, r.Uploads, r.HyperLink, true }).ToArray()));
        }

        protected void Development_AddRR_SH_LinkTable()
        {
        }

        protected void Development_AddILATable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\ila.json");
            List<ILA> objs = JsonSerializer.Deserialize<List<ILA>>(jsonString);
            _migrationBuilder.InsertData(
                table: "ILAs",
                columns: new[] { "Name", "Description", "ProviderId", "IsSelfPaced", "IsOptional", "IsPublished", "HasPilotData", "IsProgramManual", "EffectiveDate", "ApprovalDate", "SubmissionDate", "ExpirationDate", "Deleted", "Active", "CBTRequiredForCourse", "StartDate", "UseForEMP" },
                values: toRectangular(objs.Select(r => new object[] { r.Name, r.Description, r.ProviderId, r.IsSelfPaced, r.IsOptional, r.IsPublished, r.HasPilotData, r.IsProgramManual, r.EffectiveDate, DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), false, true, r.CBTRequiredForCourse, DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), false }).ToArray()));
        }

        protected void Development_AddTrainingTopic_CategoryTable()
        {
            _migrationBuilder.InsertData(
                table: "TrainingTopic_Categories",
                columns: new[] { "Name", "Active" },
                values: new object[,]
                {
                    { "Basic AC/DC Electricity", true },
                    { "Basic Power System Mathematic Concepts", true },
                    { "Characteristics of the Bulk Electric System", true },
                    { "System Protection Principles", true },
                    { "Interconnected Power System Operations", true },
                    { "Emergency Operations", true },
                    { "Power System Restoration", true },
                    { "Market Operations" , true},
                    { "Tools" , true},
                    { "System Operator Situational Awareness" , true},
                    { "Policies and Procedures" , true},
                    { "NERC Reliability Standards", true },
                }
                );
        }

        protected void Development_AddNERCTargetAudienceTable()
        {
            _migrationBuilder.InsertData(
                     table: "NERCTargetAudiences",
                     columns: new[] { "Name", "Active" },
                     values: new object[,]
                     {
                        { "Transmission Operator", true },
                        { "Reliability Operator", true },
                        { "Balancing and Interchange Operator", true },
                        { "Generator Operator", true },
                        { "Market Operator", true },
                        { "Operators and Planning Engineers", true },
                        { "Control Room Supervision/ Management and Support Staff", true },
                     });
        }

        protected void Development_AddRatingScaleTable()
        {
            string idpreviewString = System.IO.File.ReadAllText(_path + "\\ratingScale.json");
            List<RatingScale> review = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RatingScale>>(idpreviewString);

            _migrationBuilder.InsertData(
              table: "RatingScales",
              columns: new[] { "Id", "Position1Text", "Position2Text", "Deleted", "Active" },
              values: toRectangular(review.Select(r => new object[] { r.Id, r.Position1Text, r.Position2Text, false, true }).ToArray()));
        }

        protected void Development_AddStudentEvaluationAvailabilityTable()
        {
            _migrationBuilder.InsertData(
                  table: "StudentEvaluationAvailabilities",
                  columns: new[] { "Name", "Active" },
                  values: new object[,]
                  {
                        { "Immedialtely Upon Enrollment", true },
                        { "Prior To Awarding Final Grade", true },
                        { "After Final Completion Grade Is Awarded", true },
                  });
        }

        protected void Development_AddILA_NERCAudience_LinkTable()
        {
        }

        protected void Development_AddStudentEvaluationFormsTable()
        {
            string idpreviewString = System.IO.File.ReadAllText(_path + "\\studentEvaluationForms.json");
            List<StudentEvaluationForm> review = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StudentEvaluationForm>>(idpreviewString);

            _migrationBuilder.InsertData(
              table: "StudentEvaluationForms",
              columns: new[] { "Id", "Name", "RatingScaleId", "IsShared", "IsAvailableForAllILAs", "IsNAOption", "IncludeComments", "Deleted", "Active" },
              values: toRectangular(review.Select(r => new object[] { r.Id, r.Name, r.RatingScaleId, r.IsShared, r.IsAvailableForAllILAs, r.IsNAOption, r.IncludeComments, false, true }).ToArray()));
        }
        protected void Development_AddStudentEvaluationWithoutEmpTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\studentEvaluationWithoutEmp.json");
            List<StudentEvaluationWithoutEmp> objs = JsonSerializer.Deserialize<List<StudentEvaluationWithoutEmp>>(jsonString);

            _migrationBuilder.InsertData(
                table: "StudentEvaluationWithoutEmp",
                columns: new[] { "StudentEvaluationId", "EmployeeId", "ClassScheduleId", "QuestionId", "DataMode", "RatingScale", "High", "Average", "Low", "Notes", "AdditionalComments", "IsCompleted", "StudentEvaluationQuestionId", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.StudentEvaluationId, r.EmployeeId, r.ClassScheduleId, r.QuestionId, r.DataMode, r.RatingScale, r.High, r.Average, r.Low, r.Notes, r.AdditionalComments, r.IsCompleted, r.StudentEvaluationId, r.Deleted, r.Active }).ToArray()));

        }
        protected void Development_AddCoverSheetTypeTable()
        {
        }

        protected void Development_AddStudentEvaluationQuestionTable()
        {
            string idpreviewString = System.IO.File.ReadAllText(_path + "\\studentEvaluationQuestions.json");
            List<StudentEvaluationQuestion> questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StudentEvaluationQuestion>>(idpreviewString);

            _migrationBuilder.InsertData(
              table: "StudentEvaluationQuestions",
              columns: new[] { "Id", "EvalFormID", "QuestionNumber", "QuestionText", "Deleted", "Active" },
              values: toRectangular(questions.Select(r => new object[] { r.Id, r.EvalFormID, r.QuestionNumber, r.QuestionText, false, true }).ToArray()));

        }

        protected void Development_AddStudentEvaluation_QuestionTable()
        {
            string studentEval_QuestionString = System.IO.File.ReadAllText(_path + "\\studentEvaluation_Questions.json");
            List<StudentEvaluation_Question> questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StudentEvaluation_Question>>(studentEval_QuestionString);
            _migrationBuilder.InsertData(
              table: "StudentEvaluation_Questions",
              columns: new[] { "Id", "StudentEvaluationId", "QuestionBankId", "Deleted", "Active" },
              values: toRectangular(questions.Select(r => new object[] { r.Id, r.StudentEvaluationId, r.QuestionBankId, r.Deleted, r.Active }).ToArray()));
        }
        protected void Development_AddQuestionBankTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\questionBanks.json");
            List<QuestionBank> objs = JsonSerializer.Deserialize<List<QuestionBank>>(jsonString);

            _migrationBuilder.InsertData(
                table: "QuestionBanks",
                columns: new[] { "Stem", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.Stem, true }).ToArray()));

        }
        protected void Development_AddCollaboratorInvitationTable()
        {
        }

        protected void Development_AddCoversheetTable()
        {
        }

        protected void Development_AddCustomEnablingObjectiveTable()
        {
        }

        protected void Development_AddStudentEvaluationAudienceTable()
        {
            _migrationBuilder.InsertData(
                 table: "StudentEvaluationAudiences",
                 columns: new[] { "Name", "Active" },
                 values: new object[,]
                 {
                        { "All Enrolled Employees",true },
                        { "First Class Only (Pilot Class)",true },
                 });
        }

        protected void Development_AddStudentEvaluationTable()
        {
            string idpreviewString = System.IO.File.ReadAllText(_path + "\\studentEvaluations.json");
            List<StudentEvaluation> review = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StudentEvaluation>>(idpreviewString);

            _migrationBuilder.InsertData(
              table: "StudentEvaluations",
              columns: new[] { "RatingScaleId", "Title", "Deleted", "Active" },
              values: toRectangular(review.Select(r => new object[] { r.RatingScaleId, r.Title, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddTaxonomyLevelTable()
        {
            _migrationBuilder.InsertData(
                   table: "TaxonomyLevels",
                   columns: new[] { "Description", "Active", "Deleted" },
                   values: new object[,]
                   {
                        { "Recall", true, false },
                        { "Application", true, false },
                        { "Analysis", true, false },
                        { "Evaluation", true, false },
                        { "Create", true, false },
                   });
        }

        protected void Development_AddTestStatusTable()
        {
            _migrationBuilder.InsertData(
                   table: "TestStatuses",
                   columns: new[] { "Description", "Active" },
                   values: new object[,]
                   {
                        { "In Development", true },
                        { "Published", true },
                        { "Inactive", true },
                        { "Draft", true },
                   });
        }

        protected void Development_AddTestTable()
        {
            string testsString = System.IO.File.ReadAllText(_path + "\\tests.json");
            List<Test> testReview = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Test>>(testsString);

            _migrationBuilder.InsertData(
              table: "Tests",
              columns: new[] { "TestStatusId", "TestTitle", "IsPublished", "RandomizeDistractors", "Deleted", "Active" },
              values: toRectangular(testReview.Select(r => new object[] { r.TestStatusId, r.TestTitle, r.IsPublished, r.RandomizeDistractors, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddTestTypeTable()
        {
            _migrationBuilder.InsertData(
                    table: "TestTypes",
                    columns: new[] { "Description", "Active" },
                    values: new object[,]
                    {
                        { "Pretest", true },
                        { "Final Test", true },
                        { "Retake", true },
                        { "CBT", true },
                        { "StudentEvaluation", true },
                    });
        }

        protected void Development_AddTestSettingTable()
        {
            _migrationBuilder.InsertData(
                     table: "TestSettings",
                     columns: new[] { "Description", "IsDefault", "IsOverride", "Active" },
                     values: new object[,]
                     {
                        { "Show Submitted Answers", false, false, true },
                        { "Show item correct/incorrect answer", false, false, true },
                        { "Randomize *Question* Order", false, false, true },
                        { "Randomize *Answer* Choices", false, false, true },
                     });
        }

        protected void Development_AddTestItemTypeTable()
        {
            _migrationBuilder.InsertData(
                    table: "TestItemTypes",
                    columns: new[] { "Description", "Active" },
                    values: new object[,]
                    {
                        { "Multiple Choice Questions", true },
                        { "Fill in the Blank", true },
                        { "True / False", true },
                        { "Short Answers", true },
                        { "Match the Column", true },
                        { "Multiple Correct Answers", true },
                    });
        }

        protected void Development_AddTestItemTable()
        {
            string idpreviewString = System.IO.File.ReadAllText(_path + "\\testItems.json");
            List<TestItem> review = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TestItem>>(idpreviewString);

            _migrationBuilder.InsertData(
              table: "TestItems",
              columns: new[] { "TestItemTypeId", "TaxonomyId", "EOId", "IsActive", "Description", "Deleted", "Active" },
              values: toRectangular(review.Select(r => new object[] { r.TestItemTypeId, r.TaxonomyId, r.EOId, r.IsActive, r.Description, r.Deleted, r.Active }).ToArray()));

        }

        protected void Development_AddILATraineeEvaluationTable()
        {
            string ilaString = System.IO.File.ReadAllText(_path + "\\ilaTraineeEvaluations.json");
            List<ILATraineeEvaluation> ilaReview = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ILATraineeEvaluation>>(ilaString);

            _migrationBuilder.InsertData(
              table: "ILATraineeEvaluations",
              columns: new[] { "TestId", "ILAId", "EvaluationTypeId", "TestTimeLimitHours", "TestTimeLimitMinutes", "Deleted", "Active" },
              values: toRectangular(ilaReview.Select(r => new object[] { r.TestId, r.ILAId, r.EvaluationTypeId, r.TestTimeLimitHours, r.TestTimeLimitMinutes, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddTestItemMatchTable()
        {
        }

        protected void Development_AddTestItemMCQTable()
        {
            string idpreviewString = System.IO.File.ReadAllText(_path + "\\testItemMCQs.json");
            List<TestItemMCQ> review = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TestItemMCQ>>(idpreviewString);

            _migrationBuilder.InsertData(
              table: "TestItemMCQs",
              columns: new[] { "TestItemId", "ChoiceDescription", "IsCorrect", "Number", "Deleted", "Active" },
              values: toRectangular(review.Select(r => new object[] { r.TestItemId, r.ChoiceDescription, r.IsCorrect, r.Number, r.Deleted, r.Active }).ToArray()));

        }

        protected void Development_AddTestItemTrueFalseTable()
        {
            string idpreviewString = System.IO.File.ReadAllText(_path + "\\testItemTrueFalses.json");
            List<TestItemTrueFalse> review = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TestItemTrueFalse>>(idpreviewString);

            _migrationBuilder.InsertData(
              table: "TestItemTrueFalses",
              columns: new[] { "TestItemId", "Choices", "IsCorrect", "Deleted", "Active" },
              values: toRectangular(review.Select(r => new object[] { r.TestItemId, r.Choices, r.IsCorrect, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddTestItemFillBlankTable()
        {
            string idpreviewString = System.IO.File.ReadAllText(_path + "\\testItemFillBlanks.json");
            List<TestItemFillBlank> review = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TestItemFillBlank>>(idpreviewString);

            _migrationBuilder.InsertData(
              table: "TestItemFillBlanks",
              columns: new[] { "TestItemId", "CorrectIndex", "Correct", "Deleted", "Active" },
              values: toRectangular(review.Select(r => new object[] { r.TestItemId, r.CorrectIndex, r.Correct, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddTestItemShortAnswerTable()
        {
            string idpreviewString = System.IO.File.ReadAllText(_path + "\\testItemShortAnswers.json");
            List<TestItemShortAnswer> review = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TestItemShortAnswer>>(idpreviewString);

            _migrationBuilder.InsertData(
              table: "TestItemShortAnswers",
              columns: new[] { "TestItemId", "Responses", "IsCaseSensitive", "AcceptableResponses", "Number", "Deleted", "Active" },
              values: toRectangular(review.Select(r => new object[] { r.TestItemId, r.Responses, r.IsCaseSensitive, r.AcceptableResponses, r.Number, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddDiscussionQuestionTable()
        {
        }

        protected void Development_AddToolCategoryTable()
        {
            _migrationBuilder.InsertData(
               table: "ToolCategories",
               columns: new[] { "Id", "Title", "Active" },
               values: new object[,]
                 {
                    { 1, "Saws", true },
                 });
        }

        protected void Development_AddSimulatorScenarioDifficultyLevelTable()
        {
            _migrationBuilder.InsertData(
                  table: "SimulatorScenarioDifficultyLevels_Old",
                  columns: new[] { "SimulatorScenarioDiffLevel", "Active" },
                  values: new object[,]
                  {
                        { "Easy", true },
                  });
        }

        protected void Development_AddSimulationScenarioSpecLookUpTable()
        {
        }

        protected void Development_AddSimulatorScenarioTable()
        {
        }

        protected void Development_AddTrainingGroup_CategoryTable()
        {
            _migrationBuilder.InsertData(
                  table: "TrainingGroup_Categories",
                  columns: new[] { "Title", "Description", "EffectiveDate", "Note", "Active" },
                  values: new object[,]
                  {
                        { "Training Group 1","This is Training Group 1",DateTime.Now,String.Empty, true },
                  });
        }

        protected void Development_AddTrainingGroupTable()
        {
            _migrationBuilder.InsertData(
                  table: "TrainingGroups",
                  columns: new[] { "CategoryId", "GroupName", "GroupDescription", "GroupNumber", "HyperLink", "PDF", "Active" },
                  values: new object[,]
                  {
                        { 1,"Training Group 1","Training Group Description 1",1,String.Empty, new byte[] { }, true },
                        { 1,"Training Group 2","Training Group Description 2",2,String.Empty, new byte[] { }, true },
                        { 1,"Training Group 3","Training Group Description 3",3,String.Empty, new byte[] { }, true },
                  });
        }

        protected void Development_AddTask_TrainingGroupTable()
        {

        }
        protected void Development_AddTaskQualificationTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\taskqualifications.json");
            List<TaskQualification> objs = JsonSerializer.Deserialize<List<TaskQualification>>(jsonString);

            _migrationBuilder.InsertData(
                table: "TaskQualifications",
                columns: new[] { "TaskId", "EmpId", "CriteriaMet", "EvaluationId", "IsReleasedToEMP", "TaskQualificationDate", "Deleted", "Active", "TQStatusId" },
                values: toRectangular(objs.Select(r => new object[] { r.TaskId, r.EmpId, r.CriteriaMet, r.EvaluationId, r.IsReleasedToEMP, r.TaskQualificationDate, r.Deleted, r.Active, r.TQStatusId }).ToArray()));
        }

        protected void Development_AddTaskQualification_Evaluator_LinksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\taskQualificationEvaluatorLinks.json");
            List<TaskQualification_Evaluator_Link> objs = JsonSerializer.Deserialize<List<TaskQualification_Evaluator_Link>>(jsonString);

            _migrationBuilder.InsertData(
                table: "TaskQualification_Evaluator_Links",
                columns: new[] { "EvaluatorId", "TaskQualificationId", "Number", "Deleted", "Active", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" },
                values: toRectangular(objs.Select(r => new object[] { r.EvaluatorId, r.TaskQualificationId, r.Number, r.Deleted, r.Active, r.CreatedBy, r.CreatedDate, r.ModifiedBy, r.ModifiedDate }).ToArray()));
        }

        protected void Development_AddActivityNotificationTable()
        {
            _migrationBuilder.InsertData(
                 table: "ActivityNotifications",
                 columns: new[] { "Title", "Active" },
                 values: new object[,]
                 {
                        { "All", true },
                        { "Tests", true },
                        { "Evaluation", true },
                        { "CBTs", true }
                 });
        }
        protected void Development_AddVersion_TrainingGroupTable()
        {

        }

        protected void Development_AddTestItemLinksTable()
        {
            string testItemLinkString = System.IO.File.ReadAllText(_path + "\\test_item_links.json");
            List<Test_Item_Link> testItemLinkReview = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Test_Item_Link>>(testItemLinkString);

            _migrationBuilder.InsertData(
              table: "Test_Item_Links",
              columns: new[] { "TestId", "TestItemId", "Sequence", "Deleted", "Active" },
              values: toRectangular(testItemLinkReview.Select(r => new object[] { r.TestId, r.TestItemId, r.Sequence, r.Deleted, r.Active }).ToArray()));

        }

        protected void Development_AddSettings_EmailNotifications()
        {
            string clientSettingsJsonString = System.IO.File.ReadAllText(_path + "\\clientsettings_notifications.json");
            List<ClientSettings_Notification> clientSettings = JsonSerializer.Deserialize<List<ClientSettings_Notification>>(clientSettingsJsonString);

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notifications",
              columns: new[] { "Name", "Enabled", "TimingText", "Active" },
              values: toRectangular(clientSettings.Select(r => new object[] { r.Name, r.Enabled, r.TimingText, true }).ToArray()));

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_AvailableCustomSettings",
              columns: new[] { "ClientSettingsNotificationId", "Setting", "Active" },
              values: toRectangular(clientSettings
                    .SelectMany(r => r.AvailableCustomSettings)
                    .Select(r => new object[] { r.ClientSettingsNotificationId, r.Setting, true })
                    .ToArray()));

            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_CustomSettings",
                columns: new[] { "ClientSettingsNotificationId", "Key", "Value", "Active" },
                values: toRectangular(clientSettings
                    .SelectMany(r => r.CustomSettings)
                    .Select(r => new object[] { r.ClientSettingsNotificationId, r.Key, r.Value, true })
                    .ToArray()));

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Active" },
              values: toRectangular(clientSettings
                .SelectMany(r => r.Steps)
                .Select(r => new object[] { r.ClientSettingsNotificationId, r.Template, r.Order, true })
                .ToArray()));

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Step_AvailableCustomSettings",
              columns: new[] { "ClientSettingsNotificationStepId", "Setting", "Active" },
              values: toRectangular(clientSettings
               .SelectMany(r => r.Steps)
                .SelectMany(r => r.AvailableCustomSettings)
                .Select(r => new object[] { r.ClientSettingsNotificationStepId, r.Setting, true }).ToArray()));

            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_Step_CustomSettings",
                columns: new[] { "ClientSettingsNotificationStepId", "Key", "Value", "Active" },
                values: toRectangular(clientSettings
                .SelectMany(r => r.Steps)
                .SelectMany(r => r.CustomSettings)
                .Select(r => new object[] { r.ClientSettingsNotificationStepId, r.Key, r.Value, true }).ToArray()));
        }
        protected void Development_AddTrainingProgramTypeTable()
        {
            _migrationBuilder.InsertData(
                 table: "TrainingProgramTypes",
                 columns: new[] { "TrainingProgramTypeTitle", "Active", "Deleted" },
                 values: new object[,]
                 {
                        { "Initial Training Program", true, false},
                        { "Continuing Training Program", true, false},
                        { "Cycle Training Program", true, false},
                 });
        }

        protected void Development_UpdateTrainingProgramTypeTable_VersionIsYear()
        {
            var trainingProgramTypes = new object[,]
            {
                { "Initial Training Program", true},
                { "Continuing Training Program", false},
                { "Cycle Training Program", true},
            };

            for (int i = 0; i < trainingProgramTypes.GetLength(0); i++)
            {
                _migrationBuilder.UpdateData(
                    table: "TrainingProgramTypes",
                    keyColumn: "TrainingProgramTypeTitle",
                    keyValue: trainingProgramTypes[i, 0],
                    column: "VersionIsYear",
                    value: trainingProgramTypes[i, 1]);
            }
        }

        protected void Development_AddTaskQualificationStatusTable()
        {
            _migrationBuilder.InsertData(
                table: "TaskQualificationStatuses",
                columns: new[] { "Name", "Description", "Active" },
                values: new object[,]
                {
                        {"Trainee Initial Qualification","Employee is a Trainee for the position, and the qualification is his/her Initial Qualification record to indicate successful performance on the Task", true },
                        {"On Time","Employee qualified on the Task within the requalification due date window", true },
                        {"Pending","Employee has not qualified on the Task, but the current date is still within the requalification due date window", true },
                        {"Delayed","Employee qualified on the Task outside of the requalification due date window", true },
                        {"Overdue","The 6-month window has passed, Employee has not qualified", true },
                        {"Requalification Not Required","Employee was flagged a Trainee for the position at the time of the Task change. Employee qualified on revised task as part of initial training", true },
                        {"No Position Qual Date","The Employee is not flagged as a Trainee and there is no Position Qual Date in the Employee window to use to confirm the task qual against", true },
                        {"Completed","Task Requalification is completed on Emp side", true }
                }

                );
        }

        protected void Development_AddRatingScaleNTable()
        {
            _migrationBuilder.InsertData(
                 table: "RatingScaleNs",
                 columns: new[] { "RatingScaleDescription", "Active" },
                 values: new object[,]
                 {
                        { "1 = Strongly Disagree, 2 = Disagree, 3 = Neutral, 4 = Agree, 5 = Strongly Agree", true },
                        { "1 = Poor, 2 = Fair, 3 = Good, 4 = Very Good, 5 = Excellent",true },
                        { "1 = Poor, 2 = Unsatisfactory, 3 = Satisfactory, 4 = Very Satisfactory, 5 = Outstanding",true },
                        { "1 = Very Poor, 2 = Poor, 3 = Average, 4 = Good, 5 = Very Good",true },

                        { "1 = Strongly Disagree, 2 = Disagree, 3 = Agree, 4 = Strongly Agree",true },
                        { "1 = Poor, 2 = Fair, 3 = Very Good, 4 = Excellent",true },
                        { "1 = Poor, 2 = Unsatisfactory, 3 = Very Satisfactory, 4 = Outstanding",true },
                        { "1 = Very Poor, 2 = Poor, 3 = Good, 4 = Very Good",true },

                        { "1 = Disagree, 2 = Neutral, 3 = Agree",true },
                        { "1 = Poor, 2 = Good, 3 = Excellent",true },
                        { "1 = Poor, 2 = Satisfactory, 3 = Outstanding",true },
                        { "1 = Very Poor, 2 = Average, 3 = Very Good",true },

                        { "1 = Yes, 2 = No", true }
                 });
        }
        protected void Development_RatingScaleExpandedTable()
        {
            _migrationBuilder.InsertData(
                 table: "RatingScaleExpanded",
                 columns: new[] { "RatingScaleNId", "Ratings", "Description", "Active" },
                 values: new object[,]
                 {
                        { 1,1, "Strongly Disagree",true },
                        { 1,2, "Disagree",true },
                        { 1,3, "Neutral",true },
                        { 1,4, "Agree",true },
                        { 1,5, "Strongly Agree",true },

                        { 2,1,"Poor",true },
                        { 2,2,"Fair",true },
                        { 2,3,"Good" ,true},
                        { 2,4,"Very Good",true },
                        { 2,5,"Excellent",true },

                        { 3,1,"Poor",true },
                        { 3,2,"Unsatisfactory",true },
                        { 3,3,"Satisfactory" ,true},
                        { 3,4,"Very Satisfactory" ,true},
                        { 3,5,"Outstanding",true },

                        { 4,1,"Very Poor",true },
                        { 4,2,"Poor" ,true},
                        { 4,3,"Average" ,true},
                        { 4,4,"Good" ,true},
                        { 4,5,"Very Good",true },

                        { 5,1,"Strongly Disagree",true },
                        { 5,2,"Disagree" ,true},
                        { 5,3,"Agree" ,true},
                        { 5,4,"Strongly Agree",true },


                        { 6,1,"Poor",true },
                        { 6,2,"Fair",true },
                        { 6,3,"Very Good",true },
                        { 6,4,"Excellent" ,true},

                        { 7,1,"Poor",true },
                        { 7,2,"Unsatisfactory",true },
                        { 7,3,"Very Satisfactory",true },
                        { 7,4,"Outstanding",true },

                        { 8,1,"Very Poor" ,true},
                        { 8,2,"Poor",true },
                        { 8,3,"Good",true },
                        { 8,4,"Very Good",true },

                        { 9,1,"Disagree" ,true},
                        { 9,2,"Neutral",true },
                        { 9,3,"Agree",true },

                        { 10,1,"Poor",true },
                        { 10,2,"Good",true },
                        { 10,3,"Excellent",true },

                        { 11,1,"Poor",true },
                        { 11,2,"Satisfactory",true },
                        { 11,3,"Outstanding",true },

                        { 12,1,"Very Poor",true },
                        { 12,2,"Average",true },
                        { 12,3,"Very Good" ,true},

                        { 13,1,"Yes",true },
                        { 13,2,"No" ,true}
                 });
        }

        protected void Development_AddClassSchedule_Roster_StatusesTable()
        {
            _migrationBuilder.InsertData(
                table: "ClassSchedule_Roster_Statuses",
                columns: new[] { "Name", "Active" },
                values: new object[,]
                {
                    { "Not Started",true },
                    { "In Progress",true },
                    { "Completed",true },
                    { "Released",true },
                });
        }

        protected void Development_AddClassScheduleTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\classSchedule.json");
            List<ClassSchedule> objs = JsonSerializer.Deserialize<List<ClassSchedule>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ClassSchedules",
                columns: new[] { "IsRecurring", "ProviderID", "InstructorId", "LocationId", "ILAID", "StartDateTime", "EndDateTime", "Deleted", "Active", "IsStartAndEndTimeEmpty" },
                values: toRectangular(objs.Select(r => new object[] { r.IsRecurring, r.ProviderID, r.InstructorId, r.LocationId, r.ILAID, r.StartDateTime, r.EndDateTime, r.Deleted, r.Active, false }).ToArray()));

        }
        protected void Development_AddClassSchedule_Evaluation_RosterTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\classSchedule_Evaluation_Roster.json");
            List<ClassSchedule_Evaluation_Roster> objs = JsonSerializer.Deserialize<List<ClassSchedule_Evaluation_Roster>>(jsonString);

            _migrationBuilder.InsertData(
                     table: "ClassSchedule_Evaluation_Roster",
                     columns: new[] { "ClassScheduleId", "StudentEvaluationId", "EmployeeId", "IsCompleted", "IsStarted", "IsReleased", "IsRecalled", "Deleted", "Active" },
                     values: toRectangular(objs.Select(r => new object[] { r.ClassScheduleId, r.StudentEvaluationId, r.EmployeeId, r.IsCompleted, r.IsStarted, r.IsReleased, r.IsRecalled, r.Deleted, r.Active }).ToArray()));
        }
        protected void Development_AddClassScheduleEmployeesTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\classScheduleEmployees.json");
            List<ClassSchedule_Employee> objs = JsonSerializer.Deserialize<List<ClassSchedule_Employee>>(jsonString);

            _migrationBuilder.InsertData(
                table: "ClassScheduleEmployees",
                columns: new[] { "ClassScheduleId", "PreTestStatusId", "TestStatusId", "RetakeStatusId", "CBTStatusId", "FinalScore", "EmployeeId", "IsEnrolled", "IsWaitlisted", "IsDropped", "IsDenied", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.ClassScheduleId, r.PreTestStatusId, r.TestStatusId, r.RetakeStatusId, r.CBTStatusId, r.FinalScore, r.EmployeeId, r.IsEnrolled, r.IsWaitlisted, r.IsDropped, r.IsDenied, r.Deleted, r.Active }).ToArray()));

        }

        protected void Development_AddEvaluationMethodTable()
        {
            _migrationBuilder.InsertData(
                table: "EvaluationMethods",
                columns: new[] { "Description", "Active" },
                values: new object[,]
                {
                    { "OJT", true },
                    { "SIM", true },
                    { "Others", true },
                });
        }

        protected void Development_AddIDP_ReviewStatusTable()
        {
            _migrationBuilder.InsertData(
                table: "IDP_ReviewStatuses",
                columns: new[] { "Name", "Description", "Active" },
                values: new object[,]
                {
                    {"Not Started","",true},
                    {"In Progress","",true},
                    {"Completed","",true},
                });
        }
        protected void Development_AddIDP_ReviewTable()
        {
            string idpreviewString = System.IO.File.ReadAllText(_path + "\\idp_review.json");
            List<IDP_Review> review = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IDP_Review>>(idpreviewString);

            _migrationBuilder.InsertData(
              table: "IDP_Review",
              columns: new[] { "StatusId", "EmployeeId", "Title", "IsStarted", "ReleaseDate", "CompletedDate", "EndDate", "IDP_ReviewStatusId", "Deleted", "Active" },
              values: toRectangular(review.Select(r => new object[] { r.StatusId, r.EmployeeId, r.Title, r.IsStarted, r.ReleaseDate, r.CompletedDate, r.EndDate, r.StatusId, false, true }).ToArray()));
        }

        protected void Development_AddDatabaseSettings()
        {
            #region ClientSettings_GeneralSettings

            string clientSettingsGeneralJsonString = System.IO.File.ReadAllText(_path + "\\clientSettings_GeneralSettings.json");
            ClientSettings_GeneralSettings clientSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<ClientSettings_GeneralSettings>(clientSettingsGeneralJsonString);

            _migrationBuilder.InsertData(
              table: "ClientSettings_GeneralSettings",
              columns: new[] { "CompanyName", "CompanyLogo", "DateFormat", "ClassStartEndTimeFormat", "CompanySpecificCoursePassingScore", "Active" },
              values: new object[] { clientSettings.CompanyName, clientSettings.CompanyLogo, clientSettings.DateFormat, clientSettings.ClassStartEndTimeFormat, clientSettings.CompanySpecificCoursePassingScore, true });

            #endregion

            #region ClientSettings_LabelReplacement

            string clientSettingsLabelReplacementsJsonString = System.IO.File.ReadAllText(_path + "\\clientSettings_LabelReplacements.json");
            List<ClientSettings_LabelReplacement> clientSettingsLabelReplacement = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClientSettings_LabelReplacement>>(clientSettingsLabelReplacementsJsonString);

            _migrationBuilder.InsertData(
             table: "ClientSettings_LabelReplacements",
             columns: new[] { "DefaultLabel", "LabelReplacement", "Active" },
             values: toRectangular(clientSettingsLabelReplacement.Select(r => new object[] { r.DefaultLabel, r.LabelReplacement, true }).ToArray()));

            #endregion

            //#region ClientSettings_License

            string clientSettingsLicenseJsonString = System.IO.File.ReadAllText(_path + "\\clientSettings_License.json");
            List<ClientSettings_License> clientSettingsLicense = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClientSettings_License>>(clientSettingsLicenseJsonString);

            _migrationBuilder.InsertData(
             table: "ClientSettings_Licenses",
             columns: new[] { "ActivationCode", "ClientId", "Active" },
             values: toRectangular(clientSettingsLicense.Select(r => new object[] { r.ActivationCode, r.ClientId, r.Active }).ToArray()));

            //#endregion

            //#region DashboardSetting

            string dashboardSettingsInfoJsonString = System.IO.File.ReadAllText(_path + "\\dashboard_settings.json");
            List<DashboardSetting> dashboardSetting = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DashboardSetting>>(dashboardSettingsInfoJsonString);

            _migrationBuilder.InsertData(
             table: "DashboardSettings",
             columns: new[] { "Name", "GroupName", "CategoryName", "Active" },
             values: toRectangular(dashboardSetting.Select(r => new object[] { r.Name, r.GroupName, r.CategoryName, true }).ToArray()));

            //#endregion

            //#region ClientUserSettings_DashboardSetting

            string jsonString = System.IO.File.ReadAllText(_path + "\\persons.json");
            List<Person> persons = JsonSerializer.Deserialize<List<Person>>(jsonString);

            List<ClientUserSettings_DashboardSetting> clientUserSettingsDashboardSetting = new List<ClientUserSettings_DashboardSetting>();

            foreach (var person in persons)
            {
                foreach (var setting in dashboardSetting)
                {
                    clientUserSettingsDashboardSetting.Add(new ClientUserSettings_DashboardSetting(persons.IndexOf(person) + 1, dashboardSetting.IndexOf(setting) + 1, true));
                }
            }

            _migrationBuilder.InsertData(
             table: "ClientUserSettings_DashboardSettings",
             columns: new[] { "ClientUserId", "DashboardSettingId", "Enabled", "Active" },
             values: toRectangular(clientUserSettingsDashboardSetting.Select(r => new object[] { r.ClientUserId, r.DashboardSettingId, r.Enabled, true }).ToArray()));

            //#endregion
        }

        protected void Development_AddMetaILAConfigurationPublishOption()
        {
            _migrationBuilder.InsertData(
                  table: "MetaILAConfigurationPublishOptions",
                  columns: new[] { "Description", "Active" },
                  values: new object[,]
                  {
                        { "Upon Clicking Start",true },
                        { "Upon Passing Previous ILA",true },
                        { "Upon Completion of Previous ILA",true },
                  });
        }

        protected void Development_AddMetaILAAssessment()
        {
            _migrationBuilder.InsertData(
                  table: "MetaILAAssessments",
                  columns: new[] { "Description", "Active" },
                  values: new object[,]
                  {
                        { "Create Meta ILA Summary Test",true },
                        {"Create Meta ILA Student Evaluation",true }
                  });
        }

        protected void Development_AddMetaILA_Status()
        {
            _migrationBuilder.InsertData(
                  table: "MetaILA_Statuses",
                  columns: new[] { "Name", "Active" },
                  values: new object[,]
                  {
                        {"Published",true },
                        {"Draft",true }
                  });
        }

        protected void Development_AddLocationCategories()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\location_categories.json");
            List<Location_Category> objs = JsonSerializer.Deserialize<List<Location_Category>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Location_Categories",
                columns: new[] { "LocCategoryTitle", "EffectiveDate", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.LocCategoryTitle, r.EffectiveDate, r.Deleted, r.Active }).ToArray()));
        }

        protected void Development_AddLocation()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\location.json");
            List<Location> objs = JsonSerializer.Deserialize<List<Location>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocCategoryID", "LocNumber", "LocName", "EffectiveDate", "Deleted", "Active" },
                values: toRectangular(objs.Select(r => new object[] { r.LocCategoryID, r.LocNumber, r.LocName, DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), r.Deleted, r.Active }).ToArray()));
        }
        protected void Development_AddPosition_TasksTable()
        {
            string jsonString = System.IO.File.ReadAllText(_path + "\\position_tasks.json");
            List<Position_Task> persons = JsonSerializer.Deserialize<List<Position_Task>>(jsonString);

            _migrationBuilder.InsertData(
                table: "Position_Tasks",
                columns: new[] { "TaskId", "PositionId", "IsR6Impacted", "Active" },
                values: toRectangular(persons.Select(r => new object[] { r.TaskId, r.PositionId, false, true }).ToArray()));
        }
        protected void Development_AddDocumentTypesTable()
        {
            _migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Name", "LinkedDataType", "Deleted", "Active" },
                values: new object[,]
                  {
                       {"Training Program Reviews","TrainingProgramReviews",false,true},
                       {"Employee Details","Employees",false,true},
                       {"DIF Survey","Positions",true,true},
                       {"OJT","Employees",true,true},
                       {"Other Employee Course Completion Info","Employees",false,true},
                       {"Sign In Sheet","ClassSchedules",false,true},
                       {"Student Evaluation","ClassSchedules",false,true},
                       {"Task Qualification","TaskQualifications",false,true},
                       {"Test","ClassScheduleRosters",false,true}
                  });
        }

        protected void Development_AddInstructorWorkbook()
        {
            _migrationBuilder.InsertData(
                 table: "InstructorWorkbook_Phases",
                 columns: new[] { "CoursePhaseDescription", "Deleted", "Active" },
                 values: new object[,]
                  {
                                   { "Analysis", false, true },
                                   { "Design", false, true },
                                   { "Develop", false, true },
                                   { "Implement", false, true},
                                   { "Evaluate", false, true}
                  });
        }

        protected void Development_UpdateMetaILAConfigurationPublishOption()
        {
            _migrationBuilder.UpdateData(
                table: "MetaILAConfigurationPublishOptions",
                keyColumn: "Description",
                keyValue: "Upon Clicking Start",
                column: "Description",
                value: "On Demand");
        }
        protected void Development_UpdateDocumentTypeTable()
        {
            _migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Name", "LinkedDataType", "Deleted", "Active" },
                values: new object[,]
                  {
                      {"Tool","ToolCategorys",false,true}
                  });
        }
        protected void Development_AddMetaIlaEmployees()
        {
            _migrationBuilder.InsertData(
              table: "ClientSettings_Notifications",
              columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
              values: new object[,]
                  {
                      {"Meta ILA - Self Paced Released", true, "Sent after a Self Paced class is released due to completion of a step in a Meta ILA", false, true},
                      {"Meta ILA - Employee - Self Registration Needed", true, "Sent to employee when they need to enroll in a self-registration enabled class after completing a step in an Meta ILA", false, true},
                      {"Meta ILA - Admin - Self Registration Needed", true, "Sent to an admin when they may need to assist an employee in enrolling in a self-registration enabled class after completing a step in an Meta ILA", false, true},
                      {"Meta ILA - Employee - Registration Needed", true, "Sent to employee when an admin needs to enroll them in a self-registration enabled class after completing a step in an Meta ILA", false, true},
                      {"Meta ILA - Admin - Registration Needed", true, "Sent to an admin when may need to assist an employee in enrolling in a self-registration enabled class after completing a step in an Meta ILA", false, true},
                      {"Meta ILA - Coursework Complete", true, "Sent to an employee when have completed the coursework for a Meta ILA.  Informs the employee about tests and evaluations to complete.", false, true},
                  });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
                      {16, @"
Hello @Model.EmployeeFirstName @Model.EmployeeLastName

Congraulations on completing the course @Model.PreviousILATitle.

Because you are enrolled in a Meta ILA, your course @Model.ILATitle has been already released.

Please log into your EMP Dashboard to complete the course.
                              
If you are unable to complete the course by the due date listed, notify your Training Administrator as soon as possible. 

Thank you!", 1, false, true },
                      {17, @"Hello @Model.EmployeeFirstName @Model.EmployeeLastName

Congraulations on completing the course @Model.PreviousILATitle.

Because you are enrolled in a Meta ILA, you need to enroll yourself in the course @Model.ILATitle.

@if(Model.RegistrationsAvailable)
{

Please log into your EMP Dashboard and use the self registration portal to enroll in a class.

If you are unable to attend the classes listed notify your Training Administrator as soon as possible.

}
else
{
	There are no upcoming classes for the ILA.Please notification your Training Administrator as soon as possible
}


Thank you!", 1, false, true },
                      {18, @"Hello,

Please be aware that @Model.EmployeeFirstName @Model.EmployeeLastName has completed the ILA. @Model.PreviousILATitle.

As part of the Meta ILA @Model.MetaILATitle they are now required to take the ILA @Model.ILATitle.

@if(Model.RegistrationsAvailable)
{
The employee has been notified that they need to self register in the ILA.  However, it is possible that the employee may request a different time.  They have been instructed to reach out to their Training Administrator if the times do not work for them.
}
else
{
The employee has been notified that they need to self register in the ILA.  However currently there are no available classes for the employee to register in.  You will need to create an additional class for the employee to continue.
}

Thank you.", 1, false, true },
                      {19, @"Hello @Model.EmployeeFirstName @Model.EmployeeLastName

Congraulations on completing the course @Model.PreviousILATitle.

Because you are enrolled in a Meta ILA, you will be enrolled in the next course @Model.ILATitle.

Please cooridinate your Training Administrator as soon as possible                              

Thank you!", 1, false, true },
                      {20, @"
Hello,

Please be aware that @Model.EmployeeFirstName @Model.EmployeeLastName has completed the ILA. @Model.PreviousILATitle.

As part of the Meta ILA @Model.MetaILATitle they are now required to take the ILA @Model.ILATitle.

The employee has been notified that they need to be enrolled in the next ILA however that ILA is not configured to allow self-registration.  You will need to assist the employee for them to continue.

Thank you.", 1, false, true },
                      {21, @"
Hello,

Congraulations on completing the course @Model.PreviousILATitle.

You have now completed the coursework for the your Meta ILA.

To complete your training on this Meta ILA you need to complete the associated test.  It has been released to your EMP portal.  

Thank you.", 1, false, true }
                  });
        }

        protected void Development_AddMetaIlaTestType()
        {
            _migrationBuilder.InsertData(
                  table: "TestTypes",
                  columns: new[] { "Description", "Active", "Deleted" },
                  values: new object[,]
                  {
                        { "Meta ILA Summary", true, false }
                  });
        }


        protected void Development_UpdateClientSettings_GeneralSettings_DateFormat()
        {
            _migrationBuilder.UpdateData(
                table: "ClientSettings_GeneralSettings",
                keyColumn: "CompanyName",
                keyValue: "QualityTrainingSystems",
                column: "DateFormat",
                value: "MM/dd/yyyy");
        }

        protected void Development_UpdateTaskReQualificationEmpSignOffData()
        {
            _migrationBuilder.UpdateData(
                table: "TaskReQualificationEmp_SignOffs",
                keyColumn: "IsCompleted",
                keyValue: true,
                columns: new[] { "IsTraineeSignOff", "IsEvaluatorSignOff" },
                values: new object[] { true, true }
                );
        }

        protected void Development_AddClassSchedule_SelfRegistrationOptionsData()
        {
            _migrationBuilder.Sql(@"INSERT INTO ClassSchedule_SelfRegistrationOptions
	        (
		        ClassScheduleId,
		        MakeAvailableForSelfReg,
		        RequireAdminApproval,
		        AcknowledgeRegDisclaimer,
		        RegDisclaimer,
		        LimitForLinkedPositions,
		        CloseRegOnStartDate,
		        EnableWaitlist,
		        SendApprovedEmail,
		        Deleted,
		        Active,
		        CreatedBy,
		        CreatedDate
		    )
            SELECT  cs.Id,
		        MakeAvailableForSelfReg,
		        RequireAdminApproval,
		        AcknowledgeRegDisclaimer,
		        RegDisclaimer,
		        LimitForLinkedPositions,
		        CloseRegOnStartDate,
		        EnableWaitlist,
		        SendApprovedEmail,
		        o.Deleted,
		        o.Active,
		        o.CreatedBy,
		        o.CreatedDate
            FROM [ILA_SelfRegistrationOptions] o
	        inner join [ClassSchedules] cs on cs.ILAID = o.Ilaid");
        }

        protected void Development_ModifySelfRegistrationOptions()
        {
            //_migrationBuilder.Sql(@"update ilas set ClassSize = ILA_SelfRegistrationOptions.classSize 
            //    from ILA_SelfRegistrationOptions
            //    where ILA_SelfRegistrationOptions.ILAId = ilas.id

            //    update ilas set ClassSize = IsNull(ClassSize, 30)

            //    update ClassSchedules set ClassSize = ILA_SelfRegistrationOptions.classSize 
            //    from ILA_SelfRegistrationOptions
            //    where ILA_SelfRegistrationOptions.ILAId = ClassSchedules.ILAId");
        }

        protected void Development_ModifyClientSettingNotificationTemplates()
        {
            string clientSettingsJsonString = System.IO.File.ReadAllText(_path + "\\clientsettings_notifications.json");
            List<ClientSettings_Notification> clientSettings = JsonSerializer.Deserialize<List<ClientSettings_Notification>>(clientSettingsJsonString);
            foreach (var clientSettingNotification in clientSettings)
            {
                foreach (var step in clientSettingNotification.Steps)
                {
                    var query = @" UPDATE ClientSettings_Notification_Steps SET Template = '" + step.Template.Replace("'", "''") + @"'  
                                WHERE ClientSettingsNotificationId IN (SELECT id FROM ClientSettings_Notifications WHERE name = '" + clientSettingNotification.Name + @"') 
                                AND [Order] = " + step.Order + @";";
                    _migrationBuilder.Sql(query);
                }
            }
        }

        protected void Development_UpdateCertifyingBodiesTableData()
        {
            _migrationBuilder.UpdateData(
            table: "CertifyingBodies",
            keyColumn: "Name",
            keyValue: "NERC",
            column: "EnableCertifyingBodyLevelCEHEditing",
            value: true
            );
        }
        protected void Development_UpdateILATaskObjectiveLinksSequenceNumber()
        {
            _migrationBuilder.Sql(@" WITH TaskSequence AS (
                                    SELECT ilaid,taskid,ROW_NUMBER() OVER (PARTITION BY ilaid ORDER BY id) AS new_sequence FROM ILA_TaskObjective_Links where Deleted=0
                                    )
                                    UPDATE ILA_TaskObjective_Links
                                    SET SequenceNumber = TaskSequence.new_sequence
                                    FROM ILA_TaskObjective_Links AS ilaTask
                                    INNER JOIN TaskSequence ON ilaTask.taskid = TaskSequence.taskid and ilaTask.ILAId=TaskSequence.ILAId; "
                                );
        }

        protected void Development_AddDIFSurvey_DevStatus()
        {

            _migrationBuilder.InsertData(
                table: "DIFSurvey_DevStatus",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "Draft", true },
                  { "Published", true },
                });
        }

        protected void Development_AddDIFSurvey_Employee_Status()
        {

            _migrationBuilder.InsertData(
                table: "DIFSurvey_Employee_Status",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "Not Started", true },
                  { "In Progress", true },
                  { "Completed", true },
                });
        }
        protected void Development_AddDIFSurvey_Task_Status()
        {

            _migrationBuilder.InsertData(
                table: "DIFSurvey_Task_Status",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "Initial Training", true },
                  { "Continuous Training", true },
                  { "No Training Required", true },
                  { "No Responses Yet", true }
                });
        }

        protected void Development_AddDIFSurvey_Task_TrainingFrequency()
        {

            _migrationBuilder.InsertData(
                table: "DIFSurvey_Task_TrainingFrequency",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "Quarterly", true },
                  { "Annually", true },
                  { "Every 2 Years", true },
                });
        }

        protected void Development_AddDIFSurvey()
        {
            _migrationBuilder.InsertData(
                table: "DIFSurvey",
                columns: new[] { "Title", "PositionId", "StartDate", "DueDate", "DevStatusId", "Active" },
                 values: new object[,]
                {
                  { "Survey 1", 1, "2024-02-29 08:00:00","2024-03-05 17:00:00",1,true },
                  { "Survey 2", 2, "2024-02-28 10:00:00","2024-03-04 12:00:00",2,true },
                  { "Survey 3", 3, "2024-02-27 12:00:00","2024-03-03 10:00:00",1,true },
                  { "Survey 4", 4, "2024-02-26 15:00:00","2024-03-02 15:00:00",2,true },
                  { "Survey 5", 5, "2024-02-26 17:00:00","2024-03-02 17:00:00",1,true }
                });
        }

        protected void Development_AddDIFSurvey_Employee()
        {
            _migrationBuilder.InsertData(
                table: "DIFSurvey_Employee",
                columns: new[] { "DIFSurveyId", "EmployeeId", "StatusId", "Active" },
                 values: new object[,]
                {
                  { 1,1,1,true},
                  { 2,2,2,true},
                  { 3,3,3,true},
                  { 4,4,1,true},
                  { 5,5,2,true}
                });
        }
        protected void Development_AddDIFSurvey_Task()
        {
            _migrationBuilder.InsertData(
                table: "DIFSurvey_Task",
                columns: new[] { "DifSurveyId", "TaskId", "TrainingStatus_CalculatedId", "TrainingStatus_OverrideId", "DIFSurvey_Task_TrainingFrequencyId", "Active" },
                 values: new object[,]
                {
                  { 1,1,1,2,1,true},
                  { 2,2,2,3,2,true},
                  { 3,3,3,3,3,true},
                  { 4,4,1,2,2,true},
                  { 5,5,4,4,2,true}
                });
        }

        protected void Development_AddDIFSurvey_Employee_Response()
        {
            _migrationBuilder.InsertData(
                table: "DIFSurvey_Employee_Response",
                columns: new[] { "DIFSurvey_EmployeeId", "DIFSurvey_TaskId", "NA", "Active" },
                 values: new object[,]
                {
                  { 1,1,false,true},
                  { 2,2,false,true},
                  { 3,3,false,true},
                  { 4,4,false,true},
                  { 5,5,false,true}

                });
        }

        protected void Development_AddEmailNotification_AdminEmployeePortalCompletions()
        {
            _migrationBuilder.InsertData(
              table: "ClientSettings_Notifications",
              columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
              values: new object[,]
                  {
                      {"Admin - Employee Portal Completions", true, "To Do Admin - Employee Portal Completions Timing Text", false, true}
                  });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
                      {22, "This email is in testing", 1, false, true }
                  });
        }

        protected void Development_UpdateInstructorWorkbook_ILADesign_Segments()
        {
            _migrationBuilder.Sql(@"UPDATE [dbo].[InstructorWorkbook_ILADesign_Segments]
            SET SegmentTitle = CONCAT('Segment', Id)");
        }

        protected void Development_AddClientSettings_Feature()
        {

            _migrationBuilder.InsertData(
                table: "ClientSettings_Features",
                columns: new[] { "Feature", "Enabled", "Active", "Deleted" },
                 values: new object[,]
                {
                  { "RSAW", true,true,false },
                  { "CEH Upload", true,true,false }
                });
        }

        protected void Development_UpdateClientSettings_LabelReplacementsEnhanced()
        {
            _migrationBuilder.InsertData(
             table: "ClientSettings_LabelReplacements",
             columns: new[] { "DefaultLabel", "Active" },
              values: new object[,]
                 {
                      {"ILA", true },
                      {"My Data", true },
                      {"Instructor",true }
                 });

            _migrationBuilder.UpdateData(
             table: "ClientSettings_LabelReplacements",
             keyColumns: new[] { "Id" },
             keyValues: new object[] { 4 },
             columns: new[] { "DefaultLabel" },
             values: new object[] { "Enabling Objective" }
             );

            _migrationBuilder.UpdateData(
               table: "ClientSettings_LabelReplacements",
               keyColumns: new[] { "Id" },
               keyValues: new object[] { 5 },
               columns: new[] { "DefaultLabel" },
               values: new object[] { "Certification" }
               );

            _migrationBuilder.UpdateData(
              table: "ClientSettings_LabelReplacements",
              keyColumns: new[] { "Id" },
              keyValues: new object[] { 6 },
              columns: new[] { "DefaultLabel" },
              values: new object[] { "Procedure" }
              );

            _migrationBuilder.UpdateData(
              table: "ClientSettings_LabelReplacements",
              keyColumns: new[] { "Id" },
              keyValues: new object[] { 7 },
              columns: new[] { "DefaultLabel" },
              values: new object[] { "Safety Hazard" }
              );

            _migrationBuilder.UpdateData(
             table: "ClientSettings_LabelReplacements",
             keyColumns: new[] { "Id" },
             keyValues: new object[] { 8 },
             columns: new[] { "DefaultLabel" },
             values: new object[] { "Tool" }
             );

            _migrationBuilder.UpdateData(
             table: "ClientSettings_LabelReplacements",
             keyColumns: new[] { "Id" },
             keyValues: new object[] { 9 },
             columns: new[] { "DefaultLabel" },
             values: new object[] { "Regulatory Requirement" }
             );

            _migrationBuilder.UpdateData(
              table: "ClientSettings_LabelReplacements",
              keyColumns: new[] { "Id" },
              keyValues: new object[] { 10 },
              columns: new[] { "DefaultLabel" },
              values: new object[] { "Definition" }
              );

            _migrationBuilder.UpdateData(
             table: "ClientSettings_LabelReplacements",
             keyColumns: new[] { "Id" },
             keyValues: new object[] { 11 },
             columns: new[] { "DefaultLabel" },
             values: new object[] { "Instruction" }
             );

            _migrationBuilder.UpdateData(
                table: "ClientSettings_LabelReplacements",
                keyColumns: new[] { "Id" },
                keyValues: new object[] { 12 },
                columns: new[] { "DefaultLabel" },
                values: new object[] { "Location" }
                );

        }

        protected void Development_Update_TblClientSettings_GeneralSettings()
        {
            _migrationBuilder.Sql("UPDATE ClientSettings_GeneralSettings SET CompanySpecificCoursePassingScore = '65'");
        }
        protected void Development_UpdateActivityNotificationsTable()
        {
            _migrationBuilder.Sql("DELETE FROM ActivityNotifications");
            _migrationBuilder.InsertData(
                 table: "ActivityNotifications",
                 columns: new[] { "Title", "Active" },
                 values: new object[,]
                 {
                        { "Tests", true },
                        { "Online Courses", true },
                        { "Procedure Reviews", true },
                        { "Task & Skill Qualifications", true },
                        { "DIF Surveys", true },
                        { "Gap Surveys", true },
                        { "Student Evaluations", true },
                 });
        }

        protected void Development_AddAdminEmailNotificationStepsSettings()
        {
            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Step_AvailableCustomSettings",
              columns: new[] { "ClientSettingsNotificationStepId", "Setting", "Active" },
              values: new object[,] { { 24, "Email Frequency", true } }
              );
            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_Step_CustomSettings",
                columns: new[] { "ClientSettingsNotificationStepId", "Key", "Value", "Active" },
                values: new object[,] { { 24, "Email Frequency", "Weekly", true } }
              );
        }

        protected void Development_UpdateAdminEmailNotificationTimingText()
        {
            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notifications",
             keyColumns: new[] { "Id", "Name" },
             keyValues: new object[] { 22, "Admin - Employee Portal Completions" },
             column: "TimingText",
             value: "Once enabled, this email will be sent to all QTD Users based on the intervals set below");
        }

        protected void Development_UpdateClientSettingsNotification_Admin()
        {
            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notifications",
             keyColumns: new[] { "Id", "Name" },
             keyValues: new object[] { 22, "Admin - Employee Portal Completions" },
             column: "Enabled",
             value: false);
        }


        protected void Development_UpdateAdminEmailNotificationStepsSettings()
        {
            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 4, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods; Hello @Model.EmployeeFirstName @Model.EmployeeLastName In order to receive
                        credit for completion of @Model.CourseTitle, you must also complete the online test.You can access the test through the
                        Employee Portal(EMP).Please review the table below for further details. <table>
                            <tr>
                                <td> Course Title </td>
                                <td> @Model.CourseTitle </td>
                            </tr>
                            <tr>
                                <td> Instructor / Location </td>
                                <td> @Model.Instructor / @Model.Location </td>
                            </tr>
                            <tr>
                                <td> Class Dates </td>
                                <td> @Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) -
                                    @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                            </tr>
                            <tr>
                                <td> Test Due Date </td>
                                <td> @Model.TestDueDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                            </tr>
                        </table>
                        If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon
                        as possible.Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 2, 3 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                        Hello  @Model.EmployeeFirstName @Model.EmployeeLastName
                        Your immediate attention is required.This is an urgent reminder from the Training Department that your
                        @Model.CertificateName certificate @Model.CertificateNumber
                        will expire in @Model.DaysUntilCertificationExpiration days.Your expiration date
                        is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date,
                        we have not received a copy of your updated @Model.CertificateName certificate.
                        After multiple notifications regarding the renewal of your @Model.CertificateName Credential,
                        your management has also received a copy of this notification.You and your management will continue
                        to receive daily reminders until the System Operations Training Team receives an updated copy of your
                        @Model.CertificateName certificate.  If you received this message in error or are no longer maintaining this
                        certificate, we ask that you let us know so we can update our records.If you have any questions,
                        please reach out to your Training Administrator.    
                        Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 2, 2 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                        Hello  @Model.EmployeeFirstName @Model.EmployeeLastName 
                        Your immediate attention is required.This is an urgent reminder from the Training Department that your
                        @Model.CertificateName certificate @Model.CertificateNumber
                        will expire in @Model.DaysUntilCertificationExpiration days.Your expiration date
                        is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date, 
                        we have not received a copy of your updated @Model.CertificateName certificate.   
                        After multiple notifications regarding the renewal of your @Model.CertificateName Credential, 
                        your management has also received a copy of this notification.You and your management will continue
                        to receive daily reminders until the System Operations Training Team receives an updated copy of your
                        @Model.CertificateName certificate.  If you received this message in error or are no longer maintaining this
                        certificate, we ask that you let us know so we can update our records.If you have any questions,
                        please reach out to your Training Administrator.    
                        Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 2, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                        Hello @Model.EmployeeFirstName @Model.EmployeeLastName
                           This is a reminder from the Training Department that your
                        @Model.CertificateName certificate @Model.CertificateNumber will expire in
                        @Model.DaysUntilCertificationExpiration
                        days.Your expiration date is 
                        @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). 
                        To date, we have not received a copy of your updated @Model.CertificateName certificate.   
                                        If you received this message in error or are no longer maintaining this certificate, 
                        we ask that you let us know so we can update our records.If you have any questions,
                        please reach out to your Training Administrator.    
                               Thank you!");

            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notification_Steps",
            keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
            keyValues: new object[] { 5, 1 },
            column: "Template",
            value: @"@using QTD2.Infrastructure.ExtensionMethods;
                        Hello @Model.EmployeeFirstName @Model.EmployeeLastName
                        You have been enrolled in a course which requires you to complete a pretest prior to the course
                        start date.You can access the pretest through the Employee Portal(EMP).Please review the table below for further
                        details.
                        <table>
                            <tr>
                                <td>Course Title</td>
                                <td>@Model.CourseTitle</td>
                            </tr>
                            <tr>
                                <td>Instructor/Location</td>
                                <td>@Model.Instructor/@Model.Location</td>
                            </tr>
                            <tr>
                                <td>Class Dates </td>
                                <td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)
                                    - @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                            </tr>
                            <tr>
                                <td>Pretest ID/Name </td>
                                <td>@Model.PretestId/@Model.PretestTitle</td>
                            </tr>
                            <tr>
                                <td>Pretest Available Date</td>
                                <td>@Model.PretestAvailableDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                            </tr>
                            <tr>
                                <td>Pretest Due Date </td>
                                <td>
                                    @Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                            </tr>
                        </table>
                        If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as
                        possible.
                        
                        Thank you!");

            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notification_Steps",
            keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
            keyValues: new object[] { 6, 1 },
            column: "Template",
            value: @"@using QTD2.Infrastructure.ExtensionMethods;
                     Hello @Model.EmployeeFirstName @Model.EmployeeLastName You have been assigned an online training course to complete.You
                     can access the course through the Employee Portal(EMP).Please review the table below for further details.
                      <table>
                         <tr>
                             <td>Course Title</td>
                             <td>@Model.ILATitle</td>
                         </tr>
                         <tr>
                             <td>Instructor/Location</td>
                             <td>@Model.Instructor/@Model.Location</td>
                         </tr>
                         <tr>
                             <td>Class Dates </td>
                             <td>
                                 @Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) -
                                 @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                         </tr>
                         <tr>
                             <td>Course Available Date</td>
                             <td>
                                 @Model.CourseStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                         </tr>
                         <tr>
                             <td>Course Due Date</td>
                             <td>
                                 @Model.CourseEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                         </tr>
                     </table>
                     If you are unable to complete the course by the due date listed,
                     notify your Training Administrator as soon as possible.Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 7, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                 Hello @Model.EmployeeFirstName @Model.EmployeeLastName You have been assigned to complete an evaluation for a course you
                 recently completed.
                 You can access the evaluation through the Employee Portal(EMP).Please review the table below for further details.
                 <table>
                     <tr>
                         <td>Course Title</td>
                         <td>@Model.ILATitle</td>
                     </tr>
                     <tr>
                         <td>Instructor/Location</td>
                         <td>@Model.Instructor/@Model.Location</td>
                     </tr>
                     <tr>
                         <td>Class Dates </td>
                         <td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)
                             - @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                     </tr>
                     <tr>
                         <td>Eval Available Date</td>
                         <td>@Model.CourseEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                     </tr>
                     <tr>
                         <td>Eval Due Date </td>
                         <td>
                             @Model.EvalDueDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                     </tr>
                 </table>
                 If you are unable to complete the evaluation by the due date listed, notify your Training Administrator.
                 Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 8, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                    Hello @Model.EmployeeFirstName @Model.EmployeeLastName You have been assigned to complete a procedure review.You can
                    complete the procedure review through the
                    Employee Portal(EMP).Please review the table below for further details.
                    <table>
                        <tr>
                            <td>Procedure Title</td>
                            <td>@Model.ProcedureTitle</td>
                        </tr>
                        <tr>
                            <td>Review Available Date</td>
                            <td>
                                @Model.ReviewStartdate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                        </tr>
                        <tr>
                            <td>Review Due Date</td>
                            <td>
                                @Model.ReviewEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                        </tr>
                    </table>
                    If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon
                    as possible.
                    Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 9, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                    Hello @Model.EmployeeFirstName @Model.EmployeeLastName You have been assigned to complete an IDP review.You can complete
                    the IDP review through the Employee
                    Portal(EMP).Please review the table below for further details.
                    <table>
                        <tr>
                            <td>IDP Title</td>
                            <td>@Model.IDPTitle</td>
                        </tr>
                        <tr>
                            <td>Review Available Date</td>
                            <td>
                                @Model.ReviewStartdate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                        </tr>
                        <tr>
                            <td>Review Due Date</td>
                            <td>
                                @Model.ReviewEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                        </tr>
                    </table>
                    If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon
                    as possible.
                    Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 12, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                Hello @Model.EmployeeFirstName @Model.EmployeeLastName Your self - registration request to enroll in @Model.CourseTitle on
                @@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) and
                @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) has been approved.
                Thank you!");


            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 13, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                Hello @Model.EmployeeFirstName @Model.EmployeeLastName Your self - registration request to enroll in @Model.CourseTitle on
                @Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) and
                @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) has been denied.
                Please contact your Training Administrator for additional information.
                Thank you");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 14, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                           Hello @Model.EmployeeFirstName @Model.EmployeeLastName You have been assigned to complete a GAP Survey.You can access
                           the GAP Survey through the Employee
                           Portal(EMP).Please review the table below for further details. This survey is being conducted to help us improve and
                           provide on target training for
                           @Model.PositionTitle position and your input is an important part of this process. 
                           <table>
                               <tr>
                                   <td>Position Title</td>
                                   <td>@Model.PositionTitle</td>
                               </tr>
                               <tr>
                                   <td>Survey Title</td>
                                   <td>@Model.SurveyTitle</td>
                               </tr>
                               <tr>
                                   <td>Survey Available Date</td>
                                   <td>
                                       @Model.SurveyStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                               </tr>
                               <tr>
                                   <td>Survey Due Date </td>
                                   <td>@Model.SurveyEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                               </tr>
                           </table>
                           If you are unable to complete the GAP Survey by the due date listed, notify your Training Administrator as soon as
                           possible.
                           Thank you!");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 15, 1 },
             column: "Template",
             value: @"@using QTD2.Infrastructure.ExtensionMethods;
                           Hello @Model.EmployeeFirstName @Model.EmployeeLastNam You have been assigned to complete a GAP Survey.You can access the
                           GAP Survey through the Employee
                           Portal(EMP).Please review the table below for further details. This survey is being conducted to help us improve and
                           provide on target training for
                           @Model.PositionTitle position and your input is an important part of this process.
                            <table>
                               <tr>
                                   <td>Position Title</td>
                                   <td>@Model.PositionTitle</td>
                               </tr>
                               <tr>
                                   <td>Survey Title</td>
                                   <td>@Model.SurveyTitle</td>
                               </tr>
                               <tr>
                                   <td>Survey Available Date</td>
                                   <td>
                                       @Model.SurveyStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                               </tr>
                               <tr>
                                   <td>Survey Due Date </td>
                                   <td>
                                       @Model.SurveyEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) </td>
                               </tr>
                           </table> 
                           If you are unable to complete the GAP Survey by the due date listed,
                           notify your Training Administrator as soon as possible. Thank you!");


        }

        protected void Development_AddEmpSettingsReleaseTypes()
        {

            _migrationBuilder.InsertData(
                table: "EmpSettingsReleaseTypes",
                columns: new[] { "Type", "Active" },
                 values: new object[,]
                {
                  { "Days", true },
                  { "Weeks", true },
                  { "Months", true },
                });
        }

        protected void Development_UpdateEmpSettingsReleaseTypes()
        {

            _migrationBuilder.Sql(
               @" UPDATE TestReleaseEMPSettings
	                SET EmpSettingsReleaseTypeId = 1 ; "
            );

            _migrationBuilder.Sql(
               @" UPDATE EvaluationReleaseEMPSettings
                    SET EmpSettingsReleaseTypeId = 1 ; "
            );
        }

        protected void Development_UpdateEmailNotificationTemplate()
        {

            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notification_Steps",
            keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
            keyValues: new object[] { 22, 1 },
            column: "Template",
            value: @"@using QTD2.Infrastructure.ExtensionMethods;
                    <style>
                        .emp-completion-table{
                            width: 100%;
                            border-collapse: collapse;
                        }
                        .emp-completion-table th {
                            border: 1px solid black;
                            padding: 8px 5px;
                            text-align: left;
                        }
                        .emp-completion-table td {
                            border: 1px solid grey;
                            text-align: left;
                            padding: 5px 5px;
                            vertical-align: top;
                            word-break: break-all;
                        }
                    </style>
                    <p>Hello,</p>
                    <p>Below please find the Employee Portal Training Completion Report. This report lists the training that has been completed and submitted by Employees via the Employee Portal.</p>
                    <p>Refer to applicable QTD reports for detailed training completion information. </p>
                    <p>To change the frequency of this report, navigate to <b>Templates and Forms > Email Notifications > Employee Portal Completions</b></p>
                    <p>If you have questions, please reach out to your training administrator.</p>
                    <table class='emp-completion-table'>
                        <thead>
                            <tr>
                                <th style='width: 30%;'>Name</th>
                                <th style='width: 30%;'>Completion Type</th>
                                <th style='width: 40%;'>Completion Date</th>
                            </tr>
                        </thead>
                        <tbody>
                            @foreach (var item in Model.Items)
                            {
                                <tr>
                                    <td>@item.Title</td>
                                    <td>@item.CompletionType</td>
                                    <td>@item.CompletionDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                </tr>
                            }
                        </tbody>
                    </table>");
        }

        protected void Development_AddTaskListReview_Type()
        {

            _migrationBuilder.InsertData(
                table: "TaskListReview_Type",
                columns: new[] { "Type", "Active" },
                 values: new object[,]
                {
                  { "Annual", true },
                  { "Quarterly", true },
                  { "Special Meeting", true }
                });
        }

        protected void Development_AddTaskListReview_Status()
        {

            _migrationBuilder.InsertData(
                table: "TaskListReview_Status",
                columns: new[] { "Type", "Active" },
                 values: new object[,]
                {
                  { "Draft", true },
                  { "Published", true }
                });
        }

        protected void Development_AddTaskReview_Status()
        {

            _migrationBuilder.InsertData(
                table: "TaskReview_Status",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "In Progress", true },
                  { "Completed", true }
                });
        }
        protected void Development_AddTaskReview_Finding()
        {

            _migrationBuilder.InsertData(
                table: "TaskReview_Finding",
                columns: new[] { "Finding", "Active" },
                 values: new object[,]
                {
                  { "No Changes Required", true },
                  { "Changes Required - No Training/Requal Required", true },
                  { "Changes Required - Training/Requalification Required", true },
                  { "Make Task Inactive", true }
                });
        }

        protected void Development_AddActionItem_Priority()
        {

            _migrationBuilder.InsertData(
                table: "ActionItem_Priority",
                columns: new[] { "Type", "Active" },
                 values: new object[,]
                {
                  { "Low", true },
                  { "Medium", true },
                  { "High", true }
                });
        }

        protected void Development_AddActionItem_OperationTypes()
        {

            _migrationBuilder.InsertData(
                table: "ActionItem_OperationTypes",
                columns: new[] { "Type", "Active" },
                 values: new object[,]
                {
                  { "CreateLink", true },
                  { "RemoveLink", true },
                  { "CreateRecord", true },
                  { "UpdateRecord", true },
                });
        }

        protected void Development_AddActionItem_OperationType_Links()
        {

            _migrationBuilder.InsertData(
                table: "ActionItem_OperationType_Links",
                columns: new[] { "ActionItemOperationName", "OperationTypeId", "Active" },
                 values: new object[,]
                {
                  { "ActionItem_SubDuty_Operation",4, true },
                  { "ActionItem_Step_Operation",2 ,true },
                  { "ActionItem_Step_Operation",3 ,true },
                  { "ActionItem_Step_Operation",4,true },
                  { "ActionItem_QuestionAndAnswer_Operation",2, true },
                  { "ActionItem_QuestionAndAnswer_Operation",3, true },
                  { "ActionItem_QuestionAndAnswer_Operation",4, true },
                  { "ActionItem_Suggestion_Operation",2, true },
                  { "ActionItem_Suggestion_Operation",3, true },
                  { "ActionItem_Suggestion_Operation",4, true },
                  { "ActionItem_Tool_Operation",1, true },
                  { "ActionItem_Tool_Operation",2, true },
                  { "ActionItem_EnablingObjective_Operation",1, true },
                  { "ActionItem_EnablingObjective_Operation",2, true },
                  { "ActionItem_Procedure_Operation",1, true },
                  { "ActionItem_Procedure_Operation",2, true },
                  { "ActionItem_RegulatoryRequirement_Operation",1, true },
                  { "ActionItem_RegulatoryRequirement_Operation",2, true },
                  { "ActionItem_SafetyHazard_Operation",1, true },
                  { "ActionItem_SafetyHazard_Operation",2, true },
                });
        }

        protected void Development_AddTaskListReviewDocumentType()
        {
            _migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Name", "LinkedDataType", "Deleted", "Active" },
                values: new object[,]
                  {
                       {"Task List Review Supporting Document","TaskListReview",false,true},
                  });
        }

        protected void Development_AddSimulatorScenarioDifficulty()
        {

            _migrationBuilder.InsertData(
                table: "SimulatorScenario_Difficultys",
                columns: new[] { "Difficulty", "Active" },
                 values: new object[,]
                {
                  { "High", true },
                  { "Medium", true },
                  { "Low", true},
                });
        }

        protected void Development_AddSimulatorScenario_CollaboratorPermission()
        {

            _migrationBuilder.InsertData(
            table: "SimulatorScenario_CollaboratorPermissions",
            columns: new[] { "Permission", "Active", "Deleted" },
             values: new object[,]
            {
                   { "Editor", true, false },
                   { "Viewer", true, false }
             });
        }

        protected void Development_AddSimulatorScenario_Status()
        {
            _migrationBuilder.InsertData(
                table: "SimulatorScenario_Status",
                columns: new[] { "Status", "Active" },
                 values: new object[,]
                {
                  { "Draft", true },
                  { "Published", true }
                });
        }

        protected void Development_AddEmailNotification_SimulatorScenarioCollaboration()
        {
            _migrationBuilder.InsertData(
              table: "ClientSettings_Notifications",
              columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
              values: new object[,]
                  {
                      {"Simulator Scenario Collaboration", false, "Sent to the QTD user when they are added to the Simulator Scenario as a Collaborator.", false, true}
                  });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
                      {23, @"@using RazorEngine.Text
                            <p>
                                Hello @Model.CollaboratorFirstName @Model.CollaboratorLastName you have been invited to participate in Simulator Scenario - @Model.SimulatorScenarioTitle .  <a href='@Model.SimulatorScenarioLink'>Click here to view it</a>.
                            </p>
                            <p>@(new RawString(Model.SimulatorScenarioMessage))</p>",
                          1, false, true }
                  });
        }

        protected void Development_CopySimulatorScenarioOldToNew()
        {
            _migrationBuilder.Sql(@"
                CREATE TABLE #ScenarioMapping (
                    OldScenarioID INT,
                    NewScenarioID INT
                );

                MERGE INTO SimulatorScenarios AS TGT
                USING (SELECT * FROM SimulatorScenarios_Old ) AS SRC ON 1 = 2
                WHEN NOT MATCHED THEN
                INSERT (Title, Description, DurationHours, DurationMinutes, Active, Deleted) 
                VALUES(SRC.SimScenarioTitle, 
                    SRC.SimScenarioDesc, 
                    SRC.SimScenarioDurationHours, 
                    SRC.SimScenarioDurationMins,
                    SRC.Active,
                    SRC.Deleted)
                OUTPUT SRC.id, inserted.id
                INTO #ScenarioMapping(OldScenarioID, NewScenarioID);

                UPDATE SimulatorScenarios
                SET Title = 'Simulator Scenario ' + CAST(SimulatorScenarios.Id AS VARCHAR(255))
                FROM SimulatorScenarios
                INNER JOIN #ScenarioMapping ON SimulatorScenarios.Id = #ScenarioMapping.NewScenarioID
                WHERE SimulatorScenarios.Title IS NULL OR SimulatorScenarios.Title = '';

                INSERT INTO SimulatorScenario_ILAs (SimulatorScenarioID, ILAID, Active, Deleted)
                SELECT 
                    map.NewScenarioID AS SimulatorScenarioID, 
                    oldILA.ILAID,
                    oldILA.Active,
                    oldILA.Deleted
                FROM 
                    SimulatorScenarioILA_Links_Old oldILA
                JOIN 
                    #ScenarioMapping map ON oldILA.SimulatorScenarioID = map.OldScenarioID;

                INSERT INTO SimulatorScenario_Tasks (SimulatorScenarioID, TaskId, Active, Deleted)
                SELECT 
                    map.NewScenarioID AS SimulatorScenarioID, 
                    oldLink.TaskID,
                    oldLink.Active,
                    oldLink.Deleted
                FROM 
                    SimulatorScenarioTaskObjectives_Links_Old oldLink
                JOIN 
                   #ScenarioMapping map ON oldLink.SimulatorScenarioID = map.OldScenarioID;

                INSERT INTO SimulatorScenario_Positions (SimulatorScenarioID, PositionID, Active, Deleted)
                SELECT 
                    map.NewScenarioID AS SimulatorScenarioID, 
                    oldPosition.PositionID,
                    oldPosition.Active,
                    oldPosition.Deleted
                FROM 
                    SimulatorScenarioPositon_Links_Old oldPosition
                JOIN 
                    #ScenarioMapping map ON oldPosition.SimulatorScenarioID = map.OldScenarioID;
               

                 INSERT INTO  SimulatorScenario_EnablingObjectives (SimulatorScenarioID, EnablingObjectiveID, Active, Deleted)
                SELECT 
                    map.NewScenarioID AS SimulatorScenarioID, 
                    oldEo.EnablingObjectiveID,
                    oldEo.Active,
                    oldEo.Deleted
                FROM 
                    SimulatorScenario_EnablingObjectives_Links_Old oldEo
                JOIN 
                    #ScenarioMapping map ON oldEo.SimulatorScenarioID = map.OldScenarioID;
            ");
        }

        protected void Development_UpdateDuplicateInstructorEmails()
        {
            _migrationBuilder.Sql(@"
               WITH DuplicateEmails AS (
                    SELECT 
                        InstructorEmail,
                        COUNT(*) AS EmailCount
                    FROM Instructors
                    WHERE InstructorEmail IS NOT NULL AND InstructorEmail <> ''
                    GROUP BY InstructorEmail
                    HAVING COUNT(*) > 1
                )

                UPDATE I
                SET InstructorEmail =  I.InstructorEmail + CAST(I.Id AS NVARCHAR(10)) 
                FROM Instructors I
                INNER JOIN DuplicateEmails D
                ON I.InstructorEmail = D.InstructorEmail
                WHERE I.InstructorEmail IS NOT NULL AND I.InstructorEmail <> ''; 
            ");
        }

        protected void Development_AddTrainingIssue_Status()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueStatuses",
                columns: new[] { "Status", "Active" },
                values: new object[,]
                {
                    {"Open",true},
                    {"Closed",true }
                }
              );
        }

        protected void Development_AddTrainingIssue_Severity()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueSeverities",
                columns: new[] { "Severity", "Active" },
                values: new object[,]
                {
                    {"Low",true},
                    {"Medium",true },
                    {"High",true }
                }
              );
        }

        protected void Development_AddTrainingIssue_DriverType()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueDriverTypes",
                columns: new[] { "Type", "Active" },
                values: new object[,]
                {
                    {"Other",true},
                    {"Survey Results",true },
                    {"Team Feedback",true }
                }
              );
        }

        protected void Development_AddTrainingIssue_DriverSubType()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueDriverSubTypes",
                columns: new[] { "SubType", "DriverTypeId", "Active" },
                values: new object[,]
                {
                    {"DIF",2,true},
                    {"GAP",2,true},
                    {"RRT Analysis",2,true},
                    {"Student/Employee",3,true},
                    {"Instructor",3,true},
                    {"Training Dept. Personnel",3,true},
                    {"Manager",3,true},
                    {"Performance Problem",3,true}
                }
              );
        }

        protected void Development_AddTrainingIssue_ActionItemPriority()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueActionItemPriorities",
                columns: new[] { "Priority", "Active" },
                values: new object[,]
                {
                    {"Low",true},
                    {"Medium",true },
                    {"High",true }
                }
              );
        }

        protected void Development_AddTrainingIssue_ActionItemStatus()
        {
            _migrationBuilder.InsertData(
                table: "TrainingIssueActionItemStatuses",
                columns: new[] { "Status", "Active" },
                values: new object[,]
                {
                    {"Not Started",true},
                    {"In Progress",true },
                    {"Completed",true }
                }
              );
        }
        protected void Development_UpdateStudentEvaluation()
        {
            _migrationBuilder.DeleteData(
                table: "StudentEvaluationAudiences",
                keyColumns: new[] { "Name" },
                keyValues: new object[,]
                {
                       {"All Enrolled Employees" },
                       {"First Class Only (Pilot Class)" },
                }
                );

            _migrationBuilder.InsertData(
                 table: "StudentEvaluationAudiences",
                 columns: new[] { "Name", "Active" },
                 values: new object[,]
                 {
                        { "All Employees and Classes",true },
                 });

            _migrationBuilder.Sql(@"
                WITH RankedEvaluations AS (
	                SELECT
		                ILAId,
		                id,
		                ROW_NUMBER() OVER (PARTITION BY ILAId ORDER BY id) AS rn
	                FROM
		                ILA_StudentEvaluation_Links
                )

                update 
	                ILA_StudentEvaluation_Links 
                set 
	                Deleted = 1
                where
	                id IN (
		                SELECT id
		                FROM RankedEvaluations
		                WHERE rn > 1
	                );
                ");
        }

        protected void Development_UpdateEvaluationReleaseEMPSettings()
        {
            _migrationBuilder.Sql(@"UPDATE EvaluationReleaseEMPSettings
                    SET 
                        [EvaluationAvailableOnStartDate] = 0,
                        [EvaluationAvailableOnEndDate] = 1,
                        [FinalGradeRequired] = 1,
                        [ReleaseOnSpecificTimeAfterClassEndDate] = 0,
                        [ReleaseAfterEndTime] = 0,
                        [ReleasePrior] = 0,
                        [ReleaseAfterGradeAssigned] = 0
                    WHERE 
                        ([EvaluationAvailableOnStartDate] = 1 AND [EvaluationAvailableOnEndDate] = 1) OR
                        ([EvaluationAvailableOnStartDate] = 1 AND [FinalGradeRequired] = 1) OR
                        ([EvaluationAvailableOnStartDate] = 1 AND [ReleaseOnSpecificTimeAfterClassEndDate] = 1) OR
                        ([EvaluationAvailableOnStartDate] = 1 AND [ReleaseAfterEndTime] = 1) OR
                        ([EvaluationAvailableOnStartDate] = 1 AND [ReleasePrior] = 1) OR
                        ([EvaluationAvailableOnStartDate] = 1 AND [ReleaseAfterGradeAssigned] = 1) OR
                        ([EvaluationAvailableOnEndDate] = 1 AND [FinalGradeRequired] = 1) OR
                        ([EvaluationAvailableOnEndDate] = 1 AND [ReleaseOnSpecificTimeAfterClassEndDate] = 1) OR
                        ([EvaluationAvailableOnEndDate] = 1 AND [ReleaseAfterEndTime] = 1) OR
                        ([EvaluationAvailableOnEndDate] = 1 AND [ReleasePrior] = 1) OR
                        ([EvaluationAvailableOnEndDate] = 1 AND [ReleaseAfterGradeAssigned] = 1) OR
                        ([FinalGradeRequired] = 1 AND [ReleaseOnSpecificTimeAfterClassEndDate] = 1) OR
                        ([FinalGradeRequired] = 1 AND [ReleaseAfterEndTime] = 1) OR
                        ([FinalGradeRequired] = 1 AND [ReleasePrior] = 1) OR
                        ([FinalGradeRequired] = 1 AND [ReleaseAfterGradeAssigned] = 1) OR
                        ([ReleaseOnSpecificTimeAfterClassEndDate] = 1 AND [ReleaseAfterEndTime] = 1) OR
                        ([ReleaseOnSpecificTimeAfterClassEndDate] = 1 AND [ReleasePrior] = 1) OR
                        ([ReleaseOnSpecificTimeAfterClassEndDate] = 1 AND [ReleaseAfterGradeAssigned] = 1) OR
                        ([ReleaseAfterEndTime] = 1 AND [ReleasePrior] = 1) OR
                        ([ReleaseAfterEndTime] = 1 AND [ReleaseAfterGradeAssigned] = 1) OR
                        ([ReleasePrior] = 1 AND [ReleaseAfterGradeAssigned] = 1);
                                 ");
        }

        protected void Development_UpdateRecord_IlaStudentEvalLinks()
        {
            _migrationBuilder.Sql(@"UPDATE ILA_StudentEvaluation_Links
                                SET studentEvalAudienceID = (SELECT Id FROM StudentEvaluationAudiences)");
        }

        protected void Development_UpdateVersionNumberinExistingVersionILAs()
        {
            _migrationBuilder.Sql(@"
				                    UPDATE main
                    SET main.[VersionNumber] = v.rn
                    FROM Version_ILAs main
                    INNER JOIN (
                        SELECT Id, ILAId, ROW_NUMBER() OVER (PARTITION BY ILAId ORDER BY Id) AS rn
                        FROM Version_ILAs
                    ) AS v
                    ON main.Id = v.Id;
            ");
        }

        protected void Development_AddData_EmployeeHistoryTableandReport()
        {
            _migrationBuilder.Sql(@"UPDATE eh
                                   SET eh.OperationType = 2, 
                                       eh.CurrentActiveStatus = CASE
                                           WHEN e.Active = 1 THEN 1
                                           ELSE 0
                                       END
                                   FROM EmployeeHistories eh
                                   JOIN Employees e ON eh.EmployeeId = e.Id
                                   WHERE e.Deleted = 1;
                                   
                                   UPDATE eh
                                   SET eh.OperationType = CASE
                                           WHEN e.Active = 1 THEN 0 
                                           ELSE 1 
                                       END,
                                       eh.CurrentActiveStatus = CASE
                                           WHEN e.Active = 1 THEN 1
                                           ELSE 0
                                       END
                                   FROM EmployeeHistories eh
                                   JOIN Employees e ON eh.EmployeeId = e.Id
                                   WHERE e.Deleted = 0; ");
        }


        protected void Development_UpdateVersionILAsToPublishedStateBasedOnEffectiveDate()
        {
            _migrationBuilder.Sql(@"
                    UPDATE [dbo].[Version_ILAs]
                    SET [State] = 4
                    WHERE CAST([EffectiveDate] AS TIME) = '00:00:00'
                ");
        }

        protected void Development_AddTableData_ClassScheduleEmpSettings()
        {
            _migrationBuilder.Sql(@"INSERT INTO ClassSchedule_TestReleaseEMPSettings
               (
                   ClassScheduleId,
                   UsePreTestAndTest,
                   PreTestRequired,
                   FinalTestId,
                   PreTestId,
                   jobDetails,
                   PreTestAvailableOnEnrollment,
                   PreTestAvailableOneStartDate,
                   PreTestScore,
                   ShowStudentSubmittedPreTestAnswers,
                   ShowCorrectIncorrectPreTestAnswers,
                   MakeAvailableBeforeDays,
                   MakeAvailableBeforeWeeks,
                   DaysOrWeeks,
                   FinalTestPassingScore,
                   MakeFinalTestAvailableImmediatelyAfterStartDate,
                   MakeFinalTestAvailableOnClassEndDate,
                   MakeFinalTestAvailableAfterCBTCompleted,
                   MakeFinalTestAvailableOnSpecificTime,
                   FinalTestSpecificTimePrior,
                   FinalTestDueDate,
                   ShowStudentSubmittedFinalTestAnswers,
                   ShowStudentSubmittedRetakeTestAnswers,
                   ShowCorrectIncorrectFinalTestAnswers,
                   ShowCorrectIncorrectRetakeTestAnswers,
                   AutoReleaseRetake,
                   RetakeEnabled,
                   NumberOfRetakes,
                   EmpSettingsReleaseTypeId,
                   Deleted,
                   Active
               )
               SELECT 
                   cs.Id,
                   emp.UsePreTestAndTest,
                   emp.PreTestRequired,
                   emp.FinalTestId,
                   emp.PreTestId,
                   emp.jobDetails,
                   emp.PreTestAvailableOnEnrollment,
                   emp.PreTestAvailableOneStartDate,
                   emp.PreTestScore,
                   emp.ShowStudentSubmittedPreTestAnswers,
                   emp.ShowCorrectIncorrectPreTestAnswers,
                   emp.MakeAvailableBeforeDays,
                   emp.MakeAvailableBeforeWeeks,
                   emp.DaysOrWeeks,
                   emp.FinalTestPassingScore,
                   emp.MakeFinalTestAvailableImmediatelyAfterStartDate,
                   emp.MakeFinalTestAvailableOnClassEndDate,
                   emp.MakeFinalTestAvailableAfterCBTCompleted,
                   emp.MakeFinalTestAvailableOnSpecificTime,
                   emp.FinalTestSpecificTimePrior,
                   emp.FinalTestDueDate,
                   emp.ShowStudentSubmittedFinalTestAnswers,
                   emp.ShowStudentSubmittedRetakeTestAnswers,
                   emp.ShowCorrectIncorrectFinalTestAnswers,
                   emp.ShowCorrectIncorrectRetakeTestAnswers,
                   emp.AutoReleaseRetake,
                   emp.RetakeEnabled,
                   emp.NumberOfRetakes,
                   emp.EmpSettingsReleaseTypeId,
                   emp.Deleted,
                   emp.Active
               FROM TestReleaseEMPSettings emp
               JOIN ClassSchedules cs ON emp.ILAId = cs.ILAId
               WHERE NOT EXISTS (
                   SELECT 1
                   FROM ClassSchedule_TestReleaseEMPSettings cs_emp
                   WHERE cs_emp.ClassScheduleId = cs.Id
               );");

            _migrationBuilder.Sql(@"INSERT INTO ClassSchedule_TestReleaseEMPSetting_Retake_Links (ClassSchedule_TestReleaseSettingId, RetakeTestId,Deleted,Active)
                    SELECT cs_emp.Id, trl.RetakeTestId,trl.Deleted,trl.Active
                    FROM ClassSchedule_TestReleaseEMPSettings cs_emp
                    INNER JOIN ClassSchedules cs ON cs_emp.ClassScheduleId = cs.Id 
                    INNER JOIN TestReleaseEMPSettings tr ON tr.ILAId = cs.ILAId 
                    INNER JOIN TestReleaseEMPSetting_Retake_Links trl ON trl.TestReleaseSettingId = tr.Id
                    WHERE NOT EXISTS (
                        SELECT 1
                        FROM ClassSchedule_TestReleaseEMPSetting_Retake_Links existing_links
                        WHERE existing_links.ClassSchedule_TestReleaseSettingId = cs_emp.Id
                        AND existing_links.RetakeTestId = trl.RetakeTestId
                    );");
        }

        protected void Development_AddTableData_ClassScheduleTQEMPSettings()
        {
            _migrationBuilder.Sql(@"WITH FilteredTQILAEmpSettings AS (
                                    SELECT t1.ILAId, t1.TQRequired, t1.Deleted, t1.Active
                                    FROM TQILAEmpSettings t1
                                    WHERE t1.Id = (
                                        SELECT TOP 1 t2.Id
                                        FROM TQILAEmpSettings t2
                                        WHERE t2.ILAId = t1.ILAId AND t2.Active = 1
                                        ORDER BY t2.Id DESC
                                    )
                                )
                                INSERT INTO ClassSchedule_TQEMPSettings (ClassScheduleId, TQRequired, Deleted, Active)
                                SELECT DISTINCT 
                                    cs.Id,         
                                    t.TQRequired, 
                                    t.Deleted,     
                                    t.Active       
                                FROM ClassSchedules cs
                                INNER JOIN FilteredTQILAEmpSettings t ON t.ILAId = cs.ILAId
                                WHERE NOT EXISTS (
                                    SELECT 1
                                    FROM ClassSchedule_TQEMPSettings cs_tq
                                    WHERE cs_tq.ClassScheduleId = cs.Id
                                );
                            ");

            _migrationBuilder.Sql(@"INSERT INTO ClassSchedule_Evaluator_Links (ClassScheduleId, EvaluatorId, Deleted, Active)
                        SELECT 
                            cs.Id, 
                            e.EvaluatorId,
                            e.Deleted,
                            e.Active
                        FROM ClassSchedules cs
                        INNER JOIN ILA_Evaluator_Links e ON e.ILAId = cs.ILAId
                        WHERE NOT EXISTS (
                            SELECT 1
                            FROM ClassSchedule_Evaluator_Links existing_links
                            WHERE existing_links.ClassScheduleId = cs.Id
                            AND existing_links.EvaluatorId = e.EvaluatorId
                        );");
        }
        protected void Development_ApplyDatabaseUpdateScripts()
        {
            _migrationBuilder.Sql(@"
            UPDATE TrainingProgramReviews
            SET ReviewDate = DATEADD(hour, 6, ReviewDate)
            WHERE DATEPART(hour, ReviewDate) = 0
              AND DATEPART(minute, ReviewDate) = 0
              AND DATEPART(second, ReviewDate) = 0;
        ");

            _migrationBuilder.Sql(@"
            UPDATE TrainingProgramReviews
            SET StartDate = DATEADD(hour, 6, StartDate)
            WHERE DATEPART(hour, StartDate) = 0
              AND DATEPART(minute, StartDate) = 0
              AND DATEPART(second, StartDate) = 0;
        ");

            _migrationBuilder.Sql(@"
            UPDATE TrainingProgramReviews
            SET EndDate = DATEADD(hour, 6, EndDate)
            WHERE DATEPART(hour, EndDate) = 0
              AND DATEPART(minute, EndDate) = 0
              AND DATEPART(second, EndDate) = 0;
        ");

            _migrationBuilder.Sql(@"
                UPDATE TaskQualifications
                SET TaskQualificationDate = DATEADD(hour, 6, TaskQualificationDate)
                WHERE DATEPART(hour, TaskQualificationDate) = 0
                AND DATEPART(minute, TaskQualificationDate) = 0
                AND DATEPART(second, TaskQualificationDate) = 0;
            ");

            _migrationBuilder.Sql(@"
                    ;WITH CTE_Data  AS (
                    SELECT
                        b.Id AS [ClassScheduleId],
                        a.studentEvalFormID AS [StudentEvaluationId]
                    FROM
                        dbo.ILA_StudentEvaluation_Links a
                    INNER JOIN dbo.ClassSchedules b
                        ON a.ILAId = b.ILAID AND b.Deleted = 0 AND b.Active = 1
                    LEFT JOIN dbo.ClassSchedule_StudentEvaluations_Links c
                        ON a.studentEvalFormID = c.StudentEvaluationId AND b.Id = c.ClassScheduleId AND c.Deleted = 0 AND c.Active = 1
                    WHERE
                        a.Deleted = 0
                        AND a.Active = 1
                        AND c.Id IS NULL
		                )

                        insert into ClassSchedule_StudentEvaluations_Links (ClassScheduleId, StudentEvaluationId, Active, Deleted)
                        select 
                        ClassScheduleId
                        ,StudentEvaluationId
                        ,1
                        ,0 
                        from CTE_Data
                        ");

            _migrationBuilder.Sql(@"update TQEmpSettings set MultipleSignOff = 1 where MultipleSignOff is null and ReleaseToAllSingleSignOff = 0");
        }

        protected void Development_UpdateDocumentTypeForCourseCompletionInfo()
        {
            _migrationBuilder.UpdateData(
               table: "DocumentTypes",
               keyColumns: new[] { "Name", "LinkedDataType" },
               keyValues: new object[] { "Other Employee Course Completion Info", "Employees" },
               columns: new[] { "LinkedDataType" },
               values: new object[] { "ClassScheduleEmployees" });
        }

        protected void Development_UpdateLaunchLinkInCbtScormRegistration()
        {
            _migrationBuilder.Sql(@"
                   UPDATE CBT_ScormRegistration
                    SET LaunchLink = CONCAT(
                                        LEFT(LaunchLink, LEN(LaunchLink) - CHARINDEX('=', REVERSE(LaunchLink))),
                                        '=', 
                                        CAST([CBTScormUploadId] AS NVARCHAR(MAX)),
                                        '.', 
                                        CAST([ClassScheduleEmployeeId] AS NVARCHAR(MAX))
                                    )
                    WHERE ISNULL(LaunchLink, '') <> ''
                    ");
        }

        protected void Development_AddScormRegistrationsForActiveClassScheduleEmployees()
        {
            _migrationBuilder.Sql(@"
                with CTE_DATA as (
                        select  
                          a.Id as [CBT_ScormUploadId]
                        , a.ConnectedDate
                        , d.id as [ClassScheduleEmployeesId]
                        from
                        dbo.CBT_ScormUpload a
                        inner join dbo.CBTs b
                             on a.CbtId = b.Id and b.Active = 1 and b.Deleted = 0
                        inner join dbo.ClassSchedules c
                            on b.ILAId = c.ILAID and c.Deleted = 0 and c.Active = 1
                        inner join dbo.ClassScheduleEmployees d
                            on c.Id = d.ClassScheduleId and d.Active = 1 and d.Deleted = 0 and d.CBTStatusId <> 3
                        left join dbo.CBT_ScormRegistration e
                            on d.Id = e.ClassScheduleEmployeeId
                        where  a.Active = 1
                        and a.Deleted = 0
                        and a.ScormStatus = 'Uploaded'
                        and e.Id is null
                    )
                    ,CTE_PartitionedByCSE as (
                        select
                        *
                        , ROW_NUMBER() OVER(PARTITION BY a.ClassScheduleEmployeesId ORDER BY a.ConnectedDate DESC) AS RowNum
                        from CTE_DATA a
                    )

                    insert into
                        dbo.CBT_ScormRegistration
                        (CBTScormUploadId, ClassScheduleEmployeeId, RegistrationCompletion, RegistrationSuccess, Deleted, Active, PassingScoreSource)
                    select
                        a.CBT_ScormUploadId, a.ClassScheduleEmployeesId, 0, 0, 0, 1, 1
                    from
                        CTE_PartitionedByCSE a
                    where
                        RowNum = 1
                 ");
        }

        protected void Development_UpdateInactiveScormRegistrations()
        {

            _migrationBuilder.Sql(@"
                    with CTE_DATA as (
	                                select 
	                                a.Id
	                                ,d.EndDateTime
	                                ,e.DueDateAmount
	                                ,f.Type
	                                ,case 
		                                when f.Type = 'Days' then DATEADD(DAY, e.DueDateAmount, d.EndDateTime)
		                                when f.Type = 'Weeks' then DATEADD(WEEK, e.DueDateAmount, d.EndDateTime)
		                                when f.Type = 'Months' then DATEADD(MONTH, e.DueDateAmount, d.EndDateTime)
		                                else '9999-12-31'
	                                end as [AdjustedDate]
	                                from 
	                                dbo.CBT_ScormRegistration a
	                                inner join dbo.CBT_ScormUpload b
		                                on a.CBTScormUploadId = b.Id and b.ScormStatus = 'Disconnected' and b.Active = 1 and b.Deleted = 0
	                                inner join dbo.ClassScheduleEmployees c
		                                on a.ClassScheduleEmployeeId = c.Id and c.Active = 1 and c.CBTStatusId <> 3 and c.Deleted = 0
	                                inner join dbo.ClassSchedules d
		                                on c.ClassScheduleId = d.Id and d.Deleted = 0 and d.Active = 1
	                                inner join dbo.CBTs e
		                                on b.CbtId = e.id and e.Deleted = 0 and e.Active = 1
	                                inner join dbo.EmpSettingsReleaseTypes f
		                                on e.EmpSettingsReleaseTypeId = f.Id and f.Deleted = 0 and f.Active = 1
                                where
                                a.Active = 1
                                and a.Deleted = 0
                                )

                                update dbo.CBT_ScormRegistration set Active = 0 where id in (select Id from CTE_DATA where AdjustedDate > GETDATE())
                                ");
        }

        protected void Development_UpdateApiRegistrationIdsForScormRegistrationsAndScormApiRegToLearner()
        {
            _migrationBuilder.Sql(@"
                ALTER TABLE dbo.ScormRegistration NOCHECK CONSTRAINT FK_ScormRegistration_reglearn;
                WITH ValidScormApiReg AS (
                    SELECT sar.api_registration_id
                    FROM dbo.ScormApiRegToLearner sar
                    WHERE TRY_CONVERT(INT, sar.api_registration_id) IS NOT NULL
                )
                UPDATE dbo.ScormApiRegToLearner
                SET api_registration_id = CONCAT(CAST(csr.CBTScormUploadId AS VARCHAR), '.', CAST(csr.ClassScheduleEmployeeId AS VARCHAR))
                FROM dbo.ScormApiRegToLearner sar
                JOIN ValidScormApiReg v
                    ON sar.api_registration_id  = v.api_registration_id
                  Join CBT_ScormRegistration csr
                On  v.api_registration_id  =csr.ClassScheduleEmployeeId
               ALTER TABLE dbo.ScormRegistration CHECK CONSTRAINT FK_ScormRegistration_reglearn;
                    ");
            _migrationBuilder.Sql(@"
               WITH ValidScormRegistration AS(
                    SELECT sr.api_registration_id
                    FROM dbo.ScormRegistration sr
                    WHERE TRY_CONVERT(INT, sr.api_registration_id) IS NOT NULL
                )
                UPDATE dbo.ScormRegistration
                SET api_registration_id = CONCAT(CAST(csr.CBTScormUploadId AS VARCHAR), '.', CAST(csr.ClassScheduleEmployeeId AS VARCHAR))
                FROM dbo.ScormRegistration sr
                JOIN ValidScormRegistration v
                    ON sr.api_registration_id = v.api_registration_id
                    Join CBT_ScormRegistration csr
                On  v.api_registration_id = csr.ClassScheduleEmployeeId
                ");
        }

        protected void Development_UpdateTaskQualificationsLinkedToDeletedTasks()
        {
            _migrationBuilder.Sql(@"
                     DECLARE @TempTQIds TABLE (
                         Id INT
                     );

                     INSERT INTO @TempTQIds
                     SELECT 
                         a.Id
                     FROM
                         dbo.TaskQualifications a
                     INNER JOIN 
                         dbo.Tasks b
                         ON a.TaskId = b.Id
                         AND b.Deleted = 1
                     WHERE 
                         a.Deleted = 0;

                     select * from @TempTQIds

                     UPDATE a
                     SET a.Deleted = 1
                     FROM dbo.TaskQualifications a
                     INNER JOIN @TempTQIds t
                         ON a.Id = t.Id; 
              ");
        }

        protected void Development_UpdateTaskActiveStatusFromHistory()
        {
            _migrationBuilder.Sql(@"
                  with CTE_Ordered_VersionTasks as (
	                        select 
	                        a.id
	                        ,a.TaskId
	                        ,cast(VersionNumber as decimal(10,2)) as [CastVersionNumber]
	                        ,VersionNumber
	                        ,ROW_NUMBER() OVER (PARTITION BY a.TaskId ORDER BY cast(a.VersionNumber as decimal(10, 2))) as [RowNumber] 
	                        from 
	                        dbo.Version_Tasks a 
                        ), CTE_Ordered_VersionTasks_With_ActivationInactivation as (
	                        select
	                        a.Id
	                        ,a.TaskId
	                        ,a.RowNumber
	                        ,InactivationActivationOccurred = 
		                        case 
			                        when exists (select 1 from dbo.Task_Histories b where a.id = Version_TaskId and b.ChangeNotes like 'Inactive Task True%') then 0
			                        when exists (select 1 from dbo.Task_Histories b where a.id = Version_TaskId and b.ChangeNotes like 'Inactive Task False%') then 1
			                        else null
		                        end
	                        from
	                        CTE_Ordered_VersionTasks a
                        ), CTE_MostRecent_VersionTask_ActivationInactivation as (
	                        select 
	                        a.TaskId
	                        ,a.Id
	                        ,a.RowNumber
	                        ,max(b.RowNumber) as [MostRecentActivationInactivation_RowNumber]
	                        from CTE_Ordered_VersionTasks a
	                        left join CTE_Ordered_VersionTasks_With_ActivationInactivation b
		                        on a.TaskId = b.TaskId and b.RowNumber <= a.RowNumber and b.InactivationActivationOccurred is not null
	                        group by
		                        a.TaskId
		                        ,a.Id
		                        ,a.RowNumber
                        )

                        update a
                        set 
                        TaskActive = 
                        case 
	                        when c.id is null then 1
	                        else c.InactivationActivationOccurred
                        end 
                        from 
                        dbo.Version_Tasks a
                        inner join CTE_MostRecent_VersionTask_ActivationInactivation b
	                        on a.Id = b.Id
                        left join CTE_Ordered_VersionTasks_With_ActivationInactivation c
	                        on b.TaskId = c.TaskId and c.RowNumber = b.MostRecentActivationInactivation_RowNumber
                    ");
        }

        protected void Development_UpdateDocumentTypesTable_Tool()
        {
            _migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumns: new[] { "Id", "Name" },
                keyValues: new object[] { 10, "Tool" },
                column: "LinkedDataType",
                value: "Tool"
                );
        }

        protected void Development_UpdateILACertificationLinksForDeletedCertifications()
        {
            _migrationBuilder.Sql(@" update a set deleted = 1 from dbo.ILACertificationLinks a inner join dbo.Certifications b on a.CertificationId = b.id and b.Deleted = 1 where a.Deleted = 0 ");
        }

        protected void Development_UpdateDeletedTaskQualificationEvaluatorLinksForDeletedEmployees()
        {
            _migrationBuilder.Sql(@"update a set Deleted = 1 from dbo.TaskQualification_Evaluator_Links a inner join dbo.Employees b on a.EvaluatorId = b.Id and b.Deleted = 1 where a.Deleted = 0 ");
        }

        protected void Development_MarkDeletedPositionTasksBasedOnTasks()
        {
            _migrationBuilder.Sql(@"update a set Deleted = 1 from dbo.Position_Tasks a inner join dbo.Tasks b on a.TaskId = b.id and b.Deleted = 1 where a.Deleted = 0");
        }

        protected void Development_UpdateSegmentObjectiveLinksData()
        {
            _migrationBuilder.Sql(@"UPDATE SegmentObjective_Links
                                    SET Deleted = 1
                                    WHERE 
                                        TaskId IS NOT NULL 
                                        AND (
                                            NOT EXISTS (
                                                SELECT 1
                                                FROM ILA_TaskObjective_Links ila
                                                INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                                WHERE ila.TaskId = SegmentObjective_Links.TaskId
                                                  AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                  AND ila.Deleted = 0 
                                            )
                                            OR 
                                            EXISTS (
                                                SELECT 1
                                                FROM ILA_TaskObjective_Links ila
                                                INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                                WHERE ila.TaskId = SegmentObjective_Links.TaskId
                                                  AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                  AND ila.Deleted = 1 
                                            )
                                        );
            ");

            _migrationBuilder.Sql(@"UPDATE SegmentObjective_Links
                                  SET Deleted = 1
                                  WHERE 
                                      EnablingObjectiveId IS NOT NULL 
                                      AND (
                                          NOT EXISTS (
                                              SELECT 1
                                              FROM ILA_EnablingObjective_Links ila
                                              INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                              WHERE ila.EnablingObjectiveId = SegmentObjective_Links.EnablingObjectiveId
                                                AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                AND ila.Deleted = 0 
                                          )
                                          OR 
                                          EXISTS (
                                              SELECT 1
                                              FROM ILA_EnablingObjective_Links ila
                                              INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                              WHERE ila.EnablingObjectiveId = SegmentObjective_Links.EnablingObjectiveId
                                                AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                AND ila.Deleted = 1 
                                          )
                                      );"
                                  );

            _migrationBuilder.Sql(@"UPDATE SegmentObjective_Links
                                    SET Deleted = 1
                                    WHERE 
                                        CustomEOId IS NOT NULL 
                                        AND (
                                            NOT EXISTS (
                                                SELECT 1
                                                FROM ILACustomObjective_Links ila
                                                INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                                WHERE ila.CustomObjId = SegmentObjective_Links.CustomEOId
                                                  AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                  AND ila.Deleted = 0 
                                            )
                                            OR 
                                            EXISTS (
                                                SELECT 1
                                                FROM ILACustomObjective_Links ila
                                                INNER JOIN ILA_Segment_Links isl ON ila.ILAId = isl.ILAId
                                                WHERE ila.CustomObjId = SegmentObjective_Links.CustomEOId
                                                  AND isl.SegmentId = SegmentObjective_Links.SegmentId
                                                  AND ila.Deleted = 1 
                                            )
                                        );"
                                );
        }

        protected void Development_AddTrainingTopicsTableData()
        {
            _migrationBuilder.InsertData(
                    table: "TrainingTopics",
                    columns: new[] { "TrainingTopic_CategoryId", "Name", "Active" },
                    values: new object[,]
                    {
                        {3,"Emergency technologies/equipment" , true }
                    }
            );

            _migrationBuilder.Sql(@"WITH OrderedLinks AS (
                        SELECT 
                            SOL.Id AS SegmentObjectiveLinkId,
                            ROW_NUMBER() OVER (
                                PARTITION BY ISeg.ILAId 
                                ORDER BY S.Id, SOL.Id
                            ) AS RowOrder
                        FROM SegmentObjective_Links SOL
                        INNER JOIN Segments S ON S.Id = SOL.SegmentId
                        INNER JOIN ILA_Segment_Links ISeg ON ISeg.SegmentId = S.Id
                        WHERE ISeg.ILAId IS NOT NULL
                    )
                    UPDATE SOL
                    SET SOL.[Order] = OL.RowOrder
                    FROM SegmentObjective_Links SOL
                    INNER JOIN OrderedLinks OL ON SOL.Id = OL.SegmentObjectiveLinkId;"
            );
        }

        protected void Development_AddData_AdminEMPCompletionNotificationAndSettings()
        {
            _migrationBuilder.Sql(@"UPDATE AdminEMPCompletionNotificationItem
                                    SET CompletionEntityName = CASE 
                                    
                                        WHEN CompletionType = 'test' THEN (
                                            SELECT t.TestTitle 
                                            FROM ClassSchedule_Roster csr
                                            INNER JOIN Tests t ON csr.TestId = t.Id
                                            WHERE csr.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                        )
                                        
                                        WHEN CompletionType = 'online courses' THEN (
                                            SELECT ila.[Name] 
                                            FROM ClassScheduleEmployees cse
                                            INNER JOIN ClassSchedules cs ON cse.ClassScheduleId = cs.Id
                                            INNER JOIN ILAs ila ON cs.ILAId = ila.Id
                                            WHERE cse.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                        )
                                        
                                        WHEN CompletionType = 'Stude' THEN (
                                            SELECT se.Title
                                            FROM ClassSchedule_Evaluation_Roster er
                                            INNER JOIN StudentEvaluations se ON er.StudentEvaluationId = se.Id
                                            WHERE er.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                        )
                                        
                                        WHEN CompletionType = 'Procedure Review' THEN (
                                            SELECT pr.ProcedureReviewTitle
                                            FROM ProcedureReview_Employees pre
                                            INNER JOIN ProcedureReviews pr ON pre.ProcedureReviewId = pr.Id
                                            WHERE pre.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                        )
                                        
                                        WHEN CompletionType = 'Task & Skill Qualifications' THEN (
                                        SELECT 
                                            da.Letter + CAST(da.Number AS VARCHAR) + '.' + CAST(sd.SubNumber AS VARCHAR) + '.' + CAST(t.Number AS VARCHAR)
                                        FROM 
                                            TaskQualifications tq
                                        INNER JOIN 
                                            Tasks t ON tq.TaskId = t.Id 
                                        INNER JOIN 
                                            SubdutyAreas sd ON sd.Id = t.SubdutyAreaId 
                                        INNER JOIN 
                                            DutyAreas da ON da.Id = sd.DutyAreaId
                                        WHERE 
                                            tq.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                    )
                                        
                                        WHEN CompletionType = 'DIF Surveys' THEN (
                                            SELECT ds.Title
                                            FROM DIFSurvey_Employee dse
                                            INNER JOIN DifSurvey ds ON dse.DifSurveyId = ds.Id
                                            WHERE dse.Id = AdminEMPCompletionNotificationItem.CompletionEntityId
                                        )
                                    END
                                    WHERE CompletionType IN ('test', 'online courses', 'Stude', 'Procedure Review', 'Task & Skill Qualifications', 'DIF Surveys');"
            );

            _migrationBuilder.InsertData(
                 table: "ClientSettings_Notification_Step_AvailableCustomSettings",
                 columns: new[] { "ClientSettingsNotificationStepId", "Setting", "Active" },
                 values: new object[,]
                 {
                     { 24, "Time of Day", true } ,
                     { 24, "Day of Week", true } ,
                     { 24, "Day # of Month", true }
                 }
                 );

            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_Step_CustomSettings",
                columns: new[] { "ClientSettingsNotificationStepId", "Key", "Value", "Active" },
                values: new object[,]
                {
                    { 24, "Time of Day", "00:00", true },
                    { 24, "Day of Week", "Sunday", true },
                    { 24, "Day # of Month", "01", true },
                }
              );

            _migrationBuilder.UpdateData(
            table: "ClientSettings_Notification_Steps",
            keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
            keyValues: new object[] { 22, 1 },
            column: "Template",
            value: @"@using QTD2.Infrastructure.ExtensionMethods;
                    <style>
                        .emp-completion-table{
                            width: 100%;
                            border-collapse: collapse;
                        }
                        .emp-completion-table th {
                            border: 1px solid black;
                            padding: 8px 5px;
                            text-align: left;
                        }
                        .emp-completion-table td {
                            border: 1px solid grey;
                            text-align: left;
                            padding: 5px 5px;
                            vertical-align: top;
                            word-break: break-all;
                        }
                    </style>
                    <p>Hello,</p>
                    <p>Below please find the Employee Portal Training Completion Report. This report lists the training that has been completed and submitted by Employees via the Employee Portal.</p>
                    <p>Refer to applicable QTD reports for detailed training completion information. </p>
                    <p>To change the frequency of this report, navigate to <b>Templates and Forms > Email Notifications > Employee Portal Completions</b></p>
                    <p>If you have questions, please reach out to your training administrator.</p>
                    @if (Model.Items.Count > 0){
                        <table class='emp-completion-table'>
                            <thead>
                                <tr>
                                    <th style='width: 15%;'>Name</th>
                                    <th style='width: 15%;'>Completion Type</th>
                                    <th style='width: 30%;'>Completion Name </th>
                                    <th style='width: 40%;'>Completion Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                @foreach (var item in Model.Items)
                                {
                                    <tr>
                                        <td>@item.Title</td>
                                        <td>@item.CompletionType </td>
                                        <td>@item.CompletionEntityName</td>
                                        <td>@item.CompletionDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                    </tr>
                                }
                            </tbody>
                        </table>
                    } else {
                        <i>No EMP completion records for this time period</i>
                    }
                    ");
        }

        protected void Development_NotificationTemplateAdjustments()
        {
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                      set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p> <p> You have been assigned to complete a Task Qualification as part of your position specific training program. An evaluator has been assigned to sign - off on this task qualification.This will be completed using the Employee Portal(EMP). Please review the table below for further details. To help you prepare for the task qualification, the task(s) below are available in a read - only format in EMP.</p><figure class=""table""><table><tbody><tr><td>Task #</td><td>@Model.TaskNumber</td></tr><tr><td>Task Statement</td><td>@Model.TaskStatement</td></tr><tr><td>Evaluator Name</td><td>@Model.EvaluatorName</td></tr></tbody></table></figure><p>If for any reason you cannot complete the assigned Task Qualification, notify your Task Qualification Evaluator and/or Training Administrator as soon as possible. Thank you!</p>'

                                      where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Task Qualification - Trainee')"
           );
        }

        protected void Development_Update_NotificationTemplate()
        {
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p> <p>Attached find a schedule of courses you are currently registered for. Please review this schedule and let us know if you have any questions.</p><p> Thank you!!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Class Schedule')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p> <p>This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days.Your expiration date is @Model.CertificateExpirationDate. To date, we have not received a copy of your updated @Model.CertificateName certificate.</p><p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records.If you have any questions, please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 2"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your attention is required. This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. Your expiration date is @Model.CertificateExpirationDate. To date, we have not received a copy of your updated @Model.CertificateName certificate.</p> <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. If you have any questions,please reach out to your Training Administrator.</p> <p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 3"
             );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your immediate attention is required. This is an urgent reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. Your expiration date is @Model.CertificateExpirationDate. To date, we have not received a copy of your updated @Model.CertificateName certificate.</p> <p>After multiple notifications regarding the renewal of your @Model.CertificateName credential, your management has also received a copy of this notification. You and your management will continue to receive daily reminders until the System Operations Training Team receives an updated copy of your @Model.CertificateName certificate.</p> <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. If you have any questions,please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 4"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Welcome to the Employee Portal (EMP)! EMP is your gateway to complete critical training activities such as computer training classes, tests, procedure reviews, and more.</p><p>Your Training Administrator has created an account for you in EMP, which you can access via the link and username below:</p><p>Link: @Model.EMPWebsite<br>Username: @Model.EmployeeUserName</p><p>For security reasons, this email does not contain your account password. Please reset your password by clicking on the “Forgot your Password?” link on the EMP login page.</p><p><strong>Important!</strong> The following internet browsers are supported: Internet Explorer, Google Chrome, Microsoft Edge. We encourage you to log in as soon as possible to ensure everything is working correctly. If you have any questions, please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Login')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>In order to receive credit for completion of @Model.CourseTitle, you must also complete the online test. You can access the test through the Employee Portal (EMP). Please review the table below for further details.</p> <figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate/@Model.EndDate</td></tr><tr><td>Test ID/Name</td><td>@Model.TestId/@Model.TestTitle</td></tr><tr><td>Test Available Date</td><td>@Model.ClassEndDate</td></tr><tr><td>Test Due Date</td><td>@Model.TestDueDate</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p> <p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Test')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been enrolled in a course which requires you to complete a pretest prior to the course start date. You can access the pretest through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate/@Model.EndDate</td></tr><tr><td>Pretest ID/Name</td><td>@Model.PretestId/@Model.PretestTitle</td></tr><tr><td>Pretest Available Date</td><td>@Model.PretestAvailableDate</td></tr><tr><td>Pretest Due Date</td><td>@Model.ClassStartDate</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Pretest')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned an online training course to complete. You can access the course through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.ILATitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate/@Model.EndDate</td></tr><tr><td>Course Available Date</td><td>@Model.CourseStartDate</td></tr><tr><td>Course Due Date</td><td>@Model.CourseEndDate</td></tr></table></figure><p>If you are unable to complete the course by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Online Course')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned to complete an evaluation for a course you recently completed. You can access the evaluation through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.ILATitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate/@Model.EndDate</td></tr><tr><td>Eval Available Date</td><td>@Model.CourseEndDate</td></tr><tr><td>Eval Due Date</td><td>@Model.EvalDueDate</td></tr></table></figure><p>If you are unable to complete the evaluation by the due date listed, notify your Training Administrator.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Student Evaluation')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned to complete a procedure review. You can complete the procedure review through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Procedure Title</td><td>@Model.ProcedureTitle</td></tr><tr><td>Review Available Date</td><td>@Model.ReviewStartdate</td></tr><tr><td>Review Due Date</td><td>@Model.ReviewEndDate</td></tr></table></figure><p>If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Procedure Review')"
            );


            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned to complete an IDP review. You can complete the IDP review through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>IDP Title</td><td>@Model.IDPTitle</td></tr><tr><td>Review Available Date</td><td>@Model.ReviewStartdate</td></tr><tr><td>Review Due Date</td><td>@Model.ReviewEndDate</td></tr></table></figure><p>If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP IDP Review')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned as an Evaluator to sign off on a Task Qualification. This will be completed using the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Task #</td><td>@Model.TaskNumber</td></tr><tr><td>Task Statement</td><td>@Model.TaskStatement</td></tr><tr><td>Trainee Name</td><td>@Model.TraineeName</td></tr></table></figure><p>If for any reason you cannot complete the assigned Task Qualification(s), notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Task Qualification - Evaluator')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Your self-registration request to enroll in @Model.CourseTitle on @Model.StartDate and @Model.EndDate has been approved.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Self-Registration Approval')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Your self-registration request to enroll in @Model.CourseTitle on @Model.StartDate and @Model.EndDate has been denied.</p><p>Please contact your Training Administrator for additional information.</p><p>Thank you</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Self-Registration Denial')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned to complete a DIF Survey. You can access the DIF Survey through the Employee Portal (EMP). Please review the table below for further details.</p><p>This survey is being conducted to help us improve both the initial and continuing training for @Model.PositionTitle position, and your input is an important part of this process. You will be asked to review and rate the tasks you perform in terms of difficulty, importance, and frequency. Further instructions are provided once you start the survey.</p><figure class=""table""> <table><tr><td>Position Title</td><td>@Model.PositionTitle</td></tr><tr><td>Survey Title</td><td>@Model.SurveyTitle</td></tr><tr><td>Survey Available Date</td><td>@Model.SurveyStartDate</td></tr><tr><td>Survey Due Date</td><td>@Model.SurveyEndDate</td></tr></table></figure><p>If you are unable to complete the DIF Survey by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP DIF Survey')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned to complete a GAP Survey. You can access the GAP Survey through the Employee Portal (EMP). Please review the table below for further details.</p><p>This survey is being conducted to help us improve and provide on-target training for @Model.PositionTitle position, and your input is an important part of this process.</p><figure class=""table""><table><tr><td>Position Title</td><td>@Model.PositionTitle</td></tr><tr><td>Survey Title</td><td>@Model.SurveyTitle</td></tr><tr><td>Survey Available Date</td><td>@Model.SurveyStartDate</td></tr><tr><td>Survey Due Date</td><td>@Model.SurveyEndDate</td></tr></table></figure><p>If you are unable to complete the GAP Survey by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP GAP Survey')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Congratulations on completing the course @Model.PreviousILATitle.</p><p>Because you are enrolled in a Meta ILA, your course @Model.ILATitle has been already released.</p><p>Please log into your EMP Dashboard to complete the course.</p><p>If you are unable to complete the course by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Self Paced Released')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Congratulations on completing the course @Model.PreviousILATitle.</p><p>Because you are enrolled in a Meta ILA, you need to enroll yourself in the course @Model.ILATitle.</p>@if(Model.RegistrationsAvailable) {<p>Please log into your EMP Dashboard and use the self-registration portal to enroll in a class.</p><p>If you are unable to attend the classes listed, notify your Training Administrator as soon as possible.</p>}else{<p>There are no upcoming classes for the ILA. Please notify your Training Administrator as soon as possible.</p>}<p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Employee - Self Registration Needed')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Congratulations on completing the course @Model.PreviousILATitle.</p><p>Because you are enrolled in a Meta ILA, you need to enroll yourself in the course @Model.ILATitle.</p>@if(Model.RegistrationsAvailable) {<p>Please log into your EMP Dashboard and use the self-registration portal to enroll in a class.</p><p>If you are unable to attend the classes listed, notify your Training Administrator as soon as possible.</p>}else{<p>There are no upcoming classes for the ILA. Please notify your Training Administrator as soon as possible.</p>}<p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Employee - Self Registration Needed')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello,<p>Please be aware that @Model.EmployeeFirstName @Model.EmployeeLastName has completed the ILA @Model.PreviousILATitle.</p><p>As part of the Meta ILA @Model.MetaILATitle, they are now required to take the ILA @Model.ILATitle.</p>@if(Model.RegistrationsAvailable) {<p>The employee has been notified that they need to self-register in the ILA. However, it is possible that the employee may request a different time. They have been instructed to reach out to their Training Administrator if the times do not work for them.</p>} else {<p>The employee has been notified that they need to self-register in the ILA. However, currently, there are no available classes for the employee to register in. You will need to create an additional class for the employee to continue.</p>}<p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Admin - Self Registration Needed')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>Congratulations on completing the course @Model.PreviousILATitle.</p><p>Because you are enrolled in a Meta ILA, you will be enrolled in the next course @Model.ILATitle.</p><p>Please coordinate with your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Employee - Registration Needed')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello,<p>Please be aware that @Model.EmployeeFirstName @Model.EmployeeLastName has completed the ILA @Model.PreviousILATitle.</p><p>As part of the Meta ILA @Model.MetaILATitle, they are now required to take the ILA @Model.ILATitle.</p><p>The employee has been notified that they need to be enrolled in the next ILA. However, that ILA is not configured to allow self-registration. You will need to assist the employee for them to continue.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Admin - Registration Needed')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = 'Hello,<p>Congratulations on completing the course @Model.PreviousILATitle.</p><p>You have now completed the coursework for your Meta ILA.</p><p>To complete your training on this Meta ILA, you need to complete the associated test. It has been released to your EMP portal.</p><p>Thank you!</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Meta ILA - Coursework Complete')"
            );
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                         set template = '@using RazorEngine.Text <p>Hello @Model.CollaboratorFirstName @Model.CollaboratorLastName, you have been invited to participate in Simulator Scenario - @Model.SimulatorScenarioTitle. <a href=""@Model.SimulatorScenarioLink"">Click here to view it</a>.</p><p>@(new RawString(Model.SimulatorScenarioMessage))</p>'

                                         where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Simulator Scenario Collaboration')"
            );
        }

        protected void Development_Update_NotifcationEmailTemplate()
        {
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p> <p>This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days.Your expiration date is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date, we have not received a copy of your updated @Model.CertificateName certificate.</p><p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records.If you have any questions, please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 2"
            );


            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your attention is required. This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. Your expiration date is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date, we have not received a copy of your updated @Model.CertificateName certificate.</p> <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. If you have any questions,please reach out to your Training Administrator.</p> <p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 3"
             );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your immediate attention is required. This is an urgent reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. Your expiration date is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date, we have not received a copy of your updated @Model.CertificateName certificate.</p> <p>After multiple notifications regarding the renewal of your @Model.CertificateName credential, your management has also received a copy of this notification. You and your management will continue to receive daily reminders until the System Operations Training Team receives an updated copy of your @Model.CertificateName certificate.</p> <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. If you have any questions,please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 4"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = ' @using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>In order to receive credit for completion of @Model.CourseTitle, you must also complete the online test. You can access the test through the Employee Portal (EMP). Please review the table below for further details.</p> <figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)/@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Test Due Date</td><td>@Model.TestDueDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p> <p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Test')"
             );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = 'Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been enrolled in a course which requires you to complete a pretest prior to the course start date. You can access the pretest through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)/@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest ID/Name</td><td>@Model.PretestId/@Model.PretestTitle</td></tr><tr><td>Pretest Available Date</td><td>@Model.PretestAvailableDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest Due Date</td><td>@Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Pretest')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned an online training course to complete. You can access the course through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.ILATitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)/@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Course Available Date</td><td>@Model.CourseStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Course Due Date</td><td>@Model.CourseEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the course by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Online Course')"
            );


            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned to complete an evaluation for a course you recently completed. You can access the evaluation through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.ILATitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)/@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Eval Available Date</td><td>@Model.CourseEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Eval Due Date</td><td>@Model.EvalDueDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the evaluation by the due date listed, notify your Training Administrator.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Student Evaluation')"
             );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned to complete a procedure review. You can complete the procedure review through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Procedure Title</td><td>@Model.ProcedureTitle</td></tr><tr><td>Review Available Date</td><td>@Model.ReviewStartdate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Review Due Date</td><td>@Model.ReviewEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Procedure Review')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned to complete an IDP review. You can complete the IDP review through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>IDP Title</td><td>@Model.IDPTitle</td></tr><tr><td>Review Available Date</td><td>@Model.ReviewStartdate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Review Due Date</td><td>@Model.ReviewEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete this procedure review by the listed due date, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP IDP Review')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your self-registration request to enroll in @Model.CourseTitle on @Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) and @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) has been approved.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Self-Registration Approval')"
             );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your self-registration request to enroll in @Model.CourseTitle on @Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) and @Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) has been denied.</p><p>Please contact your Training Administrator for additional information.</p><p>Thank you</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Self-Registration Denial')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned to complete a DIF Survey. You can access the DIF Survey through the Employee Portal (EMP). Please review the table below for further details.</p><p>This survey is being conducted to help us improve both the initial and continuing training for @Model.PositionTitle position, and your input is an important part of this process. You will be asked to review and rate the tasks you perform in terms of difficulty, importance, and frequency. Further instructions are provided once you start the survey.</p><figure class=""table""> <table><tr><td>Position Title</td><td>@Model.PositionTitle</td></tr><tr><td>Survey Title</td><td>@Model.SurveyTitle</td></tr><tr><td>Survey Available Date</td><td>@Model.SurveyStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Survey Due Date</td><td>@Model.SurveyEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the DIF Survey by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP DIF Survey')"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods; <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been assigned to complete a GAP Survey. You can access the GAP Survey through the Employee Portal (EMP). Please review the table below for further details.</p><p>This survey is being conducted to help us improve and provide on-target training for @Model.PositionTitle position, and your input is an important part of this process.</p><figure class=""table""><table><tr><td>Position Title</td><td>@Model.PositionTitle</td></tr><tr><td>Survey Title</td><td>@Model.SurveyTitle</td></tr><tr><td>Survey Available Date</td><td>@Model.SurveyStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Survey Due Date</td><td>@Model.SurveyEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the GAP Survey by the due date listed, notify your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP GAP Survey')"
             );
        }

        protected void Development_UpdateTaskEoLinksOfDeletedEO()
        {
            _migrationBuilder.Sql(@"UPDATE Task_EnablingObjective_Links SET Deleted = 1 WHERE EnablingObjectiveId IN ( SELECT Id FROM EnablingObjectives WHERE Deleted = 1 );");
        }

        protected void Development_Update_NotificationEmailTemplatesWithUsing()
        {
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>Your immediate attention is required. This is an urgent reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. Your expiration date is @Model.CertificateExpirationDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId). To date, we have not received a copy of your updated @Model.CertificateName certificate.</p> <p>After multiple notifications regarding the renewal of your @Model.CertificateName credential, your management has also received a copy of this notification. You and your management will continue to receive daily reminders until the System Operations Training Team receives an updated copy of your @Model.CertificateName certificate.</p> <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. If you have any questions,please reach out to your Training Administrator.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'Certification Expiration')   AND Id = 4"
            );

            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been enrolled in a course which requires you to complete a pretest prior to the course start date. You can access the pretest through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)/@Model.ClassEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest ID/Name</td><td>@Model.PretestId/@Model.PretestTitle</td></tr><tr><td>Pretest Available Date</td><td>@Model.PretestAvailableDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest Due Date</td><td>@Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Pretest')"
            );
        }

        protected void Development_UndeleteCompletedClassesAndClearIDPInfo()
        {
            _migrationBuilder.Sql(@"DECLARE @ClassScheduleIds TABLE (Id INT);
                INSERT INTO @ClassScheduleIds (Id)
                SELECT a.Id
                FROM dbo.ClassSchedules a
                WHERE a.Deleted = 1
                AND EXISTS (
                    SELECT 1
                    FROM dbo.ClassScheduleEmployees aa
                    WHERE a.Id = aa.ClassScheduleId
                      AND aa.FinalGrade IS NOT NULL
                )
                update dbo.ClassSchedules set Deleted = 0 where id in (select * from @ClassScheduleIds)
                update dbo.ClassScheduleEmployees set Deleted = 0 where ClassScheduleId in (select * from @ClassScheduleIds)
                ");

            _migrationBuilder.Sql(@"update a
                set deleted = 1
                from
                dbo.IDPSchedules a
                inner join dbo.ClassSchedules b
	                on a.ClassScheduleId = b.Id and b.Deleted = 1 and not exists(
		                select 1
		                from dbo.ClassScheduleEmployees aa
		                where 
		                b.Id = aa.ClassScheduleId
		                and aa.FinalGrade is not null
	                )
                where
                a.Deleted = 0");
        }

        protected void Development_UpdateEMPPretestNotificationEmailTemplate()
        {
            _migrationBuilder.Sql(@"update  [dbo].[ClientSettings_Notification_Steps]
                                 set template = '@using QTD2.Infrastructure.ExtensionMethods;<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p><p>You have been enrolled in a course which requires you to complete a pretest prior to the course start date. You can access the pretest through the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Course Title</td><td>@Model.CourseTitle</td></tr><tr><td>Instructor/Location</td><td>@Model.Instructor/@Model.Location</td></tr><tr><td>Class Dates</td><td>@Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId) - @Model.ClassEndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest ID/Name</td><td>@Model.PretestId/@Model.PretestTitle</td></tr><tr><td>Pretest Available Date</td><td>@Model.PretestAvailableDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr><tr><td>Pretest Due Date</td><td>@Model.ClassStartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td></tr></table></figure><p>If you are unable to complete the pretest by the due date listed, contact your Training Administrator as soon as possible.</p><p>Thank you!</p>'

                                 where ClientSettingsNotificationId = (select id from ClientSettings_Notifications where name = 'EMP Pretest')"
           );
        }

        protected void Development_AddProfessionlaHoursToCertificationTable()
        {
            _migrationBuilder.InsertData(
            table: "Certifications",
            columns: new[] { "Name", "CertifyingBodyId", "RenewalTimeFrame", "CreditHrsReq", "AllowRolloverHours", "AdditionalHours", "EffectiveDate", "Active", "Deleted", "RenewalInterval" },
            values: new object[,]
            {
                 { "Professional", 2, true, false, false, 0.00, DateTime.Now, true, false, 1 },
            });
        }

        protected void Development_UpdateVersionTasksIsInUseFlag()
        {
            _migrationBuilder.Sql(@"
                WITH CTE_Ordered_VersionTasks_For_Tasks_Without_IsInUse_VersionTask AS (
                    SELECT 
                        a.Id,
                        ROW_NUMBER() OVER (PARTITION BY a.TaskId ORDER BY a.Id DESC) AS rn
                    FROM 
                        dbo.Version_Tasks a
                    WHERE 
                        a.Deleted = 0
                        AND NOT EXISTS (
                            SELECT 1 
                            FROM dbo.Version_Tasks bb 
                            WHERE a.TaskId = bb.TaskId 
                                AND bb.Deleted = 0 
                                AND bb.IsInUse = 1
                        )
                )

                UPDATE dbo.Version_Tasks 
                SET IsInUse = 1
                FROM dbo.Version_Tasks a
                INNER JOIN CTE_Ordered_VersionTasks_For_Tasks_Without_IsInUse_VersionTask b
                    ON a.Id = b.Id 
                    AND b.rn = 1;
            ");
        }

        protected void Development_MigrateDataFromProcedure_ILA_LinksToILA_Procedure_Links()
        {
            _migrationBuilder.Sql(@"INSERT INTO ILA_Procedure_Links
                                    ([ILAId]
                                    ,[ProcedureId]
                                    ,[Deleted]
                                    ,[Active]
                                    ,[CreatedBy]
                                    ,[CreatedDate]
                                    ,[ModifiedBy]
                                    ,[ModifiedDate])
                                         SELECT p.[ILAId]
                                               ,p.[ProcedureId]
                                               ,p.[Deleted]
                                               ,p.[Active]
                                               ,p.[CreatedBy]
                                               ,p.[CreatedDate]
                                               ,p.[ModifiedBy]
                                               ,p.[ModifiedDate]
                                         FROM Procedure_ILA_Links p
                                         WHERE NOT EXISTS (
                                             SELECT 1 
                                             FROM ILA_Procedure_Links i
                                             WHERE i.[ILAId] = p.[ILAId]
                                               AND i.[ProcedureId] = p.[ProcedureId]
                                         );");
        }

        protected void Development_AddNewReportFilters_AccordingToRRTTypes()
        {
            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
                values: new object[,]
                {
                    {1,"Select RRT's", "String", "Single", DateTime.MinValue, DateTime.MinValue, "rrttypes", false, true,null,null  }
                }
            );
        }

        protected void Development_UpdateReportFilters_ILAByProvider()
        {
            _migrationBuilder.DeleteData(
                table: "ReportSkeletonFilters",
                keyColumns: new[] { "ReportSkeletonId", "Name" },
                keyValues: new object[,]
                {
                 {4, "Delivery Method" },
                 {4,"ILAs with Approvals expiring within Months" }
                }
            );

            _migrationBuilder.UpdateData(
               table: "ReportSkeletonFilters",
               keyColumns: new[] { "ReportSkeletonId", "Name" },
               keyValues: new object[] { 4, "ILA Status" },
               column: "DefaultValue",
               value: "Active Only"
            );

            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
                values: new object[,]
                {
                    {4,"Show ILA Application Dates", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null  }
                }
            );
        }

        protected void Development_UpdateReport_ILAByCompletionHistory()
        {
            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue" },
                values: new object[,] {
                          {13, "Active/Inactive Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true,"Active Only"},
                            }
                );

            _migrationBuilder.UpdateData(
            table: "ReportSkeletonFilters",
            keyColumns: new[] { "ReportSkeletonId", "Name" },
            keyValues: new object[] { 13, "Completion Type" },
            column: "FilterOption",
            value: "coursecompletionstatus"
         );

        }

        protected void Development_UpdateReport_TaskByPosition()
        {
            _migrationBuilder.UpdateData(
            table: "ReportSkeletonFilters",
            keyColumns: new[] { "ReportSkeletonId", "Name" },
            keyValues: new object[] { 1, "Show Specific Tasks" },
            column: "DefaultValue",
            value: "All Active Tasks"
           );
        }

        protected void Development_UpdateReport_TrainingIssueList()
        {
            _migrationBuilder.UpdateData(
            table: "ReportSkeletons",
            keyColumns: new[] { "DefaultTitle" },
            keyValues: new object[] { "Issues List" },
            column: "DefaultTitle",
            value: "Training Issues List"
           );

            _migrationBuilder.DeleteData(
                table: "ReportSkeletonFilters",
                keyColumns: new[] { "ReportSkeletonId", "Name" },
                keyValues: new object[,]
                {
                     {26, "Program Type" },
                     {26, "ILA" },
                     {26, "OJT / Task Qualification" },
                     {26, "Open Only" },
                }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                  {26, "Select Training Component", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingissuecomponent", false, true,"listAllSelected",null},
                  {26, "Severity Level", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingissueseveritylevel", false, true,"listAllSelected",null},
                  {26, "Open/Closed Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingissuestatus", false, true,"All",null}
              }
            );

            _migrationBuilder.DeleteData(
            table: "ReportSkeletonColumns",
            keyColumn: "ReportSkeletonId",
            keyValue: 26
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                   {26, "Issue ID",  false, true },
                   {26, "Created Date",  false, true },
                   {26, "Due Date",  false, true },
                   {26, "Training Issue Title",  false, true },
                   {26, "Severity",  false, true },
                   {26, "Driver",  false, true },
                   {26, "Training Component",  false, true },
                   {26, "Pending Action Items",  false, true },
                   {26, "Open/Closed Status",  false, true },
                   {26, "Active/Inactive Status",  false, true },
             });
        }
        protected void Development_AddReport_TrainingIssueDetails()
        {
            _migrationBuilder.InsertData(
               table: "ReportSkeletons",
               columns: new[] { "DefaultTitle", "Deleted", "Active" },
               values: new object[,]
                 {
                    {"Training Issue Details", false, true }
                 }
            );

            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Order", "Deleted", "Active" },
                  values: new object[,]
                  {
                    {42, 109, 6, false, true }
                  }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                  {109, "Select Training Issue", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingissue", false, true,"listAllSelected",null}
               }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                   {109, "Issue ID",  false, true },
                   {109, "Training Issue Title",  false, true },
                   {109, "Created Date",  false, true },
                   {109, "Due Date",  false, true },
                   {109, "Severity",  false, true },
                   {109, "Issue Status",  false, true },
                   {109, "Driver",  false, true },
                   {109, "Data Element",  false, true },
                   {109, "Description",  false, true },
                   {109, "Planned Response",  false, true },
                   {109, "Action Items",  false, true }
             }
            );

            _migrationBuilder.UpdateData(
            table: "ReportSkeleton_Subcategory_Reports",
            keyColumns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId" },
            keyValues: new object[] { 42, 6 },
            column: "Order",
            value: "7");
        }

        protected void Development_AddReport_TrainingIssuesActionItems()
        {
            _migrationBuilder.InsertData(
               table: "ReportSkeletons",
               columns: new[] { "DefaultTitle", "Deleted", "Active" },
               values: new object[,]
                 {
                    {"Training Issue Action Items", false, true }
                 }
            );

            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Order", "Deleted", "Active" },
                  values: new object[,]
                  {
                    {42, 110, 7, false, true }
                  }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                  {110, "Select Training Issue", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingissue", false, true,"listAllSelected",null},
                  {110, "Action Step Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "actionitemstepstatus", false, true,"All",null},
              }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                   {110, "Issue ID",  false, true },
                   {110, "Training Issue Title",  false, true },
                   {110, "Created Date",  false, true },
                   {110, "Due Date",  false, true },
                   {110, "Severity",  false, true },
                   {110, "Driver",  false, true },
                   {110, "Data Element",  false, true },
                   {110, "Description",  false, true },
                   {110, "Planned Response",  false, true },
                   {110, "Assigned To",  false, true },
                   {110, "Priority",  false, true },
                   {110, "Date Assigned",  false, true },
                   {110, "Action Step Due Date",  false, true },
                   {110, "Date Completed",  false, true },
                   {110, "Status",  false, true },
                   {110, "Notes",  false, true }
             }
            );

            _migrationBuilder.UpdateData(
            table: "ReportSkeleton_Subcategory_Reports",
            keyColumns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId" },
            keyValues: new object[] { 42, 6 },
            column: "Order",
            value: "8");
        }

        protected void Development_UpdateReport_TasksByTrainingTaskGroup()
        {
            _migrationBuilder.UpdateData(
            table: "ReportSkeletons",
            keyColumns: new[] { "Id", "DefaultTitle" },
            keyValues: new object[] { 5, "Tasks by Task Group" },
            column: "DefaultTitle",
            value: "Tasks by Training Task Group"
           );

            _migrationBuilder.DeleteData(
            table: "ReportSkeletonColumns",
            keyColumn: "ReportSkeletonId",
            keyValue: 5
            );

            _migrationBuilder.DeleteData(
            table: "ReportSkeletonFilters",
            keyColumn: "ReportSkeletonId",
            keyValue: 5
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue" },
              values: new object[,]
              {
                  {5, "Select Task Group", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingtasksgroup", false, true,null},
                  {5,"Include Tasks not Assigned to a Task Group", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false"},
                  {5,"Reliability Related Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false" },
                  {5,"Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false" },
                  {5,"Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false" },
              }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
                {
                      {5, "Duty Area",  false, true },
                      {5, "Sub-Duty Area",  false, true },
                      {5, "Linked Positions",  false, true },
                }
            );
        }

        protected void Development_AddNewReportFilters_EmployeeTrainingTowardNERCRecertification()
        {
            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
                values: new object[,]
                {
                    {58, "Include Failed Grade", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null}
                }
            );
        }

        protected void Development_UpdateReportFilters_TasksByPosition()
        {
            _migrationBuilder.DeleteData(
                table: "ReportSkeletonFilters",
                keyColumns: new[] { "ReportSkeletonId", "Name" },
                keyValues: new object[,]
                {
                     {1, "Show Specific Tasks" },
                     {1, "Include Pseudo Tasks" },
                     {1, "Select RRT's" },
                }
            );

            _migrationBuilder.UpdateData(
            table: "ReportSkeletonFilters",
            keyColumns: new[] { "ReportSkeletonId", "Name" },
            keyValues: new object[] { 1, "Group By Training Task Groups" },
            column: "Name",
            value: "Group by Training Task Group"
           );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue" },
              values: new object[,]
              {
                  {1, "Select Task Type", "String", "Single", DateTime.MinValue, DateTime.MinValue, "tasktype", false, true,"Tasks (Regular only)"},
                  {1,"RR Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false" },
                  {1,"All/Active/Inactive Tasks", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true,"Active Only" },
                  {1,"Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false" },
              }
            );
        }
        protected void Development_AddClientSettings_Feature_PublicClasses()
        {

            _migrationBuilder.InsertData(
                table: "ClientSettings_Features",
                columns: new[] { "Feature", "Enabled", "Active", "Deleted" },
                 values: new object[,]
                {
                  { "Public Classes", false,true,false },
                });


        }

        protected void Development_UpdateReportFilters_TrainingIssuesActionItems()
        {
            _migrationBuilder.UpdateData(
              table: "ReportSkeletonFilters",
              keyColumns: new[] { "ReportSkeletonId", "Name" },
              keyValues: new object[] { 110, "Select Training Issue" },
              column: "DefaultValue",
              value: null
             );
        }

        protected void Development_UpdateReportFilters_TrainingIssuesDetails()
        {
            _migrationBuilder.UpdateData(
              table: "ReportSkeletonFilters",
              keyColumns: new[] { "ReportSkeletonId", "Name" },
              keyValues: new object[] { 109, "Select Training Issue" },
              column: "DefaultValue",
              value: null
             );
        }

        protected void Development_AddReportFilters_TrainingProgramCompletionHistory()
        {
            _migrationBuilder.InsertData(
             table: "ReportSkeletonFilters",
             columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue" },
             values: new object[,]
             {
                  {50,"Current Position Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"true" },
                  {50,"All/Active/Inactive Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true,"Active Only" },
             }
           );
        }

        protected void Development_UpdateReportFilters_TrainingProgramCompletionHistory()
        {
            _migrationBuilder.DeleteData(
                 table: "ReportSkeletonFilters",
                 keyColumn: "Name",
                 keyValue: "Current Position Only"
                 );
        }
        protected void Development_AddDataToTrainingIssueActionItemsAssigneeName()
        {
            _migrationBuilder.Sql(@"UPDATE a
                SET AssigneeName = CONCAT(p.FirstName, ' ', p.LastName)
                FROM TrainingIssueActionItems a
                JOIN QTDUsers u ON a.AssignedToId = u.Id
                JOIN Persons p ON u.PersonId = p.Id
                WHERE a.AssignedToId IS NOT NULL;"
            );
        }

        protected void Development_SeedInitialReportData()
        {
            _migrationBuilder.InsertData(
             table: "ReportSkeleton_Categories",
             columns: new[] { "Name", "Deleted", "Active" },
             values: new object[,]
               {
                                   { "All Reports", false, true },
                                   { "Per 005", false, true },
                                   { "CEH Total 1", false, true },
                                   { "CEH Total 2", false, true},
               });

            _migrationBuilder.InsertData(
             table: "ReportSkeleton_Subcategories",
             columns: new[] { "Id", "Name", "ReportSkeleton_CategoryId", "Order", "Deleted", "Active" },
             values: new object[,]
             {
                    {  13, "R1", 2, 1, false, true },
                    {  14, "R1.1", 2, 2, false, true },
                    {  15, "R1.1.1", 2, 3, false, true },
                    {  16, "R1.2", 2, 4, false, true },
                    {  17, "R1.3", 2, 5, false, true },
                    {  18, "R1.4", 2, 6, false, true },
                    {  19 , "R2", 2, 7, false, true },
                    {  20, "R2.1", 2, 8, false, true },
                    {  21, "R2.2", 2, 9, false, true },
                    {  22, "R2.3", 2, 10, false, true },
                    {  23, "R2.4", 2, 11, false, true },
                    {  24 ,"R3", 2, 12, false, true },
                    {  25, "R3.1", 2, 13, false, true },
                    {  26, "R4", 2, 14, false, true },
                    {  27, "R4.1", 2, 15, false, true },
                    {  28, "R5", 2, 16, false, true },
                    {  29, "R5.1", 2, 17, false, true },
                    {  30, "R6", 2, 18, false, true },
                    {  31, "R6.1", 2, 19, false, true },
                    {  32, "Reports", 3, 1, false, true },
                    {  33, "Reports", 4, 1, false, true },
                    {  35, "Employee Data", 1, 1, false, true },
                    {  36, "Positions", 1, 2, false, true },
                    {  37, "Tasks", 1, 3, false, true },
                    {  38, "Enabling Objectives", 1, 4, false, true },
                    {  39, "Training Design & Development", 1, 8, false, true },
                    {  40, "Training Implementation & Delivery", 1, 9, false, true },
                    {  41, "Task Qualification & Requalification Records", 1, 10, false, true },
                    {  42, "Evaluations", 1, 11, false, true },
                    {  43, "NERC Reports", 1, 12, false, true },
                    {  44, "Meta ILAs", 1, 13, false, true },
                    {  45, "Employee Portal Completions", 1, 14, false, true },
                    {  47, "Surveys", 1, 7, false, true },
                    {  48, "Procedures", 1, 5, false, true },
                    {  49, "Data Quality Control", 1, 15, false, true },
                    {  50, "Safety Hazards", 1, 6, false, true }
             });

            _migrationBuilder.InsertData(
                table: "ReportSkeletons",
                columns: new[] { "Id", "DefaultTitle", "Deleted", "Active" },
                values: new object[,]
                {
                        { 1, "Tasks by Positions", false, true },
                        { 2, "IDP Review Completion History", false, true },
                        { 3, "Training Schedule By Class", false, true },
                        { 4, "ILA By Provider", false, true },
                        { 5, "Tasks by Task Group", false, true },
                        { 6, "Student Evaluation - Blank Form", false, true },
                        { 7, "Position Details", false, true },
                        { 8, "Employees By Position", false, true },
                        { 9, "Position Linkages", false, true },
                        { 10, "Employees By Organization", false, true },
                        { 11, "Employee Position History", false, true },
                        { 12, "Employee Certification History", false, true },
                        { 13, "ILA Completion History", false, true },
                        { 14, "Classes by ILA", false, true },
                        { 15, "ILA Design Specification", false, true },
                        { 16, "Task Qualification Evaluators", false, true },
                        { 17, "Student Evaluation Results - Self Paced", false, true },
                        { 18, "Task Requalification by Position", false, true },
                        { 19, "Task Qualification Records", false, true },
                        { 20, "Test Items", false, true },
                        { 21, "Test Specifications", false, true },
                        { 22, "Task Requalification by Employee", false, true },
                        { 23, "List of Task Evaluators", false, true },
                        { 24, "Student Evaluation Results - Instructor Led", false, true },
                        { 25, "List of NERC Certified Operators", false, true },
                        { 26, "Issues List", false, true },
                        { 27, "Training Summary by Position", false, true },
                        { 28, "OJT Guide By Position", false, true },
                        { 29, "OJT Guide By Task", false, true },
                        { 30, "OJT Guide By ILA", false, true },
                        { 31, "Task Qualification Sheets by Position", false, true },
                        { 32, "Task Qualification Sheets by Task", false, true },
                        { 33, "Task Qualification Sheets by ILA", false, true },
                        { 34, "Procedure Review Completion History", false, true },
                        { 35, "Training Program Review", false, true },
                        { 36, "Enabling Objectives By Task", false, true },
                        { 37, "Enabling Objectives By Position", false, true },
                        { 38, "Employee Training Needs Assessment", false, true },
                        { 39, "Class Roster", false, true },
                        { 40, "Annual Positions Task List Review", false, true },
                        { 41, "EOP Hours for Designated Years", false, true },
                        { 42, "Task and Enabling Objective By ILA", false, true },
                        { 43, "Record of Task/ EO Qualifications", false, true },
                        { 44, "Task History by Position", false, true },
                        { 45, "Training Material for Task and Associated EOs by Positions", false, true },
                        { 46, "Training Program By Positions", false, true },
                        { 47, "Employee Training Status", false, true },
                        { 48, "Training Module Completion History By Employee", false, true },
                        { 49, "Training Module Completion History", false, true },
                        { 50, "Training Program Completion History", false, true },
                        { 51, "Class Sign In Sheet", false, true },
                        { 53, "DIF Survey - Blank Form", false, true },
                        { 54, "DIF Survey - Individual Feedback", false, true },
                        { 55, "DIF Survey - Aggregate Results", false, true },
                        { 56, "Year To Date Hours For Certified Employees", false, true },
                        { 57, "Total NERC CEHS for the Year to Date", false, true },
                        { 58, "Employee Training Toward NERC Recertification", false, true },
                        { 59, "Employee Training Toward Cert and All Required Training", false, true },
                        { 60, "Employee Delinquency Report", false, true },
                        { 61, "Procedure Review Completion History by Employee", false, true },
                        { 62, "Tasks Met by Position", false, true },
                        { 63, "Tasks Met by Employee", false, true },
                        { 64, "Employee Active/Inactive History", false, true },
                        { 65, "Reliability Related Task Impact Matrix (R5)", false, true },
                        { 66, "EMP Test Summary by Classes", false, true },
                        { 67, "Employee Training Toward All Cert and Req. Training Summary", false, true },
                        { 68, "EMP Task Qualification Details", false, true },
                        { 69, "Employee Course Schedule for Given Year", false, true },
                        { 70, "Employee IDP Completion Status Report", false, true },
                        { 71, "Task History By Task", false, true },
                        { 72, "Employee Training Toward Procedures & Regulatory Requirements", false, true },
                        { 73, "Procedure & Regulatory Requirement Training Summary by Position", false, true },
                        { 74, "Summary of Task Qualification by Sub-Duty Area", false, true },
                        { 75, "Employee Task Qualification Dates by Task", false, true },
                        { 76, "Employee Task Qualification Records for Given Position", false, true },
                        { 77, "EMP Test Statistics", false, true },
                        { 78, "SCORM Completion Summary by Classes", false, true },
                        { 79, "SCORM Test Completion Statistics", false, true },
                        { 80, "OJT Training Log", false, true },
                        { 81, "Procedure by Issuing Authority", false, true },
                        { 82, "Procedures by Task", false, true },
                        { 83, "Enabling Objectives Not Linked to Tasks", false, true },
                        { 84, "Enabling Objectives Not Linked to ILA", false, true },
                        { 85, "Tasks by Duty Area", false, true },
                        { 86, "Enabling Objectives by Category", false, true },
                        { 87, "Test Report - Paper Based Version", false, true },
                        { 88, "Tasks by Enabling Objectives", false, true },
                        { 89, "ILAs by Task", false, true },
                        { 90, "ILAs by Enabling Objective", false, true },
                        { 91, "ILAs with EOs Required By Positions", false, true },
                        { 92, "Skills Qualification Training Guide by Position or Skill", false, true },
                        { 93, "Skill Qualification Assessment by Position or by Task", false, true },
                        { 94, "Safety Hazards by Position Matrix", false, true },
                        { 95, "Safety Hazards by Category", false, true },
                        { 96, "Class Certificates", false, true },
                        { 97, "Pretest & Final Test Comparison Report", false, true },
                        { 98, "Certified Employees for Given Certificate", false, true },
                        { 99, "Tasks Not Linked to ILA", false, true },
                        { 100, "Tasks Not Assigned to Position", false, true },
                        { 101, "Tasks by Procedure", false, true },
                        { 102, "ILAs by Meta ILA", false, true },
                        { 103, "Tasks by Position Matrix", false, true },
                        { 104, "Test List", false, true },
                        { 105, "Safety Hazards by Task", false, true },
                        { 106, "Enabling Objectives by Position Matrix", false, true },
                        { 107, "Enabling Objectives by Safety Hazard", false, true },
                        { 108, "NERC ILA Application - Report Version", false, true }
                });


            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Deleted", "Active", "Order", },
                  values: new object[,]
                  {

                        {13,  15,  false, true,   1   },
                        {14,  1,   false, true,   1   },
                        {15,  18, false, true,  1   },
                        {16 , 15  ,false, true, 1   },
                        {17 , 14  ,false, true, 1   },
                        {18 , 26  ,false, true, 1   },
                        {18 , 35  ,false, true, 2   },
                        {21 , 15  ,false, true, 1   },
                        {22 , 14  ,false, true, 1   },
                        {23 , 26  ,false, true, 1   },
                        {23 , 35  ,false, true, 2   },
                        {24 , 22  ,false, true, 1   },
                        {24 , 18  ,false, true, 2   },
                        {24 , 31  ,false, true, 3   },
                        {25 , 22  ,false, true, 1   },
                        {25 , 18  ,false, true, 2   },
                        {25 , 31  ,false, true, 3   },
                        {26 , 15  ,false, true, 1   },
                        {27 , 15  ,false, true, 1   },
                        {28 , 15  ,false, true, 1   },
                        {29 , 26  ,false, true, 1   },
                        {29 , 35  ,false, true, 2   },
                        {30 , 15  ,false, true, 1   },
                        {30 , 14  ,false, true, 2   },
                        {31 , 26  ,false, true, 1   },
                        {31 , 35  ,false, true, 2   },
                        {33 , 13  ,false, true, 1   },
                        {16 , 36  ,false, true, 2   },
                        {21 , 36  ,false, true, 2   },
                        {13 , 37  ,false, true, 2   },
                        {21 , 37  ,false, true, 3   },
                        {28 , 37  ,false, true, 2   },
                        {30 , 37  ,false, true, 3   },
                        {13 , 38  ,false, true, 3   },
                        {24 , 38  ,false, true, 4   },
                        {28 , 38  ,false, true, 3   },
                        {30 , 38  ,false, true, 4   },
                        {17 , 39  ,false, true, 2   },
                        {22 , 39  ,false, true, 2   },
                        {30 , 39  ,false, true, 5   },
                        {33 , 39  ,false, true, 2   },
                        {14 , 40  ,false, true, 2   },
                        {15 , 40  ,false, true, 2   },
                        {19 , 40  ,false, true, 1   },
                        {20 , 40  ,false, true, 1   },
                        {26 , 41  ,false, true, 2   },
                        {27 , 41  ,false, true, 2   },
                        {16 , 42  ,false, true, 3   },
                        {21 , 42  ,false, true, 4   },
                        {24 , 43  ,false, true, 5   },
                        {25 , 43  ,false, true, 4   },
                        {15 , 44  ,false, true, 3   },
                        {24 , 44  ,false, true, 6   },
                        {25 , 44  ,false, true, 5   },
                        {16 , 45  ,false, true, 4   },
                        {21 , 45  ,false, true, 5   },
                        {13 , 46  ,false, true, 4   },
                        {16 , 46  ,false, true, 5   },
                        {21 , 46  ,false, true, 6   },
                        {28 , 46  ,false, true, 4   },
                        {30 , 46  ,false, true, 6   },
                        {26 , 47  ,false, true, 3   },
                        {27 , 47  ,false, true, 3   },
                        {19 , 1   ,false, true, 2   },
                        {20 , 1   ,false, true, 2   },
                        {24 , 8   ,false, true, 7   },
                        {25 , 8   ,false, true, 6   },
                        {24 , 10  ,false, true, 8   },
                        {25 , 10  ,false, true, 7   },
                        {24 , 19  ,false, true, 9   },
                        {25 , 19  ,false, true, 8   },
                        {24 , 32  ,false, true, 10  },
                        {25 , 32  ,false, true, 9   },
                        {24 , 33  ,false, true, 11  },
                        {25 , 33  ,false, true, 10  },
                        {16 , 48  ,false, true, 6   },
                        {16 , 49  ,false, true, 7   },
                        {16 , 50  ,false, true, 8   },
                        {17 , 50  ,false, true, 3   },
                        {21 , 50  ,false, true, 7   },
                        {22 , 50  ,false, true, 3   },
                        {30 , 50  ,false, true, 7   },
                        {32 , 17  ,false, true, 1   },
                        {32 , 24  ,false, true, 2   },
                        {33 , 17  ,false, true, 3   },
                        {33 , 24  ,false, true, 4   },
                        {13 , 13  ,false, true, 5   },
                        {17 , 51  ,false, true, 4   },
                        {22 , 51  ,false, true, 4   },
                        {30 , 51  ,false, true, 8   },
                        {33 , 51  ,false, true, 5   },
                        {35 , 10  ,false, true, 1   },
                        {35 , 8   ,false, true, 2   },
                        {43 , 25  ,false, true, 10  },
                        {35 , 11  ,false, true, 4   },
                        {35 , 12  ,false, true, 5   },
                        {35 , 38  ,false, true, 7   },
                        {35 , 47  ,false, true, 8   },
                        {36 , 7   ,false, true, 1   },
                        {36 , 9   ,false, true, 2   },
                        {37 , 1   ,false, true, 1   },
                        {37 , 5   ,false, true, 3   },
                        {37 , 44  ,false, true, 6   },
                        {38 , 37  ,false, true, 2   },
                        {38 , 36  ,false, true, 3   },
                        {39 , 46  ,false, true, 1   },
                        {39 , 4   ,false, true, 2   },
                        {39 , 15  ,false, true, 3   },
                        {39 , 42  ,false, true, 4   },
                        {39 , 45  ,false, true, 5   },
                        {39 , 28  ,false, true, 6   },
                        {39 , 29  ,false, true, 7   },
                        {39 , 30  ,false, true, 8   },
                        {39 , 31  ,false, true, 10  },
                        {39 , 32  ,false, true, 11  },
                        {39 , 33  ,false, true, 12  },
                        {39 , 20  ,false, true, 15  },
                        {39 , 21  ,false, true, 18  },
                        {40 , 3   ,false, true, 1   },
                        {40 , 39  ,false, true, 2   },
                        {40 , 51  ,false, true, 3   },
                        {40 , 14  ,false, true, 4   },
                        {40 , 50  ,false, true, 5   },
                        {40 , 13  ,false, true, 6   },
                        {41 , 16  ,false, true, 1   },
                        {41 , 19  ,false, true, 2   },
                        {41 , 43  ,false, true, 6   },
                        {41 , 18  ,false, true, 8   },
                        {41 , 22  ,false, true, 9   },
                        {42 , 6   ,false, true, 6   },
                        {42 , 24  ,false, true, 1   },
                        {42 , 17  ,false, true, 2   },
                        {42 , 40  ,false, true, 3   },
                        {42 , 35  ,false, true, 4   },
                        {42 , 26  ,false, true, 5   },
                        {43 , 27  ,false, true, 8   },
                        {43 , 41  ,false, true, 9   },
                        {44 , 48  ,false, true, 2   },
                        {44 , 49  ,false, true, 3   },
                        {44 , 2   ,false, true, 4   },
                        {45 , 34  ,false, true, 4   },
                        {47 , 53  ,false, true, 1   },
                        {47 , 54  ,false, true, 2   },
                        {47 , 55  ,false, true, 3   },
                        {43 , 56  ,false, true, 7   },
                        {43 , 57  ,false, true, 6   },
                        {43 , 58  ,false, true, 2   },
                        {43 , 59  ,false, true, 3   },
                        {43 , 60  ,false, true, 5   },
                        {45 , 61  ,false, true, 5   },
                        {41 , 62  ,false, true, 5   },
                        {41 , 63  ,false, true, 4   },
                        {35 , 64  ,false, true, 6   },
                        {36 , 65  ,false, true, 3   },
                        {45 , 66  ,false, true, 1   },
                        {43 , 67  ,false, true, 4   },
                        {45 , 68  ,false, true, 6   },
                        {35 , 69  ,false, true, 9   },
                        {35 , 70  ,false, true, 10  },
                        {37 , 71  ,false, true, 7   },
                        {35 , 72  ,false, true, 11  },
                        {35 , 73  ,false, true, 12  },
                        {41 , 74  ,false, true, 7   },
                        {41 , 75  ,false, true, 10  },
                        {41 , 76  ,false, true, 3   },
                        {45 , 77  ,false, true, 3   },
                        {45 , 78  ,false, true, 7   },
                        {45 , 79  ,false, true, 8   },
                        {39 , 80  ,false, true, 9   },
                        {48 , 81  ,false, true, 1   },
                        {48 , 82  ,false, true, 2   },
                        {49 , 83  ,false, true, 3   },
                        {49 , 84  ,false, true, 4   },
                        {37 , 85  ,false, true, 2   },
                        {38 , 86  ,false, true, 1   },
                        {39 , 87  ,false, true, 17  },
                        {37 , 88  ,false, true, 5   },
                        {49 , 89  ,false, true, 5   },
                        {49 , 90  ,false, true, 6   },
                        {49 , 91  ,false, true, 7   },
                        {39 , 92  ,false, true, 13  },
                        {39 , 93  ,false, true, 14  },
                        {50 , 94  ,false, true, 4   },
                        {50 , 95  ,false, true, 1   },
                        {40 , 96  ,false, true, 7   },
                        {45 , 97  ,false, true, 2   },
                        {35 , 98  ,false, true, 3   },
                        {49 , 99  ,false, true, 2   },
                        {49 , 100 ,false, true, 1   },
                        {37 , 101 ,false, true, 4   },
                        {44 , 102 ,false, true, 1   },
                        {36 , 103 ,false, true, 4   },
                        {39 , 104 ,false, true, 16  },
                        {50 , 105 ,false, true, 2   },
                        {38 , 106 ,false, true, 4   },
                        {50 , 107 ,false, true, 3   },
                        { 43,  108, false, true,1  },
                  });


            _migrationBuilder.InsertData(
     table: "ReportSkeletonFilters",
     columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
     values: new object[,]
     {
        { 1, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 1, "Group By Training Task Groups", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 1, "Show Specific Tasks", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TaskOptions", false, true, null, null },
        { 1, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 2, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 2, "IDP Reviewers", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "IDP Reviewers", false, true, null, null },
        { 2, "Include Inactive Employees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 2, "IDP Status", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "IDP Status", false, true, null, null },
        { 3, "Employee Active Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 3, "Class", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "ClassSchedule", false, true, null, null },
        { 4, "Providers", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Providers", false, true, null, null },
        { 4, "ILA Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 4, "ILAs with Approvals expiring within Months", "String", "Single", DateTime.MinValue, DateTime.MinValue, "MonthNumber", false, true, null, null },
        { 4, "Delivery Method", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "DeliveryMethods", false, true, null, null },
        { 5, "Task Group Range", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TaskGroupDescriptions", false, true, null, null },
        { 5, "Task Group Id", "Int", "Single", DateTime.MinValue, DateTime.MinValue, "TaskGroups", false, true, null, null },
        { 6, "Select Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "allclassschedule", false, true, null, null },
        { 6, "Select Form", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "studentevaluationforms", false, true, null, null },
        { 7, "Active Only/Inactive Only/All Positions", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 7, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 7, "Select Position Version", "Int", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 8, "By Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 8, "Active Only/Inactive Only/All Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 8, "Include Current Position Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 8, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 9, "Select Positions", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 9, "Tasks Linked to Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 9, "Enabling Objectives Linked to Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 9, "EOs flagged as Skills Linked to Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 9, "Meta EOs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 9, "Employee", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 9, "All", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 9, "Select Date Range", "Date", "Range", new DateTime(2000, 1, 1), DateTime.MinValue, null, false, true, null, null },
        { 10, "By Organization", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Organizations", false, true, null, null },
        { 10, "Active Only/Inactive Only/All Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 11, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 12, "Employee Filter", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 13, "ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Courses", false, true, null, null },
        { 13, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 13, "Completion Type", "String", "Single", DateTime.MinValue, DateTime.MinValue, "completiontype", false, true, null, null },
        { 14, "ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Courses", false, true, null, null },
        { 14, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 14, "Select Instructor", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "instructor", false, true, null, null },
        { 14, "Select Location", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "location", false, true, null, null },
        { 15, "ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "courses", false, true, null, null },
        { 16, "Select Task Qualification Evaluators", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "taskqualification", false, true, null, null },
        { 16, "Show Assigned and Pending Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 16, "Show Completed Task Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 17, "Select ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "SelfpacedCourses", false, true, null, null },
        { 17, "Select Form", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "StudentEval", false, true, null, null },
        { 17, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 18, "Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 18, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 18, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 18, "R-R Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 19, "Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 19, "Current Position(s) only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 19, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 19, "Reliability Related Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 19, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 19, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 19, "Include trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 20, "Select Test Items", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TaskListReviewStatus", false, true, "Active", null },
        { 20, "Item Type", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "testitemtypes", false, true, "1, 2, 3, 4, 5, 6", null },
        { 20, "Taxonomy Level", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "taxonomylevel", false, true, "1, 2, 3, 4, 5", null },
        { 20, "Only Test Items with No EO linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 20, "Only Test Items not linked to Test", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 21, "Tests", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Tests", false, true, null, null },
        { 21, "Show Test Completion Statistics", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 21, "Show Correct Answer", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 22, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Employees", false, true, null, null },
        { 22, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 22, "R-R Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 22, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 23, "Evaluators To Filter", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 23, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 24, "Select Form", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "StudentEval", false, true, null, null },
        { 24, "Training Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingclasses", false, true, null, null },
        { 25, "Filter by Organization", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Organizations", false, true, null, null },
        { 25, "All Company Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 26, "Program Type", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TrainingPrograms", false, true, null, null },
        { 26, "ILA", "String", "Single", DateTime.MinValue, DateTime.MinValue, "Courses", false, true, null, null },
        { 26, "OJT / Task Qualification", "String", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 26, "Open Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 27, "Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 27, "Organization", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Organizations", false, true, null, null },
        { 28, "Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 28, "Show RR Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 28, "Task Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 29, "Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 29, "Show RR Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 30, "ILA", "String", "Single", DateTime.MinValue, DateTime.MinValue, "courses", false, true, null, null },
        { 30, "Show RR Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 30, "Task Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 31, "Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 31, "Only R-R* for any of the Positions", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 31, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 31, "Task Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "true", null },
        { 32, "Task", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 33, "ILA", "String", "Single", DateTime.MinValue, DateTime.MinValue, "courses", false, true, null, null },
        { 33, "Only R-R* for any of the Positions", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 33, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 33, "Task Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "true", null },
        { 34, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 34, "Procedures", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "procedures", false, true, null, null },
        { 34, "Employee Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, null, null },
        { 34, "Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 34, "Organization", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Organizations", false, true, null, null },
        { 34, "Completion Type", "String", "Single", DateTime.MinValue, DateTime.MinValue, "CompletionType", false, true, null, null },
        { 35, "Select Training Program Review", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TrainingProgramReviews", false, true, null, null },
        { 36, "Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 37, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 37, "Select Objectives to Include", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "enablingobjectivetypes", false, true, "listAllSelected", null },
        { 38, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 38, "Select Training Program", "String", "Single", DateTime.MinValue, DateTime.MinValue, "initialtrainingprograms", false, true, null, null },
        { 38, "Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 39, "Training Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "ClassRosterSchedule", false, true, null, null },
        { 40, "Task List Reviews", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "TaskListReview", false, true, null, null },
        { 40, "Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "TaskListReviewStatus", false, true, "All", null },
        { 41, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 41, "Summary Report", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 41, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 42, "ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Courses", false, true, null, null },
        { 42, "Include Task and EO Filter", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "TaskEoIncludeOption", false, true, "1, 2, 3", null },
        { 43, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 43, "Include Evaluator and Mode of Qualification", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 43, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 43, "Print All Completed Tasks First", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 44, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 44, "Exclude Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 44, "Include Task Modification Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 44, "Include *R-R Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 44, "All Tasks Changed Since", "Date", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 45, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 45, "Show Training Resources", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 45, "Include ILAs that cover the EO associated with the Task", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 46, "Select Training Program", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingprogramtype", false, true, null, null },
        { 46, "Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 47, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 47, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 48, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 48, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 48, "Training Module", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingmoduleoption", false, true, null, null },
        { 48, "Include Inactive Employees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 48, "Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 49, "Training Module", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingmodule", false, true, null, null },
        { 49, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 49, "Training Module Option", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingmoduleoption", false, true, null, null },
        { 49, "Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 49, "Include Inactive Employee", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 50, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 50, "Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 50, "Select Training Program", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingprogramtype", false, true, null, null },
        { 51, "Training Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "ClassSignInSchedule", false, true, null, null },
        { 53, "Select DIF Survey", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "DifSurvey", false, true, null, null },
        { 54, "Select Survey", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "DifSurvey", false, true, null, null },
        { 54, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 55, "Select Survey", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "DifSurvey", false, true, null, null },
        { 55, "Task Active Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 55, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 55, "Training Frequency", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingfrequency", false, true, "0", null },
        { 56, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 56, "Select Year", "String", "Single", DateTime.MinValue, DateTime.MinValue, "YearFilter", false, true, null, null },
        { 57, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 57, "Select Certificates & Training Requirements", "String", "Single", DateTime.MinValue, DateTime.MinValue, "certifications", false, true, null, null },
        { 58, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 58, "Include Scheduled ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 58, "Include NERC Provider ILAs only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 59, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 60, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 61, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 61, "Published Procedure Reviews", "String", "Single", DateTime.MinValue, DateTime.MinValue, "procedurereviewstatus", false, true, "Published", null },
        { 61, "Completion Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 61, "Completion Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "completionstatus", false, true, "Completed", null },
        { 62, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 62, "Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 62, "Current Positions only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 62, "Reliability-Related Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 62, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 62, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 62, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 63, "Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 63, "Current Positions Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 63, "Reliability Related Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 63, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 63, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 63, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 64, "Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 65, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "taskr5impacted", false, true, null, null },
        { 66, "Select Class", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "allclassschedule", false, true, null, null },
        { 66, "Select Test Type", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tests", false, true, null, null },
        { 66, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 66, "Include Test Answer Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 66, "Show only Failed Grades", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 67, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 68, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 68, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 68, "Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 68, "Task Qualification Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "onlycompletionstatus", false, true, "All", null },
        { 68, "Evaluator Completion Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "onlycompletionstatus", false, true, "All", null },
        { 69, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 69, "Select Year", "String", "Single", DateTime.MinValue, DateTime.MinValue, "YearFilter", false, true, null, null },
        { 69, "Active/Inactive/All ILAs", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 69, "ILA Completion Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "completedstatus", false, true, "All", null },
        { 70, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 70, "Select Year", "String", "Single", DateTime.MinValue, DateTime.MinValue, "YearFilter", false, true, null, null },
        { 70, "Active/Inactive/All ILAs", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 70, "ILA Completion Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "completedstatus", false, true, "All", null },
        { 71, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 71, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 72, "Select Employee", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 72, "Select Procedures", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "procedures", false, true, null, null },
        { 72, "Select Regulatory Requirements", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "regulatoryrequirement", false, true, null, null },
        { 72, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 73, "Select Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, null },
        { 73, "Active Only/Inactive Only/All Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 73, "Current Position(s) Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "true", null },
        { 73, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 73, "Select up to 10 Procedures", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "procedures", false, true, null, 10 },
        { 73, "Select up to 10 Regulatory Requirements", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "regulatoryrequirement", false, true, null, 10 },
        { 73, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 74, "Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 74, "Select Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 74, "Reliability Related Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 74, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 74, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 74, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 74, "Include Task Qualification Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 75, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 75, "RR Tasks only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 75, "Active Only/Inactive Only/All Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 75, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 75, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 76, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 76, "Select Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 76, "Reliability Related Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 76, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 76, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 76, "Date Range", "Date", "Range", DateTime.MinValue, DateTime.MinValue, null, false, true, null, null },
        { 76, "Include Trainees", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 77, "Select Class", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "classsigninschedule", false, true, null, null },
        { 77, "Select Test Type", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tests", false, true, null, null },
        { 78, "Select Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "classschedulewithilaproviderlocation", false, true, null, null },
        { 78, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 78, "Include Test Answer Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 78, "Show only Failed Grades", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 79, "Select Class", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "classschedulewithilaproviderlocation", false, true, null, null },
        { 80, "Select Employee", "String", "Single", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 80, "Select Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 80, "Task Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 80, "R-R Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 80, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 81, "Issuing Authority", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "issuingauthority", false, true, null, null },
        { 81, "Include Inactive Procedures", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 82, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 82, "Include Inactive Procedures", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 83, "All/Active/Inactive Enabling Objectives", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 83, "Include Meta EOs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 83, "Include Skill Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 84, "All/Active/Inactive Enabling Objectives", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 84, "Include Meta EOs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 84, "Include Skill Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 85, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 86, "Select Enabling Objective Categories", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "enablingobjectivecategories", false, true, null, null },
        { 86, "Enabling Objective Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "activestatus", false, true, "Active Only", null },
        { 86, "Show Meta EOs Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 86, "Show Skill Qualifications Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 86, "Show Category, Sub-Category, And Topic Labels only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 87, "Select Test", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tests", false, true, null, null },
        { 87, "Show Correct Answer", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 88, "Select Enabling Objective", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectenablingobjective", false, true, null, null },
        { 88, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 88, "Only show EOs with Tasks Linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 89, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 89, "Include Tasks With No ILAs Linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 90, "Select Enabling Objectives", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectenablingobjective", false, true, null, null },
        { 90, "Include Enabling Objectives With No ILAs Linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 91, "Select Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 91, "Include Positions With No ILAs Linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 92, "Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 92, "Select Skill Qualification", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectskillqualification", false, true, null, null },
        { 92, "Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "onlyactiveinactivestatus", false, true, "Active", null },
        { 92, "Include Employee Name and Certificate No", "String", "Single", DateTime.MinValue, DateTime.MinValue, "employeewithnerccertification", false, true, null, null },
        { 93, "Position", "String", "Single", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, null },
        { 93, "Select Skill Qualification", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectskillqualification", false, true, null, null },
        { 93, "Status", "String", "Single", DateTime.MinValue, DateTime.MinValue, "onlyactiveinactivestatus", false, true, "Active", null },
        { 93, "Include Employee Name and Certificate No", "String", "Single", DateTime.MinValue, DateTime.MinValue, "employeewithnerccertification", false, true, null, null },
        { 94, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, 10 },
        { 94, "Include Inactive Safety Hazards", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 95, "Safety Hazard Category", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "safetyhazardscategories", false, true, null, null },
        { 95, "Include Safety Hazard Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 95, "Include Inactive Safety Hazards", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 96, "Select Class", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "ClassSignInSchedule", false, true, null, null },
        { 96, "Print for all registered students before grade is awarded", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 96, "Include Failed Students", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 97, "Select Classes", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "classseswithilastatusproviderlocation", false, true, null, null },
        { 97, "Select Employees", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "employees", false, true, null, null },
        { 97, "Include Test Items Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 97, "Show only Failed Pretest Grades", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 98, "Select Certificate", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectcertificate", false, true, null, null },
        { 98, "Filter by Organization", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Organizations", false, true, null, null },
        { 98, "All Company Employees", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 99, "Active/Inactive/All Tasks", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 99, "Reliability Related Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 99, "Include Meta Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 99, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 100, "Active/Inactive/All Tasks", "String", "Single", DateTime.MinValue, DateTime.MinValue, "ActiveStatus", false, true, "Active Only", null },
        { 100, "Reliability Related Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 100, "Include Meta Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 100, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 101, "Procedure", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "procedures", false, true, null, null },
        { 101, "Reliability Related Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 101, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 102, "Select Meta ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "metaila", false, true, null, null },
        { 102, "Include Objectives linked to ILAs", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "objectivelinkedtoila", false, true, null, null },
        { 103, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true, null, 10 },
        { 103, "Reliability Related Tasks Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 103, "Include Meta Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 103, "Include Inactive Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 103, "Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 104, "Select ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Courses", false, true, null, null },
        { 104, "Test Type", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "testtype", false, true, "listAllSelected", null },
        { 104, "Status", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "teststatus", false, true, "listAllSelected", null },
        { 105, "Select Tasks", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "tasks", false, true, null, null },
        { 105, "Include Safety Hazard Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 105, "Include Inactive Safety Hazards", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 106, "Positions", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "positions", false, true, null, 10 },
        { 106, "Include Meta Enabling Objectives", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 106, "Include Skill Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 106, "Include Inactive Enabling Objectives", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 107, "Safety Hazards", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "safetyhazard", false, true, null, null },
        { 107, "Include Safety Hazard Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 107, "Include Meta Enabling Objectives", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 107, "Include Skill Qualifications", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 107, "Include Inactive Enabling Objectives", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true, "false", null },
        { 108, "Select ILA", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectilas", false, true, null, null }
     }
 );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
                {
                         { 1, "Include Enabling Objectives", false, true },
                         { 1, "Positions Description", false, true },
                         { 1, "R-R Definition", false, true },
                         { 1, "Review Date", false, true },
                         { 1, "Safety Hazards", false, true },
                         { 1, "Task Details", false, true },
                         { 1, "Task Steps & Sub-Steps", false, true },
                         { 1, "Tools", false, true },
                         { 2, "Completed Date", false, true },
                         { 2, "Completion Status", false, true },
                         { 2, "IDP Review Response", false, true },
                         { 2, "Position Abbreviation", false, true },
                         { 2, "Release Date", false, true },
                         { 2, "Employee Name", false, true },
                         { 4, "Approval Date", false, true },
                         { 4, "ILA Application Date", false, true },
                         { 4, "ILA Number", false, true },
                         { 4, "ILA Title", false, true },
                         { 4, "Nerc - EOPs", false, true },
                         { 4, "Operational Topic", false, true },
                         { 4, "Other", false, true },
                         { 4, "Partial Credit?", false, true },
                         { 4, "Per-005 EOPs", false, true },
                         { 4, "Regional 2", false, true },
                         { 4, "Regional", false, true },
                         { 4, "Self-paced ?", false, true },
                         { 4, "Simulator", false, true },
                         { 4, "Standards", false, true },
                         { 4, "Total CEHs", false, true },
                         { 4, "Total", false, true },
                         { 5, "Duty Area", false, true },
                         { 5, "SubDutyArea", false, true },
                         { 5, "TaskDescription", false, true },
                         { 5, "TaskId", false, true },
                         { 5, "Topic", false, true },
                         { 6, "Class End Date & Time", false, true },
                         { 6, "Class Start Date & Time", false, true },
                         { 6, "Evaluation Rating Scale", false, true },
                         { 6, "Form Name", false, true },
                         { 6, "ILA Name", false, true },
                         { 6, "ILA Number", false, true },
                         { 6, "Include General Comments Section", false, true },
                         { 6, "Instructor", false, true },
                         { 6, "Location", false, true },
                         { 6, "Provider", false, true },
                         { 7, "Effective Date", false, true },
                         { 7, "Note", false, true },
                         { 7, "Position Abbreviation", false, true },
                         { 7, "Position Description", false, true },
                         { 8, "Employee Email Address", false, true },
                         { 8, "Employee Notes", false, true },
                         { 8, "Employee Number", false, true },
                         { 8, "NERC Cert #", false, true },
                         { 8, "NERC Cert Expiration Date", false, true },
                         { 8, "NERC Cert Issue Date", false, true },
                         { 8, "NERC Cert Recert Date", false, true },
                         { 8, "NERC Cert Type", false, true },
                         { 8, "Position Qualification Date", false, true },
                         { 8, "Task Qualification Evaluator", false, true },
                         { 9, "Position Qualification Date", false, true },
                         { 9, "Start Date", false, true },
                         { 9, "Trainee Status", false, true },
                         { 10, "Email Address", false, true },
                         { 10, "Employee Notes", false, true },
                         { 10, "Employee Number", false, true },
                         { 10, "NERC Cert #", false, true },
                         { 10, "NERC Cert Expiration Date", false, true },
                         { 10, "NERC Cert Issue Date", false, true },
                         { 10, "NERC Cert Recert Date", false, true },
                         { 10, "NERC Cert Type", false, true },
                         { 10, "Position", false, true },
                         { 10, "Task Qualification Evaluator", false, true },
                         { 11, "Current Positions", false, true },
                         { 11, "Employee", false, true },
                         { 11, "End Date", false, true },
                         { 11, "Organization", false, true },
                         { 11, "Positions", false, true },
                         { 11, "Qualification Date", false, true },
                         { 11, "Start Date", false, true },
                         { 12, "Cert Area", false, true },
                         { 12, "Certification #", false, true },
                         { 12, "Employee", false, true },
                         { 12, "Expiration Date", false, true },
                         { 12, "Issue Date", false, true },
                         { 12, "Organization", false, true },
                         { 12, "Positions", false, true },
                         { 12, "Renewal Date", false, true },
                         { 13, "Certification Area", false, true },
                         { 13, "Course Location", false, true },
                         { 13, "Employee ID", false, true },
                         { 13, "Employee", false, true },
                         { 13, "Grade", false, true },
                         { 13, "ILA ID", false, true },
                         { 13, "ILA Provider", false, true },
                         { 13, "ILA Title", false, true },
                         { 13, "Instructor", false, true },
                         { 13, "NERC Cert #", false, true },
                         { 13, "Organization", false, true },
                         { 13, "Positions", false, true },
                         { 13, "Score", false, true },
                         { 14, "Class End Date", false, true },
                         { 14, "Class Start Date", false, true },
                         { 14, "ILA", false, true },
                         { 14, "Instructor", false, true },
                         { 14, "Location", false, true },
                         { 14, "Prov. ID", false, true },
                         { 14, "Year", false, true },
                         { 15, "ILA Description", false, true },
                         { 15, "ILA Evaluation Methods", false, true },
                         { 15, "ILA Prerequisites", false, true },
                         { 15, "ILA Procedures", false, true },
                         { 15, "ILA Segment and Content", false, true },
                         { 15, "ILA Training Plan", false, true },
                         { 15, "ILA Training Resources", false, true },
                         { 15, "List of Objectives", false, true },
                         { 15, "Position", false, true },
                         { 15, "Regulatory Requirements", false, true },
                         { 15, "Safety Hazards", false, true },
                         { 15, "Topic", false, true },
                         { 15, "Use Sequenced List", false, true },
                         { 16, "Evaluator Name", false, true },
                         { 16, "Number of Pending Qualifications", false, true },
                         { 16, "Organization", false, true },
                         { 16, "Position Abbreviation", false, true },
                         { 17, "Date Range", false, true },
                         { 17, "Delivery Method", false, true },
                         { 17, "Evaluation Comments", false, true },
                         { 17, "Evaluation Rating Scale", false, true },
                         { 17, "Form Name", false, true },
                         { 17, "ILA", false, true },
                         { 17, "Provider", false, true },
                         { 18, "Employee", false, true },
                         { 18, "ILA Name/ID", false, true },
                         { 18, "Positions Qualification Date", false, true },
                         { 18, "Requalification Date", false, true },
                         { 18, "Requalification Status", false, true },
                         { 19, "Comments", false, true },
                         { 19, "Date", false, true },
                         { 19, "Evaluation Method", false, true },
                         { 19, "Evaluator", false, true },
                         { 19, "Met Criteria", false, true },
                         { 19, "Task Description", false, true },
                         { 19, "Task Number", false, true },
                         { 20, "EO #", false, true },
                         { 20, "EO Description", false, true },
                         { 20, "Item ID", false, true },
                         { 20, "Item Stem", false, true },
                         { 20, "Item Type", false, true },
                         { 20, "Status", false, true },
                         { 20, "Taxonomy Level", false, true },
                         { 21, "ILA Number", false, true },
                         { 21, "ILA Title", false, true },
                         { 21, "Provider", false, true },
                         { 21, "Status", false, true },
                         { 21, "Test Title", false, true },
                         { 21, "Test Type", false, true },
                         { 22, "ILA ID", false, true },
                         { 22, "Position", false, true },
                         { 22, "Qualification Date", false, true },
                         { 22, "Requalification Date", false, true },
                         { 22, "Requalification Status", false, true },
                         { 22, "Task Description", false, true },
                         { 22, "Task Number", false, true },
                         { 22, "Task Revision Date", false, true },
                         { 23, "Employee Name", false, true },
                         { 23, "Organizations", false, true },
                         { 23, "Positions", false, true },
                         { 23, "Show Assigned Task Qualifications", false, true },
                         { 24, "Class End Date & Time", false, true },
                         { 24, "Class Start Date & Time", false, true },
                         { 24, "Evaluation Comments", false, true },
                         { 24, "Form Name", false, true },
                         { 24, "ILA", false, true },
                         { 24, "Instructor", false, true },
                         { 24, "Location", false, true },
                         { 24, "Provider", false, true },
                         { 25, "Cert Area", false, true },
                         { 25, "Employee Name", false, true },
                         { 25, "Expiration Date", false, true },
                         { 25, "NERC Cert #", false, true },
                         { 25, "Organization", false, true },
                         { 25, "Position", false, true },
                         { 25, "Recert Date", false, true },
                         { 26, "Component Type", false, true },
                         { 26, "Issue Date", false, true },
                         { 26, "Issue ID", false, true },
                         { 26, "Issue Type", false, true },
                         { 26, "Severity", false, true },
                         { 26, "Status", false, true },
                         { 27, "Cert Area", false, true },
                         { 27, "Cert Date", false, true },
                         { 27, "Cert Number", false, true },
                         { 27, "Employee Name / Number", false, true },
                         { 27, "Met Annual Requirement", false, true },
                         { 27, "NERC Standards", false, true },
                         { 27, "Other", false, true },
                         { 27, "Prof", false, true },
                         { 27, "Regional - Met Annual Requirement", false, true },
                         { 27, "Regional 2", false, true },
                         { 27, "Regional", false, true },
                         { 27, "Simulation Hours", false, true },
                         { 27, "Total CEHs", false, true },
                         { 27, "Total Training Hours", false, true },
                         { 28, "Cover Sheet", false, true },
                         { 28, "Include Pseudo Tasks", false, true },
                         { 28, "Include Safety Hazards", false, true },
                         { 28, "Position", false, true },
                         { 28, "Show EO linked to the Tasks", false, true },
                         { 28, "Show Procedures linked to the Tasks", false, true },
                         { 28, "Show Task Details", false, true },
                         { 28, "Show Task Questions and Answers", false, true },
                         { 28, "Show Task Questions Only", false, true },
                         { 28, "Show Task Specific Suggestions", false, true },
                         { 28, "Sub-steps", false, true },
                         { 29, "Cover Sheet", false, true },
                         { 29, "Include Pseudo Tasks", false, true },
                         { 29, "Include Safety Hazards", false, true },
                         { 29, "Show EO linked to the Tasks", false, true },
                         { 29, "Show Procedures linked to the Tasks", false, true },
                         { 29, "Show Task Details", false, true },
                         { 29, "Show Task Questions and Answers", false, true },
                         { 29, "Show Task Questions Only", false, true },
                         { 29, "Show Task Specific Suggestions", false, true },
                         { 29, "Sub-steps", false, true },
                         { 30, "Cover Sheet", false, true },
                         { 30, "ILA", false, true },
                         { 30, "Include Pseudo Tasks", false, true },
                         { 30, "Include Safety Hazards", false, true },
                         { 30, "Show EO linked to the Tasks", false, true },
                         { 30, "Show Procedures linked to the Tasks", false, true },
                         { 30, "Show Task Details", false, true },
                         { 30, "Show Task Questions and Answers", false, true },
                         { 30, "Show Task Questions Only", false, true },
                         { 30, "Show Task Specific Suggestions", false, true },
                         { 30, "Sub-steps", false, true },
                         { 31, "EOs linked to Task", false, true },
                         { 31, "Positions", false, true },
                         { 31, "Procedures linked to Task", false, true },
                         { 31, "Safety Hazards", false, true },
                         { 31, "Show Task Questions and Answers", false, true },
                         { 31, "Show Task Questions Only", false, true },
                         { 31, "Show Task Specific Suggestions", false, true },
                         { 31, "Sub-steps", false, true },
                         { 31, "Task Details", false, true },
                         { 31, "Task Groups", false, true },
                         { 32, "EOs linked to Task", false, true },
                         { 32, "Positions", false, true },
                         { 32, "Procedures linked to Task", false, true },
                         { 32, "Safety Hazards", false, true },
                         { 32, "Show Task Questions and Answers", false, true },
                         { 32, "Show Task Questions Only", false, true },
                         { 32, "Show Task Specific Suggestions", false, true },
                         { 32, "Sub-steps", false, true },
                         { 32, "Task Details", false, true },
                         { 32, "Task Groups", false, true },
                         { 33, "EOs linked to Task", false, true },
                         { 33, "Procedures linked to Task", false, true },
                         { 33, "Safety Hazards", false, true },
                         { 33, "Show Task Questions and Answers", false, true },
                         { 33, "Show Task Questions Only", false, true },
                         { 33, "Show Task Specific Suggestions", false, true },
                         { 33, "Sub-steps", false, true },
                         { 33, "Task Details", false, true },
                         { 34, "Comments", false, true },
                         { 34, "Completed Date", false, true },
                         { 34, "Employee Name", false, true },
                         { 34, "Issuing Authority", false, true },
                         { 34, "Organizations", false, true },
                         { 34, "Positions Abbreviation", false, true },
                         { 34, "Positions", false, true },
                         { 34, "Procedure number", false, true },
                         { 34, "Procedure Review Response", false, true },
                         { 34, "Procedure Title", false, true },
                         { 34, "Release Date", false, true },
                         { 34, "Status", false, true },
                         { 35, "Conclusions", false, true },
                         { 35, "Description of Evaluation", false, true },
                         { 35, "Historical Background", false, true },
                         { 35, "Results and Recommendations", false, true },
                         { 35, "Summary of Evaluation", false, true },
                         { 35, "Supporting Documents", false, true },
                         { 38, "Completed", false, true },
                         { 38, "Grade", false, true },
                         { 38, "ILA#", false, true },
                         { 38, "Individual Learning Activity Title", false, true },
                         { 38, "Planned", false, true },
                         { 38, "Scheduled", false, true },
                         { 38, "Score", false, true },
                         { 39, "Email", false, true },
                         { 39, "Emp #", false, true },
                         { 39, "Employee Name - Details", false, true },
                         { 39, "Grade", false, true },
                         { 39, "Phone", false, true },
                         { 39, "Score", false, true },
                         { 40, "Action Items", false, true },
                         { 40, "History Date", false, true },
                         { 40, "Requal Reqd.", false, true },
                         { 40, "Review Date", false, true },
                         { 40, "Review Findings", false, true },
                         { 40, "Reviewed By", false, true },
                         { 40, "Signature/Conclusion", false, true },
                         { 40, "Task #", false, true },
                         { 40, "Task History", false, true },
                         { 40, "Task Statement", false, true },
                         { 43, "Date", false, true },
                         { 43, "Evaluation Method", false, true },
                         { 43, "Evaluator", false, true },
                         { 43, "ILA Id/Name", false, true },
                         { 43, "Task #", false, true },
                         { 43, "Task Description", false, true },
                         { 46, "Individual Learning Activity Title", false, true },
                         { 46, "NERC CEHs-Sim", false, true },
                         { 46, "NERC CEHs-Stand", false, true },
                         { 46, "NERC CEHs-Total", false, true },
                         { 46, "Other", false, true },
                         { 46, "PER-005-EOPs", false, true },
                         { 46, "Prof", false, true },
                         { 46, "Provider ID/Individual Learning A", false, true },
                         { 46, "Reg", false, true },
                         { 46, "Total", false, true },
                         { 47, "Completed", false, true },
                         { 47, "Grade", false, true },
                         { 47, "Individual Learning Activity T", false, true },
                         { 47, "Location/Instructor (Proctor if self-paced)", false, true },
                         { 47, "NERC CEHs* - Sim", false, true },
                         { 47, "NERC CEHs* - Stand", false, true },
                         { 47, "NERC CEHs* - Total", false, true },
                         { 47, "Other", false, true },
                         { 47, "PER-005-EOPS", false, true },
                         { 47, "PER-005-inc.sim.", false, true },
                         { 47, "Provider ID / ILA#", false, true },
                         { 47, "Reg", false, true },
                         { 47, "Reg2", false, true },
                         { 47, "Score", false, true },
                         { 47, "Total", false, true },
                         { 48, "Completed Date", false, true },
                         { 48, "Grade", false, true },
                         { 48, "ILA Title", false, true },
                         { 48, "Location", false, true },
                         { 48, "Scheduled Date", false, true },
                         { 48, "Score", false, true },
                         { 49, "Completed Date", false, true },
                         { 49, "Employee", false, true },
                         { 49, "Grade", false, true },
                         { 49, "Location", false, true },
                         { 49, "Scheduled Date", false, true },
                         { 49, "Score", false, true },
                         { 50, "Completed", false, true },
                         { 50, "Employee/ID", false, true },
                         { 50, "Grade", false, true },
                         { 50, "Individual Learning Activity Title", false, true },
                         { 50, "Instructor", false, true },
                         { 50, "Location", false, true },
                         { 50, "NERC CEHs-Sim", false, true },
                         { 50, "NERC CEHs-Stand", false, true },
                         { 50, "NERC CEHs-Total", false, true },
                         { 50, "NERC Cert.#", false, true },
                         { 50, "Other", false, true },
                         { 50, "PER-005-EOPs", false, true },
                         { 50, "Prof", false, true },
                         { 50, "Provider ID/Individual Learning A", false, true },
                         { 50, "Reg", false, true },
                         { 50, "Reg. Cert.#", false, true },
                         { 50, "Score", false, true },
                         { 50, "Score", false, true },
                         { 50, "Total", false, true },
                         { 51, "Date", false, true },
                         { 51, "Email", false, true },
                         { 51, "Emp #", false, true },
                         { 51, "Employee Name - Details", false, true },
                         { 51, "Phone", false, true },
                         { 51, "Signature", false, true },
                         { 53, "Include Survey Instructions", false, true },
                         { 54, "Additional Comments", false, true },
                         { 54, "Task Comments", false, true },
                         { 55, "Additional Comments", false, true },
                         { 55, "Override Status", false, true },
                         { 55, "Recommended Training", false, true },
                         { 55, "Task Comments", false, true },
                         { 55, "Training Frequency", false, true },
                         { 56, "Employee Name", false, true },
                         { 56, "Organization", false, true },
                         { 56, "Position Abbreviation", false, true },
                         { 56, "Position", false, true },
                         { 57, "Certificate Name", false, true },
                         { 57, "Certificate Number", false, true },
                         { 57, "Completed YTD", false, true },
                         { 57, "Employee Name", false, true },
                         { 57, "Organization", false, true },
                         { 57, "Planned for Current Year", false, true },
                         { 57, "Position Abbreviation", false, true },
                         { 57, "Scheduled to Complete YTD", false, true },
                         { 57, "Still to Complete YTD", false, true },
                         { 58, "Cert Area", false, true },
                         { 58, "Completed Date", false, true },
                         { 58, "Employee Name", false, true },
                         { 58, "Expiration Date", false, true },
                         { 58, "ILA #", false, true },
                         { 58, "ILA Title", false, true },
                         { 58, "NERC Cert #", false, true },
                         { 58, "Position", false, true },
                         { 58, "Recertification Date", false, true },
                         { 59, "Cert #", false, true },
                         { 59, "Cert Area", false, true },
                         { 59, "Completed Date", false, true },
                         { 59, "Employee Name", false, true },
                         { 59, "Expiration Date", false, true },
                         { 59, "ILA #", false, true },
                         { 59, "ILA Title", false, true },
                         { 59, "Position", false, true },
                         { 59, "Recertification Date", false, true },
                         { 60, "Cert #", false, true },
                         { 60, "Cert Area", false, true },
                         { 60, "Employee Name", false, true },
                         { 60, "Expiration Date", false, true },
                         { 60, "Position Abbreviation", false, true },
                         { 61, "Comments", false, true },
                         { 61, "Completed Date", false, true },
                         { 61, "Issuing Authority", false, true },
                         { 61, "Organization", false, true },
                         { 61, "Position", false, true },
                         { 61, "Procedure Number", false, true },
                         { 61, "Procedure Review Response", false, true },
                         { 61, "Procedure Review Title", false, true },
                         { 61, "Procedure Title", false, true },
                         { 61, "Release Date", false, true },
                         { 61, "Status", false, true },
                         { 62, "Completed Tasks", false, true },
                         { 62, "Employee", false, true },
                         { 62, "Position", false, true },
                         { 62, "Total Tasks", false, true },
                         { 63, "Completed Tasks", false, true },
                         { 63, "Employee", false, true },
                         { 63, "Position", false, true },
                         { 63, "Total Tasks", false, true },
                         { 64, "Effective Date", false, true },
                         { 64, "Employee Name", false, true },
                         { 64, "Employee Number", false, true },
                         { 64, "Employee Status", false, true },
                         { 64, "Organizations", false, true },
                         { 64, "Positions", false, true },
                         { 64, "Reason for Status Change", false, true },
                         { 65, "Linked Task #", false, true },
                         { 65, "Linked Task Description", false, true },
                         { 65, "Linked Tasks Positions", false, true },
                         { 65, "Positions", false, true },
                         { 66, "Class End DateTime", false, true },
                         { 66, "Class Start DateTime", false, true },
                         { 66, "Completed Date", false, true },
                         { 66, "Completion Statistics Graph", false, true },
                         { 66, "Correct/Incorrect Answer", false, true },
                         { 66, "Cut Score", false, true },
                         { 66, "Disclaimer", false, true },
                         { 66, "Employee Name", false, true },
                         { 66, "ILA Number", false, true },
                         { 66, "ILA Title", false, true },
                         { 66, "Interrupted", false, true },
                         { 66, "Organization", false, true },
                         { 66, "Position", false, true },
                         { 66, "Release Date", false, true },
                         { 66, "Restarted", false, true },
                         { 66, "Submitted Response", false, true },
                         { 66, "Test Grade", false, true },
                         { 66, "Test Item", false, true },
                         { 66, "Test Score", false, true },
                         { 66, "Test Time", false, true },
                         { 66, "Test Title", false, true },
                         { 66, "Test Type", false, true },
                         { 67, "Employee Name", false, true },
                         { 67, "Employee Number", false, true },
                         { 67, "Positions", false, true },
                         { 67, "Show Certificate Details", false, true },
                         { 67, "Show Employee Certification Details", false, true },
                         { 68, "Comments", false, true },
                         { 68, "Date", false, true },
                         { 68, "Evaluation Method", false, true },
                         { 68, "Evaluator", false, true },
                         { 68, "Met Criteria", false, true },
                         { 68, "Position", false, true },
                         { 68, "Step #", false, true },
                         { 68, "Step/Sub-Step", false, true },
                         { 68, "Step Comments", false, true },
                         { 68, "Step Evaluator", false, true },
                         { 68, "Step Met", false, true },
                         { 68, "Step Qualification Date", false, true },
                         { 68, "Task Description", false, true },
                         { 68, "Task Number", false, true },
                         { 69, "Completed Date", false, true },
                         { 69, "Delivery Method", false, true },
                         { 69, "Employee Name", false, true },
                         { 69, "Grade Notes", false, true },
                         { 69, "Grade", false, true },
                         { 69, "IDP Specific Information", false, true },
                         { 69, "ILA Number", false, true },
                         { 69, "ILA Title", false, true },
                         { 69, "Organization", false, true },
                         { 69, "Planned Date", false, true },
                         { 69, "Position", false, true },
                         { 69, "Scheduled Date", false, true },
                         { 69, "Score", false, true },
                         { 69, "Self-Paced", false, true },
                         { 70, "Certificate Information", false, true },
                         { 70, "Completed Date", false, true },
                         { 70, "Employee Name", false, true },
                         { 70, "Grade Notes", false, true },
                         { 70, "Grade", false, true },
                         { 70, "IDP Specific Information", false, true },
                         { 70, "ILA Number", false, true },
                         { 70, "ILA Title", false, true },
                         { 70, "Organization", false, true },
                         { 70, "Percent Completed per IDP", false, true },
                         { 70, "Position", false, true },
                         { 70, "Scheduled Date", false, true },
                         { 70, "Score", false, true },
                         { 70, "Total Training Hours", false, true },
                         { 71, "History Date", false, true },
                         { 71, "Notes", false, true },
                         { 71, "R-R", false, true },
                         { 71, "Requalification Due Date", false, true },
                         { 71, "Requalification Required", false, true },
                         { 71, "Revision Summary", false, true },
                         { 71, "Status (Active/Inactive)", false, true },
                         { 71, "Task #", false, true },
                         { 71, "Task Description", false, true },
                         { 71, "Username", false, true },
                         { 72, "Completion Date", false, true },
                         { 72, "Employee Name", false, true },
                         { 72, "Employee Number", false, true },
                         { 72, "Grade notes", false, true },
                         { 72, "Grade", false, true },
                         { 72, "ILA #", false, true },
                         { 72, "ILA Title", false, true },
                         { 72, "Organization", false, true },
                         { 72, "Position", false, true },
                         { 72, "Procedure/Regulatory requirement #", false, true },
                         { 72, "Procedure/Regulatory Requirement Title", false, true },
                         { 72, "Score", false, true },
                         { 73, "Employee Name", false, true },
                         { 73, "Employee Number", false, true },
                         { 73, "Position", false, true },
                         { 74, "Completed Tasks", false, true },
                         { 74, "Employee Name", false, true },
                         { 74, "Evaluation Method", false, true },
                         { 74, "Evaluator", false, true },
                         { 74, "Organization", false, true },
                         { 74, "Position", false, true },
                         { 74, "Total Tasks", false, true },
                         { 75, "Duty Area", false, true },
                         { 75, "Employee Name", false, true },
                         { 75, "Employee Status", false, true },
                         { 75, "Last Task Qualification Date", false, true },
                         { 75, "Sub-Duty Area", false, true },
                         { 75, "Task Description", false, true },
                         { 75, "Task Number", false, true },
                         { 76, "Comments", false, true },
                         { 76, "Date", false, true },
                         { 76, "Evaluation Method", false, true },
                         { 76, "Evaluator", false, true },
                         { 76, "Met Criteria", false, true },
                         { 76, "Task Description", false, true },
                         { 76, "Task Number", false, true },
                         { 77, "Class End DateTime", false, true },
                         { 77, "Class Start DateTime", false, true },
                         { 77, "Cut Score", false, true },
                         { 77, "Employee Name", false, true },
                         { 77, "Enabling Objective #", false, true },
                         { 77, "ILA Number", false, true },
                         { 77, "ILA Title", false, true },
                         { 77, "# Selected Answer", false, true },
                         { 77, "Taxonomy Level", false, true },
                         { 77, "Test Item #", false, true },
                         { 77, "Test Response Bar Chart", false, true },
                         { 77, "Test Title", false, true },
                         { 77, "Test Type", false, true },
                         { 78, "CBT Completion Graph", false, true },
                         { 78, "CBT Learning Instructions", false, true },
                         { 78, "Class Start and End Date", false, true },
                         { 78, "Class Start and End Time", false, true },
                         { 78, "Completed Date", false, true },
                         { 78, "Correct/Incorrect Answer", false, true },
                         { 78, "Employee Name", false, true },
                         { 78, "ILA Number", false, true },
                         { 78, "ILA Title", false, true },
                         { 78, "Last Access Date", false, true },
                         { 78, "Organization", false, true },
                         { 78, "Position", false, true },
                         { 78, "Release Date", false, true },
                         { 78, "Status", false, true },
                         { 78, "Submitted Response", false, true },
                         { 78, "Test Grade", false, true },
                         { 78, "Test Item", false, true },
                         { 78, "Test Score", false, true },
                         { 78, "Total Time", false, true },
                         { 79, "CBT Test Response Bar Chart", false, true },
                         { 79, "Class Start and End Date", false, true },
                         { 79, "Class Start and End Time", false, true },
                         { 79, "Employee Name", false, true },
                         { 79, "ILA Number", false, true },
                         { 79, "ILA Title", false, true },
                         { 79, "# Selected Answer", false, true },
                         { 80, "Position Description", false, true },
                         { 80, "R-R Definition", false, true },
                         { 80, "Review Date", false, true },
                         { 80, "Show EO linked to the Tasks", false, true },
                         { 80, "Show Procedures linked to the Tasks", false, true },
                         { 80, "Show Safety Hazards linked to the Tasks", false, true },
                         { 80, "Show Task Details", false, true },
                         { 80, "Show Task Questions and Answers", false, true },
                         { 80, "Show Task Questions Only", false, true },
                         { 80, "Show Task Specific Suggestions", false, true },
                         { 80, "Steps/Sub-Steps", false, true },
                         { 81, "Effective Date", false, true },
                         { 81, "Hyperlink Linked", false, true },
                         { 81, "PDF linked", false, true },
                         { 81, "Procedure Number", false, true },
                         { 81, "Procedure Title", false, true },
                         { 81, "Revision Number", false, true },
                         { 82, "Duty Area", false, true },
                         { 82, "Procedure Number", false, true },
                         { 82, "Procedure Title", false, true },
                         { 82, "Sub-Duty Area", false, true },
                         { 83, "Category", false, true },
                         { 83, "Sub-Category", false, true },
                         { 83, "Topic", false, true },
                         { 84, "Category", false, true },
                         { 84, "Sub-Category", false, true },
                         { 84, "Topic", false, true },
                         { 85, "Enabling Objectives", false, true },
                         { 85, "Procedures", false, true },
                         { 85, "Regulatory Requirements", false, true },
                         { 85, "Safety Hazards", false, true },
                         { 85, "Task Details", false, true },
                         { 85, "Task Steps & Sub-Steps", false, true },
                         { 85, "Tools", false, true },
                         { 86, "Category", false, true },
                         { 86, "Show EOs linked to Meta EO", false, true },
                         { 86, "Show Meta label", false, true },
                         { 86, "Show Skill Qualification label", false, true },
                         { 86, "Sub-Category", false, true },
                         { 86, "Topic", false, true },
                         { 87, "Certificate Number", false, true },
                         { 87, "Date", false, true },
                         { 87, "Employee Name", false, true },
                         { 87, "Enabling Objective #", false, true },
                         { 87, "ILA #", false, true },
                         { 87, "ILA Title", false, true },
                         { 87, "Test Instructions", false, true },
                         { 87, "Test Item ID", false, true },
                         { 87, "Test Time Limit", false, true },
                         { 87, "Test Type", false, true },
                         { 88, "Category", false, true },
                         { 88, "Show EOs linked to Meta", false, true },
                         { 88, "Show Meta label", false, true },
                         { 88, "Show Skill Qualification label", false, true },
                         { 88, "Sub-Category", false, true },
                         { 88, "Topic", false, true },
                         { 92, "Date", false, true },
                         { 92, "Evaluator Notes", false, true },
                         { 92, "Evaluator Signature", false, true },
                         { 92, "Position", false, true },
                         { 92, "Show Procedures", false, true },
                         { 92, "Show Regulatory Requirements", false, true },
                         { 92, "Show Safety Hazards", false, true },
                         { 92, "Show Skill Details", false, true },
                         { 92, "Show Skill Questions and Answers", false, true },
                         { 92, "Show Skill Questions Only", false, true },
                         { 92, "Show Skill Specific Suggestions", false, true },
                         { 92, "Show Tasks", false, true },
                         { 93, "Evaluator Final Signature & Date", false, true },
                         { 93, "Evaluator Notes", false, true },
                         { 93, "Final Result", false, true },
                         { 93, "Position", false, true },
                         { 93, "Show Procedures", false, true },
                         { 93, "Show Regulatory Requirements", false, true },
                         { 93, "Show Safety Hazards", false, true },
                         { 93, "Show Skill Details", false, true },
                         { 93, "Show Skill Questions and Answers", false, true },
                         { 93, "Show Skill Questions Only", false, true },
                         { 93, "Show Skill Specific Suggestions", false, true },
                         { 93, "Show Tasks", false, true },
                         { 93, "Trainee Final Signature & Date", false, true },
                         { 95, "Effective Date", false, true },
                         { 95, "Hyperlink Linked", false, true },
                         { 95, "PDF linked", false, true },
                         { 95, "Revision Number", false, true },
                         { 95, "Safety Hazard Number", false, true },
                         { 95, "Safety Hazard Title", false, true },
                         { 96, "Certificate No.", false, true },
                         { 96, "Grade", false, true },
                         { 96, "Instructor", false, true },
                         { 96, "Location", false, true },
                         { 96, "Provider Contact Info", false, true },
                         { 96, "Provider Contact No.", false, true },
                         { 96, "Provider ID", false, true },
                         { 96, "Score", false, true },
                         { 96, "Training Provider Name", false, true },
                         { 96, "Training Provider Signature", false, true },
                         { 97, "# of Employees enrolled", false, true },
                         { 97, "Certificate Number", false, true },
                         { 97, "Class Final Test Average", false, true },
                         { 97, "Class Pretest Average", false, true },
                         { 97, "Class Start and End Date", false, true },
                         { 97, "Class Start and End Time", false, true },
                         { 97, "Employee Name", false, true },
                         { 97, "Final Test Cut Score", false, true },
                         { 97, "Final Test Score", false, true },
                         { 97, "ILA Number", false, true },
                         { 97, "ILA Title", false, true },
                         { 97, "Organization", false, true },
                         { 97, "Position", false, true },
                         { 97, "Pretest & Final Test Completion Graph", false, true },
                         { 97, "Pretest Cut Score", false, true },
                         { 97, "Pretest Score", false, true },
                         { 98, "Certificate #", false, true },
                         { 98, "Employee Name", false, true },
                         { 98, "Expiration Date", false, true },
                         { 98, "Organization", false, true },
                         { 98, "Position", false, true },
                         { 98, "Renewal Date", false, true },
                         { 99, "Duty Area", false, true },
                         { 99, "Sub-Duty Area", false, true },
                         { 100, "Duty Area", false, true },
                         { 100, "Sub-Duty Area", false, true },
                         { 101, "Duty Area", false, true },
                         { 101, "Issuing Authority", false, true },
                         { 101, "Sub-Duty Area", false, true },
                         { 102, "Delivery Method", false, true },
                         { 102, "EMP Release Criteria", false, true },
                         { 102, "Meta ILA Description", false, true },
                         { 102, "Meta ILA Student Evaluation Linked", false, true },
                         { 102, "Meta ILA Summary Test Linked", false, true },
                         { 102, "Show EOs linked to Meta EO", false, true },
                         { 102, "Show Tasks Linked to Meta Task", false, true },
                         { 104, "ILA #", false, true },
                         { 104, "ILA Title", false, true },
                         { 104, "No. of Items", false, true },
                         { 104, "Status", false, true },
                         { 104, "Test ID", false, true },
                         { 104, "Test Title", false, true },
                         { 105, "Duty Area", false, true },
                         { 105, "Safety Hazard Number", false, true },
                         { 105, "Safety Hazard Title", false, true },
                         { 105, "Show Meta Task Label", false, true },
                         { 105, "Show Tasks Linked to Meta Task", false, true },
                         { 105, "Sub-Duty Area", false, true },
                         { 107, "Category", false, true },
                         { 107, "Safety Hazard No.", false, true },
                         { 107, "Safety Hazard Title", false, true },
                         { 107, "Show EOs Linked to Meta EO", false, true },
                         { 107, "Show Meta EO label", false, true },
                         { 107, "Show SQ Label", false, true }
                });

        }

        protected void Development_AddReport_TrainingProgramQualificationCard()
        {
            _migrationBuilder.InsertData(
               table: "ReportSkeletons",
               columns: new[] { "DefaultTitle", "Deleted", "Active" },
               values: new object[,]
                 {
                    {"Training Program Qualification Card", false, true }
                 }
            );

            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Order", "Deleted", "Active" },
                  values: new object[,]
                  {
                    {40, 111, 6, false, true }
                  }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                {111, "Select Position", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "Positions", false, true,null,null},
                {111, "Training Program", "String", "Single", DateTime.MinValue, DateTime.MinValue, "trainingprogramtype", false, true,null,null},
                {111, "Include Employee Name", "String", "Single", DateTime.MinValue, DateTime.MinValue, "employeename", false, true,null,null},
                {111,"Include Meta Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null},
                {111,"Include Pseudo Tasks", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null}
              }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                 {111, "Position",  false, true },
                 {111, "Training Program Version",  false, true },
                 {111, "Training Program Date Range",  false, true },
                 {111, "Overall Completion Sign-offs",  false, true },
                 {111, "Task Qualification Sign-offs",  false, true },
                 {111, "Task Qualification Methods",  false, true },
                 {111, "Meta Task Label",  false, true },
                 {111, "Include Tasks linked to Meta Task",  false, true },

             }
            );

            _migrationBuilder.UpdateData(
            table: "ReportSkeleton_Subcategory_Reports",
            keyColumns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId" },
            keyValues: new object[] { 40, 13 },
            column: "Order",
            value: "7");

            _migrationBuilder.UpdateData(
           table: "ReportSkeleton_Subcategory_Reports",
           keyColumns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId" },
           keyValues: new object[] { 40, 96 },
           column: "Order",
           value: "8");
        }

        protected void Development_AddDataToClassScheduleTQEmpSettingsFromTQILAEmpSettings()
        {
            _migrationBuilder.Sql(@"UPDATE cste
                SET 
                    cste.ReleaseOnClassStart = ilaSet.ReleaseOnClassStart,
                    cste.ReleaseOnClassEnd = ilaSet.ReleaseOnClassEnd,
                    cste.SpecificTime = ilaSet.SpecificTime,
                    cste.PriorToSpecificTime = ilaSet.PriorToSpecificTime
                FROM ClassSchedule_TQEMPSettings cste
                JOIN ClassSchedules cs ON cs.Id = cste.ClassScheduleId
                JOIN TQILAEmpSettings ilaSet ON cs.ILAId = ilaSet.ILAId;
            ");
        }

        protected void Development_UpdateTaskByPositionReportFilters()
        {
            _migrationBuilder.DeleteData(
                table: "ReportSkeletonFilters",
                keyColumns: new[] { "ReportSkeletonId", "Name" },
                keyValues: new object[,]
                {
                     {1, "Group by Training Task Group" }
                }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonFilters",
             columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
             values: new object[,]
             {
                  {1, "Select Task Group", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "trainingtasksgroup", false, true,null,null}
             }
           );
        }

        protected void Development_UpdateReportFilters_StudentEvaluationInstructorLed()
        {
            _migrationBuilder.InsertData(
             table: "ReportSkeletonFilters",
             columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
             values: new object[,]
             {
                   {24,"Show Summary of Comments Only", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null},
             }
           );
        }

        protected void Development_SeedDataToEnrolledPropertyOfMetaILAEmployees()
        {
            _migrationBuilder.Sql(@"UPDATE MetaILA_Employees SET Enrolled = 1");
        }

        protected void Development_AddEmailNotification_PublicClassScheduleRequest()
        {
            _migrationBuilder.InsertData(
              table: "ClientSettings_Notifications",
              columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
              values: new object[,]
                  {
               {"Public Class Schedule Request", false, "Once enabled,this email will be sent to QTDUser when an employees request for public class request.", false, true}
                  });

            _migrationBuilder.InsertData(
             table: "ClientSettings_Notifications",
             columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
             values: new object[,]
                 {
               {"Public Class Schedule Request Accepted", false, "Once enabled,this email will be sent when a request is accepted for employees.", false, true}
                 });

            _migrationBuilder.InsertData(
            table: "ClientSettings_Notifications",
            columns: new[] { "Name", "Enabled", "TimingText", "Deleted", "Active" },
            values: new object[,]
                {
               {"Public Class Schedule Request Rejected", false, "Once enabled,this email will be sent when a request is rejected for employees.", false, true}
                });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
               {24, @"@using QTD2.Infrastructure.ExtensionMethods
                        <style>
                        .class-schedule-request-table{
                            width: 100%;
                            border-collapse: collapse;
                        }
                        .class-schedule-request-table th {
                            border: 1px solid black;
                            padding: 8px 5px;
                            text-align: left;
                        }
                        .class-schedule-request-table td {
                            border: 1px solid grey;
                            text-align: left;
                            padding: 5px 5px;
                            vertical-align: top;
                            word-break: break-all;
                        }
                    </style>
                        <p>
                            Hello, You have requests for the public class schedule.
                        </p>
                        
                        <table class='class-schedule-request-table'>
                            <thead>
                                <tr>
                                    <th style=''width: 15%;''>User Name</th>
                                    <th style=''width: 15%;''>Start Date</th>
                                    <th style=''width: 15%;''>End Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>@(Model.FirstName + "" "" + Model.LastName)</td>
                                    <td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                    <td>@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                </tr>
                            </tbody>
                        </table>",
                   1, false, true }
                  });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
               {25, @"@using QTD2.Infrastructure.ExtensionMethods

                       <style>
                        .class-schedule-request-table{
                            width: 100%;
                            border-collapse: collapse;
                        }
                        .class-schedule-request-table th {
                            border: 1px solid black;
                            padding: 8px 5px;
                            text-align: left;
                        }
                        .class-schedule-request-table td {
                            border: 1px solid grey;
                            text-align: left;
                            padding: 5px 5px;
                            vertical-align: top;
                            word-break: break-all;
                        }
                    </style>
                     <p>
                          Hello, @Model.FirstName @Model.LastName your request  has been <b>accepted</b> for class scheduled on.
                     </p>
                       <table class='class-schedule-request-table'>
                            <thead>
                                <tr>
                                    <th style=''width: 15%;''>User Name</th>
                                    <th style=''width: 15%;''>Start Date</th>
                                    <th style=''width: 15%;''>End Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>@(Model.FirstName + "" "" + Model.LastName)</td>
                                    <td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                    <td>@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                </tr>
                            </tbody>
                        </table>
                    ",
                   1, false, true }
                  });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Steps",
              columns: new[] { "ClientSettingsNotificationId", "Template", "Order", "Deleted", "Active" },
               values: new object[,]
                  {
               {26, @"@using QTD2.Infrastructure.ExtensionMethods
                        <style>
                        .class-schedule-request-table{
                            width: 100%;
                            border-collapse: collapse;
                        }
                        .class-schedule-request-table th {
                            border: 1px solid black;
                            padding: 8px 5px;
                            text-align: left;
                        }
                        .class-schedule-request-table td {
                            border: 1px solid grey;
                            text-align: left;
                            padding: 5px 5px;
                            vertical-align: top;
                            word-break: break-all;
                        }
                    </style>
                        
                     <p>
                         Hello, @Model.FirstName @Model.LastName your request has been <b>denied</b> for class scheduled on.
                     </p>
                        <table class='class-schedule-request-table'>
                            <thead>
                                <tr>
                                    <th style=''width: 15%;''>User Name</th>
                                    <th style=''width: 15%;''>Start Date</th>
                                    <th style=''width: 15%;''>End Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>@(Model.FirstName + "" "" + Model.LastName)</td>
                                    <td>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                    <td>@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</td>
                                </tr>
                            </tbody>
                        </table>
                    ",
                   1, false, true }
                  });

            _migrationBuilder.InsertData(
              table: "ClientSettings_Notification_Step_AvailableCustomSettings",
              columns: new[] { "ClientSettingsNotificationStepId", "Setting", "Active" },
              values: new object[,] { { 26, "Email Frequency", true } }
              );
            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_Step_CustomSettings",
                columns: new[] { "ClientSettingsNotificationStepId", "Key", "Value", "Active" },
                values: new object[,] { { 26, "Email Frequency", "Weekly", true } }
              );
        }

        protected void Development_UpdateEmailNotificationPublicClassScheduleRequestTemplate()
        {
            _migrationBuilder.UpdateData(
              table: "ClientSettings_Notification_Steps",
              keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
              keyValues: new object[] { 24, 1 },
              column: "Template",
               value: (@"    
                     <p>
                         Hello, A registration request(s) has been submitted via the Public Course Registration Portal. Please log into the QTD Admin site to review the submitted request(s).
                     </p>
                        
                    ")
                  );

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notification_Steps",
             keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
             keyValues: new object[] { 25, 1 },
             column: "Template",
              value:
               (@"@using QTD2.Infrastructure.ExtensionMethods
                                                
                   <div>  
                        <p>Congratulations! Your Public Course Portal Registration has been approved and you may now complete the following class in the Employee Portal.</p>     
                    
                    <p> <b>@Model.CourseTitle</b> on
                    <b>@Model.StartDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</b> and <b>@Model.EndDate.ConvertWithTimeZoneName(Model.DefaultTimeZoneId)</b></p>
                    <p>To access the Employee Portal navigate to <a href=""@Model.Url"">@Model.Url</a> and use the Forgot Password button to create a password.</p>   
                    
                    <p>Thank you,<p/>
                    <p>Your Training Department</p>
                    </div> 
                        
                    ")
                 );

            _migrationBuilder.UpdateData(
              table: "ClientSettings_Notification_Steps",
              keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
              keyValues: new object[] { 26, 1 },
              column: "Template",
               value: (@"@using QTD2.Infrastructure.ExtensionMethods
                       <div>
                         <p>Hello, @Model.FirstName @Model.LastName </p>    
                         <p>Your Public Portal registration request has been denied.</p>
                         <p>Please contact your Training Administrator for additional information.</p>
                          <p>  Thank you..</p>
                       </div>
                        
                    ")
                  );

            _migrationBuilder.InsertData(
                table: "ClientSettings_Notification_Step_CustomSettings",
                columns: new[] { "ClientSettingsNotificationStepId", "Key", "Value", "Active" },
                values: new object[,] { { 26, "Send To Others", "", true } }
              );
        }

        protected void Development_UpdateReportFiltersForILACompletionHistory()
        {
            _migrationBuilder.Sql(@"
                UPDATE rf
                SET rf.Value = 'Completed & Not Completed within Date Range'
                FROM ReportFilters rf
                INNER JOIN Reports r ON rf.ReportId = r.Id
                WHERE r.ReportSkeletonId = 13
                AND rf.Name = 'Completion Type'
                AND rf.Value = 'ALL';
                       ");
        }

        protected void Development_AddReport_ProceduresByEnablingObjectives()
        {
            _migrationBuilder.InsertData(
               table: "ReportSkeletons",
               columns: new[] { "DefaultTitle", "Deleted", "Active" },
               values: new object[,]
                 {
                    {"Procedures By Enabling Objectives", false, true }
                 }
            );

            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Order", "Deleted", "Active" },
                  values: new object[,]
                  {
                    {48, 112, 3, false, true }
                  }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                {112, "Select Enabling Objective", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "selectenablingobjective", false, true,null,null},
                {112,"Only show EOs with Procedure Linked", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null}
              }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                 {112, "Category",  false, true },
                 {112, "Sub-Category",  false, true },
                 {112, "Topic",  false, true },
                 {112, "Show EOs linked to Meta",  false, true },
                 {112, "Show Meta label",  false, true },
                 {112, "Show Skill Qualification label",  false, true },
             }
            );
        }

        protected void Development_AddDataToTaskListReviewPositionLinksTable()
        {
            _migrationBuilder.Sql(@"INSERT INTO TaskListReview_PositionLinks(TaskListReviewId, PositionId, Deleted, Active)
                        SELECT Id AS TaskListReviewId, PositionId, Deleted, Active
                        FROM TaskListReview
                        WHERE PositionId IS NOT NULL
                          AND NOT EXISTS (
                              SELECT 1
                              FROM TaskListReview_PositionLinks link
                              WHERE link.TaskListReviewId = TaskListReview.Id
                                AND link.PositionId = TaskListReview.PositionId
                        );"
            );
        }

        protected void Development_UpdateInternalIdentifiersForCertifications()
        {
            _migrationBuilder.Sql(@"  UPDATE Certifications SET InternalIdentifier = 'Other' WHERE Name = 'Other';
                                UPDATE Certifications SET InternalIdentifier = 'Reliability Coordinator'  WHERE Name = 'Reliability Coordinator';
                                UPDATE Certifications SET InternalIdentifier = 'Balancing and Interchange/Transmission Operator' WHERE Name = 'Balancing and Interchange/Transmission Operator';
                                UPDATE Certifications SET InternalIdentifier = 'Balancing and Interchange Operator' WHERE Name = 'Balancing and Interchange Operator';
                                UPDATE Certifications SET InternalIdentifier = 'Transmission Operator' WHERE Name = 'Transmission Operator';
                                UPDATE Certifications SET InternalIdentifier = 'Emergency Response' WHERE Name = 'Emergency Response';
                                UPDATE Certifications SET InternalIdentifier = 'Reg' WHERE Name = 'Reg';
                                UPDATE Certifications SET InternalIdentifier = 'Reg2' WHERE Name = 'Reg2';
                        ");
        }

        protected void Development_AddReportFilter_EmployeeDelinquencyReport()
        {
            _migrationBuilder.InsertData(
                table: "ReportSkeletonFilters",
                columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
                values: new object[,]
                {
                  { 60,"Sort Employees by Organization", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null}
                }
              );
        }

        protected void Development_UpdateReportSkeletonColumns_AnnualTaskListReviewReport()
        {
            _migrationBuilder.UpdateData(
            table: "ReportSkeletonColumns",
            keyColumns: new[] { "ReportSkeletonId", "ColumnName" },
            keyValues: new object[] { 40, "History Date" },
            column: "ColumnName",
            value: "Recent History Date");
        }

        protected void Development_Seed_TaskReviewStatus_NotStarted()
        {
            _migrationBuilder.InsertData(
                table: "TaskReview_Status",
                columns: new[] { "Status", "Active" },
                values: new object[,]
                {
                  { "Not Started",true }
                });
        }

        protected void Development_UpdateReportSkeletonFilter_TaskAndEnabingObjectiveByILA()
        {
            _migrationBuilder.UpdateData(
            table: "ReportSkeletonFilters",
            keyColumns: new[] { "ReportSkeletonId", "Name" },
            keyValues: new object[] { 42, "Include Task and EO Filter" },
            column: "DefaultValue",
            value: "1,2,3,4,5,6");
        }

        protected void Development_MigrateReviewedByFromTrainerName()
        {
            _migrationBuilder.Sql(@"  UPDATE tlr
                SET ReviewedBy = p.FirstName + ' ' + p.LastName
                FROM TaskListReview tlr
                JOIN QtdUsers q
                    ON tlr.TrainerId = q.Id
                JOIN Persons p
                    ON q.PersonId = p.Id
                        ");
        }

        protected void Development_Update_ClientSettings_Notification_StepsTemplate()
        {
            _migrationBuilder.UpdateData(
                table: "ClientSettings_Notification_Steps",
                keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
                keyValues: new object[] { 2, 1 },
                column: "Template",
                value: @"@using QTD2.Infrastructure.ExtensionMethods;
                <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p>
                <p>This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. 
                Your expiration date is @Model.CertificateExpirationDate.ToString(""MM/dd/yyyy""). 
                To date, we have not received a copy of your updated @Model.CertificateName certificate.</p>
                <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. 
                If you have any questions, please reach out to your Training Administrator.</p>
                <p>Thank you!</p>"
            );

            _migrationBuilder.UpdateData(
                table: "ClientSettings_Notification_Steps",
                keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
                keyValues: new object[] { 2, 2 },
                column: "Template",
                value: @"@using QTD2.Infrastructure.ExtensionMethods;
                <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p>
                <p>Your attention is required. This is a reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. 
                Your expiration date is @Model.CertificateExpirationDate.ToString(""MM/dd/yyyy""). 
                To date, we have not received a copy of your updated @Model.CertificateName certificate.</p>
                <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. 
                If you have any questions, please reach out to your Training Administrator.</p>
                <p>Thank you!</p>"
            );

            _migrationBuilder.UpdateData(
                table: "ClientSettings_Notification_Steps",
                keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
                keyValues: new object[] { 2, 3 },
                column: "Template",
                value: @"@using QTD2.Infrastructure.ExtensionMethods;
                <p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p>
                <p>Your immediate attention is required. This is an urgent reminder from the Training Department that your @Model.CertificateName certificate @Model.CertificateNumber will expire in @Model.DaysUntilCertificationExpiration days. 
                Your expiration date is @Model.CertificateExpirationDate.ToString(""MM/dd/yyyy""). 
                To date, we have not received a copy of your updated @Model.CertificateName certificate.</p>
                <p>After multiple notifications regarding the renewal of your @Model.CertificateName credential, your management has also received a copy of this notification. 
                You and your management will continue to receive daily reminders until the System Operations Training Team receives an updated copy of your @Model.CertificateName certificate.</p>
                <p>If you received this message in error or are no longer maintaining this certificate, we ask that you let us know so we can update our records. 
                If you have any questions, please reach out to your Training Administrator.</p>
                <p>Thank you!</p>"
            );

        }

        protected void Development_AddSkillQualificationStatusTable()
        {
            _migrationBuilder.InsertData(
                table: "SkillQualificationStatus",
                columns: new[] { "Name", "Description", "Active" },
                values: new object[,]
                {
                        {"Trainee Initial Qualification","Employee is a Trainee for the position, and the qualification is his/her Initial Qualification record to indicate successful performance on the Skill", true },
                        {"On Time","Employee qualified on the Skill within the requalification due date window", true },
                        {"Pending","Employee has not qualified on the Skill, but the current date is still within the requalification due date window", true },
                        {"Delayed","Employee qualified on the Skill outside of the requalification due date window", true },
                        {"Overdue","The 6-month window has passed, Employee has not qualified", true },
                        {"Requalification Not Required","Employee was flagged a Trainee for the position at the time of the Skill change. Employee qualified on revised skill as part of initial training", true },
                        {"No Position Qual Date","The Employee is not flagged as a Trainee and there is no Position Qual Date in the Employee window to use to confirm the skill qual against", true },
                        {"Completed","Skill Requalification is completed on Emp side", true }
                });
        }
        protected void Development_UpdateClassRoasterReportSkeletonColumn()
        {
            _migrationBuilder.UpdateData(
                table: "ReportSkeletonColumns",
                        keyColumns: new[] { "ReportSkeletonId", "ColumnName" },
                        keyValues: new object[] { 39, "Employee Name - Details" },
                        column: "ColumnName",
                        value: "Employee Details"
            );
        }

        protected void Development_AddReport_ILAsBySafetyHazard()
        {
            _migrationBuilder.InsertData(
               table: "ReportSkeletons",
               columns: new[] { "DefaultTitle", "Deleted", "Active" },
               values: new object[,]
                 {
                    {"ILAs by Safety Hazard", false, true }
                 }
            );

            _migrationBuilder.InsertData(
                  table: "ReportSkeleton_Subcategory_Reports",
                  columns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId", "Order", "Deleted", "Active" },
                  values: new object[,]
                  {
                    {50, 113, 4, false, true }
                  }
            );

            _migrationBuilder.InsertData(
              table: "ReportSkeletonFilters",
              columns: new[] { "ReportSkeletonId", "Name", "PropertyType", "ValueType", "MinOption", "MaxOption", "FilterOption", "Deleted", "Active", "DefaultValue", "MaxAllowedSelections" },
              values: new object[,]
              {
                {113, "Safety Hazards", "Int", "Array", DateTime.MinValue, DateTime.MinValue, "safetyhazard", false, true,null,null},
                {113,"Include Safety Hazard Details", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null},
                {113,"Include Meta ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null},
                {113,"Include Inactive ILAs", "Boolean", "Single", DateTime.MinValue, DateTime.MinValue, null, false, true,"false",null}
              }
            );

            _migrationBuilder.InsertData(
             table: "ReportSkeletonColumns",
             columns: new[] { "ReportSkeletonId", "ColumnName", "Deleted", "Active" },
             values: new object[,]
             {
                 {113, "Category",  false, true },
                 {113, "Safety Hazard No.",  false, true },
                 {113, "Safety Hazard Title",  false, true },
                 {113, "ILA No.",  false, true },
                 {113, "ILA Title",  false, true },
                 {113, "Show Meta ILA Label",  false, true },
                 {113, "Shows ILAs linked to Meta ILA",  false, true },
             }
            );

            _migrationBuilder.UpdateData(
           table: "ReportSkeleton_Subcategory_Reports",
           keyColumns: new[] { "ReportSkeleton_SubcategoryId", "ReportSkeletonId" },
           keyValues: new object[] { 50, 94 },
           column: "Order",
           value: "5");
        }

        protected void Development_SeedSimulatorScenarioEventsAsync()
        {
            _migrationBuilder.Sql(@" INSERT INTO [dbo].[SimulatorScenario_Events]
                                       ([SimulatorScenarioId]
                                       ,[Order]
                                       ,[Title]
                                       ,[Description]
                                       ,[Deleted]
                                       ,[Active]
                                       ,[CreatedBy]
                                       ,[CreatedDate]
                                       ,[ModifiedBy]
                                       ,[ModifiedDate])
                            SELECT     [SimulatorScenarioId]
                                      ,[Order]
                                      ,[Title]
                                      ,[Description]
                                      ,[Deleted]
                                      ,[Active]
                                      ,[CreatedBy]
                                      ,[CreatedDate]
                                      ,[ModifiedBy]
                                      ,[ModifiedDate]
                            FROM [dbo].[SimulatorScenario_EventAndScripts];
                        ");

            _migrationBuilder.Sql(@" 
                        INSERT INTO [dbo].[SimulatorScenario_Scripts]
                                  ([Title]
                                   ,[Description]
                                   ,[InitiatorId]
                                   ,[Time]
                                   ,[EventId]  
                                   ,[Deleted]
                                   ,[Active]
                                   ,[CreatedBy]
                                   ,[CreatedDate]
                                   ,[ModifiedBy]
                                   ,[ModifiedDate])
                        SELECT     
                                  es.[Title]
                                 ,es.[Description]
                                 ,es.[InitiatorId]
                                 ,es.[Time]
                                 ,ev.[Id] AS [EventId]   
                                 ,es.[Deleted]
                                 ,es.[Active]
                                 ,es.[CreatedBy]
                                 ,es.[CreatedDate]
                                 ,es.[ModifiedBy]
                                 ,es.[ModifiedDate]
                        FROM [dbo].[SimulatorScenario_EventAndScripts] es
                        INNER JOIN [dbo].[SimulatorScenario_Events] ev
                            ON ev.[Id] = es.[Id]
                        WHERE NOT EXISTS (
                            SELECT 1
                            FROM [dbo].[SimulatorScenario_Scripts] s
                            WHERE s.[EventId] = ev.[Id]
                        );
                  ");

            _migrationBuilder.Sql(@" 
              INSERT INTO [dbo].[SimulatorScenario_Script_Criterias]
                       ([ScriptId]
                       ,[CriteriaId]
                       ,[Deleted]
                       ,[Active]
                       ,[CreatedBy]
                       ,[CreatedDate]
                       ,[ModifiedBy]
                       ,[ModifiedDate])
             SELECT     sc.[Id] AS [ScriptId]
                       ,esc.[CriteriaId]
                       ,esc.[Deleted]
                       ,esc.[Active]
                       ,esc.[CreatedBy]
                       ,esc.[CreatedDate]
                       ,esc.[ModifiedBy]
                       ,esc.[ModifiedDate]
             FROM [dbo].[SimulatorScenario_EventAndScript_Criterias] esc
             INNER JOIN [dbo].[SimulatorScenario_EventAndScripts] es
                 ON es.[Id] = esc.[EventAndScriptId]
             INNER JOIN [dbo].[SimulatorScenario_Scripts] sc
                 ON sc.[EventId] = es.[Id]
             WHERE NOT EXISTS (
                 SELECT 1
                 FROM [dbo].[SimulatorScenario_Script_Criterias] ssc
                 WHERE ssc.[ScriptId] = sc.[Id]
                   AND ssc.[CriteriaId] = esc.[CriteriaId]
             );
          ");
        }

        protected void Development_UpdateSimulatorScenarioILAsAndPrerequisitesForDeletedILAs()
        {
            _migrationBuilder.Sql(@" UPDATE ssIla
                            SET ssIla.Deleted = 1
                            FROM [dbo].[SimulatorScenario_ILAs] ssIla
                            INNER JOIN [dbo].[ILAs] ila
                                ON ssIla.ILAID = ila.Id
                            WHERE ila.Deleted = 1 
                            ");

            _migrationBuilder.Sql(@" UPDATE ssPre
                        SET ssPre.Deleted = 1
                        FROM [dbo].[SimulatorScenario_Prerequisites] ssPre
                        INNER JOIN [dbo].[ILAs] ila
                            ON ssPre.PrerequisiteId = ila.Id
                        WHERE ila.Deleted = 1 
                        ");
        }

        protected void Development_CopySafetyHazardILALinksToILASafetyHazardLinks()
        {
            _migrationBuilder.Sql(@"
                    INSERT INTO [dbo].[ILA_SafetyHazard_Links]
                           ([ILAId]
                           ,[SafetyHazardId]
                           ,[Deleted]
                           ,[Active]
                           ,[CreatedBy]
                           ,[CreatedDate]
                           ,[ModifiedBy]
                           ,[ModifiedDate])
                    SELECT DISTINCT
                           sh.[ILAId]
                          ,sh.[SafetyHazardId]
                          ,sh.[Deleted]
                          ,sh.[Active]
                          ,sh.[CreatedBy]
                          ,sh.[CreatedDate]
                          ,sh.[ModifiedBy]
                          ,sh.[ModifiedDate]
                    FROM [dbo].[SafetyHazard_ILA_Links] sh
                    WHERE NOT EXISTS (
                        SELECT 1
                        FROM [dbo].[ILA_SafetyHazard_Links] ila
                        WHERE ila.[ILAId] = sh.[ILAId]
                          AND ila.[SafetyHazardId] = sh.[SafetyHazardId]
                    );
                ");
        }

        protected void Development_MigrateSimulatorScenarioScriptInitiatorData()
        {
            _migrationBuilder.Sql(@"
                                       UPDATE s
                    SET s.SimulatorScenario_PositionId = sp.Id
                    FROM SimulatorScenario_Scripts s
                    INNER JOIN SimulatorScenario_Events e
                        ON e.Id = s.EventId
                    INNER JOIN SimulatorScenario_Positions sp
                        ON sp.SimulatorScenarioId = e.SimulatorScenarioId
                       AND sp.PositionId = s.InitiatorId
                    WHERE s.InitiatorId IS NOT NULL;
                ");
        }

        protected void Development_Update_ClientSettingsNotification_StepsTemplateForTaskQualification()
        {
            _migrationBuilder.UpdateData(
                table: "ClientSettings_Notification_Steps",
                keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
                keyValues: new object[] { 10, 1 },
                column: "Template",
                value: @"<p>Hello @Model.EmployeeFirstName @Model.EmployeeLastName,</p> <p> You have been assigned to complete a Task/Skill Qualification as part of your position specific training program. An evaluator has been assigned to sign - off on this task/skill qualification.This will be completed using the Employee Portal(EMP). Please review the table below for further details. To help you prepare for the task/skill qualification, the task(s)/skill(s) below are available in a read - only format in EMP.</p><figure class=""table""><table><tbody><tr><td>Task/Skill #</td><td>@Model.TaskNumber</td></tr><tr><td>Task/Skill Statement</td><td>@Model.TaskStatement</td></tr><tr><td>Evaluator Name</td><td>@Model.EvaluatorName</td></tr></tbody></table></figure><p>If for any reason you cannot complete the assigned Task/Skill Qualification, notify your TaskSkill Qualification Evaluator and/or Training Administrator as soon as possible. Thank you!</p>'"
            );

            _migrationBuilder.UpdateData(
                table: "ClientSettings_Notification_Steps",
                keyColumns: new[] { "ClientSettingsNotificationId", "Order" },
                keyValues: new object[] { 11, 1 },
                column: "Template",
                value: @"Hello @Model.EmployeeFirstName @Model.EmployeeLastName,<p>You have been assigned as an Evaluator to sign off on a Task/Skill Qualification. This will be completed using the Employee Portal (EMP). Please review the table below for further details.</p><figure class=""table""><table><tr><td>Task/Skill #</td><td>@Model.TaskNumber</td></tr><tr><td>Task/Skill Statement</td><td>@Model.TaskStatement</td></tr><tr><td>Trainee Name</td><td>@Model.TraineeName</td></tr></table></figure><p>If for any reason you cannot complete the assigned Task/Skill Qualification(s), notify your Training Administrator as soon as possible.</p><p>Thank you!</p>"
            );

            _migrationBuilder.UpdateData(
           table: "ClientSettings_Notifications",
            keyColumns: new[] { "Id", "Name" },
            keyValues: new object[] { 10, "EMP Task Qualification - Trainee" },
            column: "Name",
            value: "EMP Task And Skill Qualification - Trainee");

            _migrationBuilder.UpdateData(
             table: "ClientSettings_Notifications",
              keyColumns: new[] { "Id", "Name" },
              keyValues: new object[] { 11, "EMP Task Qualification - Evaluator" },
              column: "Name",
              value: "EMP Task And Skill Qualification - Evaluator");
        }

    }
}
