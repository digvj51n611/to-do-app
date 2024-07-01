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
            base.OnConfiguring( builder );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.User.Id);
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("PK_TaskItem");
                entity.ToTable("TaskItems", "todo_users");
                entity.Property(t => t.Title).HasColumnName("Title");
                entity.Property(t => t.Description).HasColumnName("Description");
                entity.Property(t => t.AddedOnUtc).HasColumnName("AddedOn");
                entity.Property(t => t.Status)
                .HasColumnName("Status")
                .HasConversion<string>();

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
