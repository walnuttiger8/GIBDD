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
    /// Логика взаимодействия для FineView.xaml
    /// </summary>
    public partial class FineView : Window
    {
        private readonly int _fineId;
        private readonly GIBDDEntities _db;
        private Fines _fine;


        public FineView(int fineId)
        {
            InitializeComponent();
            _db = new GIBDDEntities();

            _fine = _db.Fines.Where(x => x.Id == fineId).FirstOrDefault();
            if (_fine == null)
            {
                throw new ArgumentException();
            }
            DataContext = _fine;
            InitializeBindings();

            historyDataGrid.ItemsSource = _fine.FineStatusHistory;
        }

        private void InitializeBindings()
        {
            var bindingMap = new Dictionary<TextBox, string>()
            {
                {   carNumTextBox,      "CarNum" },
                {   licenseNumTextBox,  "LicenseNum" },
            };

            foreach (var binding in bindingMap)
            {
                binding.Key.SetBinding(TextBox.TextProperty, new Binding()
                {
                    Source = _fine,
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

        private void showImageButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new PhotoView(_fine.PhotoId);
            view.Show();
        }
    }
}
