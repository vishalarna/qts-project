using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ContentImpl
{
    public decimal Id { get; set; }

    public decimal FkContent { get; set; }

    public decimal FkOwner { get; set; }

    public decimal OwnerType { get; set; }

    public decimal FieldType { get; set; }

    public decimal MimeType { get; set; }

    public decimal Crc { get; set; }

    public decimal ByteSize { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public string Title { get; set; }

    public decimal? IsPublished { get; set; }

    public decimal? ContentWebDisplay { get; set; }

    public decimal? IsDefault { get; set; }

    public decimal? DefaultOrder { get; set; }

    public decimal? FkQuestion { get; set; }

    public string Description { get; set; }

    public decimal? FkExternalContentId { get; set; }

    public decimal? ExternalContentApp { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public decimal IsApproved { get; set; }

    public virtual Content FkContentNavigation { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
