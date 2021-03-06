﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TestTaskApp.EntityFramework.Entities;

namespace TestTaskApp.EntityFramework
{
    public class TestTaskAppContext : DbContext
    {
        public TestTaskAppContext()
            : base("DbTestTaskAppConnection")
        {
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            objectContext.SavingChanges += OnSavingChanges;
        }

        public DbSet<DbTestEntity> TestEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DbTestEntity>()
                .ToTable("TestEntity");

            modelBuilder.Entity<DbTestEntity>()
                .Property(t => t.Name)
                .IsUnicode(true);

        }

        private void OnSavingChanges(object sender, EventArgs args)
        {
            var now = DateTime.Now.ToUniversalTime();
            foreach (var entry in this.ChangeTracker.Entries<IDateTracked>())
            {
                var entity = entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = now;
                        entity.UpdatedDate = now;
                        break;
                    case EntityState.Modified:
                        entity.UpdatedDate = now;
                        break;
                }
            }
            this.ChangeTracker.DetectChanges();
        }
    }
}
