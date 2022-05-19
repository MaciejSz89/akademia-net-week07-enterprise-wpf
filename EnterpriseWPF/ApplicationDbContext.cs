using EmployeesWPF.Models.Configurations;
using EmployeesWPF.Models.Domains;
using System;
using System.Data.Entity;
using System.Linq;

namespace EmployeesWPF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=EmployeesDataContext")
        {
            
        }


         public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
        }
    }


}