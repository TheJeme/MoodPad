using MoodPad.Properties;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoodPad
{
    /// <summary>
    /// Interaction logic for ColorpickerWindow.xaml
    /// </summary>
    public partial class ColorpickerWindow : Window
    {
        private string target;

        public ColorpickerWindow(string target, Color hexColor)
        {
            InitializeComponent();

            this.target = target;        
            ColorPickCanvas.SelectedColor = hexColor;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string hexColor = "#" + ColorPickCanvas.R.ToString("X2") + ColorPickCanvas.G.ToString("X2") + ColorPickCanvas.B.ToString("X2");

            switch (target)
            {
                case "Font":
                    (this.Owner as ConfigureWindow).fontColorButton.Background = new SolidColorBrush(Color.FromRgb(ColorPickCanvas.R, ColorPickCanvas.G, ColorPickCanvas.B));
                    Settings.Default["FontColor"] = hexColor;
                    if (((ComboBoxItem)(this.Owner as ConfigureWindow).themeBox.SelectedItem).Content.ToString() == "Custom")
                    {
                        Settings.Default["CustomFontColor"] = hexColor;
                    }
                    break;

                case "Background":
                    (this.Owner as ConfigureWindow).backgroundColorButton.Background = new SolidColorBrush(Color.FromRgb(ColorPickCanvas.R, ColorPickCanvas.G, ColorPickCanvas.B));
                    Settings.Default["BackgroundColor"] = hexColor;
                    if (((ComboBoxItem)(this.Owner as ConfigureWindow).themeBox.SelectedItem).Content.ToString() == "Custom")
                    {
                        Settings.Default["CustomBackgroundColor"] = hexColor;
                    }
                    break;
            }

            Settings.Default.Save();
            this.Close();
        }
    }
}
