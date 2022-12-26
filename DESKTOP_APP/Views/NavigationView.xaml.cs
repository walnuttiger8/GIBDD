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

namespace DESKTOP_APP.Views
{
    /// <summary>
    /// Логика взаимодействия для NavigationView.xaml
    /// </summary>
    public partial class NavigationView : Window
    {
        public NavigationView()
        {
            InitializeComponent();
        }

        private void driversButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new DriversListView();
            view.Show();
        }

        private void vehiclesButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new VehiclesListView();
            view.Show();
        }

        private void finesButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new FinesListView();
            view.Show();
        }
    }
}
