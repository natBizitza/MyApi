using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyApi.Models
{
    public partial class oesiaContext : DbContext
    {
        public oesiaContext()
        {
        }

        public oesiaContext(DbContextOptions<oesiaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Submodule> Submodule { get; set; }
        public virtual DbSet<Subtask> Subtask { get; set; }
        public virtual DbSet<Tasks> Task { get; set; }
        public virtual DbSet<UserProject> UserProject { get; set; }
        public virtual DbSet<UserSubtask> UserSubtask { get; set; }
        public virtual DbSet<UserTask> UserTask { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=tcp:oesia.database.windows.net,1433;Initial Catalog=oesia;Persist Security Info=False;User ID=adminoesia;Password=Gruponoguay1.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreationDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasIndex(e => e.ProjectId);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Module)
                    .HasForeignKey(d => d.ProjectId);
            });

            modelBuilder.Entity<Submodule>(entity =>
            {
                entity.HasIndex(e => e.ModuleId);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Submodule)
                    .HasForeignKey(d => d.ModuleId);
            });

            modelBuilder.Entity<Subtask>(entity =>
            {
                entity.HasIndex(e => e.TaskId);

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Subtask)
                    .HasForeignKey(d => d.TaskId);
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.HasIndex(e => e.SubmoduleId);

                entity.HasOne(d => d.Submodule)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.SubmoduleId);
            });

            modelBuilder.Entity<UserProject>(entity =>
            {
                entity.HasIndex(e => e.AppUsersId);

                entity.HasIndex(e => e.ProjectsId);

                entity.HasOne(d => d.AppUsers)
                    .WithMany(p => p.UserProject)
                    .HasForeignKey(d => d.AppUsersId);

                entity.HasOne(d => d.Projects)
                    .WithMany(p => p.UserProject)
                    .HasForeignKey(d => d.ProjectsId);
            });

            modelBuilder.Entity<UserSubtask>(entity =>
            {
                entity.HasIndex(e => e.AppUsersId);

                entity.HasIndex(e => e.SubtasksId);

                entity.Property(e => e.PauseTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.PlayTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.RecordTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.StopTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.HasOne(d => d.AppUsers)
                    .WithMany(p => p.UserSubtask)
                    .HasForeignKey(d => d.AppUsersId);

                entity.HasOne(d => d.Subtasks)
                    .WithMany(p => p.UserSubtask)
                    .HasForeignKey(d => d.SubtasksId);
            });

            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.HasIndex(e => e.AppUsersId);

                entity.HasIndex(e => e.TasksId);

                entity.HasOne(d => d.AppUsers)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.AppUsersId);

                entity.HasOne(d => d.Tasks)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.TasksId);
            });
        }
    }
}
