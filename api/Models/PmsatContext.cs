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

    public virtual DbSet<Board> Boards { get; set; }

    public virtual DbSet<Commit> Commits { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

    public virtual DbSet<PullRequest> PullRequests { get; set; }

    public virtual DbSet<Repository> Repositories { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TaskP> TaskPs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-QDI89RC;Database=PMSAT;User ID=sa;Password= 123456;Trusted_Connection=False;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnalysisResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Analysis__3214EC07C480C3D3");

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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__AnalysisR__Proje__3F466844");
        });

        modelBuilder.Entity<Board>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Board__3214EC07102FB867");

            entity.ToTable("Board");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Project).WithMany(p => p.Boards)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Board__ProjectId__4D94879B");
        });

        modelBuilder.Entity<Commit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Commits__3214EC079B502F20");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Message).HasMaxLength(255);

            entity.HasOne(d => d.Repository).WithMany(p => p.Commits)
                .HasForeignKey(d => d.RepositoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Commits__Reposit__44FF419A");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Issue__3214EC073F2C1FF2");

            entity.ToTable("Issue");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Detail).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Task).WithMany(p => p.Issues)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Issue__TaskId__5CD6CB2B");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Project__3214EC07D7F16EF4");

            entity.ToTable("Project");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<ProjectMember>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProjectId }).HasName("PK__ProjectM__00E967A3DA5364C0");

            entity.ToTable("ProjectMember");

            entity.Property(e => e.Role).HasMaxLength(50);

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__ProjectMe__Proje__3C69FB99");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ProjectMe__UserI__3B75D760");
        });

        modelBuilder.Entity<PullRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PullRequ__3214EC075A2DB0D2");

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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PullReque__Repos__47DBAE45");
        });

        modelBuilder.Entity<Repository>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reposito__3214EC076C5C9A9E");

            entity.ToTable("Repository");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Owner).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Repositories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Repositor__UserI__4222D4EF");
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sprint__3214EC0719D658EA");

            entity.ToTable("Sprint");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.Sprints)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Sprint__ProjectI__4AB81AF0");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tag__3214EC078ECEEA94");

            entity.ToTable("Tag");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<TaskP>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaskP__3214EC07770FE707");

            entity.ToTable("TaskP");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Updated).HasColumnType("datetime");

            entity.HasOne(d => d.Assignee).WithMany(p => p.TaskPAssignees)
                .HasForeignKey(d => d.AssigneeId)
                .HasConstraintName("FK__TaskP__AssigneeI__5165187F");

            entity.HasOne(d => d.Board).WithMany(p => p.TaskPs)
                .HasForeignKey(d => d.BoardId)
                .HasConstraintName("FK__TaskP__BoardId__5441852A");

            entity.HasOne(d => d.Project).WithMany(p => p.TaskPs)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TaskP__ProjectId__52593CB8");

            entity.HasOne(d => d.Reporter).WithMany(p => p.TaskPReporters)
                .HasForeignKey(d => d.ReporterId)
                .HasConstraintName("FK__TaskP__ReporterI__5070F446");

            entity.HasOne(d => d.Sprint).WithMany(p => p.TaskPs)
                .HasForeignKey(d => d.SprintId)
                .HasConstraintName("FK__TaskP__SprintId__534D60F1");

            entity.HasMany(d => d.Tags).WithMany(p => p.Tasks)
                .UsingEntity<Dictionary<string, object>>(
                    "TaskTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TaskTag__TagId__59FA5E80"),
                    l => l.HasOne<TaskP>().WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TaskTag__TaskId__59063A47"),
                    j =>
                    {
                        j.HasKey("TaskId", "TagId").HasName("PK__TaskTag__AA3E862B1FDA87FD");
                        j.ToTable("TaskTag");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC079CE2CD02");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Workflow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workflow__3214EC073E1814F5");

            entity.ToTable("Workflow");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CurrentStatus).HasMaxLength(50);
            entity.Property(e => e.NewStatus).HasMaxLength(50);
            entity.Property(e => e.OldStatus).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Task).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Workflow__TaskId__5FB337D6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
