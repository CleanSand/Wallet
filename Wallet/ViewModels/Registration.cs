using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wallet;
using Wallet.View;

namespace Wallet.ViewModels
{
    public class Registration : BaseViewModel
    {
        private User _user = new User();
        private RelayCommand _submitRegistration;
        private User _place;
        private string _password;
        private string _login;
        private string _surname;
        private string _name;
        private string _patronymic;
        private DateTime _birthdate;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get {return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                _patronymic = value;
                OnPropertyChanged();
            }
        }
        public DateTime BirthDate
        {
            get { return _birthdate; }
            set
            {
                _birthdate = value;
                OnPropertyChanged();
            }
        }
        Random random = new Random();
        public RelayCommand SumbitRegistration
        {
            get
            {
                return _submitRegistration ??
                    (_submitRegistration = new RelayCommand((x) =>
                    {
                        var user = Helper.GetContext().Users.Any(x => Login == x.Login);
                        if (user == false)
                        {
                            _user.Login = _login;

                            _user.Password = _password;

                            _user.Surname = _surname;

                            _user.Name = _name;

                            _user.Patronymic = _patronymic;

                            _user.BirthDate = _birthdate;

                            _user.WalletNumber = random.Next(1000000, 9999999);


                            Helper.GetContext().Users.Add(_user);
                            try
                            {
                                Helper.GetContext().SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Данные не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            if (_user.Login != null & _user.Password != null & _user.Surname != null & _user.Name != null & _user.Patronymic != null & _user.BirthDate != null)
                            {
                                MessageBox.Show("Пользователь Зарегистрирован", "Зарегистрирован", MessageBoxButton.OK, MessageBoxImage.Information);
                                AutorizationWindow authorization = new AutorizationWindow();
                                authorization.Show();
                                ((Window)x).Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Данный логин занят", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }));
            }
        }
    }
}
