﻿using ToDoApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ToDoApp.data
{
    public partial class ToDoDbContext : DbContext
    {
        public virtual DbSet<TaskItem> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set;}
        public virtual DbSet<Data.Entities.TaskStatus> TaskStatuses { get; set; }
        //public ToDoDbContext()
        //{

        //}
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring( DbContextOptionsBuilder builder )
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.TaskStatus)
                .WithMany()
                .HasForeignKey(t => t.TaskStatusId)
                .IsRequired();
            modelBuilder.Entity<Data.Entities.TaskStatus>(entity =>
            {
                entity.HasKey(s => s.Id).HasName("PK_TaskStatus");
                entity.ToTable("TaskStatus", "todo_users");
                entity.Property(s => s.Status)
                .HasColumnName("Status")
                .HasConversion<string>();
            });
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("PK_TaskItem");
                entity.ToTable("TaskItems", "todo_users");
                entity.Property(t => t.Title)
                .HasColumnName("Title")
                .IsRequired();

                entity.Property(t => t.Description)
                .HasColumnName("Description");

                entity.Property(t => t.AddedOnUtc)
                .HasColumnName("AddedOn")
                .IsRequired();
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id)
                .HasName("PK_User");
                entity.HasAlternateKey(u => u.Username);
                entity.ToTable("Users", "todo_users");
                entity.Property(u => u.Username).HasColumnName("Username")
                .IsRequired();
                entity.Property(u => u.Password).HasColumnName("Password")
                .IsRequired();
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
