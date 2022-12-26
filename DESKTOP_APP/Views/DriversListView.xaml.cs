using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            var view = new CreateDriverView();
            var result = view.ShowDialog();

            if (result == true)
            {
                driversListView.ItemsSource = _db.Drivers.ToList();
            }
        }

        private void readButton_Click(object sender, RoutedEventArgs e)
        {
            var driver = driversListView.SelectedItem as Drivers;

            var view = new DriverView(driver.Id);
            var result = view.ShowDialog();

            if (result == true)
            {
                driversListView.ItemsSource = _db.Drivers.ToList();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Подтверждение удаления", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var result2 = MessageBox.Show("Вы точно уверены?", "Окончательное подтверждение удаления", MessageBoxButton.YesNo);
                if (result2 == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Удалено");
                    var driver = driversListView.SelectedItem as Drivers;
                    if (driver == null)
                    {
                        MessageBox.Show("Выберите водителя!");
                        return;
                    }

                    _db.Drivers.Remove(driver);
                    _db.Entry(driver).State = System.Data.Entity.EntityState.Deleted;
                    _db.SaveChanges();
                    driversListView.ItemsSource = _db.Drivers.ToList();

                }
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchQuery = searchTextBox.Text;
            driversListView.ItemsSource = _db.Drivers.Where(x =>
                x.FirstName.Contains(searchQuery) | x.LastName.Contains(searchQuery) | x.MiddleName.Contains(searchQuery)
            ).ToList();
        }
    }
}
