using ToDoApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDoApp.data
{
    public partial class ToDoDbContext : DbContext
    {
        DbSet<TaskItem> Tasks { get; set; }
        DbSet<User> users { get; set;}
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
                entity.Property(t => t.Title).HasColumnName("Title");
                entity.Property(t => t.Description).HasColumnName("Description");
                entity.Property(t => t.AddedOnUtc).HasColumnName("AddedOn");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("PK_User");
                entity.ToTable("Users", "todo_users");
                entity.Property(t => t.Username).HasColumnName("username");
                entity.Property(t => t.Password).HasColumnName("password");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
