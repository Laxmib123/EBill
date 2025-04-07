using EBill.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EBill.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        
        public DbSet<Test> tests {  get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Products.HasData();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationRole>().HasData(
                 new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "e8507878-ad77-4c11-9216-2b03bc55621c" },
                new ApplicationRole { Id = 2, Name = "Manager", NormalizedName = "USER", ConcurrencyStamp = "e7c965c6-a4c3-4594-aca9-62b386b98f09" },
                new ApplicationRole { Id = 3, Name = "Staff", NormalizedName = "STAFF", ConcurrencyStamp = "0c872e6d-292e-4bd5-a383-f21c486209aa" }
            );

           // modelBuilder.Entity<IdentityUserRole<string>>().HasData(
           //    new IdentityUserRole<string> { UserId = existingUserId, RoleId = adminRoleId }
           //);

        }

    }
}
