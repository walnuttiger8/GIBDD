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
using REG_MARK_LIB;

namespace DESKTOP_APP.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateVehicleView.xaml
    /// </summary>
    public partial class CreateVehicleView : Window
    {
        private readonly GIBDDEntities _db;
        public Vehicles Vehicle { get; set; }
        public CreateVehicleView(Drivers driver = null)
        {
            InitializeComponent();

            _db = new GIBDDEntities();

            driversComboBox.ItemsSource = _db.Drivers.ToList();
            driversComboBox.SelectedItem = driver;

            colorComboBox.ItemsSource = _db.CarColors.ToList();
            engineTypeComboBox.ItemsSource = _db.EngineTypes.ToList();
            typeOfDriveComboBox.ItemsSource = _db.TypeOfDrive.ToList();
            regionComboBox.ItemsSource = _db.RegionCodes.ToList();
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var driver = driversComboBox.SelectedItem as Drivers;
            if (driver == null)
            {
                MessageBox.Show("Укажите водителя");
                return;
            }
            var color = colorComboBox.SelectedItem as CarColors;
            if (color == null)
            {
                MessageBox.Show("Укажите цвет");
                return;
            }
            var engingeType = engineTypeComboBox.SelectedItem as EngineTypes;
            if (engingeType == null)
            {
                MessageBox.Show("Укажите тип двигателя");
                return;
            }
            var typeOfDrive = typeOfDriveComboBox.SelectedItem as TypeOfDrive;
            if (typeOfDrive == null)
            {
                MessageBox.Show("Укажите привод");
                return;
            }

            int year;
            try
            {
                year = int.Parse(yearTextBox.Text);
            } catch
            {
                MessageBox.Show("Некорректный год");
                return;
            }

            int weight;
            try
            {
                weight = int.Parse(weightTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Некорректный вес");
                return;
            }
            var region = regionComboBox.SelectedItem as RegionCodes;

            if (region == null)
            {
                MessageBox.Show("Укажите регион");
                return;
            }

            var code = region.RegionCodeCodes.FirstOrDefault();

            if (code == null)
            {
                MessageBox.Show("Не удалось определить код региона");
                return;
            }


            var vehicle = new Vehicles()
            {
                DriverId = driver.Id,
                VINCode = VinTextBox.Text,
                Manufacturer = manufacturerTextBox.Text,
                Model = modelTextBox.Text,
                Year = year,
                Weight = weight,
                CarColorId = color.Id,
                EngineTypeId = engingeType.Id,
                TypeOfDriveId = typeOfDrive.Id,
                DateCreated = DateTime.Now
            };

            var rn = GetRegNumber(code);

            try
            {
                _db.Vehicles.Add(vehicle);
                _db.SaveChanges();
                Vehicle = vehicle;

                _db.RegNumbers.Add(new RegNumbers()
                {
                    VehicleId = vehicle.Id,
                    Number = rn.Number.Value,
                    Series = rn.Series.ToString(),
                    DateCreated = DateTime.Now,
                    RegionCodeCodeId=code.Id
                });
                _db.SaveChanges();
            } catch
            {
                foreach (var error in _db.GetValidationErrors())
                {
                    foreach (var message in error.ValidationErrors)
                    {
                        MessageBox.Show(message.ErrorMessage);
                    }
                }
                return;
            }
            DialogResult = true;
        }

        private RegNumber GetRegNumber(RegionCodeCodes code)
        {
            RegNumber regMark;
            var lastNumber = _db.RegNumbers.Where(x => x.RegionCodeCodes.Id == code.Id).OrderByDescending(x => x.DateCreated).FirstOrDefault();

            if (lastNumber == null)
            {
                regMark = new RegNumber(new RegNumberSeries("aaa"), new RegNumberNumber(1), code.Code);
            } else
            {
                regMark = new RegNumber(new RegNumberSeries(lastNumber.Series), new RegNumberNumber(lastNumber.Number), code.Code);
            }

            regMark += 1;

            return regMark;
        }
    }
}
