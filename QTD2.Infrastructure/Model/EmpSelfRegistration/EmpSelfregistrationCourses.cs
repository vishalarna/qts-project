using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EmpSelfRegistration
{
    public class EmpSelfregistrationCourses
    {
        public string Provider { get; set; }
        public string ILATitle { get; set; }
        public string ILANum { get; set; }
        public int TotalCourses { get; set; }



        public List<EmpSelfregistrationEmployees> EmpSelfregistrationEmployees { get; set; } = new List<EmpSelfregistrationEmployees>();
        public EmpSelfregistrationCourses() { }
        public EmpSelfregistrationCourses(string provider,string iLATitle,string iLANum,int totalCourses)
        {
            Provider = provider;
            ILATitle = iLATitle;
            ILANum = iLANum;
            TotalCourses = totalCourses;
        }

    }

    public class EmpSelfregistrationEmployees
    {

        public int ClassId { get; set; }
        public int ILAId { get; set; }
        public string ILATitle { get; set; }
        public string ILANum { get; set; }
        public DateTime ClassStartDateTime { get; set; }
        public DateTime ClassEndDateTime { get; set; }

        public string ClassDetail { get; set; }

        public string Location { get; set; }
        public string Instructor { get; set; }
        public int SeatsAvailable { get; set; }
        public bool? IsEnrolled { get; set; }
        public bool? IsDenied { get; set; }
        public bool? IsDropped { get; set; }

        public bool? IsWaitListEnabled { get; set; }
        public bool RequiredAdminApproval { get; set; }
        public bool? IsInWaitList { get; set; }

        public string? Acknolwedgement { get; set; }

        public bool CloseRegistrationOnStartDate { get; set; }

        public bool? IsAwaitingForApproval { get; set; }

        public EmpSelfregistrationEmployees() { }
         
        public EmpSelfregistrationEmployees(int classId,int iLAId,string iLATitle,string iLANum, DateTime classStartDateTime,DateTime classEndDateTime,string classDetail,string location, string instructor, int seatAvailable, bool? isEnrolled, bool? isDenied,bool? isDropped,bool isWaitListEnabled,bool requiredAdminApproval, bool? isInWaitList, string? acknolwedgement)
        {
            ClassId = classId;
            ILAId = iLAId;
            ILATitle = iLATitle;
            ILANum = iLANum;
            ClassStartDateTime = classStartDateTime;
            ClassEndDateTime = classEndDateTime;
            Location = location;
            Instructor = instructor;
            SeatsAvailable = seatAvailable;
            IsEnrolled = isEnrolled;
            IsDenied = isDenied;
            IsDropped = isDropped;
            IsWaitListEnabled = isWaitListEnabled;
            RequiredAdminApproval = requiredAdminApproval;
            IsInWaitList = isWaitListEnabled;
            Acknolwedgement = acknolwedgement;

        }

    }

    public class EmpCourses
    {
        public string Provider { get; set; }
        public string ILATitle { get; set; }
        public string ILANum { get; set; }
        public int ClassId { get; set; }
        public int ILAId { get; set; }
        public DateTime ClassStartDateTime { get; set; }
        public DateTime ClassEndDateTime { get; set; }
        public string Location { get; set; }
        public string Instructor { get; set; }
        public int SeatsAvailable { get; set; }
        public bool? IsEnrolled { get; set; }
        public bool? IsDenied { get; set; }
        public bool? IsDropped { get; set; }

        public bool? IsWaitListEnabled { get; set; }
        public bool RequiredAdminApproval { get; set; }
        public bool? IsInWaitList { get; set; }
        public string? Acknolwedgement { get; set; }
        public bool? IsAwaitingForApproval { get; set; }
        public EmpCourses() { }
        public EmpCourses(string provider,string iLATitle, string iLANum,int classId,int iLAId, DateTime classStartDateTime, DateTime classEndDateTime,string location, string instructor)
        {
            Provider = provider;
            ILATitle = iLATitle;
            ILANum = ILANum;
            ClassId = classId;
            ILAId = iLAId;
            ClassStartDateTime = classStartDateTime;
            ClassEndDateTime = classEndDateTime;
            Location = location;
            Instructor = instructor;
        }

        public void SetSeatsAvailable(int seats)
        {
            SeatsAvailable = seats;
        }

        public void setIsEnrolled(bool? isEnrolled)
        {
            IsEnrolled = isEnrolled;
        }

        public void setIsDenied(bool? isDenied)
        {
            IsDenied = isDenied;
        }
        public void setIsDropped(bool? isDropped)
        {
            IsDropped = isDropped;
        }

        public void setIsWaitListEnabled(bool? isWaitListEnabled)
        {
            IsWaitListEnabled = isWaitListEnabled;
        }
        public void setRequiredAdminApproval(bool requiredAdminApproval)
        {
            RequiredAdminApproval = requiredAdminApproval;
        }
        public void setIsInWaitList(bool? isInWaitList)
        {
            IsInWaitList = isInWaitList;
        }
    }
}
