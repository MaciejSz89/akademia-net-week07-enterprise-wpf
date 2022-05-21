using EnterpriseWPF.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseWPF
{
    public class Repository
    {
        public void AddEmployee(Employee employee)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }

        }
        public void UpdateEmployee(Employee employee)
        {
            using (var context = new ApplicationDbContext())
            {
                UpdateEmployeeProperties(context, employee);

                context.SaveChanges();
            }
        }

        private void UpdateEmployeeProperties(ApplicationDbContext context, Employee employee)
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
    }
}
