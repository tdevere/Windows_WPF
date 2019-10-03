using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


namespace Windows_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static int counter = 0;

        protected override void OnStartup(StartupEventArgs e)
        {
            Microsoft.AppCenter.Crashes.Crashes.SendingErrorReport += Crashes_SendingErrorReport;
            Microsoft.AppCenter.Crashes.Crashes.SentErrorReport += Crashes_SentErrorReport;
            Microsoft.AppCenter.Crashes.Crashes.FailedToSendErrorReport += Crashes_FailedToSendErrorReport;

            AppCenter.LogLevel = LogLevel.Verbose;

            AppCenter.Start("2ab98fdb-0d65-41b8-bd1a-ad0f436bd47d",
                   typeof(Analytics), typeof(Crashes));

            Crashes.GetErrorAttachments = (ErrorReport report) =>
            {
                string file1 = @"C:\Users\tdevere\source\repos\Windows_WPF\File_One.txt";
                string file2 = @"C:\Users\tdevere\source\repos\Windows_WPF\File_Two.txt";
                StringBuilder crashdata = new StringBuilder("Crash Report");
                crashdata.AppendLine(System.Environment.NewLine);
                crashdata.AppendLine($"File ONE: {DateTime.Now.ToLongTimeString()}");
                crashdata.AppendLine(File.ReadAllText(file1));
                crashdata.AppendLine($"File TWO: {DateTime.Now.ToLongTimeString()}");
                crashdata.AppendLine(File.ReadAllText(file2));

                return new ErrorAttachmentLog[]
               {
                    ErrorAttachmentLog.AttachmentWithText(crashdata.ToString(), $"CrashData at {DateTime.Now.ToLongTimeString()}"),
                    //ErrorAttachmentLog.AttachmentWithBinary(Encoding.UTF8.GetBytes("Fake image"), $"fake_image_Ticks {DateTime.Now.Ticks.ToString()}.jpeg", "image/jpeg")
               };


                // Your code goes here.
                //return new ErrorAttachmentLog[]
                //{
                //    ErrorAttachmentLog.AttachmentWithText($"Hello world! Ticks {DateTime.Now.Ticks.ToString()}", "hello.txt"),
                //    ErrorAttachmentLog.AttachmentWithBinary(Encoding.UTF8.GetBytes("Fake image"), $"fake_image_Ticks {DateTime.Now.Ticks.ToString()}.jpeg", "image/jpeg")
                //};
            };


            //Crashes.GetErrorAttachments = (ErrorReport report) =>
            //{
            //    string file1 = @"C:\Users\tdevere\source\repos\Windows_WPF\File_One.txt";
            //    string file2 = @"C:\Users\tdevere\source\repos\Windows_WPF\File_Two.txt";
            //    StringBuilder crashdata = new StringBuilder("Crash Report");
            //    crashdata.AppendLine("File 1");
            //    crashdata.AppendLine(File.ReadAllText(file1));
            //    crashdata.AppendLine("File 2");
            //    crashdata.AppendLine(File.ReadAllText(file2));

            //    return new ErrorAttachmentLog[]
            //   {
            //        ErrorAttachmentLog.AttachmentWithText(crashdata.ToString(), $"Crash Report at {DateTime.Now.ToLongTimeString()}.txt"),
            //   };
            //};

            base.OnStartup(e);
        }

        private void Crashes_FailedToSendErrorReport(object sender, FailedToSendErrorReportEventArgs e)
        {
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent($"Crashes_FailedToSendErrorReport Count = { counter.ToString() } at {DateTime.Now.ToLongTimeString()}");
            App.counter++;
        }

        private void Crashes_SentErrorReport(object sender, SentErrorReportEventArgs e)
        {
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent($"Crashes_SentErrorReport Count = { counter.ToString() } at {DateTime.Now.ToLongTimeString()}");
            App.counter++;
        }

        private void Crashes_SendingErrorReport(object sender, Microsoft.AppCenter.Crashes.SendingErrorReportEventArgs e)
        {

            Microsoft.AppCenter.Analytics.Analytics.TrackEvent($"Crashes_SendingErrorReport Count = { counter.ToString() } at {DateTime.Now.ToLongTimeString()}");
            App.counter++;

        }

    }
}
