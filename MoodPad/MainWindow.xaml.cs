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
        public List<TextBox> listOfTextBoxes;

        public MainWindow()
        {
            InitializeComponent();

            listOfTextBoxes = new List<TextBox>();
            MakeNewTab();
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
            tbox.TextChanged += new TextChangedEventHandler(TextChanged_Event);
            listOfTextBoxes.Add(tbox);
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

        /** Menu Format Events **/

        private void ConfigureItem_Click(object sender, RoutedEventArgs e)
        {
            var ConfigWin = new ConfigureWindow();
            ConfigWin.Owner = this;
            ConfigWin.Show();
        }

        /** Menu Insert Events **/

        private void DateTimeItem_Click(object sender, RoutedEventArgs e)
        {
            DateTime datenow = DateTime.Now;
            var txtBox = tabControl.SelectedContent as TextBox;
            txtBox.Text += datenow;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (1 == 1)
            {
                string msg = "You have not saved. Do you want to exit?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "MoodPad",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.No) e.Cancel = true;
            }
        }

        private void NewCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FindCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FindCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (FindPanel.Visibility == Visibility.Visible)
            {
                FindPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                FindPanel.Visibility = Visibility.Visible;
                startIndex = 0;
            }
        }
        int startIndex;
        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var txtBox = tabControl.SelectedContent as TextBox;

            if (txtBox.Text.Contains(FindTextLabel.Text))
            {
                txtBox.Focus();
                string searchString = FindTextLabel.Text;
                startIndex = txtBox.Text.IndexOf(searchString, startIndex);
                try
                {
                    txtBox.Select(startIndex, searchString.Length);
                }
                catch
                {
                    startIndex = 0;
                    if (txtBox.Text.IndexOf(searchString, startIndex) == txtBox.Text.LastIndexOf(searchString, startIndex))
                    {
                        txtBox.Select(startIndex, searchString.Length);
                    }
                }
                startIndex += searchString.Length;
            }
        }

        private void TextChanged_Event(object sender, TextChangedEventArgs e)
        {
            startIndex = 0;
        }
    }
}
