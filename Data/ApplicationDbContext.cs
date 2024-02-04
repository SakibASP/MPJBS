using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MPJBS.Models;
using MPJBS.Models.CustomIdentity;
using MPJBS.ViewModels;

namespace MPJBS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DynamicMenuItem>().HasNoKey();
        }

        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<MemberTypes> MemberTypes { get; set; }
        public virtual DbSet<Collection> Collection { get; set; }
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<MenuItem> MenuItem { get; set; }
        public virtual DbSet<MenuToRole> MenuToRole { get; set; }

        //View Models
        public virtual DbSet<DynamicMenuItem> DynamicMenuItem { get; set; }
    }
}
