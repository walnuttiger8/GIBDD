using APPLICATION.Services;
using Microsoft.Win32;
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
    /// Логика взаимодействия для ExportFSSPView.xaml
    /// </summary>
    public partial class ExportFSSPView : Window
    {
        private readonly FSSPExporterService _fsspExporterService;
        public ExportFSSPView()
        {
            InitializeComponent();
            _fsspExporterService = new FSSPExporterService();
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
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
    }
}
