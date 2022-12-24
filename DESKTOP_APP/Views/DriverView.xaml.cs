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
            InitializeBindings();
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
        }

        private void showImageButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new PhotoView(_driver.PhotoId);
            view.Show();
        }
    }
}
