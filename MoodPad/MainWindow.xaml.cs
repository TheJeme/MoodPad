using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoodPad
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

        
        private void MakeNewTab()
        {
            TabItem ti = new TabItem();
            ti.Foreground = new SolidColorBrush(Colors.White);

            StackPanel sp = new StackPanel();
            TextBlock tb = new TextBlock();
            tb.Text = "New Page";
            Button b = new Button();
            b.Content = " X ";

            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(tb);
            sp.Children.Add(b);
            ti.Header = sp;

            TextBox tbox = new TextBox();
            tbox.Background = new SolidColorBrush(Color.FromRgb(37, 49, 53));
            tbox.BorderThickness = new Thickness(0);
            tbox.AcceptsReturn = true;
            tbox.AcceptsTab = true;
            tbox.Foreground = new SolidColorBrush(Colors.White);
            tbox.FontSize = 15;
            tbox.Padding = new Thickness(2);
            tbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            tbox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            ti.Content = tbox;
            tabControl.Items.Add(ti);
        }

        /** Menu File Events **/

        private void NewItem_Click(object sender, RoutedEventArgs e)
        {
            MakeNewTab();
        }

        private void OpenItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAsItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /** Menu Edit Events **/

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FindItem_Click(object sender, RoutedEventArgs e)
        {

        }

        /** Menu Format Events **/

        private void PropertiesItem_Click(object sender, RoutedEventArgs e)
        {

        }

        /** Menu Insert Events **/

        private void DateTimeItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
