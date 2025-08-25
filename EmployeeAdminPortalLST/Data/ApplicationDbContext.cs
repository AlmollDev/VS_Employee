using EmployeeAdminPortalLST.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortalLST.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
