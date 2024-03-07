using Membership_Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Membership_Managment.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<FeeCollection> FeeCollections { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Member>()
                .HasMany(d => d.DocumentList)
                .WithOne(m => m.Member)
                .HasForeignKey(m => m.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Member>()
                .HasMany(d => d.FeeCollection)
                .WithOne(m => m.Member)
                .HasForeignKey(m => m.MemberId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<FeeCollection>()
               .Property(m => m.Amount)
               .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Member>()
                .Property(m => m.MembershipAmount)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }


    }
}
