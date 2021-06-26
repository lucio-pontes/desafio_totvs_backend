using Microsoft.EntityFrameworkCore;
using R3ctApp.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace R3ctApp.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DataSet's
        #region
        public DbSet<HireCandidate> HireCandidates { get; set; }
        
        public DbSet<HireJob> HireJobs { get; set; }
        
        public DbSet<HireCurriculum> HireCurriculums { get; set; }
        
        public DbSet<HireProcess> HireProcess { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
