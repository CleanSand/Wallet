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
    /// Логика взаимодействия для HomePageWindow.xaml
    /// </summary>
    public partial class HomePageWindow : Window
    {
        public HomePageWindow()
        {
            InitializeComponent();
            DataContext = new { Home = new HomePage(), AuthorizationVM = new Autorization() };
        }
    }
}
