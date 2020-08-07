using System.Windows.Controls;

namespace MoodPad
{
    public class TextFile
    {
        public TextBox TextBox { get; set; }
        public TextBlock Header { get; set; }
        public string FilePath { get; set; }
        public bool IsSaved { get; set; }
    }
}
