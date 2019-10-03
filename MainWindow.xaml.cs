using Microsoft.AppCenter.Crashes;
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


namespace Windows_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            InitializeComponent();
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
    }
}
