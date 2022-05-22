using EnterpriseWPF.Models.Domains;
using EnterpriseWPF.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseWPF.Models.Converters
{
    public static class EmployeeConverter
    {
        public static EmployeeWrapper ToWrapper(this Employee model)
        {
            EmployeeWrapper employeeWrapper = new EmployeeWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Salary = model.Salary,
                Comments = model.Comments,
                HireDate = model.HireDate,
                DismissalDate = model.DismissalDate,
                IsHired = model.IsHired
            };
            return employeeWrapper;
        }

        public static Employee ToDao(this EmployeeWrapper model)
        {
            Employee employee = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Salary = model.Salary,
                Comments = model.Comments,
                HireDate = model.HireDate,
                DismissalDate = model.DismissalDate,
                IsHired = model.IsHired
            };
            return employee;
        }
    }
}
