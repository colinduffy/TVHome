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
using System.Globalization;
using System.Net;
using System.Xml;
using System.IO;

namespace TVHome.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl, ISwitchable
    {
        private const string W_UNDERGROUND_KEY = "55b326d548914076";

        public Home()
        {
            InitializeComponent();
            InitializeClock();
            InitializeWeather();
            //GetImage();                 C:\Users\Colin\Documents\College\SE491 (Sr. Desing I)\Project\Info\WpfApplication2\Images

        }

        public void InitializeClock()
        {
           // MessageBox.Show("hello");
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.timeText.Text = DateTime.Now.ToString("hh:mm");
            }, this.Dispatcher);
        }

        public void InitializeWeather()
        {
            ParesWUndergroundResponse();
        }

        private void ParesWUndergroundResponse()
        {
            string place = "";
            string obs_time = "";
            string weather1 = "";
            string temperature_string = "";
            string relative_humidity = "";
            string wind_string = "";
            string pressure_mb = "";
            string dewpoint_string = "";
            string visibility_km = "";
            string latitude = "";
            string longitude = "";
        
            var cli = new WebClient();
            string weather = cli.DownloadString("http://api.wunderground.com/api/" + W_UNDERGROUND_KEY + "/conditions/q/64108.xml");
            this.weatherText.Text = "HELLO";
            //this.weatherText.Text += weather;

            using (XmlReader reader = XmlReader.Create(new StringReader(weather)))
            {
                // Parse the file and display each of the nodes.
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name.Equals("temperature_string"))
                            {
                                reader.Read();
                                this.temperatureText.Text = reader.Value;
                            }
                            else if (reader.Name.Equals("relative_humidity"))
                            {
                                reader.Read();
                                relative_humidity = reader.Value;
                            }
                            else if (reader.Name.Equals("wind_string"))
                            {
                                reader.Read();
                                wind_string = reader.Value;
                            }
                            break;
                    }
                }
            }
        }

        private void GetImage()
        {
            
            using (WebClient client = new WebClient())
            {
                // Set the save directory here
                string directory = "/path/to/save/directory/";

                this.temperatureText.Text = directory;
                // Create the directory if it doesn not already exist
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);


                //Set the working directory
                Directory.SetCurrentDirectory(directory);


                // Set desired size, width, and height
                // I know only that the default(s1280,w1280,h720)
                // and 1080p(s1920,w1920,h1080) work for certain.
                // Other resolutions may be available, but no guarantees
                string picSize = "s1920";
                string picWidth = "w1920";
                string picHeight = "h1080";


                // Download the chromecast screensaver html
                string htmlCode = client.DownloadString("https://clients3.google.com/cast/chromecast/home");
                

                // Separator used to split html code
                string[] separators = { "x22" };


                // Split the html code
                string[] lines = htmlCode.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                
                foreach (string line in lines)
                {
                    // See if the line contains a URL is for an image file
                    if (line.Contains("googleusercontent") && line.Contains(".jpg"))
                    {
                        // If so, clean it up so we can use it and
                        // set the desired image resolution for download
                        string fileURL = line.Replace("\\", "").Replace("s1280", picSize).Replace("w1280", picWidth).Replace("h720", picHeight);


                        //Clean up any html percent codes in the filename
                        //NOTE: You may see others in you downloaded image filenames,
                        //Just append another .Replace("string to replace", "replacement string")
                        //to remove any annoying characters you find in the filenames
                        fileURL = fileURL.Replace("%2B", " ").Replace("%25", "");


                        //Let's use this to find the filename
                        //within the file URL
                        separators = new string[] { "/" };


                        string[] fileNames = fileURL.Split(separators, StringSplitOptions.RemoveEmptyEntries);


                        foreach (string fileName in fileNames)
                        {
                            // If we have a valid filename then 
                            // download the file if it doesn't already exist
                            if (fileName.Contains(".jpg") && !File.Exists(fileName))
                            {
                                //Since this program is running in a terminal,
                                //let the user know which files are being downloaded
                                Console.WriteLine("Downloading {0}", fileName);
                                this.temperatureText.Text += "\n" + fileName;

                                //Download the file and save it
                                client.DownloadFile(fileURL, directory + fileName);
                            }


                        }
                    }
                }
            }
        }

        public void UtilizeState(object state) {  }

   
    }
}

