using Microsoft.EntityFrameworkCore;
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
                entity.Property(e => e.Id)
                    .UseIdentityColumn();

                entity.Property(e => e.Cod)
                    .IsRequired()
                    .HasMaxLength(50); // Add appropriate max length

                entity.Property(e => e.Denumire)
                    .IsRequired()
                    .HasMaxLength(100); // Add appropriate max length

                entity.Property(e => e.Descriere)
                    .HasMaxLength(500); // Add appropriate max length

                entity.Property(e => e.Icon)
                    .HasMaxLength(100); // Add appropriate max length

                entity.Property(e => e.Activ)
                  .IsRequired()
                  .HasConversion<bool>();

                // Configure the relationship with Bon
                entity.HasMany(g => g.Bonuri)
                    .WithOne(b => b.Ghiseu)
                    .HasForeignKey(b => b.IdGhiseu)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Bon>(entity =>
            {
                entity.ToTable("Bon", "bon");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .UseIdentityColumn();

                entity.Property(e => e.IdGhiseu)
                    .IsRequired();

                entity.Property(e => e.Stare)
                    .HasConversion<int>()
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                entity.Property(e => e.ModifiedAt)
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                // Configure the relationship with Ghiseu
                entity.HasOne(b => b.Ghiseu)
                    .WithMany(g => g.Bonuri)
                    .HasForeignKey(b => b.IdGhiseu)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}