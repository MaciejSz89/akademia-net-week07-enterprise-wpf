using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseWPF
{
    public class UserData : IDataErrorInfo
    {
        private bool _isLoginEntered;
        private bool _isPasswordEntered;

        public string Login { get; set; }
        public string Password { get; set; }
       
        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Login):
                        if (string.IsNullOrWhiteSpace(Login))
                        {
                            Error = "Pole Login jest wymagane.";
                            _isLoginEntered = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLoginEntered = true;
                        }
                        break;

                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                        {
                            Error = "Pole Nazwa serwera jest wymagane.";
                            _isPasswordEntered = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isPasswordEntered = true;
                        }
                        break;                  

                    default:
                        break;
                }
                return Error;
            }
        }

        public bool IsLoginDataValid()
        {

            if (Login == Properties.Settings.Default.UserDataLogin && Password == Properties.Settings.Default.UserDataPassword)
                return true;

            return false;
        }

        public bool IsDataEntered
        {
            get
            {
                return _isLoginEntered && _isPasswordEntered;
            }
        }
    }
}
