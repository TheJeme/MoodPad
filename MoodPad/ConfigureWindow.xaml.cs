using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Font_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
        }

        private void BoldBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.FontWeight = FontWeights.Normal;
            }
        }

        private void ItalicBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.FontStyle = FontStyles.Italic;
            }
        }

        private void ItalicBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.FontStyle = FontStyles.Normal;
            }
        }

        private void UnderlinedBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.TextDecorations = TextDecorations.Underline;
            }
        }

        private void UnderlinedBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.TextDecorations = null;
            }
        }

        private void WrapTextBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.TextWrapping = TextWrapping.Wrap;
            }
        }

        private void WrapTextBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var txtbox in (this.Owner as MainWindow).listOfTextBoxes)
            {
                txtbox.TextWrapping = TextWrapping.NoWrap;
            }
        }
    }
}
