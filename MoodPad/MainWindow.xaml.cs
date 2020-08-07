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

        public List<TextFile> listOfTextFiles;

        public static Color fontColor, backgroundColor;

        public MainWindow(string filePath)
        {
            InitializeComponent();

            listOfTextFiles = new List<TextFile>();

            if (File.Exists(filePath))
            {
                MakeNewTab(Path.GetFileName(filePath), File.ReadAllText(filePath, Encoding.UTF8), filePath);
                bugBox.Visibility = Visibility.Visible;
                ConfigureStyle();
            }
        }

        public void ConfigureStyle()
        {
            fontColor = (Color)ColorConverter.ConvertFromString(Settings.Default["FontColor"].ToString());
            backgroundColor = (Color)ColorConverter.ConvertFromString(Settings.Default["BackgroundColor"].ToString());

            bugBox.Background = new SolidColorBrush(backgroundColor);

            foreach (var txtbox in listOfTextFiles)
            {
                txtbox.TextBox.Foreground = new SolidColorBrush(fontColor);
                txtbox.TextBox.Background = new SolidColorBrush(backgroundColor);
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
            listOfTextFiles.Add(new TextFile { TextBox = tbox, Header = headerTextBlock, FilePath = filePath, IsSaved = true });
            tabControl.Items.Add(ti);
        }

        private void NewFile()
        {
            MakeNewTab();
            ConfigureStyle();
            tabControl.SelectedIndex = listOfTextFiles.Count - 1;
        }

        private void OpenFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";  // Default is text files but accepts all kind of files

            Nullable<bool> result = dlg.ShowDialog();

            if (result.HasValue && result.Value)
            {
                // Checks if file is already opened
                for (int indexOfTextFile = 0; indexOfTextFile < listOfTextFiles.Count; indexOfTextFile++)
                {
                    // If same file is opened already then returns from function but opens it as active tab
                    if (dlg.FileName == listOfTextFiles[indexOfTextFile].FilePath)
                    {
                        tabControl.SelectedIndex = indexOfTextFile;
                        return;  
                    }
                }
                MakeNewTab(Path.GetFileName(dlg.FileName), File.ReadAllText(dlg.FileName, Encoding.UTF8), dlg.FileName); // Uses UTF-8 to enable most characters
                ConfigureStyle();
                tabControl.SelectedIndex = listOfTextFiles.Count - 1; // Sets current active tab to just opened one
            }
        }

        private void SaveFile()
        {
            if (listOfTextFiles[tabControl.SelectedIndex].FilePath == "")
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";

                if (dlg.ShowDialog() == true)
                {
                    listOfTextFiles[tabControl.SelectedIndex].FilePath = dlg.FileName;
                    listOfTextFiles[tabControl.SelectedIndex].Header.Text = Path.GetFileName(dlg.FileName);
                    File.WriteAllText(listOfTextFiles[tabControl.SelectedIndex].FilePath, listOfTextFiles[tabControl.SelectedIndex].TextBox.Text);
                }
            }
            else
            {
                File.WriteAllText(listOfTextFiles[tabControl.SelectedIndex].FilePath, listOfTextFiles[tabControl.SelectedIndex].TextBox.Text);
            }
            listOfTextFiles[tabControl.SelectedIndex].IsSaved = true;
        }

        private void SaveAsFile()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                listOfTextFiles[tabControl.SelectedIndex].FilePath = dlg.FileName;
                listOfTextFiles[tabControl.SelectedIndex].Header.Text = Path.GetFileName(dlg.FileName);
                File.WriteAllText(listOfTextFiles[tabControl.SelectedIndex].FilePath, listOfTextFiles[tabControl.SelectedIndex].TextBox.Text);
                listOfTextFiles[tabControl.SelectedIndex].IsSaved = true;
            }
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
            if (tabControl.SelectedItem == null) return;

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
            NewFile();
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
            e.CanExecute = tabControl.SelectedItem != null; ;
        }

        private void SaveFileCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SaveFile();
        }

        private void SaveAsFileCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = tabControl.SelectedItem != null;
        }

        private void SaveAsFileCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SaveAsFile();
        }

        private void FindCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = tabControl.SelectedItem != null;
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

            listOfTextFiles[tabControl.SelectedIndex].IsSaved = false;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startIndex = 0;
            bugBox.Visibility = Visibility.Visible;

            if (tabControl.SelectedItem == null)
            {
                findPanel.Visibility = Visibility.Collapsed;
                bugBox.Visibility = Visibility.Collapsed;
            }
        }

        private void deleteTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (listOfTextFiles[tabControl.SelectedIndex].IsSaved == false)
            {
                string msg = "You have not saved. Do you want to close this tab?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "MoodPad",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    listOfTextFiles.RemoveAt(tabControl.SelectedIndex);
                    tabControl.Items.Remove(tabControl.SelectedItem);
                    tabControl.Items.Refresh();
                }
            }
            else
            {
                listOfTextFiles.RemoveAt(tabControl.SelectedIndex);
                tabControl.Items.Remove(tabControl.SelectedItem);
                tabControl.Items.Refresh();
            }
        }
    }
}
