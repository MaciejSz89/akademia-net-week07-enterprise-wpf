using EnterpriseWPF.Commands;
using EnterpriseWPF.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EnterpriseWPF.ViewModels
{
    public class AddEditEmployeeViewModel : ViewModelBase
    {
        public AddEditEmployeeViewModel(Employee employee = null)
        {
            if (employee == null)
            {
                Employee = new Employee();
            }
            else
            {
                Employee = employee;
                IsUpdate = true;
            }

            IsHiredChangedCommand = new RelayCommand(IsHiredChanged, CanEmploymentStatusChange);
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);

        }


        public ICommand IsHiredChangedCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private Repository _repository = new Repository();


        private Employee _employee;

        public Employee Employee
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

        private bool _isUpdate;

        public bool IsUpdate
        {
            get
            {
                return _isUpdate;
            }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }

        private bool CanEmploymentStatusChange(object obj)
        {
            return Employee.IsHired;
        }

        private void IsHiredChanged(object obj)
        {
            if (!Employee.IsHired)
            {
                Employee.DismissalDate = DateTime.Now;
                OnPropertyChanged(nameof(Employee));
            }
        }


        private void Cancel(object obj)
        {
            var window = obj as Window;
            CloseWindow(window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void Confirm(object obj)
        {
            if(!IsUpdate)
            {
                _repository.AddEmployee(Employee);
            }
            else
            {
                _repository.UpdateEmployee(Employee);
            }
            var window = obj as Window;
            CloseWindow(window);
        }




    }
}
