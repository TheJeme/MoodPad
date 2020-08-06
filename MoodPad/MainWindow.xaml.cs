using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using MoodPad.Properties;

namespace MoodPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int startIndex;

        public List<TextFile> listOfTextBoxes;

        public static Color fontColor, backgroundColor;

        public MainWindow()
        {
            InitializeComponent();

            listOfTextBoxes = new List<TextFile>();
            MakeNewTab();

            ConfigureStyle();
        }

        public void ConfigureStyle()
        {
            fontColor = (Color)ColorConverter.ConvertFromString(Settings.Default["FontColor"].ToString());
            backgroundColor = (Color)ColorConverter.ConvertFromString(Settings.Default["BackgroundColor"].ToString());

            bugBox.Background = new SolidColorBrush(backgroundColor);

            foreach (var txtbox in listOfTextBoxes)
            {
                txtbox.TextBox.Background = new SolidColorBrush(backgroundColor);
                txtbox.TextBox.Foreground = new SolidColorBrush(fontColor);
                txtbox.TextBox.FontFamily = new FontFamily(Settings.Default["FontFamily"].ToString());
                txtbox.TextBox.FontSize = Convert.ToDouble(Settings.Default["FontSize"].ToString());

                if (Convert.ToBoolean(Settings.Default["IsBold"]))
                {
                    txtbox.TextBox.FontWeight = FontWeights.Bold;
                }
                if (Convert.ToBoolean(Settings.Default["IsItalic"]))
                {
                    txtbox.TextBox.FontStyle = FontStyles.Italic;
                }
                if (Convert.ToBoolean(Settings.Default["IsUnderline"]))
                {
                    txtbox.TextBox.TextDecorations = TextDecorations.Underline;
                }
                if (Convert.ToBoolean(Settings.Default["IsWrapText"]))
                {
                    txtbox.TextBox.TextWrapping = TextWrapping.Wrap;
                }
            }
        }

        private void MakeNewTab(string tabName = "New Page", string textContent = "", string filePath = "")
        {
            TabItem ti = new TabItem();
            ti.Foreground = new SolidColorBrush(Colors.White);

            StackPanel sp = new StackPanel();
            TextBlock headerTextBlock = new TextBlock();
            headerTextBlock.Text = tabName;
            Button b = new Button();
            b.Content = "X";
            b.Click += deleteTabButton_Click;
            b.FontWeight = FontWeights.Bold;
            b.Width = 20;
            b.Height = 20;
            b.Foreground = new SolidColorBrush(Colors.White);
            b.Background = new SolidColorBrush(Colors.Transparent);
            b.BorderThickness = new Thickness(0);
            b.Margin = new Thickness(10,0,0,0);

            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(headerTextBlock);
            sp.Children.Add(b);
            ti.Header = sp;

            TextBox tbox = new TextBox();
            tbox.Text = textContent;
            tbox.Background = new SolidColorBrush(Color.FromRgb(37, 49, 53));
            tbox.BorderThickness = new Thickness(0);
            tbox.AcceptsReturn = true;
            tbox.AcceptsTab = true;
            tbox.Foreground = new SolidColorBrush(Colors.White);
            tbox.Padding = new Thickness(2);
            tbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            tbox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            tbox.TextChanged += new TextChangedEventHandler(TextChanged_Event);
            ti.Content = tbox;
            listOfTextBoxes.Add(new TextFile { TextBox = tbox, Header = headerTextBlock, FilePath = filePath });
            tabControl.Items.Add(ti);
        }

        private void OpenFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";

            Nullable<bool> result = dlg.ShowDialog();

            if (result.HasValue && result.Value)
            {
                MakeNewTab(Path.GetFileName(dlg.FileName), File.ReadAllText(dlg.FileName), dlg.FileName);
                ConfigureStyle();
                //var txtBox = tabControl.SelectedContent as TextBox;
                //txtBox.Text = File.ReadAllText(dlg.FileName);
            }
        }

        private void SaveFile()
        {
            if (listOfTextBoxes[tabControl.SelectedIndex].FilePath == "")
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";

                if (dlg.ShowDialog() == true)
                {
                    listOfTextBoxes[tabControl.SelectedIndex].FilePath = dlg.FileName;
                    listOfTextBoxes[tabControl.SelectedIndex].Header.Text = Path.GetFileName(dlg.FileName);
                    File.WriteAllText(listOfTextBoxes[tabControl.SelectedIndex].FilePath, listOfTextBoxes[tabControl.SelectedIndex].TextBox.Text);
                }
            }
            else
            {
                File.WriteAllText(listOfTextBoxes[tabControl.SelectedIndex].FilePath, listOfTextBoxes[tabControl.SelectedIndex].TextBox.Text);
            }
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

        private void NewFileCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewFileCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MakeNewTab();
            ConfigureStyle();
        }

        private void OpenFileCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenFileCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            OpenFile();
        }

        private void SaveFileCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveFileCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SaveFile();
        }

        private void SaveAsFileCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveAsFileCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }

        private void FindCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void FindCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (findPanel.Visibility == Visibility.Visible)
            {
                findPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                findPanel.Visibility = Visibility.Visible;
                startIndex = 0;
            }
        }

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

        private void deleteTabButton_Click(object sender, RoutedEventArgs e)
        {
            listOfTextBoxes.RemoveAt(tabControl.SelectedIndex);
            tabControl.Items.Remove(tabControl.SelectedItem);
            tabControl.Items.Refresh();
        }
    }
}
