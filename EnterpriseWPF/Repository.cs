using EnterpriseWPF.Models.Converters;
using EnterpriseWPF.Models.Domains;
using EnterpriseWPF.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseWPF
{
    public class Repository
    {
        public void AddEmployee(EmployeeWrapper employee)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Employees.Add(employee.ToDao());
                context.SaveChanges();
            }

        }
        public void UpdateEmployee(EmployeeWrapper employee)
        {
            using (var context = new ApplicationDbContext())
            {
                UpdateEmployeeProperties(context, employee);

                context.SaveChanges();
            }
        }

        private void UpdateEmployeeProperties(ApplicationDbContext context, EmployeeWrapper employee)
        {
            var employeeToUpdate = context.Employees.Find(employee.Id);
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.Salary = employee.Salary;
            employeeToUpdate.HireDate = employee.HireDate;
            employeeToUpdate.DismissalDate = employee.DismissalDate;
            employeeToUpdate.IsHired = employee.IsHired;
            employeeToUpdate.Comments = employee.Comments;
        }

        public List<EmployeeWrapper> GetEmployees()
        {   
            using (var context = new ApplicationDbContext())
            {
                var employees = context.Employees.ToList();
                return employees
                    .Select(x => x.ToWrapper())
                    .ToList();
            }
        }

    }
}
