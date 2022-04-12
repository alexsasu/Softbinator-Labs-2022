using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using slabs_project.Models.Entities;

namespace slabs_project.Models
{
    public class ProjDbContext : IdentityDbContext<User, Role, int,
                                    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ProjDbContext(DbContextOptions options) : base(options) { }

        public DbSet<SessionToken> SessionTokens { get; set; }

        public DbSet<Refugee>? Refugees { get; set; }
        public DbSet<RefugeeDetails>? RefugeesDetails { get; set; }
        public DbSet<Center>? Centers { get; set; }
        public DbSet<NGO>? NGOs { get; set; }
        public DbSet<RefugeeNGO>? RefugeeNGOs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Refugee>()
                .HasOne(x => x.RefugeeDetails)
                .WithOne(x => x.Refugee);

            modelBuilder.Entity<Center>()
                .HasMany(x => x.Refugees)
                .WithOne(x => x.Center);

            modelBuilder.Entity<RefugeeNGO>().HasKey(key => new { key.RefugeeId, key.NGOId });

            modelBuilder.Entity<RefugeeNGO>()
                .HasOne(x => x.Refugee)
                .WithMany(x => x.RefugeeNGOs)
                .HasForeignKey(x => x.RefugeeId);

            modelBuilder.Entity<RefugeeNGO>()
                .HasOne(x => x.NGO)
                .WithMany(x => x.RefugeeNGOs)
                .HasForeignKey(x => x.NGOId);

            modelBuilder.Entity<UserRole>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });

                ur.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
                ur.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            });
        }
    }
}
