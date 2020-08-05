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

        public ColorpickerWindow(string target)
        {
            this.target = target;

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (target)
            {
                case "Font":
                    (this.Owner as ConfigureWindow).fontColorButton.Background = new SolidColorBrush(Color.FromRgb(ColorPickCanvas.R, ColorPickCanvas.G, ColorPickCanvas.B));
                    break;
                case "Background":
                    (this.Owner as ConfigureWindow).backgroundColorButton.Background = new SolidColorBrush(Color.FromRgb(ColorPickCanvas.R, ColorPickCanvas.G, ColorPickCanvas.B));
                    break;
                case "Background2":
                    (this.Owner as ConfigureWindow).backgroundColorButton2.Background = new SolidColorBrush(Color.FromRgb(ColorPickCanvas.R, ColorPickCanvas.G, ColorPickCanvas.B));
                    break;
            }
            this.Close();
        }
    }
}
