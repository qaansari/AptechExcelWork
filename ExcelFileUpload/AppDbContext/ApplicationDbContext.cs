using ExcelFileUpload.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcelFileUpload.AppDbContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
