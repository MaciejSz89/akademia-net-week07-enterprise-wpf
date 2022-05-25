using EnterpriseWPF.Commands;
using EnterpriseWPF.Views;
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
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            UserData = new UserData();
            ConfirmCommand = new AsyncRelayCommand(Confirm, CanConfirm);
            CloseCommand = new RelayCommand(Close);
        }

        private void Close(object obj)
        {
            App.Current.Shutdown();
        }

        private bool CanConfirm(object obj)
        {
            return UserData.IsDataEntered;
        }

        private async Task Confirm(object obj)
        {
            var loginWindow = obj as MetroWindow;
            var mainWindow = new MainWindowView();

            if (UserData.IsLoginDataValid())
            {
                
                mainWindow.Show();
                loginWindow.Close();
            }
            else
            {
    
                var height = loginWindow.Height;
                var width = loginWindow.Width;
                var left = loginWindow.Left;
                var top = loginWindow.Top;


                loginWindow.Height = mainWindow.Height;
                loginWindow.Width = mainWindow.Width;
                loginWindow.Left = left - (mainWindow.Width - width) / 2;
                loginWindow.Top = top - (mainWindow.Top - top) / 2;
                var dialog = await loginWindow.ShowMessageAsync("Błąd danych", "Podano błędne dane logowania", MessageDialogStyle.Affirmative);


                if (dialog==MessageDialogResult.Affirmative)
                {
                    loginWindow.Height = height;
                    loginWindow.Width = width;
                    loginWindow.Left = left;
                    loginWindow.Top = top;
                    loginWindow.Show();

                }
            }
        }

        private UserData _userData;
        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public UserData UserData
        {
            get
            {
                return _userData;
            }
            set
            {
                _userData = value;
                OnPropertyChanged();
            }
        }

    }
}
