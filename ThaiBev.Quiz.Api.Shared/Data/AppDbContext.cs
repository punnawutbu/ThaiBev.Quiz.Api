using Microsoft.EntityFrameworkCore;
using ThaiBev.Quiz.Api.Shared.Models;

namespace ThaiBev.Quiz.Api.Shared.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Question>()
                .HasKey(q => q.Id);

            modelBuilder.Entity<Question>()
                .HasIndex(q => q.QuestionNo)
                .IsUnique();

            modelBuilder.Entity<Question>()
                .Property(q => q.Text)
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Choices)
                .WithOne()
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Choice>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Choice>()
                .Property(c => c.Text)
                .HasMaxLength(300)
                .IsRequired();

            modelBuilder.Entity<Choice>()
                .HasIndex(c => new { c.QuestionId, c.ChoiceNo })
                .IsUnique();
        }
    }
}