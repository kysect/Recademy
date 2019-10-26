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

        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ReviewResponse> ReviewResponses { get; set; }
        public DbSet<ReviewRequest> ReviewRequests { get; set; }
        public DbSet<ProjectInfo> ProjectInfos { get; set; }
    }
}
