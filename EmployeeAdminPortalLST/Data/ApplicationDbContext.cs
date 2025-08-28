using EmployeeAdminPortalLST.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortalLST.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {


        }
    
    
        public DbSet<Employee> Employees { get; set; }
        //dynamic DbContext
        public DbSet<T> SetOf<T>() where T : class => SetOf<T>();


    }
}
