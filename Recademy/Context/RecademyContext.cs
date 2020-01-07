using Microsoft.EntityFrameworkCore;
using Recademy.Models;

namespace Recademy.Context
{
    public class RecademyContext : DbContext
    {
        public RecademyContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSkill>()
                .HasKey(bc => new { bc.UserId, bc.SkillName });

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.Skill)
                .WithMany(s => s.UserSkills)
                .HasForeignKey(s => s.SkillName);

            modelBuilder.Entity<ProjectSkill>()
                .HasKey(ps => new { ps.SkillName, ps.ProjectId });
            modelBuilder.Entity<ProjectSkill>()
                .HasOne(ps => ps.ProjectInfo)
                .WithMany(p => p.Skills)
                .HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<ProjectSkill>()
                .HasOne(ps => ps.Skill)
                .WithMany(s => s.ProjectSkills)
                .HasForeignKey(s => s.SkillName);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ReviewResponse> ReviewResponses { get; set; }
        public DbSet<ReviewRequest> ReviewRequests { get; set; }
        public DbSet<ProjectInfo> ProjectInfos { get; set; }

        public DbSet<UserSkill> UserSkills { get; set; }

        public DbSet<ProjectSkill> ProjectSkills { get; set; }

    }
}
