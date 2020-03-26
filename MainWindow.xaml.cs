using Microsoft.AppCenter.Crashes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows_WPF;

namespace Windows_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Guid SessionTracker;

        public MainWindow()
        {   
            InitializeComponent();
            SessionTracker = Guid.NewGuid();
        }
        private void Sample_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent($"Sample_Click Count {App.counter.ToString()} at {DateTime.Now.ToLongTimeString()}");
            App.counter++;
        }

        private void TrackException_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent($"TrackException_Click Count {App.counter.ToString()} at {DateTime.Now.ToLongTimeString()}");
            App.counter++;

            Microsoft.AppCenter.Crashes.Crashes.TrackError(new Exception($"TrackException_Click Count {App.counter.ToString()} at {DateTime.Now.ToLongTimeString()}"), new Dictionary<string, string>());
        }

        private void ThrowException_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent($"ThrowException_Click START Count {App.counter.ToString()}");
            App.counter++;

            throw new Exception($"ThrowException_Click at {DateTime.Now.ToLongTimeString()}");

        }

        private void SendError_Click(object sender, RoutedEventArgs e)
        {
            Exception ex = new Exception(SessionTracker.ToString());

            string textLog = GetLogText();

            Dictionary<string, string> properties = new Dictionary<string, string>
            {
                { "SessionTracker", SessionTracker.ToString() },
                { "DateTime", DateTime.Now.ToLongTimeString() }
            };

            var attachments = new ErrorAttachmentLog[]
            {
                ErrorAttachmentLog.AttachmentWithText(DateTime.Now.ToLongTimeString(), $"{SessionTracker.ToString()}.txt")
            };

            //Crashes.TrackError(ex, properties);
            Crashes.TrackError(ex, properties, attachments: attachments);
            //Crashes.TrackError(ex, attachments: attachments);
           // MessageBox.Show($"Sent Handled Error Test to AppCenter");
        }

        private string GetLogText()
        { 
            return DateTime.Now.ToLongTimeString();
        }
    }
}
