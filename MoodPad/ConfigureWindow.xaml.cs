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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetConfigureSettings();
        }

        private void SetConfigureSettings()
        {
            themeBox.SelectedIndex = Convert.ToInt16(Settings.Default["ThemeIndex"].ToString());

            fontFamilyBox.SelectedValue = new FontFamily(Settings.Default["FontFamily"].ToString());
            fontSizeBox.SelectedValue = Convert.ToDouble(Settings.Default["FontSize"].ToString());
            

            fontColorButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.Default["FontColor"].ToString()));
            backgroundColorButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.Default["BackgroundColor"].ToString()));

            boldBox.IsChecked = Convert.ToBoolean(Settings.Default["IsBold"]);
            italicBox.IsChecked = Convert.ToBoolean(Settings.Default["IsItalic"]);
            underlinedBox.IsChecked = Convert.ToBoolean(Settings.Default["IsUnderline"]);

            wrapTextBox.IsChecked = Convert.ToBoolean(Settings.Default["IsWrapText"]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.Owner as MainWindow).ConfigureStyle();
            Application.Current.MainWindow.Focus();            
            this.Close();
        }

        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToInt16(Settings.Default["ThemeIndex"].ToString()) == themeBox.SelectedIndex) return;

            string fontColor = "#000000";
            string backgroundColor = "#FFFFFF";

            Settings.Default["ThemeIndex"] = themeBox.SelectedIndex;

            switch (((ComboBoxItem)themeBox.SelectedItem).Content.ToString())
            {
                case "Default":
                    fontColor = "#FFFFFF";
                    backgroundColor = "#253135";
                    break;

                case "Light":
                    fontColor = "#000000";
                    backgroundColor = "#FFFFFF";
                    break;

                case "Dark":
                    fontColor = "#FFFFFF";
                    backgroundColor = "#161616";
                    break;

                case "Sini":
                    fontColor = "#079ec1";
                    backgroundColor = "#292F4F";
                    break;

                case "Chocolate":
                    fontColor = "#a53622";
                    backgroundColor = "#301C15";
                    break;

                case "Vitamin":
                    fontColor = "#ACF74B";
                    backgroundColor = "#282c34";
                    break;

                case "Amethyst":
                    fontColor = "#B379E8";
                    backgroundColor = "#302858";
                    break;

                case "Pinky":
                    fontColor = "#E44CD3";
                    backgroundColor = "#FFC6F5";                 
                    break;

                case "Custom":
                    fontColor = Settings.Default["CustomFontColor"].ToString();
                    backgroundColor = Settings.Default["CustomBackgroundColor"].ToString();
                    break;
            }

            fontColorButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fontColor));
            backgroundColorButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(backgroundColor));

            Settings.Default["FontColor"] = fontColor;
            Settings.Default["BackgroundColor"] = backgroundColor;
            Settings.Default.Save();
            (this.Owner as MainWindow).ConfigureStyle();
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
            Settings.Default["FontFamily"] = fontFamilyBox.SelectedValue.ToString();
            Settings.Default.Save();
        }

        private void Size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
                {
                    txtbox.FontSize = Convert.ToDouble(fontSizeBox.SelectedValue.ToString());
                }
            }
            catch
            {

            }
            Settings.Default["FontSize"] = Convert.ToDouble(fontSizeBox.SelectedItem);
            Settings.Default.Save();
        }

        private void FontColor_Click(object sender, RoutedEventArgs e)
        {
            var colorpickWin = new ColorpickerWindow("Font", (Color)ColorConverter.ConvertFromString(Settings.Default["FontColor"].ToString()));
            colorpickWin.Owner = this;
            colorpickWin.ShowDialog();
        }

        private void BackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            var colorpickWin = new ColorpickerWindow("Background", (Color)ColorConverter.ConvertFromString(Settings.Default["BackgroundColor"].ToString()));
            colorpickWin.Owner = this;
            colorpickWin.ShowDialog();
        }

        private void BackgroundColor2_Click(object sender, RoutedEventArgs e)
        {
            var colorpickWin = new ColorpickerWindow("Background2", (Color)ColorConverter.ConvertFromString(Settings.Default["BackgroundColor2"].ToString()));
            colorpickWin.Owner = this;
            colorpickWin.ShowDialog();
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
