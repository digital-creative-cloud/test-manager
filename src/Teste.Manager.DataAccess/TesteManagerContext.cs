using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Teste.Manager.Domain;

namespace Teste.Manager.DataAccess
{
    public class TestManagerContext : DbContext
    {
        public TestManagerContext(
            DbContextOptions<TestManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Feature> Feature { get; set; }
        public DbSet<Step> Step { get; set; }
        public DbSet<TestCase> TestCase { get; set; }
        public DbSet<FeaturesToTestCases> FeaturesToTestCases { get; set; }
        public DbSet<TestCasesToSteps> TestCasesToSteps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Feature>(etd =>
            {
                etd.HasKey(c => c.Id);
                etd.Property(c => c.Name).HasMaxLength(100).IsRequired();
                etd.Property(c => c.Describe).HasMaxLength(300).IsRequired();
                etd.HasMany(c => c.FeaturesToTestCases).WithOne(x => x.Feature).HasForeignKey(z => z.FeatureId);
            });

            modelBuilder.Entity<FeaturesToTestCases>(etd =>
            {
                etd.HasKey(c => new { c.FeatureId, c.TestCaseId});
                etd.HasOne(c => c.Feature).WithMany(x => x.FeaturesToTestCases).HasForeignKey(z => z.FeatureId);
                etd.HasOne(c => c.TestCase).WithMany(x => x.FeaturesToTestCases).HasForeignKey(z => z.TestCaseId);
            });

            modelBuilder.Entity<TestCase>(etd =>
            {
                etd.HasKey(c => c.Id);
                etd.Property(c => c.ProcessBeginning).HasMaxLength(100).IsRequired();
                etd.Property(c => c.Name).HasMaxLength(100).IsRequired();
                etd.Property(c => c.DeviceType).HasConversion<int>().IsRequired();
                etd.Property(c => c.DeviceInterface).HasConversion<int>().IsRequired();
                etd.HasMany(c => c.FeaturesToTestCases).WithOne(x => x.TestCase).HasForeignKey(z => z.TestCaseId);
                etd.HasMany(c => c.TestCasesToSteps).WithOne(x => x.TestCase).HasForeignKey(z => z.TestCaseId);
            });

            modelBuilder.Entity<TestCasesToSteps>(etd =>
            {
                etd.HasKey(c => new { c.StepId, c.TestCaseId, c.StepOrder });
                etd.Property(c => c.StepOrder).IsRequired();
                etd.HasOne(c => c.Step).WithMany(x => x.TestCasesToSteps).HasForeignKey(z => z.StepId);
                etd.HasOne(c => c.TestCase).WithMany(x => x.TestCasesToSteps).HasForeignKey(z => z.TestCaseId);
            });

            modelBuilder.Entity<Step>(etd =>
            {
                etd.HasKey(c => c.Id);
                etd.Property(c => c.Name).HasMaxLength(100).IsRequired();
                etd.Property(c => c.TypeId).HasConversion<int>().IsRequired();
                etd.Property(c => c.Value).HasMaxLength(1000).IsRequired();
                etd.Property(c => c.Path).HasMaxLength(1000).IsRequired();
                etd.HasMany(c => c.TestCasesToSteps).WithOne(x => x.Step).HasForeignKey(z => z.StepId);
            });
        }
    }
}
