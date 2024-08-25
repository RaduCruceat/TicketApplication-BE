using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using TicketApplication.Data.Entities;

namespace TicketApplication.Data.Context
{
    public class BonContext : DbContext
    {
        public BonContext(DbContextOptions<BonContext> options) : base(options)
        {
        }

        public DbSet<Ghiseu> Ghiseu { get; set; }
        public DbSet<Bon> Bon { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ghiseu>(entity =>
            {
                entity.ToTable("Ghiseu", "bon");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.Cod).IsRequired();
                entity.Property(e => e.Denumire).IsRequired();
                entity.Property(e => e.Descriere);
                entity.Property(e => e.Icon);
                entity.Property(e => e.Activ);
            });

            modelBuilder.Entity<Bon>(entity =>
            {
                entity.ToTable("Bon", "bon");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.IdGhiseu);
                entity.Property(e => e.Stare).HasConversion<int>();
                entity.Property(e => e.Stare);
                entity.Property(e => e.CreatedAt);
                entity.Property(e => e.ModifiedAt);

                entity.HasOne(d => d.Ghiseu)
                    .WithMany(p => p.Bonuri)
                    .HasForeignKey(d => d.IdGhiseu);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}