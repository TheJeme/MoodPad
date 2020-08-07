using System;
using System.Windows;

namespace MoodPad
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void Application_Start(object sender, StartupEventArgs args)
        {
            string[] a = Environment.GetCommandLineArgs();

            string filepath = "";

            if (a.Length > 1)
            {
                filepath = a[1];
            }

            MainWindow main = new MainWindow(filepath);
            main.Show();
        }
    }
}
