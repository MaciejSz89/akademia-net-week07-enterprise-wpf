using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseWPF.Models.Wrappers
{
    public class EmployeeWrapper : IDataErrorInfo
    {
    

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? DismissalDate { get; set; }
        public string Comments { get; set; }

        public bool IsHired { get; set; }

        public bool IsDismissalDateValid { get; set; }

        private bool _isFirstNameValid;
        private bool _isLastNameValid;
        private bool _isSalaryValid;

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "Pole Imię jest wymagane.";
                            _isFirstNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isFirstNameValid = true;
                        }
                        break;
                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Pole Nazwisko jest wymagane.";
                            _isLastNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLastNameValid = true;
                        }
                        break;
                    case nameof(Salary):
                        if (Salary <= 0)
                        {
                            Error = "Pole Pensja jest wymagane.";
                            _isSalaryValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isSalaryValid = true;
                        }
                        break;
                    case nameof(DismissalDate):
                        if (!IsHired && DismissalDate < HireDate)
                        {
                            Error = "Data zwolnienia nie może być wcześniejsza niż data zatrudnienia";
                            IsDismissalDateValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            IsDismissalDateValid = true;
                        }
                        break;
                    default:
                        break;
                }
                return Error;
            }

        }
        public bool IsValid
        {
            get
            {
                return _isFirstNameValid && _isLastNameValid && _isSalaryValid && IsDismissalDateValid;
            }
        }


    }
}
