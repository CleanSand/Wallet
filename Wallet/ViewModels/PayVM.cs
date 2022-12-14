using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wallet.View;

namespace Wallet.ViewModels
{
    public class PayVM : BaseViewModel
    {
        private RelayCommand _openWindow1;
        private RelayCommand _openWindow2;
        private RelayCommand _openWindow3;
        private ObservableCollection<Currency> _currencyList;
        private RelayCommand _submit;
        private Currency _getFromCurrency;
        private Payment _payment= new Payment();
        private decimal _amount;
        public RelayCommand OpenWindow1
        {
            get
            {
                return _openWindow1 ??
                    (_openWindow1 = new RelayCommand((x) =>
                    {
                        HomePageWindow homePageWindow = new HomePageWindow();
                        homePageWindow.Show();
                        ((Window)x).Close();
                    }));
            }
        }
        public RelayCommand OpenWindow2
        {
            get
            {
                return _openWindow2 ??
                    (_openWindow2 = new RelayCommand((x) =>
                    {
                        ExchangeWindow exchangeWindow = new ExchangeWindow();
                        exchangeWindow.Show();
                        ((Window)x).Close();
                    }));
            }
        }

        public RelayCommand OpenWindow3
        {
            get
            {
                return _openWindow3 ??
                    (_openWindow3 = new RelayCommand((x) =>
                    {
                        HistoryWindow historyWindow = new HistoryWindow();
                        historyWindow.Show();
                        ((Window)x).Close();
                    }));
            }
        }
        public ObservableCollection<Currency> CurrencyList
        {

            get => _currencyList;
            set
            {
                _currencyList = value;
                OnPropertyChanged();
            }
        }
        
        public Currency GetFromCurrency
        {
            get { return _getFromCurrency; }
            set
            {
                _getFromCurrency = value;
                OnPropertyChanged();
            }
        }
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }
        
        public RelayCommand Sumbit
        {
            
            get
            {
                return _submit ??
                    (_submit = new RelayCommand((x) =>
                    {
                        var selectlist = Helper.GetContext().ListOfCoins.FirstOrDefault(x => x.IdCoinsNavigation.IdCurrency == GetFromCurrency.IdCurrency);
                        var selectcoin = Helper.GetContext().Coins.FirstOrDefault(x => x.IdCurrency == GetFromCurrency.IdCurrency);
                        _payment.TimePayment = DateTime.Now;
                        _payment.PaymentAmount = _amount;
                        _payment.IdListOfCoins = selectlist.IdListOfCoins;
                        selectcoin.NumberOfCoins += _amount;
                        Helper.GetContext().Payments.Add(_payment);
                        try
                        {
                            Helper.GetContext().SaveChanges();
                            MessageBox.Show("Баланс пополнен", "Успешно", MessageBoxButton.OK, MessageBoxImage.None);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Данные не заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }));
            }
        }
        public PayVM()
        {
            _currencyList = new ObservableCollection<Currency>(Helper.GetContext().Currencies);
        }
    }
}
