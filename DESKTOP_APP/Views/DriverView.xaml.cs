using APPLICATION;
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
    /// Логика взаимодействия для DriverView.xaml
    /// </summary>
    public partial class DriverView : Window
    {
        private readonly GIBDDEntities _db;
        private Drivers _driver;

        public DriverView(Guid driverGuid)
        {
            InitializeComponent();
            _db = new GIBDDEntities();

            _driver = _db.Drivers.Where(x => x.Id == driverGuid).FirstOrDefault();
            if (_driver == null)
            {
                throw new ArgumentException();
            }
            DataContext = _driver;
            InitializeBindings();

            vehiclesListView.ItemsSource = _driver.Vehicles;
        }

        private void InitializeBindings()
        {
            var bindingMap = new Dictionary<TextBox, string>()
            {
                {   driverLastNameTextBox,     "LastName" },
                {   driverFirstNameTextBox,    "FirstName" },
                {   driverMiddleNameTextBox,   "MiddleName" },
                {   driverPassportTextBox,     "Passport" },
                {   driverRegAddressTextBox,   "RegAddress" },
                {   driverLiveAddressTextBox,  "LiveAddress" },
                {   driverPlaceOfWorkTextBox,  "PlaceOfWork" },
                {   driverWorkPositionTextBox, "WorkPosition" },
                {   driverEmailTextBox,        "Email" },
                {   driverRemarksTextBox,      "Remarks" },
            };

            foreach (var binding in bindingMap)
            {
                binding.Key.SetBinding(TextBox.TextProperty, new Binding()
                {
                    Source = _driver,
                    Mode = BindingMode.TwoWay,
                    Path = new PropertyPath(binding.Value)
                });
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            _db.SaveChanges();
            DialogResult = true;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void showVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            var vehicle = vehiclesListView.SelectedItem as Vehicles;

            if (vehicle == null)
            {
                MessageBox.Show("ТС не выбрано!");
                return;
            }

            var view = new RegNumberView(vehicle.Id);
            view.Show();
        }

        private string GetSeries()
        {
            var random = new Random();
            return string.Join("", Enumerable.Range(0, 4).Select(x => random.Next(0, 9).ToString()));
        }

        private string GetNumber()
        {
            var random = new Random();
            return string.Join("", Enumerable.Range(0, 6).Select(x => random.Next(0, 9).ToString()));
        }

        private bool CreateLicense()
        {
            var view = new ChooseCategoriesView();

            if (view.ShowDialog() == false)
            {
                return false;
            }

            var categories = view.Categories;
            var licenseDate = DateTime.Now;
            var license = new DriverLicenses()
            {
                DriverId = _driver.Id,
                LicenseDate = DateTime.Now,
                ExpireDate = licenseDate.AddYears(10),
                Series = GetSeries(),
                Number = GetNumber(),
                DateCreated = licenseDate,
            };

            var result = MessageBox.Show($"Будет создано ВУ: \n" +
                $"Дата регистрации: {license.LicenseDate.ToShortDateString()}\n" +
                $"Действительное до: {license.ExpireDate.ToShortDateString()}\n" +
                $"Категории ВУ: {string.Join(", ", categories.Select(x => x.Code.ToString()))}",
                "Подтверждение создания", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                return false;
            }

            _db.DriverLicenses.Add(license);
            _db.SaveChanges();
            foreach (var category in categories)
            {
                _db.DriverLicenseCategory.Add(new DriverLicenseCategory()
                {
                    DriverLicenseCategoryId = category.Id,
                    DriverLicenseId = license.Id,
                    DateCreated = license.DateCreated,
                });
            }
            _db.SaveChanges();
            return true;
        }

        private void showLicenseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var view = new LicenseView(_driver.Id);
                view.Show();
            } catch (DriverNotFoundException)
            {
                MessageBox.Show("Ошибка, не удалось найти водителя, обратитесь к администратору");
            } catch ( LicenseNotFoundException)
            {
                var result = MessageBox.Show("Не найдено водительское удостеверение, создать?", "Не найдено водительское удостоверение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var created = CreateLicense();
                    if (created)
                    {
                        try
                        {
                            var view2 = new LicenseView(_driver.Id);
                            view2.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Возникла ошибка, обратитесь к администратору");
                        }
                    }
                }
            }
            
        }

        private void createVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new CreateVehicleView(_driver);
            if (view.ShowDialog() == true)
            {
                vehiclesListView.ItemsSource = _db.Vehicles.Where(x => x.DriverId == _driver.Id).ToList();
            }
        }
    }
}
