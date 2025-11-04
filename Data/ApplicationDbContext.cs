using _13_ORM_Geenid.Models;
using Microsoft.EntityFrameworkCore;

namespace _13_ORM_Geenid.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Alleeli> Alleelid { get; set; }
        public DbSet<Geeni> Geenid { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Alleeli konfiguratsioon
            modelBuilder.Entity<Alleeli>(entity =>
            {
                entity.HasKey(a => a.Id);
                
                entity.Property(a => a.Nimetus)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(a => a.Positiivne)
                    .IsRequired();

                // Index nimetuse jaoks kiiremaks otsinguks
                entity.HasIndex(a => a.Nimetus);
            });

            // Geeni konfiguratsioon
            modelBuilder.Entity<Geeni>(entity =>
            {
                entity.HasKey(g => g.Id);

                entity.Property(g => g.Alleel1Id)
                    .IsRequired();

                entity.Property(g => g.Alleel2Id)
                    .IsRequired();

                // Esimese alleeli seos
                entity.HasOne(g => g.Alleel1)
                    .WithMany()
                    .HasForeignKey(g => g.Alleel1Id)
                    .OnDelete(DeleteBehavior.Restrict);

                // Teise alleeli seos
                entity.HasOne(g => g.Alleel2)
                    .WithMany()
                    .HasForeignKey(g => g.Alleel2Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
