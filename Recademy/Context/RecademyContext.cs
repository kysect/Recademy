using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            modelBuilder.Entity<ProjectSkill>()
                .HasKey(bc => new { bc.ProjectId, bc.SkillName });

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
