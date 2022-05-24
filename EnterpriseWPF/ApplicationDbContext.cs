using EnterpriseWPF.Models.Configurations;
using EnterpriseWPF.Models.Domains;
using System;
using System.Data.Entity;
using System.Linq;

namespace EnterpriseWPF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base(_connectionString)
        {

        }
        private static string _connectionString = new ConnectionSettings().ConnectionString;



        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
        }
    }


}