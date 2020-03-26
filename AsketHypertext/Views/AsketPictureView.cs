using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AsketHypertext.Models;

namespace AsketHypertext.Views
{
    public class AsketPictureView : Image
    {
        public AsketPictureView(AsketPicture model)
        {
            Name = model.Id;
            Source = new BitmapImage(new Uri($"pack://application:,,,/Sources/{model.Source}"));
            Stretch = Stretch.None;
        }
    }
}
