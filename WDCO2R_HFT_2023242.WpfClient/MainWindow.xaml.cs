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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WDCO2R_HFT_2023242.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bt_games_Click(object sender, RoutedEventArgs e)
        {
            BoardGameWindow boardGameWindow = new BoardGameWindow();
            boardGameWindow.Show();
        }

        private void bt_cust_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            this.Visibility = Visibility.Visible;
            customerWindow.ShowDialog();
        }

        private void bt_rent_Click(object sender, RoutedEventArgs e)
        {
            RentalWindow rentalWindow = new RentalWindow();
            this.Visibility = Visibility.Visible;
            rentalWindow.ShowDialog();
        }
    }
}
