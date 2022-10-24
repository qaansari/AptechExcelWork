using ExcelFileUpload.Helpers;
using ExcelFileUpload.Models.User_Model;
using Microsoft.EntityFrameworkCore;

namespace ExcelFileUpload.AppDbContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleID=1,
                Title="Super Admin"
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                UserID=1,
                FirstName="Super",
                LastName="Admin",
                Email="super_admin@share.com",
                Password="123".HashedWithSalt(),
                IsActive=true,
                RoleID=1
            });
        }
    }
}
