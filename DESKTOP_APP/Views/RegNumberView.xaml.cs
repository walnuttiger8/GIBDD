using APPLICATION;
using System;
using System.Linq;
using REG_MARK_LIB;
using SB_UTILS;
using System.Threading;

using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace DESKTOP_APP.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateRegNumberView.xaml
    /// </summary>
    /// 
    public static class ImageExtensions
    {
        public static ImageSource ToImageSource(this Bitmap image)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
    }
    public partial class RegNumberView : Window
    {
        private readonly GIBDDEntities _db;
        private readonly Vehicles _vehicle;
        private RegNumbers _regNumber;
        public RegNumberView(int vehiceId)
        {
            InitializeComponent();

            _db = new GIBDDEntities();
            _vehicle = _db.Vehicles.Where(x => x.Id == vehiceId).FirstOrDefault();
            _regNumber = _vehicle.RegNumbers.FirstOrDefault();

            if (_vehicle == null)
            {
                throw new ArgumentException();
            }

            InitizlizeRegNumber();
            InitializeImage();
            //InitizlizeBackground();

            vinTextBlock.Text = _vehicle.VINCode.Trim();
            manufacturerTextBlock.Text = _vehicle.Manufacturer.Trim();
            modelTextBlock.Text = _vehicle.Model.Trim();
            yearTextBlock.Text = _vehicle.Year.ToString() + " г.";
            weightTextBlock.Text = _vehicle.Weight.ToString() + " кг.";
            colorTextBlock.Text = _vehicle.CarColors.Name;
            engineTypeTextBlock.Text = _vehicle.EngineTypes.LocalizedName;
            typeOfDriveTextBlock.Text = _vehicle.TypeOfDrive.Name;

            colorImage.ToolTip = _vehicle.CarColors.ColorName;
        }

        public void InitizlizeBackground()
        {
            var bc = new BrushConverter();
            Background = (System.Windows.Media.Brush)bc.ConvertFrom(_vehicle.CarColors.HexCode);
        }

        public void InitizlizeRegNumber()
        {
            var r = GetRegNumber();
            RenderRegNumber(r);
        }

        private void RenderRegNumber(RegNumber r)
        {
            firstSeriesLabel.Content = string.Join("", r.Series.ToString().Take(1)).ToUpper();
            secondSeriesLable.Content = string.Join("", r.Series.ToString().Skip(1).Take(2)).ToUpper();
            numberLabel.Content = r.Number.ToString();
            regionCodeLabel.Content = r.RegionCode.ToString();

            regionCodeLabel.ToolTip = _regNumber.RegionCodeCodes.RegionCodes.RegionNameRU.ToString();
        }

        public void InitializeImage()
        {
            Bitmap bm = new Bitmap(Resource1.car2);

            Enumerable.Range(0, bm.Width)
                .Select(x => Enumerable.Range(0, bm.Height)
                .Select(y => new Tuple<int, int>(x, y)))
                .SelectMany(x => x)
                .Where(x => bm.GetPixel(x.Item1, x.Item2) == System.Drawing.Color.FromArgb(0, 255, 0)).ToList()
                .ForEach(x => bm.SetPixel(x.Item1, x.Item2, ColorTranslator.FromHtml(_vehicle.CarColors.HexCode)));

            colorImage.Source = bm.ToImageSource();
        }

        private void OlegNumber()
        {

            var start = GetRegNumber();

            var target = new RegNumber(new RegNumberSeries("mka"), new RegNumberNumber(58), 716);

            Task.Run(() =>
            {
                foreach (var number in RegNumber.Range(start, target))
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        RenderRegNumber(number);
                    });
                    Task.Delay(10).Wait();
                }
            });  
        }

        private RegNumber GetRegNumber()
        {
            var series = new RegNumberSeries(_regNumber.Series.ToLower());
            var number = new RegNumberNumber(_regNumber.Number);

            return new RegNumber(series, number, _regNumber.RegionCodeCodes.Code);
        }

        private void colorImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OlegNumber();
        }
    }
}
