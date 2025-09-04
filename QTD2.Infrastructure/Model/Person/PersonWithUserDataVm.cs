using System;

namespace QTD2.Infrastructure.Model.Person
{
    public class PersonWithUserDataVm
    {
        public int? PersonId { get; set; }
        public int? EmployeeId { get; set; }
        public int? InstructorId { get; set; }
        public int? QTDUserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public bool IsEmployee { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? EmployeeAddress { get; set; }
        public string? EmployeeCity { get; set; }
        public string? EmployeeState { get; set; }
        public string? EmployeeZipCode { get; set; }
        public string? EmployeePhoneNumber { get; set; }
        public string? EmployeeWorkLocation { get; set; }
        public string? EmployeeNotes { get; set; }
        public bool IsTQEvaluator { get; set; }
        public bool IsInstructor { get; set; }
        public string? InstructorCategoryTitle { get; set; }
        public int? InstructorCategoryId { get; set; }
        public string? InstructorNumber { get; set; }
        public string? InstructorDescription { get; set; }
        public bool InstructorIsworkbookadmin { get; set; }
        public DateTime InstructorEffectiveDate { get; set; }
        public bool IsQTDUser { get; set; }
        public bool? IsQTDUserActive { get; set; }
        public bool? IsInstructorActive { get; set; }
        public bool? IsEmployeeActive { get; set; }
        public bool Active { get; set; }
    }

}
