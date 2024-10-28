using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SWD392_PublicService.Models;

public partial class Swd392PublicSystemContext : DbContext
{
    public Swd392PublicSystemContext()
    {
    }

    public Swd392PublicSystemContext(DbContextOptions<Swd392PublicSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationDocument> ApplicationDocuments { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<ProcessingAgency> ProcessingAgencies { get; set; }

    public virtual DbSet<PublicService> PublicServices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("MyCnn"));
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Applicat__C93A4F7936D9644A");

            entity.ToTable("Application");

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.AgencyId).HasColumnName("AgencyID");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Note).HasMaxLength(512);
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.SubmissionDate).HasColumnType("date");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Agency).WithMany(p => p.Applications)
                .HasForeignKey(d => d.AgencyId)
                .HasConstraintName("FK__Applicati__Agenc__31EC6D26");

            entity.HasOne(d => d.Service).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Applicati__Servi__30F848ED");

            entity.HasOne(d => d.User).WithMany(p => p.Applications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Applicati__UserI__300424B4");
        });

        modelBuilder.Entity<ApplicationDocument>(entity =>
        {
            entity.HasKey(e => new { e.ApplicationId, e.DocumentId }).HasName("PK__Applicat__2891A18F6C3A939C");

            entity.ToTable("ApplicationDocument");

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.AttachDate).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(512);

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationDocuments)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Applicati__Appli__4222D4EF");

            entity.HasOne(d => d.Document).WithMany(p => p.ApplicationDocuments)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Applicati__Docum__4316F928");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD036922B8");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF6F1138A907");

            entity.ToTable("Document");

            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Path).HasMaxLength(256);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Documents)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Document__Create__3F466844");
        });

        modelBuilder.Entity<ProcessingAgency>(entity =>
        {
            entity.HasKey(e => e.AgencyId).HasName("PK__Processi__95C546FBBDBF803B");

            entity.ToTable("ProcessingAgency");

            entity.Property(e => e.AgencyId).HasColumnName("AgencyID");
            entity.Property(e => e.District).HasMaxLength(80);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<PublicService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__PublicSe__C51BB0EADC1557B7");

            entity.ToTable("PublicService");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.DateCreated).HasColumnType("date");
            entity.Property(e => e.DateDeleted).HasColumnType("date");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Description).HasMaxLength(1024);
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.ServiceFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ServiceName).HasMaxLength(200);

            entity.HasOne(d => d.Department).WithMany(p => p.PublicServices)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__PublicSer__Depar__2A4B4B5E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3AED73A715");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(256);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC4635820B");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(30);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__RoleID__2D27B809");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
