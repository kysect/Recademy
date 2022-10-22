using Microsoft.EntityFrameworkCore;
using Recademy.Core.Models;
using Recademy.Core.Models.Achievements;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Models.Roles;
using Recademy.Core.Models.Skills;
using Recademy.Core.Models.Users;
using Recademy.DataAccess.Seeding;

namespace Recademy.DataAccess;

public class RecademyContext : DbContext
{
    private readonly IDbContextSeeder _seeder;

    public RecademyContext(DbContextOptions options, IDbContextSeeder seeder) : base(options)
    {
        _seeder = seeder;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _seeder.Seed(modelBuilder);

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

        modelBuilder.Entity<ReviewResponseUpvote>()
            .HasKey(ru => new { ReviewId = ru.ReviewResponseId, ru.UserId });

        modelBuilder.Entity<UserAchievementInfo>()
            .HasKey(ua => new { ua.UserId, ua.AchievementId });

        modelBuilder.Entity<UserRoleAssociation>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });
    }

    public DbSet<User> Users { get; set; }
    public DbSet<RecademyUser> RecademyUsers { get; set; }

    public DbSet<Skill> Skills { get; set; }
    public DbSet<ReviewResponse> ReviewResponses { get; set; }
    public DbSet<ReviewRequest> ReviewRequests { get; set; }
    public DbSet<ProjectInfo> ProjectInfos { get; set; }

    public DbSet<UserSkill> UserSkills { get; set; }
    public DbSet<ProjectSkill> ProjectSkills { get; set; }
    public DbSet<ReviewResponseUpvote> ReviewResponseUpvotes { get; set; }
    public DbSet<UserAchievementInfo> UserAchievementInfos { get; set; }
    public DbSet<UserRoleAssociation> UserRoleAssociations { get; set; }
    public DbSet<UserAchievementRequest> UserAchievementRequests { get; set; }
    public DbSet<UserAchievementResponse> UserAchievementResponses { get; set; }


    public DbSet<Settings> Settings { get; set; }
}