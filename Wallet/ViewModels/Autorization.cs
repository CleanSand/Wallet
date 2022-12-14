using Wallet;
using Wallet.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wallet.ViewModels
{
    public class Autorization : BaseViewModel
    {
        private string _password;
        private string _login;
        private RelayCommand _openWindow;
        private RelayCommand _openWindow2;
        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand OpenWindow
        {
            get
            {
                return _openWindow ??
                    (_openWindow = new RelayCommand((x) =>
                    {
                        RegistrationWindow registration = new RegistrationWindow();
                        registration.Show();
                        ((Window)x).Close();
                    }));
            }
        }
        public static User AuthorizedUser { get; set; }
        public RelayCommand OpenWindow2
        {
            get
            {
                return _openWindow2 ??
                    (_openWindow2 = new RelayCommand((x) =>
                    {
                        var user = Helper.GetContext().Users.SingleOrDefault(x => x.Login == Login & x.Password == Password);
                        if (user != null)
                        {
                            AuthorizedUser = user;
                            HomePageWindow homePage = new HomePageWindow();
                            homePage.Show();
                            ((Window)x).Close();
                        }
                        if (user == null)
                        {
                            MessageBox.Show("Данные введены неверно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }));
            }
        }
        public Autorization()
        {

        }
    }
}
