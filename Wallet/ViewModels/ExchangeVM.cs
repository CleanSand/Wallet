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
    public class ExchangeVM : BaseViewModel
    {
        private History _history = new History();
        private RelayCommand _openWindow1;
        private RelayCommand _openWindow2;
        private RelayCommand _openWindow3;
        private ObservableCollection<Currency> _currencyList;
        private Exchange _exchange;
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
                        HistoryWindow historyWindow = new HistoryWindow();
                        historyWindow.Show();
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
                        PayWindow payWindow = new PayWindow();
                        payWindow.Show();
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
        public ExchangeVM()
        {
            _currencyList = new ObservableCollection<Currency>(Helper.GetContext().Currencies);
            ConvertCurrencies = new RelayCommand(x =>
            {
                var listofcoins = new ObservableCollection<ListOfCoin>(Helper.GetContext().ListOfCoins.Where(x => x.IdUser == Autorization.AuthorizedUser.IdUser));
                var selectcoin2 = listofcoins.FirstOrDefault(x => x.IdCoinsNavigation.IdCurrency == SetToCurrency.IdCurrency)!.IdCoinsNavigation;
                var selectcoin = listofcoins.FirstOrDefault(x => x.IdCoinsNavigation.IdCurrency == GetFromCurrency.IdCurrency)!.IdCoinsNavigation;

                try
                {
                    if (decimal.Parse(GetCurrency) <= selectcoin.NumberOfCoins)
                    {
                        if (GetFromCurrency != null && SetToCurrency != null && GetFromCurrency != SetToCurrency)
                        {
                            if (_getCurrency == null) return;
                            MessageBoxResult msg = MessageBox.Show($"Сумма перевода составляет: {decimal.Parse(GetCurrency) * Exchange.Size}", "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (msg == MessageBoxResult.Yes)
                            {
                                selectcoin.NumberOfCoins -= decimal.Parse(GetCurrency);
                                selectcoin2.NumberOfCoins += decimal.Parse(GetCurrency) * Exchange.Size;
                                _history.IdListOfCoins = listofcoins.FirstOrDefault(x => x.IdCoinsNavigation.IdCurrency == GetFromCurrency.IdCurrency)!.IdListOfCoins;
                                _history.Description = $"Перевёл в { selectcoin2.IdCurrencyNavigation.Name} на сумму {decimal.Parse(GetCurrency)} в размере {GetCurrency}";
                                Helper.GetContext().Histories.Add(_history);
                                Helper.GetContext().SaveChanges();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ошибка в выборе валюты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы превысили кол-во валюты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Не выбрано кол-во валюты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
            });
        }

        private string _getCurrency;

        public string GetCurrency
        {
            get { return _getCurrency; }
            set
            {
                _getCurrency = value;
                OnPropertyChanged();
            }
        }

        private Currency _getFromCurrency;

        public Currency GetFromCurrency
        {
            get { return _getFromCurrency; }
            set
            {
                _getFromCurrency = value;
                this.Exchange = new Exchange();
                OnPropertyChanged();
            }
        }

        private Currency _setToCurrency;

        public Currency SetToCurrency
        {
            get { return _setToCurrency; }
            set
            {
                _setToCurrency = value;
                this.Exchange = new Exchange();
                OnPropertyChanged();
            }
        }

        private RelayCommand _convertCurrencies;

        public RelayCommand ConvertCurrencies
        {
            get { return _convertCurrencies; }
            set
            {
                _convertCurrencies = value;
            }
        }

        public Exchange Exchange
        {
            get => _exchange;
            set
            {
                if (_setToCurrency != null && _getFromCurrency != null && _setToCurrency != _getFromCurrency) 
                    _exchange = Helper.GetContext().Exchanges.FirstOrDefault(e => e.IdGiveCurrency == GetFromCurrency.IdCurrency && e.IdGetCurrency == SetToCurrency.IdCurrency)!;
            }
        }
    }
}
