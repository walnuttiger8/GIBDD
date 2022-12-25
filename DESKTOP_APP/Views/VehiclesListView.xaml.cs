using APPLICATION;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для VehiclesListView.xaml
    /// </summary>
    public partial class VehiclesListView : Window
    {
        private readonly GIBDDEntities _db;
        public List<Vehicles> Vehicles { get; set; }

        public VehiclesListView()
        {
            InitializeComponent();

            _db = new GIBDDEntities();

            _db.Vehicles.Load();

            Vehicles = _db.Vehicles.ToList();

            vehiclesListView.ItemsSource = Vehicles;

        }

        private Vehicles GetSelectedVehicle()
        {
            var vehilce = vehiclesListView.SelectedItem as Vehicles;
            return vehilce;
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private List<Vehicles> SearchVehicles(string vin)
        {
            var vehicles = _db.Vehicles.Where(x => vin.Contains(vin)).ToList();
            return vehicles;
        }

        private void readButton_Click(object sender, RoutedEventArgs e)
        {
            var vehicle = GetSelectedVehicle();
            if (vehicle == null)
            {
                MessageBox.Show("ТС не выбрано");
                return;
            }
            var view = new RegNumberView(vehicle.Id);
            view.Show();
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
                    // TODO: Добавить удаление
                }
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
