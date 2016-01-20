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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TVHome.Views
{
    /// <summary>
    /// Interaction logic for Timer.xaml
    /// </summary>
    public partial class Timer : UserControl, ISwitchable
    {
        private double time;
        private TimeSpan timeSpan;
        private DateTime currentTime;
        private DateTime startTime;

        public Timer() : this(2)
        {}

        public Timer(int seconds) : this(new TimeSpan(0,0,seconds))
        {}

        public Timer(TimeSpan timeSpan)
        {
            InitializeComponent();
            this.timeSpan = timeSpan;
            startTime = DateTime.Now;
            
            Start();
        }

        private void Start()
        {
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 50), DispatcherPriority.Normal, delegate
            {
                timeSpan = timeSpan.Subtract(new TimeSpan(0, 0, 0, 0, 50));

                if (timeSpan.CompareTo(TimeSpan.Zero) <= 0)
                { 
                    Stop();
                }
                else 
                {
                    timerText.Text = timeSpan.ToString();  
                }

            }, this.Dispatcher);
        }

        private void Stop()
        {
            //System.Media.SystemSounds.Asterisk.Play();
            //System.Media.SystemSounds.Beep.Play();
            Switcher.Switch(new Home());
        }

        public void UtilizeState(object state) { }
    }
}
