using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using MoodPad.Properties;

namespace MoodPad
{
    /// <summary>
    /// Interaction logic for ConfigureWindow.xaml
    /// </summary>
    public partial class ConfigureWindow : Window
    {
        public ConfigureWindow()
        {
            InitializeComponent();

            SetConfigureSettings();
        }

        private void SetConfigureSettings()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Font_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
                {
                    txtbox.FontFamily = new FontFamily(fontFamilyBox.SelectedValue.ToString());
                }
            }
            catch
            {

            }
        }

        private void Size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
                {
                    txtbox.FontSize = Convert.ToDouble((fontSizeBox.SelectedItem as ComboBoxItem).Content);
                }
            }
            catch
            {

            }
        }

        private void FontColor_Click(object sender, RoutedEventArgs e)
        {
            var colorpickWin = new ColorpickerWindow("Font");
            colorpickWin.Owner = this;
            colorpickWin.Show();
        }

        private void BackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            var colorpickWin = new ColorpickerWindow("Background");
            colorpickWin.Owner = this;
            colorpickWin.Show();
        }

        private void BackgroundColor2_Click(object sender, RoutedEventArgs e)
        {
            var colorpickWin = new ColorpickerWindow("Background2");
            colorpickWin.Owner = this;
            colorpickWin.Show();
        }

        private void BoldBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.FontWeight = FontWeights.Bold;
            }
            Settings.Default["IsBold"] = true;
            Settings.Default.Save();
        }

        private void BoldBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.FontWeight = FontWeights.Normal;
            }
            Settings.Default["IsBold"] = false;
            Settings.Default.Save();
        }

        private void ItalicBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.FontStyle = FontStyles.Italic;
            }
            Settings.Default["IsItalic"] = true;
            Settings.Default.Save();
        }

        private void ItalicBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.FontStyle = FontStyles.Normal;
            }
            Settings.Default["IsItalic"] = false;
            Settings.Default.Save();
        }

        private void UnderlinedBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.TextDecorations = TextDecorations.Underline;
            }
            Settings.Default["IsUnderline"] = true;
            Settings.Default.Save();
        }

        private void UnderlinedBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.TextDecorations = null;
            }
            Settings.Default["IsUnderline"] = false;
            Settings.Default.Save();
        }

        private void WrapTextBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.TextWrapping = TextWrapping.Wrap;
            }
            Settings.Default["IsWrapText"] = true;
            Settings.Default.Save();
        }

        private void WrapTextBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.TextWrapping = TextWrapping.NoWrap;
            }
            Settings.Default["IsWrapText"] = false;
            Settings.Default.Save();
        }
    }
}
