using EnterpriseWPF.Commands;
using EnterpriseWPF.Models.Wrappers;
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
            DismissEmployeeCommand = new RelayCommand(DismissEmployee, CanDismissEmployee);
            ConnectionSettingsCommand = new RelayCommand(ConnectionSettings);
        }



        private ConnectionSettings _connectionSettings;

        private ObservableCollection<EmployeeWrapper> _employees;

        private EmployeeWrapper _selectedEmployee;
        private Repository _repository = new Repository();

        public EmployeeWrapper SelectedEmployee
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
        public ICommand ConnectionSettingsCommand { get; set; }

        public ObservableCollection<EmployeeWrapper> Employees
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

        private void DismissEmployee(object obj)
        {
            var dismissEmployeeView = new DismissEmployeeView(obj as EmployeeWrapper);
            dismissEmployeeView.Closed += DismissEmployeeView_Closed;
            dismissEmployeeView.ShowDialog();
        }

        private void DismissEmployeeView_Closed(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            using (var dataContext = new ApplicationDbContext())
            {
                Employees = new ObservableCollection<EmployeeWrapper>(_repository.GetEmployees());
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
            var addEditEmployeeView = new AddEditEmployeeView(obj as EmployeeWrapper);
            addEditEmployeeView.Closed += AddEditEmployeeView_Closed;
            addEditEmployeeView.ShowDialog();
        }

        private void AddEditEmployeeView_Closed(object sender, EventArgs e)
        {
            RefreshData();
        }

        private async void LoadedWindow(object obj)
        {
            if (!_connectionSettings.IsConnectionAvailable())
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await metroWindow.ShowMessageAsync("Nie można nawiązać połączenia z bazą danych", "Czy chcesz poprawić ustawienia połączenia?", MessageDialogStyle.AffirmativeAndNegative);
                if (dialog == MessageDialogResult.Affirmative)
                {
                    OpenConnectionSettings();
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

        private void ConnectionSettings(object obj)
        {
            OpenConnectionSettings();
        }

        private void OpenConnectionSettings()
        {
            var connectionSettingsView = new ConnectionSettingsView(false);
            connectionSettingsView.ShowDialog();
        }



    }
}
