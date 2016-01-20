using System;
using System.Windows;
using System.Windows.Controls;
using TVHome.Views;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows.Threading;
using TVHome.SpeechRecognition;

namespace TVHome
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {
        public PageSwitcher()
        {
            InitializeComponent();
            InitializeBackground();

            SpeechHandler sh = new SpeechHandler();

            Switcher.pageSwitcher = this;
            Switcher.Switch(new Home());
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }

        private void InitializeBackground()
        {
            BackgroundController.Init();
            this.Background = BackgroundController.GetBackgroundImage();
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 5), DispatcherPriority.Normal, UpdateBackground, this.Dispatcher);
        }

        private void UpdateBackground(object sender, EventArgs e)
        {
            this.Background = BackgroundController.GetBackgroundImage();
        }
    }
}