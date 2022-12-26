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
    /// Логика взаимодействия для ChooseCategoriesView.xaml
    /// </summary>
    public partial class ChooseCategoriesView : Window
    {
        private readonly GIBDDEntities _db;

        private Dictionary<CheckBox, DriverLicenseCategories> _enabledCategories;
        public List<DriverLicenseCategories> Categories { get; set; } = new List<DriverLicenseCategories>();
        public ChooseCategoriesView()
        {
            InitializeComponent();
            _db = new GIBDDEntities();

            var categories = _db.DriverLicenseCategories.ToList();

            _enabledCategories = new Dictionary<CheckBox, DriverLicenseCategories>();

            foreach (var category in categories)
            {
                var panel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                };
                var label = new Label()
                {
                    Content = category.Code,
                };
                var rb = new CheckBox();
                panel.Children.Add(rb);
                panel.Children.Add(label);

                _enabledCategories.Add(rb, category);

                mainStackPanel.Children.Add(panel);
            }
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            Categories.Clear();
            foreach (var item in _enabledCategories)
            {
                if (item.Key.IsChecked == true)
                {
                    Categories.Add(item.Value);
                }
            }
            DialogResult = true;
        }
    }
}
