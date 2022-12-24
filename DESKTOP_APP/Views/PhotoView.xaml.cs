using APPLICATION;
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

namespace DESKTOP_APP.Views
{
    /// <summary>
    /// Логика взаимодействия для PhotoView.xaml
    /// </summary>
    public partial class PhotoView : Window
    {
        private readonly GIBDDEntities _db;
        private readonly int _photoId;
        public PhotoView(int id)
        {
            _db = new GIBDDEntities();
            InitializeComponent();
            _photoId = id;

            var photo = _db.Photos.Where(x => x.Id == _photoId).FirstOrDefault();
            if (photo == null)
            {
                throw new ArgumentException();
            }

            var image = new BitmapImage();

            using (var mem = new MemoryStream(photo.Data))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();

            displayImage.Source = image;
        }
    }
}
