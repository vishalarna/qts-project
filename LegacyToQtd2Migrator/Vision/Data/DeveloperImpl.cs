using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class DeveloperImpl
{
    public decimal Id { get; set; }

    public decimal FkDeveloper { get; set; }

    public string Email { get; set; }

    public string LoginName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public string Title { get; set; }

    public string Department { get; set; }

    public string Phone { get; set; }

    public string Fax { get; set; }

    public decimal? Archived { get; set; }

    public decimal? Enabled { get; set; }

    public decimal? CanRunSecurity { get; set; }

    public decimal? CanEditLists { get; set; }

    public decimal? IsAdmin { get; set; }

    public decimal? CanAlterSharedTables { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal? CanRunVision { get; set; }

    public decimal? CanRunContent { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public decimal IsLicensed { get; set; }

    public decimal CanRunImportExport { get; set; }

    public decimal CanRunTableUtility { get; set; }

    public decimal CanChangeTaskQuals { get; set; }

    public string IdpId { get; set; }

    public decimal CanChangeAlternates { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkDeveloperNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }
}
