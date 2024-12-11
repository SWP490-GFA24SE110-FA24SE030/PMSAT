using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public partial class PmsatContext : DbContext
{
    public PmsatContext()
    {
    }

    public PmsatContext(DbContextOptions<PmsatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnalysisResult> AnalysisResults { get; set; }

    public virtual DbSet<Commit> Commits { get; set; }

    public virtual DbSet<EvaluationResult> EvaluationResults { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

    public virtual DbSet<PullRequest> PullRequests { get; set; }

    public virtual DbSet<Repository> Repositories { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<TaskP> TaskPs { get; set; }

    public virtual DbSet<TaskSprint> TaskSprints { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=PMSAT;Uid=sa;Pwd=1234;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnalysisResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Analysis__3214EC0703BF3B97");

            entity.ToTable("AnalysisResult");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CodeComplexity).HasMaxLength(50);
            entity.Property(e => e.Coverage).HasMaxLength(50);
            entity.Property(e => e.QualityMetrics).HasMaxLength(50);
            entity.Property(e => e.RequirementError).HasMaxLength(50);
            entity.Property(e => e.SecurityError).HasMaxLength(50);
            entity.Property(e => e.SyntaxError).HasMaxLength(50);

            entity.HasOne(d => d.Project).WithMany(p => p.AnalysisResults)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__AnalysisR__Proje__5441852A");
        });

        modelBuilder.Entity<Commit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Commits__3214EC07E33AEF30");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Message).HasMaxLength(255);

            entity.HasOne(d => d.Repository).WithMany(p => p.Commits)
                .HasForeignKey(d => d.RepositoryId)
                .HasConstraintName("FK__Commits__Reposit__5CD6CB2B");
        });

        modelBuilder.Entity<EvaluationResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Evaluati__3214EC07FE268ABB");

            entity.ToTable("EvaluationResult");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AreasForImprovement).HasMaxLength(255);
            entity.Property(e => e.ReviewerComments).HasMaxLength(255);
            entity.Property(e => e.Strengths).HasMaxLength(255);
            entity.Property(e => e.WorkTrendAnalysis).HasMaxLength(255);

            entity.HasOne(d => d.ProjectMember).WithMany(p => p.EvaluationResults)
                .HasForeignKey(d => d.ProjectMemberId)
                .HasConstraintName("FK__Evaluatio__Proje__571DF1D5");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC07730A31CC");

            entity.ToTable("Feedback");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Detail).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedback__UserId__4BAC3F29");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Issue__3214EC071A00E884");

            entity.ToTable("Issue");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Detail).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Task).WithMany(p => p.Issues)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__Issue__TaskId__693CA210");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Project__3214EC07648CAF24");

            entity.ToTable("Project");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<ProjectMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProjectM__3214EC079F643737");

            entity.ToTable("ProjectMember");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Role).HasMaxLength(50);

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__ProjectMe__Proje__5165187F");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ProjectMe__UserI__5070F446");
        });

        modelBuilder.Entity<PullRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PullRequ__3214EC07A7ADAFD4");

            entity.ToTable("PullRequest");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Body).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.MergedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(255);

            entity.HasOne(d => d.Repository).WithMany(p => p.PullRequests)
                .HasForeignKey(d => d.RepositoryId)
                .HasConstraintName("FK__PullReque__Repos__5FB337D6");
        });

        modelBuilder.Entity<Repository>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reposito__3214EC07153E9BDE");

            entity.ToTable("Repository");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Owner).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(255);

            entity.HasOne(d => d.ProjectMember).WithMany(p => p.Repositories)
                .HasForeignKey(d => d.ProjectMemberId)
                .HasConstraintName("FK__Repositor__Proje__59FA5E80");
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sprint__3214EC077C99018C");

            entity.ToTable("Sprint");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Project).WithMany(p => p.Sprints)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Sprint__ProjectI__628FA481");
        });

        modelBuilder.Entity<TaskP>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaskP__3214EC07713E2414");

            entity.ToTable("TaskP");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Project).WithMany(p => p.TaskPs)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__TaskP__ProjectId__66603565");

            entity.HasOne(d => d.ProjectMember).WithMany(p => p.TaskPs)
                .HasForeignKey(d => d.ProjectMemberId)
                .HasConstraintName("FK__TaskP__ProjectMe__656C112C");
        });

        modelBuilder.Entity<TaskSprint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaskSpri__3214EC0715563BFF");

            entity.ToTable("TaskSprint");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.UpdateStartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedEndDate).HasColumnType("datetime");

            entity.HasOne(d => d.Sprint).WithMany(p => p.TaskSprints)
                .HasForeignKey(d => d.SprintId)
                .HasConstraintName("FK__TaskSprin__Sprin__6EF57B66");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskSprints)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__TaskSprin__TaskI__6FE99F9F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07539B6287");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Workflow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workflow__3214EC0781FFCEE9");

            entity.ToTable("Workflow");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CurrentStatus).HasMaxLength(50);
            entity.Property(e => e.NewStatus).HasMaxLength(50);
            entity.Property(e => e.OldStatus).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Task).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__Workflow__TaskId__6C190EBB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
