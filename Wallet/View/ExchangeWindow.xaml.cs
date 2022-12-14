using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wallet.ViewModels;

namespace Wallet.View
{
    /// <summary>
    /// Логика взаимодействия для ExchangeWindow.xaml
    /// </summary>
    public partial class ExchangeWindow : Window
    {
        public ExchangeWindow()
        {
            InitializeComponent();
            DataContext = new { Exchan = new ExchangeVM(), AuthorizationVM = new Autorization() };
        }
    }
}
