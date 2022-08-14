using HR_SYSTEM_V1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR_SYSTEM_V1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<DiscountExtra> DiscountExtras { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }

        public virtual DbSet<Salary> Salaries { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserCalims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleCalims", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens" ,"Security");
            builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            builder.Entity<ApplicationUser>().ToTable("Users", "Security")
            .Ignore(c => c.AccessFailedCount)
            .Ignore(c => c.LockoutEnabled)
            .Ignore(c => c.TwoFactorEnabled)
            .Ignore(c => c.ConcurrencyStamp)
            .Ignore(c => c.LockoutEnd)
            .Ignore(c => c.EmailConfirmed)
            .Ignore(c => c.TwoFactorEnabled)
            .Ignore(c => c.LockoutEnd)
            .Ignore(c => c.AccessFailedCount)
            .Ignore(c => c.PhoneNumberConfirmed)
            .Ignore(c => c.Email)
            .Ignore(c => c.NormalizedEmail)
            .Ignore(c => c.PhoneNumber);
        }
    }
}