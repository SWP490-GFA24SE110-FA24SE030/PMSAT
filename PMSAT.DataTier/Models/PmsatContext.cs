using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PMSAT.DataTier.Models;

public partial class PmsatContext : DbContext
{
    public PmsatContext()
    {
    }

    public PmsatContext(DbContextOptions<PmsatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Commit> Commits { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<IntegrationConfig> IntegrationConfigs { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectAnalysisResult> ProjectAnalysisResults { get; set; }

    public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

    public virtual DbSet<ProjectRole> ProjectRoles { get; set; }

    public virtual DbSet<PullRequest> PullRequests { get; set; }

    public virtual DbSet<Repository> Repositories { get; set; }

    public virtual DbSet<RoleInProject> RoleInProjects { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskSprint> TaskSprints { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorkFlow> WorkFlows { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=14.225.204.144,6789;Database=PMSAT;Uid=pmsat;Pwd=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Commit>(entity =>
        {
            entity.ToTable("Commit");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Message).HasMaxLength(250);

            entity.HasOne(d => d.Repository).WithMany(p => p.Commits)
                .HasForeignKey(d => d.RepositoryId)
                .HasConstraintName("FK_Commit_Repository");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedback");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Content).HasMaxLength(250);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Feedback_User");
        });

        modelBuilder.Entity<IntegrationConfig>(entity =>
        {
            entity.ToTable("IntegrationConfig");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ApiKey).HasMaxLength(50);
            entity.Property(e => e.Settings).HasMaxLength(50);
            entity.Property(e => e.ToolName).HasMaxLength(50);
            entity.Property(e => e.Url).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.IntegrationConfigs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_IntegrationConfig_User");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProjectAnalysisResult>(entity =>
        {
            entity.ToTable("ProjectAnalysisResult");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Complexity).HasMaxLength(50);
            entity.Property(e => e.Converage).HasMaxLength(50);
            entity.Property(e => e.QualityMetrics).HasMaxLength(50);
            entity.Property(e => e.RequirementError).HasMaxLength(50);
            entity.Property(e => e.SecurityError).HasMaxLength(50);
            entity.Property(e => e.SyntaxError).HasMaxLength(50);

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectAnalysisResults)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ProjectAnalysisResult_Project");
        });

        modelBuilder.Entity<ProjectMember>(entity =>
        {
            entity.ToTable("ProjectMember");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ProjectMember_Project");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ProjectMember_User");
        });

        modelBuilder.Entity<ProjectRole>(entity =>
        {
            entity.ToTable("ProjectRole");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<PullRequest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PullRequest");

            entity.Property(e => e.Body).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.MergedAt).HasColumnType("datetime");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Url).HasMaxLength(50);

            entity.HasOne(d => d.Repository).WithMany()
                .HasForeignKey(d => d.RepositoryId)
                .HasConstraintName("FK_PullRequest_Repository");
        });

        modelBuilder.Entity<Repository>(entity =>
        {
            entity.ToTable("Repository");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Owner)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Url).HasMaxLength(50);

            entity.HasOne(d => d.ProjectMember).WithMany(p => p.Repositories)
                .HasForeignKey(d => d.ProjectMemberId)
                .HasConstraintName("FK_Repository_ProjectMember");
        });

        modelBuilder.Entity<RoleInProject>(entity =>
        {
            entity.ToTable("RoleInProject");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.ProjectMember).WithMany(p => p.RoleInProjects)
                .HasForeignKey(d => d.ProjectMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleInProject_ProjectMember");

            entity.HasOne(d => d.ProjectRole).WithMany(p => p.RoleInProjects)
                .HasForeignKey(d => d.ProjectRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleInProject_ProjectRole");
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.ToTable("Sprint");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.Sprints)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_Sprint_Project");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.ToTable("Task");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ProjectMember).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectMemberId)
                .HasConstraintName("FK_Task_ProjectMember");
        });

        modelBuilder.Entity<TaskSprint>(entity =>
        {
            entity.ToTable("TaskSprint");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Sprint).WithMany(p => p.TaskSprints)
                .HasForeignKey(d => d.SprintId)
                .HasConstraintName("FK_TaskSprint_Sprint");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskSprints)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_TaskSprint_Task");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WorkFlow>(entity =>
        {
            entity.ToTable("WorkFlow");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FromStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ToStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Task).WithMany(p => p.WorkFlows)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_WorkFlow_Task");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
