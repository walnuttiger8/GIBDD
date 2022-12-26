using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для CreateDriverView.xaml
    /// </summary>
    public partial class CreateDriverView : Window
    {
        private readonly GIBDDEntities _db;
        public CreateDriverView()
        {
            InitializeComponent();
            _db = new GIBDDEntities();
        }

        private void CreateDriver()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "jpg files (*.jpg)|*.jpg"
            };
            if (dialog.ShowDialog() == true)
            {
                var filename = dialog.FileName;
                
            } else
            {
                MessageBox.Show("Фото не выбрано!");
                return;
            }
            var f = File.ReadAllBytes(dialog.FileName);

            var photo = new Photos()
            {
                Data = f,
                Name = dialog.FileName,
            };

            _db.Photos.Add(photo);
            _db.SaveChanges();

            var d = new Drivers()
            {
                Id = new Guid(),
                LastName = driverLastNameTextBox.Text,
                FirstName = driverFirstNameTextBox.Text,
                MiddleName = driverMiddleNameTextBox.Text,
                Passport = driverPassportTextBox.Text,
                PlaceOfWork = driverPlaceOfWorkTextBox.Text,
                WorkPosition = driverWorkPositionTextBox.Text,
                Email = driverEmailTextBox.Text,
                LiveAddress = driverLiveAddressTextBox.Text,
                RegAddress = driverRegAddressTextBox.Text,
                Remarks = driverRemarksTextBox.Text,
                PhotoId = photo.Id,
                PhoneMobile= phoneMobileTextBox.Text
            };

            _db.Drivers.Add(d);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
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

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            CreateDriver();
        }
    }
}
