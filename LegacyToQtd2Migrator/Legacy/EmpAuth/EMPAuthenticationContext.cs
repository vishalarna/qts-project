using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.EmpAuth
{
    public partial class EMPAuthenticationContext : DbContext
    {
        public EMPAuthenticationContext()
        {
        }

        public EMPAuthenticationContext(DbContextOptions<EMPAuthenticationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblEaAuthlog> TblEaAuthlogs { get; set; }
        public virtual DbSet<TblEaCompany> TblEaCompanies { get; set; }
        public virtual DbSet<TblEaEventList> TblEaEventLists { get; set; }
        public virtual DbSet<TblEaPwdResetCode> TblEaPwdResetCodes { get; set; }
        public virtual DbSet<TblEaUser> TblEaUsers { get; set; }
        public virtual DbSet<TblQtdSignon> TblQtdSignons { get; set; }
        public virtual DbSet<TblQtsActivationCode> TblQtsActivationCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=WSAMZN-0N13MTBJ;Database=EMPAuthentication;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblEaAuthlog>(entity =>
            {
                entity.HasKey(e => e.AuthEventId);

                entity.ToTable("tblEA_Authlog");

                entity.Property(e => e.AuthEventId).HasColumnName("AuthEventID");

                entity.Property(e => e.AuthEventNotes)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.RhuserId).HasColumnName("RHUserID");

                entity.Property(e => e.UserIpaddress)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("UserIPAddress");

                entity.HasOne(d => d.EventCodeNavigation)
                    .WithMany(p => p.TblEaAuthlogs)
                    .HasForeignKey(d => d.EventCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Authlog_EventList");
            });

            modelBuilder.Entity<TblEaCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("tblEA_Companies");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CnStr)
                    .HasMaxLength(454)
                    .IsUnicode(false)
                    .HasComputedColumnSql("(((('Data Source='+[DBServer])+';Initial Catalog=')+[DBName])+';Integrated Security=true')", false);

                entity.Property(e => e.Company)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Dbname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DBName");

                entity.Property(e => e.Dbserver)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DBServer");

                entity.Property(e => e.QtdscormServer)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("QTDScormServer")
                    .HasDefaultValueSql("('localHost')");

                entity.Property(e => e.ScormServer)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEaEventList>(entity =>
            {
                entity.HasKey(e => e.EventCode)
                    .HasName("PK_EventDescription_EventCode");

                entity.ToTable("tblEA_EventList");

                entity.Property(e => e.EventCode).ValueGeneratedNever();

                entity.Property(e => e.EventDetails)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EventType)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEaPwdResetCode>(entity =>
            {
                entity.ToTable("tblEA_PwdResetCodes");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.ResetCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEaUser>(entity =>
            {
                entity.HasKey(e => e.RhuserId)
                    .HasName("PK_tblEA_Users_2");

                entity.ToTable("tblEA_Users");

                entity.HasIndex(e => e.Email, "UQ__tblEA_Us__7614F5F659FA5E80")
                    .IsUnique();

                entity.Property(e => e.RhuserId).HasColumnName("RHUserID");

                entity.Property(e => e.Company)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Eid)
                    .HasColumnName("EID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.LockOut)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate()-(1))");

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasMaxLength(8000);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Uname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("uname");

                entity.HasOne(d => d.CompanyNavigation)
                    .WithMany(p => p.TblEaUsers)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblEA_Users_tblEA_Companies");
            });

            modelBuilder.Entity<TblQtdSignon>(entity =>
            {
                entity.ToTable("tblQTD_Signons");

                entity.Property(e => e.AccessLevel).IsUnicode(false);

                entity.Property(e => e.ActivationCode).IsUnicode(false);

                entity.Property(e => e.AppTimestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("App_Timestamp");

                entity.Property(e => e.ClientId)
                    .IsUnicode(false)
                    .HasColumnName("ClientID");

                entity.Property(e => e.CompanyName).IsUnicode(false);

                entity.Property(e => e.CurrentUser).IsUnicode(false);

                entity.Property(e => e.Empaccess).HasColumnName("EMPAccess");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Ipaddress)
                    .IsUnicode(false)
                    .HasColumnName("IPAddress");

                entity.Property(e => e.MachineName).IsUnicode(false);

                entity.Property(e => e.QtdreleaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("QTDReleaseDate");

                entity.Property(e => e.Qtdversion)
                    .IsUnicode(false)
                    .HasColumnName("QTDVersion");

                entity.Property(e => e.Requestdate).HasColumnType("datetime");

                entity.Property(e => e.Sqldbname)
                    .IsUnicode(false)
                    .HasColumnName("SQLDBName");

                entity.Property(e => e.SqlserverName)
                    .IsUnicode(false)
                    .HasColumnName("SQLServerName");

                entity.Property(e => e.Tdtaccess).HasColumnName("TDTAccess");

                entity.Property(e => e.WorkingMode).IsUnicode(false);
            });

            modelBuilder.Entity<TblQtsActivationCode>(entity =>
            {
                entity.ToTable("tblQTS_ActivationCodes");

                entity.Property(e => e.ActivationCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CodeReleaseYear).HasColumnName("CodeRelease_Year");

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PrimaryEmail)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
