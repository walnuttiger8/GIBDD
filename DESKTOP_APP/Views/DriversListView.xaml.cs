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
using APPLICATION;

namespace DESKTOP_APP.Views
{
    /// <summary>
    /// Логика взаимодействия для DriversListView.xaml
    /// </summary>
    public partial class DriversListView : Window
    {
        private readonly GIBDDEntities _db;
        public DriversListView()
        {
            InitializeComponent();
            _db = new GIBDDEntities();

            driversListView.ItemsSource = _db.Drivers.ToList();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void readButton_Click(object sender, RoutedEventArgs e)
        {
            var driver = driversListView.SelectedItem as Drivers;

            var view = new DriverView(driver.Id);
            view.Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
