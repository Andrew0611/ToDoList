using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Server.Models
{
    public partial class MemorandumContext : DbContext
    {
        public MemorandumContext()
        {
        }

        public MemorandumContext(DbContextOptions<MemorandumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Todolist> Todolists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configurationRoot.GetConnectionString("Memorandum"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todolist>(entity =>
            {
                entity.ToTable("Todolist");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Describe)
                    .HasMaxLength(200)
                    .HasColumnName("describe");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiryDate");

                entity.Property(e => e.Subjects)
                    .HasMaxLength(10)
                    .HasColumnName("subjects");

                entity.Property(e => e.TimeLeft)
                    .HasColumnName("timeLeft")
                    .HasComputedColumnSql("(datediff(day,getdate(),[expiryDate]))", false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
