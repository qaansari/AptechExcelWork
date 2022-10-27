using ExcelFileUpload.Helpers;
using ExcelFileUpload.Models;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<ExcelFiles> ExcelFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleID = 1,
                Title = "Super Admin",
                IsActive=true
            },
            new Role
            {
                RoleID = 2,
                Title="Sub Admin",
                IsActive = true
            });
            
            modelBuilder.Entity<User>().HasData(new User
            {
                UserID=1,
                FirstName="Super",
                LastName="Admin",
                Email="super_admin@stoke.com",
                Password="123".HashedWithSalt(),
                ImageName= "user-default.png",
                IsActive =true,
                RoleID=1
            },
            new User
            {
                UserID = 2,
                FirstName = "Sub",
                LastName = "Admin",
                Email = "sub_admin@stoke.com",
                Password = "123".HashedWithSalt(),
                ImageName = "user-default.png",
                IsActive = true,
                RoleID = 2
            });
        }
    }
}
