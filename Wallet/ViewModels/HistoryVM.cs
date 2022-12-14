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
    public class HistoryVM : BaseViewModel
    {
        private RelayCommand _openWindow1;
        private RelayCommand _openWindow2;
        private RelayCommand _openWindow3;
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
                        PayWindow payWindow = new PayWindow();
                        payWindow.Show();
                        ((Window)x).Close();
                    }));
            }
        }

        private ObservableCollection<History> _list;

        public ObservableCollection<History> Lists
        {

            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        public HistoryVM()
        {
            Lists = new ObservableCollection<History>(Helper.GetContext().Histories.Where(x => x.IdListOfCoinsNavigation.IdUser == Autorization.AuthorizedUser.IdUser));
        }
    }
}
