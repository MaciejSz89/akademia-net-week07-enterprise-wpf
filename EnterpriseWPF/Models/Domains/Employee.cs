using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseWPF.Models.Domains
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; } = DateTime.Now;

        [Column(TypeName = "date")]
        public DateTime? DismissalDate { get; set; }
        public string Comments { get; set; }
        public bool IsHired { get; set; } = true;



    }
}
