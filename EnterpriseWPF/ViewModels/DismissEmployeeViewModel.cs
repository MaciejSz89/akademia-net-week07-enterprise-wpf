using EnterpriseWPF.Commands;
using EnterpriseWPF.Models.Wrappers;
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
            Employee = employee;
            DismissalDate = DateTime.Now;
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private Repository _repository = new Repository();

        private DateTime _dismissalDate;

        public DateTime DismissalDate
        {
            get
            {
                return _dismissalDate;
            }
            set
            {
                _dismissalDate = value;
                OnPropertyChanged();
            }
        }


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
            _repository.DismissEmployee(Employee, DismissalDate);

            var window = obj as Window;
            CloseWindow(window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }


    }
}
