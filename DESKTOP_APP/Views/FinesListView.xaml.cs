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
using APPLICATION.Services;
using Microsoft.Win32;

namespace DESKTOP_APP.Views
{
    /// <summary>
    /// Логика взаимодействия для FinesListView.xaml
    /// </summary>
    public partial class FinesListView : Window
    {
        private readonly GIBDDEntities _db;
        private readonly FSSPExporterService _fsspExporterService;
        public FinesListView()
        {
            InitializeComponent();
            _db = new GIBDDEntities();
            _fsspExporterService = new FSSPExporterService();

            finesListView.ItemsSource = _db.Fines.ToList();

            searchStatusComboBox.ItemsSource = _db.FineStatuses.ToList();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            var fines = _fsspExporterService.GetFinesToExport();

            var csv = new StringBuilder();

            foreach (var f in fines)
            {
                csv.AppendLine($"{f.Id}; {f.ExternalId}; {f.CarNum}; {f.LicenseNum}");
            }

            var sfd = new SaveFileDialog()
            {
                Filter = "csv files (*.csv)|*.csv"
            };
            if (sfd.ShowDialog() == true)
            {
                var filename = sfd.FileName;
                System.IO.File.WriteAllText(filename, csv.ToString());
            }
            _fsspExporterService.SendToFSSP(fines);
        }

        private void readButton_Click(object sender, RoutedEventArgs e)
        {
            var fine = finesListView.SelectedItem as Fines;

            if (fine == null)
            {
                MessageBox.Show("Штраф не выбран");
                return;
            }

            var view = new FineView(fine.Id);

            view.ShowDialog();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private List<Fines> GetFinesByStatus(FineStatuses status)
        {
            var result = new List<Fines>();

            foreach (var fine in _db.Fines)
            {
                var statusHistory = fine.FineStatusHistory;
                var lastStatusDt = statusHistory.Select(x => x.DateCreated).Max();
                var lastStatus = statusHistory.Where(x => x.DateCreated == lastStatusDt).FirstOrDefault();

                if (lastStatus.FineStatusId == status.Id)
                {
                    result.Add(fine);
                }
            }

            return result;
        }

        private void searchStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var status = searchStatusComboBox.SelectedItem as FineStatuses;

            if (status == null)
            {
                return;
            }

            var fines = GetFinesByStatus(status);
            finesListView.ItemsSource = fines;
        }
    }
}
