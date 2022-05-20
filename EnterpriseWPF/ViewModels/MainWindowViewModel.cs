using EnterpriseWPF.Commands;
using EnterpriseWPF.Models.Domains;
using EnterpriseWPF.ViewModels;
using EnterpriseWPF.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EnterpriseWPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {
            _connectionSettings = new ConnectionSettings();
            LoadedWindowCommand = new RelayCommand(LoadedWindow);
            AddEmployeeCommand = new RelayCommand(AddEditEmployee);
            EditEmployeeCommand = new RelayCommand(AddEditEmployee, CanEditEmployee);
            DismissEmployeeCommand = new AsyncRelayCommand(DismissEmployee, CanDismissEmployee);
        }



        private ConnectionSettings _connectionSettings;

        private ObservableCollection<Employee> _employees;

        private Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand DismissEmployeeCommand { get; set; }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

        private async Task DismissEmployee(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Zwolnienie pracownika", $"Czy na pewno chcesz zwolnić pracownika {SelectedEmployee.FirstName} {SelectedEmployee.LastName}?", MessageDialogStyle.AffirmativeAndNegative);
            if (dialog != MessageDialogResult.Affirmative)
                return;


            //_repository.DeleteStudent(SelectedStudent.Id);

            RefreshData();
        }

        private void RefreshData()
        {
            using (var dataContext = new ApplicationDbContext())
            {
                Employees = new ObservableCollection<Employee>(dataContext.Employees.ToList());
            }
        }

        private bool CanDismissEmployee(object arg)
        {
            return SelectedEmployee != null && SelectedEmployee?.IsHired == true;
        }

        private bool CanEditEmployee(object arg)
        {
            return SelectedEmployee != null;
        }

        private void AddEditEmployee(object obj)
        {
            var addEditEmployeeView = new AddEditEmployeeView(obj as Employee);
            addEditEmployeeView.ShowDialog();
        }

        private async void LoadedWindow(object obj)
        {
            if (!_connectionSettings.IsConnectionAvailable())
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await metroWindow.ShowMessageAsync("Nie można nawiązać połączenia z bazą danych", "Czy chcesz poprawić ustawienia połączenia?", MessageDialogStyle.AffirmativeAndNegative);
                if (dialog == MessageDialogResult.Affirmative)
                {
                    var connectionSettingsView = new ConnectionSettingsView(false);
                    connectionSettingsView.ShowDialog();
                }
                else if (dialog == MessageDialogResult.Negative)
                {
                    Application.Current.Shutdown();
                }
            }
            else
            {
                RefreshData();
            }

        }

    }
}
