using EnterpriseWPF.Commands;
using EnterpriseWPF.Models.Wrappers;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EnterpriseWPF.ViewModels
{
    public class DismissEmployeeViewModel : ViewModelBase
    {

        public DismissEmployeeViewModel(EmployeeWrapper employee)
        {       
            employee.IsHired = false;
            employee.DismissalDate = DateTime.Now;
            Employee = employee;
            ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        private bool CanConfirm(object obj)
        {
            return Employee.IsDismissalDateValid;
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private Repository _repository = new Repository();




        private EmployeeWrapper _employee;
        public EmployeeWrapper Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }


        private void Cancel(object obj)
        {
            var window = obj as Window;
            CloseWindow(window);
        }

        private void Confirm(object obj)
        {
            _repository.UpdateEmployee(Employee);

            var window = obj as MetroWindow;
            CloseWindow(window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }


    }
}
