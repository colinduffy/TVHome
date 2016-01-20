using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TVHome
{
    public static class BackgroundController
    {
        private static bool scene;

        public static void Init()
        {
            scene = true;
        }

        public static ImageBrush GetBackgroundImage()
        {
          ImageBrush brush = new ImageBrush();
          if (scene)
          {
              brush.ImageSource = new BitmapImage(new Uri("Images/images.jpg", UriKind.Relative));
          }
          else
          {
              brush.ImageSource = new BitmapImage(new Uri("Images/scenic.png", UriKind.Relative));
          }
          scene = !scene;
          return brush;
        }
    }
}
